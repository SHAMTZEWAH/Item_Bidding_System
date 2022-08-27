using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace Item_Bidding_System.User
{
	public partial class Cart : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Label1.Text = getTotalPrice().ToString("0.00");
			Session["totalPrice"] = getTotalPrice();
		}

        protected void BtnAdd_Click(object sender, EventArgs e)
        {


            DataListItem item = ((Button)sender).NamingContainer as DataListItem;
            Label labelQuantity = (Label)item.FindControl("quantity");
            double value = double.Parse(labelQuantity.Text);
            value = value + 1;
            labelQuantity.Text = value.ToString();
            double newValue = double.Parse(labelQuantity.Text);

            HiddenField hfProductId = (HiddenField)item.FindControl("HiddenField1");
            String productId = hfProductId.Value;

            calculateUnitTotalPrice(newValue, productId);

            //------------------------------UpdateCartDatabase--------------------------------
            updateCartDatabase(value, productId);


        }

        string getAccId()
        {
            string accId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT accId FROM Account WHERE username = @name AND email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                accId = (string)cmdRetrieve.ExecuteScalar();

            }
            catch (NullReferenceException ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return accId;
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            DataListItem item = ((Button)sender).NamingContainer as DataListItem;
            Label labelProductName = (Label)item.FindControl("productName");
            String productName = labelProductName.Text;

            HiddenField hfProductId = (HiddenField)item.FindControl("HiddenField1");
            String productId = hfProductId.Value;

            string DeleteProduct = "Delete From CartTable Where CartTable.productId = @productId AND CartTable.accId = @accId";
            SqlCommand deleteCommand = new SqlCommand(DeleteProduct, con);

            deleteCommand.Parameters.AddWithValue("@accId", accId);
            deleteCommand.Parameters.AddWithValue("@productId", productId);
            deleteCommand.ExecuteNonQuery();

            con.Close();

            Response.Redirect("Cart.aspx");
        }

        protected void BtnMinus_Click(object sender, EventArgs e)
        {
            DataListItem item = ((Button)sender).NamingContainer as DataListItem;
            Label labelQuantity = (Label)item.FindControl("quantity");
            double value = double.Parse(labelQuantity.Text);
            if (value > 0)
            {
                value = value - 1;
            }
            labelQuantity.Text = value.ToString();
            double newValue = double.Parse(labelQuantity.Text);

            HiddenField hfProductId = (HiddenField)item.FindControl("HiddenField1");
            String productId = hfProductId.Value;

            calculateUnitTotalPrice(newValue, productId);

            //------------------------------UpdateCartDatabase--------------------------------
            updateCartDatabase(value, productId);

        }

        //check stock
        public void updateCartDatabase(double quantity, string productId)
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string retrieveStock = "Select stock From Product Where productId = @productId"; //check stock quantity
            SqlCommand retrieveStockCmd = new SqlCommand(retrieveStock, con);
            retrieveStockCmd.Parameters.AddWithValue("@productId", productId);
            string stock = retrieveStockCmd.ExecuteScalar().ToString();
            con.Close();

            if (quantity <= double.Parse(stock))
            {

                con.Open();

                string updateQuantity = "UPDATE CartTable SET quantity = @quantity FROM CartTable WHERE CartTable.accId = @accId AND CartTable.productId = @productId";
                SqlCommand updateQuantityCmd = new SqlCommand(updateQuantity, con);
                updateQuantityCmd.Parameters.AddWithValue("@accId", accId);
                updateQuantityCmd.Parameters.AddWithValue("@productId", productId);
                updateQuantityCmd.Parameters.AddWithValue("@quantity", quantity);
                updateQuantityCmd.ExecuteNonQuery();

                con.Close();


            }
            else
            {

                quantity -= 1;
                con.Open();

                string updateQuantity = "UPDATE CartTable SET quantity = @quantity FROM CartTable WHERE CartTable.accId = @accId AND CartTable.productId = @productId";
                SqlCommand updateQuantityCmd = new SqlCommand(updateQuantity, con);
                updateQuantityCmd.Parameters.AddWithValue("@accId", accId);
                updateQuantityCmd.Parameters.AddWithValue("@productId", productId);
                updateQuantityCmd.Parameters.AddWithValue("@quantity", quantity);
                updateQuantityCmd.ExecuteNonQuery();

                con.Close();
                System.Windows.Forms.MessageBox.Show("Sorry we run out of stock", "Alert");

            }
            Response.Redirect("Cart.aspx");

        }

        double calculateUnitTotalPrice(double quantity, string productId)
        {
            double totalPrice = 0;

            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string getUnitTotal = "Select (OrderProduct.totalPrice/OrderProduct.quantity) AS price From Product INNER JOIN " +
                "OrderProduct.productid = Product.productId where Product.productId = @productId";
            SqlCommand getUnitTotalCommand = new SqlCommand(getUnitTotal, con);
            getUnitTotalCommand.Parameters.AddWithValue("@productId", productId);
            var unitPrice = getUnitTotalCommand.ExecuteScalar();
            totalPrice = quantity * double.Parse(unitPrice.ToString());

            con.Close();
            return totalPrice;
        }
        double getTotalPrice()
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            String getTotal = "Select CartTable.quantity, (OrderProduct.totalPrice/OrderProduct.quantity) AS price " +
                "From CartTable Inner Join " +
                "Product On CartTable.productId = Product.productId INNER JOIN " +
                "OrderProduct ON OrderProduct.productId = CartTable.productId INNER JOIN " +
                "Account ON CartTable.accId = Account.accId INNER JOIN " +
                "ProductDetails.productDetailsId = Product.productDetailsId " +
                "WHERE CartTable.accId = @accId AND CartTable.productId = @productId";
            SqlCommand getTotalCmd = new SqlCommand(getTotal, con);
            getTotalCmd.Parameters.AddWithValue("@accId", accId);
            getTotalCmd.Parameters.AddWithValue("@productId", accId);
            SqlDataReader priceAndQuantity = getTotalCmd.ExecuteReader();
            double totalPrice = 0;

            while (priceAndQuantity.Read())
            {

                totalPrice = totalPrice + (double.Parse(priceAndQuantity["price"].ToString()) * (double.Parse(priceAndQuantity["quantity"].ToString())));
            }
            con.Close();
            return totalPrice;



        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "0")
            {
                Response.Write("<script>alert('Please select some item')</script>");
            }

            else
            {
                Response.Redirect("paymentPage.aspx");
            }
        }
    }
}