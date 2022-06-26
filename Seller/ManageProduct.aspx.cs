using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ManageProduct : System.Web.UI.Page, IPostBackEventHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getConnection();
                btnCreateStore.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this, "clickDiv");

                int subStoreCount = 0;
                subStoreCount = getSubStoreCount();
                for (int i = 1; i <= subStoreCount; i++)
                {
                    addBtnControl(i);
                }
            }
        }

        void getConnection()
        {
            string username = User.Identity.Name;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);
            

            //prepare command
            SqlCommand cmdRetrieve;
            string query = "SELECT ProductPhoto.productPhotoURL, ProductDetails.productName, MAX(BidTable.bidPrice) AS Bid, FixedPriceProduct.productPrice, Product.productStock " +
                "FROM ProductPhoto INNER JOIN " +
                "Product ON ProductPhoto.productId = Product.productId INNER JOIN " +
                "BidTable ON Product.productId = BidTable.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON Product.productId = FixedPriceProduct.productId INNER JOIN " +
                "SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN " +
                "Seller ON Substore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Account.username = @username " +
                "GROUP BY Product.addDateTime, ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock " +
                "ORDER BY Product.addDateTime ";
            
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", username);
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
            ddlBid.SelectedValue = ddlBid.Items[0].Value;
            for (int i = 0; i < ddlBid.Items.Count; i++)
            {
                ddlBid.Items[i].Text = String.Format("{0:0.00}",Double.Parse(ddlBid.Items[i].Text.ToString()));
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
            int subStoreCount = getSubStoreCount();
            addBtnControl(subStoreCount);
            updateSubStoreCount(subStoreCount);

            for (int i = 1; i <= subStoreCount; i++)
            {
                addBtnControl(i);
            }
        }

        //Post back event for create substore button
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
            string query = "SELECT COUNT(SubStore.subStoreId) AS SubStoreCount FROM Seller, Account, SubStore WHERE Account.accId = Seller.accId AND Seller.sellerId = SubStore.sellerId AND Account.username = @username";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
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

        //get seller id
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

        //update substore
        void updateSubStoreCount(int subStoreCount)
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
                cmdRetrieve.Parameters.AddWithValue("@subStoreName", "SubStore " + subStoreCount.ToString());
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
        }

        //Create substore button
        void addBtnControl(int subStoreCount)
        {
            //div container
            HtmlGenericControl createDiv = new HtmlGenericControl("DIV");
            createDiv.ID = "createDiv";
            createDiv.Attributes["class"] += "flex-row ";
            createDiv.Attributes["class"] += "small-right-gap";
            SubStoreCon.Controls.Add(createDiv);

            //substore button
            Button btnSubStore = new Button();
            btnSubStore.ID = "btnSubStore";
            btnSubStore.Text = "SubStore " + subStoreCount.ToString();
            btnSubStore.CssClass += "btn-medium-lightgray ";
            btnSubStore.CssClass += "marginless";
            btnSubStore.Click += (sender, e) =>
            {
                //what you want to do when click
                btnSubStore_Click(sender, e);
            };
            createDiv.Controls.Add(btnSubStore);

            //cancel button
            ImageButton btnCancel = new ImageButton();
            btnCancel.ID = "btnCancel";
            btnCancel.ImageUrl = "https://cdn.pixabay.com/photo/2012/04/12/20/12/x-30465_960_720.png";
            btnCancel.CssClass += "small-image ";
            btnCancel.CssClass += "cancel-container";
            btnCancel.Click += (sender, e) =>
            {
                btnCancel_Click(sender, e);
            };
            createDiv.Controls.Add(btnCancel);
        }
        

        //Each substore event
        void btnSubStore_Click(Object sender, EventArgs e)
        {

        }

        //Remove substore event
        void btnCancel_Click(Object sender, EventArgs e)
        {

        }

        void Page_Error()
        {
            Response.Write("<div>Sorry for the error</div>");
            Response.Write(Server.GetLastError().Message + "<br />");
            Server.ClearError();
        }
    }
}