using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ManageOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                loadData();
            }
            try
            {
                if (Request.QueryString["orderFilter"] != null)
                {
                    loadData();
                    //if(Request.QueryString["orderFilter"] == "Awaiting Payment" || Request.QueryString["orderFilter"] == "All")
                    //{
                    //    //have confirm bid button
                    //    loadData();
                    //}
                    //else
                    //{
                    //    //for viewing purposes
                    //    loadData();
                    //}
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        int getOrderProductCount()
        {
            int orderCount = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(OrderProduct.orderProductId) FROM OrderProduct";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                orderCount = (int)cmdRetrieve.ExecuteScalar();
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
            return orderCount;
        }
        int getOrderCount()
        {
            int orderCount = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(Orders.orderId) FROM Orders";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                orderCount = (int)cmdRetrieve.ExecuteScalar();
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
            return orderCount;
        }

        void updateOrderStatus()
        {

        }

        void createNewOrder(string orderStatus, int quantity, double totalPrice, string addressId,  string productId)
        {
            int orderCount = 0;
            int orderProductCount = 0;
            string orderId = "";
            string orderProductId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command
            SqlCommand cmdRetrieve;

            //get details
            string query = "INSERT INTO Orders(orderId, orderStatus) " +
                "VALUES(@orderId, @status)";
            string queryOrderProduct = "INSERT INTO OrderProduct(orderProductId, quantity, createOrderDateTime, totalPrice, addressId, productId, orderId) " +
                "VALUES(@orderProductId, @quantity, CONVERT(DATETIME, GETDATE(), 120), @totalPrice, @addressId, @productId, @orderId)";

            try
            {
                //get order count
                orderCount = getOrderCount();
                if(orderCount + 1 > 9)
                {
                    orderId = "o_0" + (orderCount+1);
                }
                else
                {
                    orderId = "o_00" + (orderCount + 1);
                }

                //get order product count
                orderProductCount = getOrderProductCount();
                if (orderProductCount + 1 > 9)
                {
                    orderProductId = "op_0" + (orderProductCount + 1);
                }
                else
                {
                    orderProductId = "op_00" + (orderProductCount + 1);
                }

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@orderId", orderId);
                cmdRetrieve.Parameters.AddWithValue("@status", orderStatus);

                cmdRetrieve = new SqlCommand(queryOrderProduct, con);
                cmdRetrieve.Parameters.AddWithValue("@orderProductId", orderProductId);
                cmdRetrieve.Parameters.AddWithValue("@quantity", quantity);
                cmdRetrieve.Parameters.AddWithValue("@totalPrice", totalPrice);
                cmdRetrieve.Parameters.AddWithValue("@addressId", addressId);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.Parameters.AddWithValue("@orderId", orderId);

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
            string query = "SELECT OrderProduct.orderId, ProductPhoto.productPhotoURL, ProductDetails.productName, ProductDetails.productBrand, ProductDetails.productModel, OrderProduct.quantity, OrderProduct.totalPrice, OrderProduct.addressId, OrderProduct.createOrderDateTime, Orders.orderStatus " +
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
                "WHERE Account.username = @name AND Account.email = @email AND ProductPhoto.photoStatus = 'Main' AND Orders.orderStatus = @filter";
            string queryWithoutStatus = "SELECT OrderProduct.orderId, ProductPhoto.productPhotoURL, ProductDetails.productName, ProductDetails.productBrand, ProductDetails.productModel, OrderProduct.quantity, OrderProduct.totalPrice, OrderProduct.addressId, OrderProduct.createOrderDateTime, Orders.orderStatus " +
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
                "WHERE Account.username = @name AND Account.email = @email AND ProductPhoto.photoStatus = 'Main'";
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                if (Request.QueryString["orderFilter"] != null)
                {
                    filter = Request.QueryString["orderFilter"];
                    if (filter == "Awaiting Payment")
                    {
                        cmdRetrieve.Parameters.AddWithValue("@status","Pending");
                    }
                    else if (filter == "To Shipped")
                    {
                        cmdRetrieve.Parameters.AddWithValue("@status","ToShipped");
                    }
                    else if (filter == "Product Received")
                    {
                        cmdRetrieve.Parameters.AddWithValue("@status","ProductReceived");
                    }
                    else
                    {
                        cmdRetrieve = new SqlCommand(queryWithoutStatus, con);
                        cmdRetrieve.Parameters.AddWithValue("@status","");
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

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonList1.SelectedValue == "Awaiting Payment")
            {
                Response.Redirect("/Seller/ManageOrder.aspx?orderFilter=Awaiting Payment");
            }
            else if(RadioButtonList1.SelectedValue == "To Shipped")
            {
                //OrderList
                Response.Redirect("/Seller/ManageOrder.aspx?orderFilter=To Shipped");
            }
            else if (RadioButtonList1.SelectedValue == "Product Received")
            {
                //OrderList
                Response.Redirect("/Seller/ManageOrder.aspx?orderFilter=Product Received");
            }
            else
            {

                Response.Redirect("/Seller/ManageOrder.aspx?orderFilter=All");
            }
        }
    }
}