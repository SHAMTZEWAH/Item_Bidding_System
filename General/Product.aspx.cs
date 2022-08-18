using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlConnection con;
            if (!IsPostBack)
            {
               
            }
            executeAllMethodAsync();

        }

        void executeAllMethodAsync()
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["selection"].ToString() != null)
                    {
                        //show category product when the user click on the category bar below the search bar
                        loadContentAsync(Request.QueryString["selection"].ToString());

                    }
                    else if (Request.QueryString["keyword"].ToString() != null) //searching keyword (on search bar)
                    {
                        //show product based on the keyword
                         loadContentAsync(Request.QueryString["keyword"].ToString());
                    }
                    else
                    {
                         loadHotProductAsync();
                    }


                }
                catch (NullReferenceException ex)
                {
                    //Show Hot product
                     loadHotProductAsync(); 
                }
                finally
                {
                    ctrl_IndexChangedHandler("");
                }
            }
        }
        void ctrl_IndexChangedHandler(string value) //not used
        {
            //ViewState[];
            //var filterControl = new Control();
            //filterControl = Page.Master.FindControl("SideMenu");
            //filterControl = filterControl.FindControl("SideMenuFilter.ascx");
            //DropDownList ddlSelect = (DropDownList)(filterControl as SideMenuFilter).NamingContainer.FindControl("radioSelect");
            //// change index
            //if (ViewState["selection"] != null)
            //{
            //    ddlSelect.SelectedValue = ViewState["selection"].ToString();
            //}
        }

        void loadHotProductAsync()
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;

            if (con.State.ToString() == "Open")
            {
                con.Close();
            }

            //execute query
                try
                {

                    con.Open();
                    cmdRetrieve = new SqlCommand();
                    cmdRetrieve.CommandType = CommandType.StoredProcedure;
                    cmdRetrieve.CommandText = "pr_HotProduct"; //need to change to "pr_HotProduct"
                    cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name); //need to remove
                    cmdRetrieve.Connection = con;
                    Repeater1.DataSource = cmdRetrieve.ExecuteReader();
                    Repeater1.DataBind();
                }
                catch (NullReferenceException exDB)
                {
                    Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                    if (lblFooter != null)
                    {
                        lblFooter.Visible = true;
                        lblFooter.Text = exDB.Message.ToString();
                    }

                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
        } 

        DataTable loadSPAsync(string filterDetails, string sp)
        {
            string username = "";
            string[] category = { };
            string minPrice = "";
            string maxPrice = "";
            string[] state = { };
            string[] sellingFormat = { };
            string selectedSellingFormat = "";
            string selection = "";
            int flag = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            //create connectionString
            SqlConnection con = new SqlConnection();
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            //apply string query
            SqlCommand cmdRetrieve;
            string queryWithoutMinAndMax = "SELECT Product.productId, ProductPhoto.productPhotoURL, ProductPhoto.productPhoto, ProductDetails.productName, MAX(BidTable.bidPrice) AS maxBid, FixedPriceProduct.productPrice, Product.productStock, ISNULL(allProduct.yourBid,0) AS yourBid " +
                "FROM ProductPhoto INNER JOIN " +
                "Product ON ProductPhoto.productId = Product.productId " +
                "FULL JOIN BidTable ON Product.productId = BidTable.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON Product.productId = FixedPriceProduct.productId LEFT JOIN " +
                "AuctionProduct ON AuctionProduct.productId = Product.productId LEFT JOIN " +
                "OrderProduct ON OrderProduct.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId INNER JOIN " +
                "Address ON Address.accId = Account.accId LEFT JOIN " +
                "vw_allProduct AS allProduct ON allProduct.productId = Product.productId " +
                "WHERE(Product.addDateTime >= DateAdd(day, 1, @secondDate) AND Product.addDateTime < DateAdd(month, 1, @startDate)) AND ProductPhoto.photoStatus = 'Main' AND " +
                "FixedPriceProduct.productPrice >= @minprice AND FixedPriceProduct.productPrice <= @maxprice AND ";
            string query = "";
            
            if (con.State.ToString() == "Open")
            {
                con.Close();
            }

            //wait for the connection string to create 
            con.ConnectionString = strCon;

            //execute query
            try
            {
                con.Open();

                //wait for the username to have value 

                    try
                    {
                        username = User.Identity.Name;
                    }
                    catch(Exception ex)
                    {
                        username = "";
                    }
                
                //wait for the category to have value
                    try
                    {
                        string qValue = Request.QueryString["category"];
                        category = qValue.Split(',');
                    }
                    catch (Exception ex)
                    {
                        category = new string[1] { "" };
                    }

                //wait for the min price to have value
                    try
                    {
                        minPrice = Request.QueryString["minPrice"];
                    }
                    catch (Exception ex)
                    {
                        minPrice = "";
                    }

                //wait for the max price to have value
                    try
                    {
                        maxPrice = Request.QueryString["maxPrice"];
                    }
                    catch (Exception ex)
                    {
                        maxPrice = "";
                    }

                //wait for the state to have value
                    try
                    {
                        string qValue = Request.QueryString["state"];
                        state = qValue.Split(',');
                    }
                    catch (Exception ex)
                    {
                        state = new string[1] { "" };
                    }

                //wait for the state to have value
                    try
                    {
                        string qValue = Request.QueryString["option"];
                        sellingFormat = qValue.Split(',');
                    }
                    catch (Exception ex)
                    {
                        sellingFormat = new string[1] {""};
                    }

                //wait for the selection to have value
                    try
                    {
                        selection = Request.QueryString["selection"];
                    }
                    catch (Exception ex)
                    {
                        selection = "";
                    }

                //if no min and max
                //if have min and max
                //if have min, but no max
                //if have max, but no min
                //if Hots
                //if Newly added
                //if Auction Product is NULL (no check & fixed product only)
                //if Auction Product is NOT NULL (have check & auction product only)
                //@secondDate ('08/02/2022'), @startDate ('08/01/2022'), @category, @minprice, @maxprice, @state, @sellingformat, selection

                //min and max price
                query = queryWithoutMinAndMax;
                if (string.IsNullOrEmpty(minPrice))
                {           
                    minPrice = "0";
                }
                if (string.IsNullOrEmpty(maxPrice))
                {
                    maxPrice = "1000000";
                }

                //Auction product is null or not null (selling format)
                flag = 0;
                foreach (string opt in sellingFormat) //OpenAuction
                {
                    if (opt == "SealedBidAuction" || opt == "OpenBidAuction")
                    {
                        query += "AuctionProduct.productId IS NOT NULL AND ";
                        ++flag;
                        break;
                    }
                    else if (string.IsNullOrEmpty(opt))
                    {
                        break;
                    }
                    ++flag;
                }
                if (flag == 1)
                {
                    query += "AuctionProduct.productId IS NULL AND ";
                }
                else if(flag==0)
                {
                    query += "";
                }

                //product category
                //Address.state LIKE '%' + @state + '%' AND ";
                flag = 0;
                query += "";
                for(int i=0; i<category.Length && !string.IsNullOrEmpty(category[i]); i++)
                {
                    query += "ProductDetails.productCategory = '"+category[i]+ "' OR ";
                    flag++;
                }
                query = query.TrimEnd(new char[] { 'O', 'R', ' ' });
                if (flag > 0)
                {
                    query += " AND ";
                }


                //address state
                flag = 0;
                query += "";
                for (int i = 0; i < state.Length && !string.IsNullOrEmpty(state[i]); i++)
                {
                    query += "Address.state = '" + state[i] + "' OR ";
                    flag++;
                }
                query = query.TrimEnd(new char[] { 'O', 'R', ' ' });
                if (flag > 0)
                {
                    query += " AND ";
                }

                query.TrimEnd(new char[] {' '});
                if(query.EndsWith("AND"))
                {
                    query = query.TrimEnd(new char[] { 'A', 'N', 'D' });
                }
                else if (query.EndsWith("OR"))
                {
                    query = query.TrimEnd(new char[] { 'O', 'R' });
                }
                query += " GROUP BY Product.productId, ProductPhoto.productPhotoURL, ProductPhoto.productPhoto, ProductDetails.productName, Product.addDateTime, FixedPriceProduct.productPrice, Product.productStock, allProduct.yourBid ";

                //selection
                if (selection == "NewlyAdded")
                {
                    query += "ORDER BY Product.addDateTime DESC";
                }
                else if (selection == "Hots")
                {
                    query += "ORDER BY COUNT(OrderProduct.productId) DESC";
                }

                //wait for the cmdRetrieve is ready
                cmdRetrieve = new SqlCommand(query, con);
                int monthInt = DateTime.Now.Month;
                    string month = "";
                    if (monthInt <= 9)
                    {
                        month += ("0" + monthInt.ToString());
                    }
                    else
                    {
                        month = monthInt.ToString();
                    }
                    cmdRetrieve.Parameters.AddWithValue("@name", username);
                    cmdRetrieve.Parameters.AddWithValue("@secondDate", (month+"/02/2022"));
                    cmdRetrieve.Parameters.AddWithValue("@startDate",(month+"/01/2022"));
                    cmdRetrieve.Parameters.AddWithValue("@minprice", minPrice);
                    cmdRetrieve.Parameters.AddWithValue("@maxprice", maxPrice);

                //wait for the dataTable to be fill before return
                adapter = new SqlDataAdapter(cmdRetrieve);
                adapter.Fill(dataTable);

                
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
            return dataTable;
        }

        void loadContentAsync(string filterDetails)
        {
            DataTable resultTable = new DataTable();

            resultTable = loadSPAsync(filterDetails, "");

            //assign dataset to the repeater
            Repeater1.DataSource = resultTable;
            Repeater1.DataBind();
        }

        void redirectToProductDetails(int rowNo)
        {
            //get prodName
            //get repeater row controls
            var prodNameCtrl = Repeater1.Items[rowNo].FindControl("prodName") as Label;
            string prodName = prodNameCtrl.Text;

            //redirect to the product details page
            Response.Redirect("/General/ProductDetails.aspx?prodName=" + prodName);
        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {
            var imgBtn = (ImageButton)sender;
            var hfRow = imgBtn.NamingContainer.FindControl("hfRow") as HiddenField;
            redirectToProductDetails(Convert.ToInt32(hfRow.Value));
        }

        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            var imgBtn = (ImageButton)sender;
            var hfRow = imgBtn.NamingContainer.FindControl("hfRow") as HiddenField;
            redirectToProductDetails(Convert.ToInt32(hfRow.Value));
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }


    }
}