using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Admin
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

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
            string query = "SELECT Product.productId, ProductDetails.productName, ProductPhoto.productPhotoURL, Productphoto.productPhoto, Product.addDateTime, Product.productBrand, Product.productModel, Seller.businessName " +
                "FROM Product INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN " +
                "Seller ON SubStore.sellerId = Seller.sellerId INNER JOIN " +
                "ProductPhoto ON ProductPhoto.productId = Product.productId ";

            string filterDate = "ORDER BY Product.addDateTime DESC";
            string filterProductName = "ORDER BY ProductDetails.productName";
            string filterProductBrand = "ORDER BY ProductDetails.productBrand";
            string filterProductModel = "ORDER BY ProductDetails.productModel";
            string filterBusinessName = "ORDER BY Seller.businessName";

            //execute query
            try
            {
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
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

        protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void imgProductPhoto_DataBinding(object sender, EventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            HiddenField hfPhotoURL = (HiddenField)img.Parent.FindControl("hfProductPhotoURL");
            HiddenField hfPhoto = (HiddenField)img.Parent.FindControl("hfProductPhoto");

            if (hfPhotoURL.Value != null)
            {
                img.ImageUrl = hfPhotoURL.Value;
            }
            else if (hfPhotoURL.Value != null)
            {

            }
             
        }
    }
}