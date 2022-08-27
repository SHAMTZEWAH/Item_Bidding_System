using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.User
{
    public partial class paymentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accId = getAccId();
            div1.Attributes["onClick"] = ClientScript.GetPostBackEventReference(this, "ClickDiv");
            div2.Attributes["onClick"] = ClientScript.GetPostBackEventReference(this, "ClickDiv2");

            if (IsPostBack)
            {
                if (Request["__EVENTTARGET"] == "__Page" &&
                    Request["__EVENTARGUMENT"] == "ClickDiv")
                {

                    div_Click();

                }

                else if (Request["__EVENTTARGET"] == "__Page" &&
                    Request["__EVENTARGUMENT"] == "ClickDiv2")
                {
                    div2_Click();

                }


            }
            else
            {
                paypal1.Visible = true;
                paypal2.Visible = true;
                paypal3.Visible = true;
                paypal4.Visible = true;

            }
            //--------------------------------------------------------------------------------
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string retrieveQuery = "Select username, phoneNo, street, poscode, city, state,country " +
                "From Account INNER JOIN " +
                "Address ON Account.accId = Address.accId " +
                "Where accId = @accId";
            SqlCommand retrieveCmd = new SqlCommand(retrieveQuery, con);
            retrieveCmd.Parameters.AddWithValue("@accId", accId);
            SqlDataReader reader = retrieveCmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Name.Text = reader["username"].ToString();
                    Contact.Text = reader["phoneNo"].ToString();
                    Address.Text = reader["street"].ToString() + ", " + reader["poscode"].ToString() + ", " + reader["city"].ToString() + ", " + reader["state"].ToString() + ", " + reader["country"].ToString();
                }
            }
            con.Close();

            //---------------------------------------------------------------------------------
            showPoint();
            double subtotalPrice = Convert.ToDouble(Session["totalPrice"]);
            subtotal.Text = "RM " + subtotalPrice.ToString("0.00");
            double shippingPrice = Convert.ToInt32(Session["delivery"]);
            shipping.Text = "RM " + shippingPrice.ToString("0.00");
            double voucherApplied = 0;
            try
            {
                voucherApplied = double.Parse(TextBoxVoucher.Text);
            }
            catch (Exception ex)
            {

            }

            double totalPrice = Convert.ToDouble(Session["totalPrice"]);
            double taxPrice = calculateTax(totalPrice);
            tax.Text = "RM " + taxPrice.ToString("0.00");

            double finalPrice = calculateFinalPrice(subtotalPrice, shippingPrice, voucherApplied, taxPrice);
            finalTotal.Text = "RM " + finalPrice.ToString("0.00");

            if (String.IsNullOrEmpty(TextBoxVoucher.Text) || TextBoxVoucher.Text == "0")
            {
                int tokenObtained = tokenEarned(finalPrice);
                pointsEarned.Text = tokenObtained.ToString();
            }
            else
            {
                pointsEarned.Text = "0";
            }

            Session["paidPrice"] = finalPrice;


            //con.Open();

            //string DeleteProduct = "Delete From ShippingOrder Where accId = @accId";
            //SqlCommand deleteCommand = new SqlCommand(DeleteProduct, con);

            //deleteCommand.Parameters.AddWithValue("@accId", 1);
            //deleteCommand.ExecuteNonQuery();

            //con.Close();

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

        protected void div_Click()
        {
            shippingSelected.Text = "*Standard Delivery";
            shipping.Text = "3.00";
            Session["delivery"] = 3.00;
            Session["deliveryType"] = "Standard";
        }

        protected void div2_Click()
        {
            shippingSelected.Text = "*Express Delivery";
            shipping.Text = "10.00";
            Session["delivery"] = 10.00;
            Session["deliveryType"] = "Express";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "1")
            {
                paypal1.Visible = true;
                paypal2.Visible = true;
                paypal3.Visible = true;
                paypal4.Visible = true;
                touchAndGo1.Visible = false;
                touchAndGo2.Visible = false;
                visa1.Visible = false;
                visa2.Visible = false;
                visa3.Visible = false;
                visa4.Visible = false;
                visa5.Visible = false;
                visa6.Visible = false;
                visa7.Visible = false;
                visaMonth.Visible = false;
                visaYear.Visible = false;
                crypto1.Visible = false;
                crypto2.Visible = false;
                crypto3.Visible = false;
                crypto4.Visible = false;

            }
            else if (DropDownList1.SelectedValue == "2")
            {
                paypal1.Visible = false;
                paypal2.Visible = false;
                paypal3.Visible = false;
                paypal4.Visible = false;
                touchAndGo1.Visible = true;
                touchAndGo2.Visible = true;
                visa1.Visible = false;
                visa2.Visible = false;
                visa3.Visible = false;
                visa4.Visible = false;
                visa5.Visible = false;
                visa6.Visible = false;
                visa7.Visible = false;
                visaMonth.Visible = false;
                visaYear.Visible = false;
                crypto1.Visible = false;
                crypto2.Visible = false;
                crypto3.Visible = false;
                crypto4.Visible = false;

            }
            else if (DropDownList1.SelectedValue == "3")
            {
                paypal1.Visible = false;
                paypal2.Visible = false;
                paypal3.Visible = false;
                paypal4.Visible = false;
                touchAndGo1.Visible = false;
                touchAndGo2.Visible = false;
                visa1.Visible = true;
                visa2.Visible = true;
                visa3.Visible = true;
                visa4.Visible = true;
                visa5.Visible = true;
                visa6.Visible = true;
                visa7.Visible = true;
                visaMonth.Visible = true;
                visaYear.Visible = true;
                crypto1.Visible = false;
                crypto2.Visible = false;
                crypto3.Visible = false;
                crypto4.Visible = false;

            }
            else if (DropDownList1.SelectedValue == "4")
            {
                paypal1.Visible = false;
                paypal2.Visible = false;
                paypal3.Visible = false;
                paypal4.Visible = false;
                touchAndGo1.Visible = false;
                touchAndGo2.Visible = false;
                visa1.Visible = false;
                visa2.Visible = false;
                visa3.Visible = false;
                visa4.Visible = false;
                visa5.Visible = false;
                visa6.Visible = false;
                visa7.Visible = false;
                visaMonth.Visible = false;
                visaYear.Visible = false;
                crypto1.Visible = true;
                crypto2.Visible = true;
                crypto3.Visible = true;
                crypto4.Visible = true;

            }
        }

        protected void Proceed_Click(object sender, EventArgs e)
        {
            //if (DropDownList1.SelectedValue == "1")
            //{
            //    Response.Write("<form action='https://www.sandbox.paypal.com/cgi-bin/webscr' method ='post' target ='_top'>");
            //    Response.Write("<input type='hidden' name='cmd' value='_s - xclick'>");
            //    Response.Write("<input type='hidden' name='hosted_button_id' value='RZSC45TKZXBAA'>");
            //    Response.Write("<input type='hidden' name='amount' value='20'>");
            //    Response.Write("<input type='hidden' name='item_name' value='Total Price'>");
            //    Response.Write("<input type='hidden' name='quantity' value='1'>");
            //    Response.Write("<input type='image' src = 'https://www.sandbox.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif' border = '0' name = 'submit' alt = 'PayPal - The safer, easier way to pay online!'>");
            //    Response.Write("<input type='hidden' name='quantity' value='1'>");

            //}
            //else if (DropDownList1.SelectedValue == "2") 
            //{
            //    Response.Write("<form action='https://www.google.com'>");
            //}
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);

            con.Open();//retieve points to compare
            string retrievePoint = "Select point from Voucher Where accId = @accId";
            SqlCommand cmd1 = new SqlCommand(retrievePoint, con);
            cmd1.Parameters.AddWithValue("@accId", accId);
            int tokenCompare = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
            con.Close();

            if (Regex.IsMatch(TextBoxVoucher.Text, @"\d") && ((paypal3.Text.ToString()).Length > 1 || (touchAndGo2.Text.ToString()).Length > 1 || (crypto3.Text.ToString()).Length > 1 || (visa5.Text.ToString()).Length > 1))
            {
                if (Convert.ToInt32(TextBoxVoucher.Text) < tokenCompare)
                {


                    Response.Write("<script>alert('Payment Successfully')</script>");
                    con.Open();

                    int voucherLeft = Convert.ToInt32(TextBoxVoucher.Text);
                    string updateVoucher = "Update Voucher Set point = point - @used";
                    SqlCommand updateVoucherCmd = new SqlCommand(updateVoucher, con);
                    updateVoucherCmd.Parameters.AddWithValue("@used", voucherLeft);
                    updateVoucherCmd.ExecuteNonQuery();
                    con.Close();

                    TextBoxVoucher.Text = voucherLeft.ToString();
                    showPoint();

                    con.Open();
                    string selectProduct = "Select CartTable.productId, CartTable.quantity from CartTable Inner Join " +
                        "Product On CartTable.productId = Product.productId INNER JOIN " +
                        "Account ON CartTable.accId = Account.accId" +
                        "WHERE Account.accId = @accId";

                    List<String> product = new List<string>();
                    List<int> quantity = new List<int>();

                    SqlCommand selectProductCmd = new SqlCommand(selectProduct, con);
                    selectProductCmd.Parameters.AddWithValue("@accId", accId);
                    SqlDataReader reader = selectProductCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        product.Add(reader["productId"].ToString());
                        quantity.Add(Convert.ToInt32(reader["quantity"]));
                    }
                    con.Close();

                    //con.Open();
                    //string selectQuantity = "Select  from CartTable Inner Join " +
                    //    "TryProduct On CartTable.productId = TryProduct.productId";
                    
                    //SqlCommand selectQuantityCmd = new SqlCommand(selectQuantity, con);
                    //SqlDataReader reader2 = selectQuantityCmd.ExecuteReader();
                    //while (reader2.Read())
                    //{
                    //    quantity.Add(Convert.ToInt32(reader2["quantity"]));
                    //}
                    //con.Close();

                    con.Open();
                    string countProduct = "Select Count(cartId) From CartTable Where accId = @accId";
                    SqlCommand countProductCmd = new SqlCommand(countProduct, con);
                    countProductCmd.Parameters.AddWithValue("@accId", accId);
                    int productCount = Convert.ToInt32((countProductCmd.ExecuteScalar()).ToString());

                    con.Close();

                    for (int i = 0; i < productCount; i++)
                    {
                        con.Open();
                        string updateStock = "Update Product Set productStock = productStock - @stock Where productId = @productId";
                        SqlCommand updateStockCmd = new SqlCommand(updateStock, con);
                        updateStockCmd.Parameters.AddWithValue("@productId", product[i]);
                        updateStockCmd.Parameters.AddWithValue("@stock", quantity[i]);
                        updateStockCmd.ExecuteNonQuery();
                        con.Close();
                    }

                    con.Open();
                    string updateShipping = "Insert into ShippingOrder(amount, shippingDesc, accId, shippingInfo, shippingStatus) VALUES(@amount, @shippingDesc, @accId, @shippingInfo, 'Not Received')";
                    SqlCommand updateShippingCmd = new SqlCommand(updateShipping, con);
                    updateShippingCmd.Parameters.AddWithValue("@amount", Session["paidPrice"]);
                    updateShippingCmd.Parameters.AddWithValue("@shippingDesc", Convert.ToInt32(productCount));
                    updateShippingCmd.Parameters.AddWithValue("@accId", accId);
                    updateShippingCmd.Parameters.AddWithValue("@shippingInfo", Session["deliveryType"]);
                    updateShippingCmd.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string deleteCart = "Delete From CartTable Where accId = @accId";
                    SqlCommand deleteCartCmd = new SqlCommand(deleteCart, con);
                    deleteCartCmd.Parameters.AddWithValue("@accId", accId);
                    deleteCartCmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    Response.Write("<script>alert('Insufficient of token')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please fill in the required field')</script>");
            }

        }

        public double calculateTax(double totalPrice)
        {

            double finalTax = totalPrice * 0.06;
            return finalTax;
        }

        public double calculateFinalPrice(double first, double second, double third, double forth)
        {

            double finalPrice = first + second - (third / 100) + forth;
            return finalPrice;

        }

        public int tokenEarned(double finalPrice)
        {

            double tokenEarned = finalPrice * 0.05;
            int adjustedToken = Convert.ToInt32(tokenEarned);
            return adjustedToken;
        }

        protected void showPoint()
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string showPoints = "Select Voucher.point From Voucher Inner Join " +
                "CartTable On CartTable.accId = Voucher.accId " +
                "Where CartTable.accId = @accId";
            SqlCommand showPointsCmd = new SqlCommand(showPoints, con);
            showPointsCmd.Parameters.AddWithValue("@accId", accId);
            LabelPoint.Text = (showPointsCmd.ExecuteScalar()).ToString();
            con.Close();

        }

        protected void btnVoucher_Click(object sender, EventArgs e)
        {
            Response.Redirect("voucherStakingPage.aspx");
        }
    }
}