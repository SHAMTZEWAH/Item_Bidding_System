using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getConnection();
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
            cmdRetrieve = new SqlCommand();
            cmdRetrieve.CommandType = CommandType.StoredProcedure;
            cmdRetrieve.CommandText = "SelectAllProducts";
            
            try
            {
                con.Open();
                cmdRetrieve.Connection = con;
                cmdRetrieve.Parameters.AddWithValue("@name", username);
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

            //prepare query
            //string queryItem = "SELECT ProductPhoto.productPhotoURL, ProductDetails.productName, MAX(BidTable.bidPrice) AS currentBid, FixedPriceProduct.productPrice, Product.productStock, BidTable.bidPrice AS yourBid FROM ProductPhoto INNER JOIN Product ON ProductPhoto.productId = Product.productId INNER JOIN BidTable ON Product.productId = BidTable.productId INNER JOIN FixedPriceProduct ON Product.productId = FixedPriceProduct.productId INNER JOIN ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId WHERE (yourBid = (SELECT bt.bidPrice FROM BidTable AS bt INNER JOIN Account ON bt.accId = Account.accId INNER JOIN Product ON bt.productId = Product.productId WHERE bt.accId = Account.accId AND Account.username=@username))";
            //SqlCommand cmdRetrieve;
            //cmdRetrieve = new SqlCommand(queryItem, con);
            //String username = HttpContext.Current.User.Identity.Name; 
            //cmdRetrieve.Parameters.AddWithValue("@username", username);

            ////execute reader
            //SqlDataReader reader = cmdRetrieve.ExecuteReader();
            //Repeater1.DataSource = reader;
            //con.Close();
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
        }

        void Page_Error()
        {
            Response.Write("<div>Sorry for the error</div>");
            Response.Write(Server.GetLastError().Message + "<br />");
            Server.ClearError();


        }
    }
}