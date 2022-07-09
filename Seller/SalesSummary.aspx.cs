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
    public partial class SalesSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Product.productId, productPhotoURL, productName, productBrand, productModel, OrderProduct.quantity, OrderProduct.totalPrice, SUM(OrderProduct.totalPrice) AS [Total Costs]" + //sum of operation cost, sumn of profit
                "FROM Product INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "ProductPhoto ON ProductPhoto.productId = Product.productId INNER JOIN " +
                "OrderProduct ON OrderProduct.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                "Account ON Seller.accId = Account.accId " +
                "WHERE Account.username = @name AND Account.email = @email " +
                "GROUP BY Product.productId, productPhotoURL, productName, productBrand, productModel, OrderProduct.quantity, OrderProduct.totalPrice";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().UserName);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LblTotalCost.Text = String.Format("{0:0.00}",reader["Total Costs"].ToString());
                    }
                }
                GridView1.DataSource = reader;
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
    }
}