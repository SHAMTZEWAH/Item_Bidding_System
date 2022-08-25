using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ManageProduct : System.Web.UI.Page, IPostBackEventHandler
    {
        private DataSet dtSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getConnection();
            }
            btnCreateStore.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this, "clickDiv");
            generateSubStore();
            //DataTable subStore = getActiveSubStore();
            //int subStoreCount = getActiveSubStore().Rows.Count;
        }

        void createDataTable()
        {
            DataTable dt = new DataTable("Product");
            DataColumn col;

            //create prodName column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "subStoreName";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            //create data set
            dtSet = new DataSet();

            //assign data table into data set
            dtSet.Tables.Add(dt);
        }

        //generate number of substore
        void generateSubStore()
        {
            try
            {
                //get the value of the data table
                getActiveSubStore();
                DataTable dt = dtSet.Tables["Product"];
                foreach(DataRow dr in dt.Rows)
                {
                    string subStoreName = (string)dr["subStoreName"];
                    subStoreName = subStoreName.Trim();
                    subStoreName = subStoreName.Replace("SubStore", "");

                    int subStoreNum = 0;
                    subStoreNum = Convert.ToInt32(subStoreName);

                    addBtnControl(subStoreNum);
                }
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
        }

        //load the product
        void getConnection()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);
            

            //prepare command
            SqlCommand cmdRetrieve;
            string query = "SELECT ProductPhoto.productPhotoURL, ProductDetails.productName, MAX(BidTable.bidPrice) AS Bid, FixedPriceProduct.productPrice, Product.productStock " +
                "FROM ProductPhoto INNER JOIN " +
                "Product ON ProductPhoto.productId = Product.productId FULL JOIN " +
                "BidTable ON Product.productId = BidTable.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON Product.productId = FixedPriceProduct.productId INNER JOIN " +
                "SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN " +
                "Seller ON Substore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Account.username = @username AND Account.email = @email AND photoStatus = 'Main' " +
                "GROUP BY Product.addDateTime, ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock " +
                "ORDER BY Product.addDateTime ";
            
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                Repeater1.DataSource = cmdRetrieve.ExecuteReader();
                Repeater1.DataBind();
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if(lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }
                
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //bid price in product drop down list
        void connectBiddingDDL(string productName, DropDownList ddlBid)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;
            string query = "SELECT bidPrice FROM BidTable, ProductDetails, Product WHERE BidTable.productId = Product.productId AND Product.productDetailsId = ProductDetails.productDetailsId AND ProductDetails.productName = @prodName ORDER BY bidPrice DESC";

            //assign query value into ddl
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodName", productName);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                ddlBid.DataSource = reader;
                ddlBid.DataValueField = "bidPrice";
                ddlBid.DataTextField = "bidPrice";
                ddlBid.DataBind();
            }
            catch(NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            if (ddlBid.Items.Count > 0)
            {
                ddlBid.SelectedValue = ddlBid.Items[0].Value;
                for (int i = 0; i < ddlBid.Items.Count; i++)
                {
                    ddlBid.Items[i].Text = String.Format("{0:0.00}", Double.Parse(ddlBid.Items[i].Text.ToString()));
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Repeater1.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblNoData");
                    lblFooter.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Reference the Repeater Item.
                RepeaterItem item = e.Item; 

                //Reference the Controls.
                string prodName = (item.FindControl("prodName") as Label).Text;
                DropDownList ddlBid = (item.FindControl("ddlBid") as DropDownList);

                //assign connection into ddl current bid
                connectBiddingDDL(prodName, ddlBid);
            }
        }

//^^^^^^^^^^^^^^^^^^^^^^ SubStore Control ^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        //Event for create substore button
        protected void btnCreateStore_Click()
        {
            getInactiveSubStore(); //dt for inactive
            DataTable dtInactive = dtSet.Tables["Product"];
            string subStoreName = "";
            int subStoreNum = 0;
            int totalSubStore = getSubStoreCount();

            if (dtInactive.Rows.Count > 0)
            {
                //update to active
                subStoreName = dtInactive.Rows[0].ItemArray[0].ToString();
                subStoreName = subStoreName.Trim().Replace("SubStore", "");
                subStoreNum = Convert.ToInt32(subStoreName);
                updateSubStoreStatus(subStoreNum, "Active");
            }
            else
            {
                try
                {
                    int subStoreCount = getSubStoreCount();

                    //update database
                    addSubStore(subStoreCount);

                    //add UI control
                    for(int i=1; i<=(subStoreCount+1); i++)
                    {
                        addBtnControl(i);
                    }
                }
                catch(Exception ex)
                {
                    Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                    if (lblFooter != null)
                    {
                        lblFooter.Visible = true;
                        lblFooter.Text = ex.Message.ToString();
                    }
                }
                
            }
        }

        //Post back event for create substore button (which is a div)
        #region IPostBackEventHandler Members
        public void RaisePostBackEvent(string eventArgument)
        {

            if (!string.IsNullOrEmpty(eventArgument))
            {

                if (eventArgument == "clickDiv")
                {
                    btnCreateStore_Click();
                }
            }
        }

        #endregion

        //Count substore
        int getSubStoreCount()
        {
            int subStoreCount = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(SubStore.subStoreId) AS SubStoreCount FROM Seller, Account, SubStore " +
                "WHERE Account.accId = Seller.accId AND Seller.sellerId = SubStore.sellerId AND Account.username = @username " +
                "AND Account.email = @email";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                subStoreCount = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return subStoreCount;
        }

        //return substore that is active
        void getActiveSubStore()
        {
            int subStoreCount = 0;
            createDataTable();
            DataTable dt = dtSet.Tables["Product"]; 

            //create connection
            SqlConnection con;
            SqlDataAdapter cmdRetrieve;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            string query = "SELECT SubStore.subStoreName FROM Seller, Account, SubStore " +
                "WHERE Account.accId = Seller.accId AND Seller.sellerId = SubStore.sellerId AND Account.username = @username AND Account.email = @email " +
                "AND SubStore.subStoreStatus = 'Active'";

            try
            {
                con.Open();
                cmdRetrieve = new SqlDataAdapter(query, con);
                cmdRetrieve.SelectCommand.Parameters.AddWithValue("@username", User.Identity.Name);
                cmdRetrieve.SelectCommand.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                cmdRetrieve.Fill(dt);
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //return substore that is inactive
        void getInactiveSubStore()
        {
            int subStoreCount = 0;
            createDataTable();
            DataTable dt = dtSet.Tables["Product"];

            //create connection
            SqlConnection con;
            SqlDataAdapter cmdRetrieve;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            string query = "SELECT SubStore.subStoreName FROM Seller, Account, SubStore " +
                "WHERE Account.accId = Seller.accId AND Seller.sellerId = SubStore.sellerId AND Account.username = @username AND Account.email = @email " +
                "AND SubStore.subStoreStatus = 'Inactive'";

            try
            {
                con.Open();
                cmdRetrieve = new SqlDataAdapter(query, con);
                cmdRetrieve.SelectCommand.Parameters.AddWithValue("@username", User.Identity.Name);
                cmdRetrieve.SelectCommand.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                cmdRetrieve.Fill(dt);
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //Get seller id
        string getSellerId()
        {
            string sellerId = "";
            SqlConnection con;
            string strCon;
            SqlCommand cmdRetrieve;

            //create connection
            strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            string query = "SELECT SellerId FROM Account, Seller WHERE Account.accId = Seller.accId AND Account.username = @username";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                sellerId = (string)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return sellerId;
        }

        //Add substore into database
        void addSubStore(int subStoreCount)
        {
            SqlConnection con;
            string strCon;
            string subStoreId = "";

            //create connection
            strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;
            if (subStoreCount+1 > 9)
            {
                subStoreId = "ss_0" + (subStoreCount+1);
            }
            else
            {
                subStoreId = "ss_00" + (subStoreCount+1);
            }

            string query = "INSERT INTO SubStore(subStoreId, subStoreName, subStoreDescription, subStoreStatus, createDateTime, sellerId)" +
                "VALUES (@subStoreId, @subStoreName, NULL, 'Active', CONVERT(DATETIME, GETDATE(), 120) , @sellerId)";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@subStoreId", subStoreId);
                cmdRetrieve.Parameters.AddWithValue("@subStoreName", "SubStore" + (subStoreCount+1).ToString());
                cmdRetrieve.Parameters.AddWithValue("@sellerId", getSellerId());
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        } //total subStore (use by create subStore)

        void updateSubStoreStatus(int subStoreNum, string status)
        {
            string strCon;
            SqlConnection con;
            string subStoreId = "";
            string subStoreName = "";
            string username = "";
            string sellerId = "";

            //create connection

            strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;
            
            //remove one of the substore
            string query = "UPDATE SubStore SET SubStore.subStoreStatus = @status WHERE SubStore.SubStoreName = @storeName AND SubStore.sellerId = @sellerId";

            try
            {
                //get seller id
                sellerId = getSellerId();

                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("status", status);
                cmdRetrieve.Parameters.AddWithValue("@storeName", ("SubStore"+subStoreNum));
                cmdRetrieve.Parameters.AddWithValue("@sellerId", sellerId);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        } //(use by create subStore)

        //Create substore button
        void addBtnControl(int subStoreCount)
        {
            //div container for one substore button
            HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
            createDiv.ID = "createDiv" + subStoreCount.ToString();
            createDiv.Attributes["class"] += "flex-row ";
            createDiv.Attributes["class"] += "small-right-gap";        

            //substore button
            Button btnSubStore = new Button();
            btnSubStore.ID = "btnSubStore" + subStoreCount.ToString();
            btnSubStore.Text = "SubStore" + subStoreCount.ToString();
            btnSubStore.CssClass += "btn-medium-lightgray ";
            btnSubStore.CssClass += "marginless";
            btnSubStore.Click += new EventHandler(btnSubStore_Click);
            createDiv.Controls.Add(btnSubStore);

            //cancel button
            ImageButton btnCancel = new ImageButton();
            btnCancel.ID = "btnCancel" + subStoreCount.ToString();
            btnCancel.ImageUrl = "https://cdn.pixabay.com/photo/2012/04/12/20/12/x-30465_960_720.png";
            btnCancel.CssClass += "small-image ";
            btnCancel.CssClass += "cancel-container";
            btnCancel.Click += new ImageClickEventHandler(btnCancel_Click);
            createDiv.Controls.Add(btnCancel);

            Panel1.Controls.Add(createDiv);

        }

        //Each substore event
        void btnSubStore_Click(Object sender, EventArgs e)
        {
            string username = "";
            string sellerId = "";
            string subStoreName = "";
            Button btnSubStore;

            //load database
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);


            //prepare command
            SqlCommand cmdRetrieve;
            //remove one of the substore
            string query = "SELECT ProductPhoto.productPhotoURL, ProductDetails.productName, MAX(BidTable.bidPrice) AS Bid, FixedPriceProduct.productPrice, Product.productStock " +
                "FROM ProductPhoto INNER JOIN " +
                "Product ON ProductPhoto.productId = Product.productId FULL JOIN " +
                "BidTable ON Product.productId = BidTable.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON Product.productId = FixedPriceProduct.productId INNER JOIN " +
                "SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN " +
                "Seller ON Substore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Seller.sellerId = @sellerId AND SubStore.subStoreName = @storeName AND photoStatus = 'Main' " +
                "GROUP BY Product.addDateTime, ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock " +
                "ORDER BY Product.addDateTime ";

            //get which substore have been chosen
            btnSubStore = (Button)sender;
            subStoreName = btnSubStore.Text;

            try
            {
                con.Open();
                //get sellerId
                sellerId = getSellerId();

                //get product
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@sellerId", sellerId);
                cmdRetrieve.Parameters.AddWithValue("@storeName", subStoreName);

                //bind to the repeater
                Repeater1.DataSource = cmdRetrieve.ExecuteReader();
                Repeater1.DataBind();

                //dynamic control still exist
                //generateSubStore();
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //Remove substore event
        void btnCancel_Click(Object sender, EventArgs e)
        {
            //remove one of the substore from database
            string username = "";
            string sellerId = "";
            string subStoreName = "";
            int subStoreNum = 0;
            int subStoreNameLen = 0;

            //get which substore have been chosen
            ImageButton btnCancel = (ImageButton)sender;
            string btnCancelId = btnCancel.ClientID;
            subStoreName = btnCancelId.Trim();

            subStoreNameLen = subStoreName.Length-1;
            int i = 0;
            int.TryParse(subStoreName[subStoreNameLen - i].ToString(), out i);
            subStoreNum = Convert.ToInt32(i);
            //btnCancel.Parent;

            //update status
            updateSubStoreStatus(subStoreNum, "Inactive");

            //dynamically display the subStore button
            //generateSubStore();

        }

        void Page_Error()
        {
            Response.Write("<div>Sorry for the error</div>");
            Response.Write(Server.GetLastError().Message + "<br />");
            Server.ClearError();
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var btnEdit = (Button)sender;
            var lblProdName = btnEdit.NamingContainer.FindControl("prodName") as Label;
            Response.Redirect("/Seller/EditProduct.aspx?prodName="+ lblProdName.Text);
        }
    }
}