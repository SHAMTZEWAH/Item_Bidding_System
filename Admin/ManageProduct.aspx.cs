using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Admin
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<CheckBox> checkBox1;

            if (!IsPostBack)
            {
                loadProductDetails();
                checkBox1 = checkBoxContainer();
                //retrieve all the checkbox which is checked
                foreach (CheckBox checkbox in checkBox1)
                {
                    displayToggle(checkbox);
                }

            }
        }

        void loadProductDetails()
        {
            //string sellerId = "";
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;
            //string getSellerId = "SELECT sellerId FROM Seller INNER JOIN Account ON Account.accId = Seller.accId WHERE Account.username = @username AND Account.email = @email";
            string query = "SELECT Product.productId, ProductDetails.productName, ProductPhoto.productPhotoURL, Productphoto.productPhoto, Product.addDateTime, ProductDetails.productBrand, ProductDetails.productModel, Seller.businessName, Product.productStatus " +
                "FROM Product INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN " +
                "Seller ON SubStore.sellerId = Seller.sellerId INNER JOIN " +
                "ProductPhoto ON ProductPhoto.productId = Product.productId " +
                "WHERE ProductPhoto.photoStatus = 'Main' ";

            string filterDate = "ORDER BY Product.addDateTime DESC";
            string filterProductName = "ORDER BY ProductDetails.productName";
            string filterProductBrand = "ORDER BY ProductDetails.productBrand";
            string filterProductModel = "ORDER BY ProductDetails.productModel";
            string filterBusinessName = "ORDER BY Seller.businessName";

            //execute query
            try
            {
                if(con.State.ToString() == "Open")
                {
                    con.Close();
                }

                con.Open();
                //get seller id
                //cmdRetrieve = new SqlCommand(getSellerId, con);
                //cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                //cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                //sellerId = (string)cmdRetrieve.ExecuteScalar();
                if (Request.QueryString["filter"] == "addDateTime")
                {
                    query = query + filterDate;
                    RadioButtonList1.SelectedValue = "addDateTime";
                }
                else if (Request.QueryString["filter"] == "productName")
                {
                    query = query + filterProductName;
                    RadioButtonList1.SelectedValue = "productName";
                }
                else if (Request.QueryString["filter"] == "productBrand")
                {
                    query = query + filterProductBrand;
                    RadioButtonList1.SelectedValue = "productBrand";
                }
                else if (Request.QueryString["filter"] == "productModel")
                {
                    query = query + filterProductModel;
                    RadioButtonList1.SelectedValue = "productModel";
                }
                else if (Request.QueryString["filter"] == "businessName")
                {
                    query = query + filterBusinessName;
                    RadioButtonList1.SelectedValue = "businessName";

                }
                //execute data
                cmdRetrieve = new SqlCommand(query, con);
                userGrid.DataSource = cmdRetrieve.ExecuteReader();
                userGrid.DataBind();
            }
            catch (NullReferenceException exDB)
            {
                Label lblFooter = (Label)userGrid.FindControl("lblNoData");
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

        void updateProductStatus(string productId, string status)
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;
            //string getSellerId = "SELECT sellerId FROM Seller INNER JOIN Account ON Account.accId = Seller.accId WHERE Account.username = @username AND Account.email = @email";
            string query = "UPDATE Product SET productStatus = @status WHERE productId = @productId"; //

            //execute query
            try
            {
                //execute query
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@status", status);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException exDB)
            {
                if (lblNoData != null)
                {
                    lblNoData.Visible = true;
                    lblNoData.Text = exDB.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        List<CheckBox> checkBoxContainer()
        {
            List<CheckBox> chkBoxContainer = new List<CheckBox>();
            CheckBox chk = new CheckBox();

            foreach (GridViewRow row in userGrid.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    Control control = row.Cells[i].FindControl("CheckBox1");
                    Control controlStatus = row.Cells[i].FindControl("lblStatusContent");
                    if (control is CheckBox)
                    {
                        chk = control as CheckBox;
                    }
                    if (controlStatus is Label)
                    {
                        Label ctrlStatus = controlStatus as Label;
                        if (ctrlStatus.Text == "Flagged")
                        {
                            chkBoxContainer.Add(chk);
                            break;
                        }
                    }
                }
            }

            return chkBoxContainer;
        }

        void displayToggle(Control sender)
        {
            var chkBox = (CheckBox)sender;

            //add new class
            string sliderFocus = "slider-focus";
            string sliderChecked = "slider-checked";
            //string sliderBeforeChecked = "slider-before-checked";

            //get the span element
            var control = chkBox.Parent.Controls.OfType<HtmlGenericControl>().LastOrDefault();

            string classes = ((HtmlGenericControl)control).Attributes["class"];

            //get status
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            Label statusControl = (Label)grvRow.FindControl("lblStatusContent");
            string status = statusControl.Text;

            //change toggle
            if (status == "Flagged")
            {
                //toggle round button move to right
                string script = "<script type=\"text/javascript\">" +
                    "document.querySelector('.slider').style.setProperty('--transformValue', '26px');" +
                    "</script>";

                //RegisterStartupScript vs RegisterClientScriptBlock
                //one is run before end of form tag, another one is after start of form tag
                ClientScript.RegisterStartupScript(this.GetType(), "transform", script);
                chkBox.Checked = true;
                classes += (classes == "") ? sliderFocus : " " + sliderFocus;
                classes += (classes == "") ? sliderChecked : " " + sliderChecked;
            }
            else
            {
                chkBox.Checked = false;
                classes = classes.Replace(sliderFocus, "");
                classes = classes.Replace(sliderChecked, "");
            }
            control.Attributes.Add("class", classes);
        }

        void updateStatusText(Object sender, string productStatus)
        {
            var chkBox = (CheckBox)sender;
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;

            Label lblProductStatus = (Label)grvRow.FindControl("lblStatusContent");
            lblProductStatus.Text = productStatus;
        }

        void updateProductStatusUI(Object sender)
        {
            var chkBox = (CheckBox)sender;
            string productId = "";
            string productStatus = "";

            //get complaint id
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            Label productIdControl = (Label)grvRow.FindControl("lblProductIdContent");
            productId = productIdControl.Text;

            try
            {
                if (chkBox.Checked == true)
                {
                    productStatus = "Flagged";
                }
                else
                {
                    productStatus = "Unflagged";
                }
                updateProductStatus(productId, productStatus);
                updateStatusText(sender, productStatus);
                displayToggle((Control)sender);
            }
            catch (Exception ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
        }  

        protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Response.Redirect("ManageProduct.aspx?filter=" + RadioButtonList1.SelectedValue);
        }
        protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
        {
            updateProductStatusUI(sender);
        }

        /*Useless function*/

        protected void imgProductPhoto_DataBinding(object sender, EventArgs e)
        {
            //ImageButton img = (ImageButton)sender;
            //HiddenField hfPhotoURL = (HiddenField)img.Parent.FindControl("hfProductPhotoURL");
            //HiddenField hfPhoto = (HiddenField)img.Parent.FindControl("hfProductPhoto");

            //if (hfPhotoURL.Value != null)
            //{
            //    img.ImageUrl = hfPhotoURL.Value;
            //}
            //else if (hfPhotoURL.Value != null)
            //{

            //}

        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkAccAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_DataBinding(object sender, EventArgs e)
        {

        }

        
        protected void userGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void userGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void userGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void userGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void userGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}