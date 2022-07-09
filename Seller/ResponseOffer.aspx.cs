using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ResponseOffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadDDL();
        }

        //load the drop down list with product name
        void loadDDL()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;

            //get details
            string query = "SELECT DISTINCT productName FROM ProductDetails INNER JOIN " +
                "Product ON ProductDetails.productDetailsId = Product.productDetailsId INNER JOIN " +
                "SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId FULL OUTER JOIN " +
                "SealedAuctionProduct ON SealedAuctionProduct.productId = Product.productId FULL OUTER JOIN " +
                "BidTable ON BidTable.productId = Product.productId " +
                "WHERE Account.username = @name AND Account.email = @email AND " +
                "SealedAuctionProduct.bidOffer IS NOT NULL OR " +
                "BidTable.productId = Product.productId";
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name",User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email",Membership.GetUser().Email);
                RadioButtonList1.DataSource = cmdRetrieve.ExecuteReader();
                RadioButtonList1.DataBind();
            }
            catch (Exception ex)
            {
                lblNoData.Visible = true;
                lblNoData.Text = ex.Message.ToString();
            }
            finally
            {
                RadioButtonList1.Items.Insert(0, new ListItem("All", ""));
                con.Close();
                con.Dispose();
            }
        }

        void loadData()
        {
            string productId = "";
            string filter = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;

            //get details
            string query = "SELECT Product.productId, productPhotoURL, productName, productBrand, productModel, , totalPrice, street, poscode, city, state, country, createOrderDateTime " +
                "FROM OrderProduct INNER JOIN " +
                "Product ON OrderProduct.productId = Product.productId INNER JOIN " +
                "ProductPhoto ON Product.productId = ProductPhoto.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "Orders ON OrderProduct.orderId = Orders.orderId INNER JOIN " +
                "FixedPriceProduct ON FixedPriceProduct.productId = Product.productId INNER JOIN " +
                "AuctionProduct ON AuctionProduct.productId = Product.productId INNER JOIN " +
                "OpenAuctionProduct ON OpenAuctionProduct.productId = Product.productId INNER JOIN " +
                "SealedAuctionProduct ON SealedAuctionProduct.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON SubStore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Account.username = @name AND Account.email = @email AND Orders.orderStatus = @filter";
            string queryWithoutStatus = "SELECT OrderProduct.orderId, ProductPhoto.productPhotoURL, ProductDetails.productName, ProductDetails.productBrand, ProductDetails.productModel, Orders.quantity, OrderProduct.totalPrice, Orders.Address, OrderProduct.dateCreatedOrder, Orders.orderStatus " +
                "FROM OrderProduct INNER JOIN " +
                "Product ON OrderProduct.productId = Product.productId INNER JOIN " +
                "ProductPhoto ON Product.productId = ProductPhoto.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "Orders ON OrderProduct.orderId = Orders.orderId INNER JOIN " +
                "FixedPriceProduct ON FixedPriceProduct.productId = Product.productId INNER JOIN " +
                "AuctionProduct ON AuctionProduct.productId = Product.productId INNER JOIN " +
                "OpenAuctionProduct ON OpenAuctionProduct.productId = Product.productId INNER JOIN " +
                "SealedAuctionProduct ON SealedAuctionProduct.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON SubStore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Account.username = @name AND Account.email = @email";
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                if (Request.QueryString["orderFilter"] != null)
                {
                    filter = Request.QueryString["orderFilter"];
                    if (filter == "Awaiting Payment")
                    {
                        cmdRetrieve.Parameters.AddWithValue("@status", "Pending");
                    }
                    else if (filter == "To Shipped")
                    {
                        cmdRetrieve.Parameters.AddWithValue("@status", "ToShipped");
                    }
                    else if (filter == "Product Received")
                    {
                        cmdRetrieve.Parameters.AddWithValue("@status", "ProductReceived");
                    }
                    else
                    {
                        cmdRetrieve = new SqlCommand(queryWithoutStatus, con);
                        cmdRetrieve.Parameters.AddWithValue("@status", "");
                    }
                    cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name);
                    cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                }
                GridView1.DataSource = cmdRetrieve.ExecuteReader();
                GridView1.DataBind();
            }
            catch (NullReferenceException ex)
            {
                lblNoData.Visible = true;
                lblNoData.Text = ex.Message.ToString();

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        void loadModalData()
        {

        }

        protected void btnReview_Click(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void btnAcceptOffer_Click(object sender, EventArgs e)
        {

        }

        protected void btnRejectOffer_Click(object sender, EventArgs e)
        {

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}