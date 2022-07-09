using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class EditProductDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //uploadFiles();
        }

        void loadData()
        {
            string path = Request.Url.LocalPath.ToString();
            string prodName = "";
            string prodId = "";
            int auctionAvailable = 0;
            int fixedAvailable = 0;
            int sealedAvailable = 0;
            int auctionDuration = 0;

            if (path.Contains("EditProduct.aspx") == true)
            {
                //create connection
                SqlConnection con;
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strCon);

                //prepare command 
                SqlCommand cmdRetrieve;
                string query = "SELECT Product.productId, ProductDetails.productName, ProductDetails.category, ProductDetails.brand, ProductDetails.model, Product.productDesc, ProductPhoto.productPhotoURL FROM " +
                    "ProductDetails INNER JOIN " +
                    "Product ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                    "ProductPhoto.productId = Product.productId INNER JOIN " +
                    "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                    "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                    "Account ON Seller.accId = Account.accId" +
                    "WHERE Account.username = @name AND Account.email = @email AND ProductDetails.productName = @prodName";
                string queryAuction = "SELECT COUNT(FixedPriceProduct.productId) AS Fixed_Count, COUNT(OpenAuctionProduct.productId) AS Open_Count, COUNT(SealedAuctionproduct.productId) AS Sealed_Count, AuctionProduct.auctionDuration, FixedPriceProduct.productPrice, AuctionProduct.startingPrice, Product.productStock " +
                    "FROM ProductDetails INNER JOIN " +
                    "Product ON ProductDetails.productDetailsId = Product.productDetailsId INNER JOIN " +
                    "FixedPriceProduct ON FixedPriceProduct.productId = Product.productId INNER JOIN " +
                    "OpenAuctionProduct ON OpenAuctionProduct.productId = Product.productId INNER JOIN " +
                    "SealedAuctionProduct ON SealedAuctionProduct.productId = Product.productId INNER JOIN " +
                    "AuctionProduct ON AuctionProduct.productId = Product.productId " +
                    "WHERE Product.productId = @prodId " +
                    "GROUP BY AuctionProduct.auctionDuration, FixedPriceProduct.productPrice, AuctionProduct.startingPrice, Product.productStock";
                //execute
                try
                {
                    con.Open();

                    //product name, category, brand, model, productPhotoURL
                    cmdRetrieve = new SqlCommand(query, con);
                    cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                    cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                    
                    //get product name
                    if(Request.QueryString["prodName"] != null)
                    {
                        prodName = Request.QueryString["prodName"].ToString();
                    }

                    cmdRetrieve.Parameters.AddWithValue("@prodName", prodName);
                    SqlDataReader reader = cmdRetrieve.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            prodId = reader["productId"].ToString();
                            txtProdName.Text = reader["productName"].ToString();
                            if (ddlProdCategory.Items.FindByValue(reader["productCategory"].ToString()) != null)
                            {
                                ddlProdCategory.Items.FindByValue(reader["productCategory"].ToString()).Selected = true;
                            }
                            txtType.Text = reader["productType"].ToString();
                            txtBrand.Text = reader["productBrand"].ToString();
                            txtModel.Text = reader["productModel"].ToString();
                            txtDesc.Text = reader["productDesc"].ToString();
                        }
                    }
                    //selling format, selling end date, fixed price, start price, stock
                    
                    cmdRetrieve = new SqlCommand(queryAuction, con);
                    cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
                    SqlDataReader sellingReader = cmdRetrieve.ExecuteReader();
                    if (sellingReader.HasRows)
                    {
                        while (sellingReader.Read())
                        {
                            fixedAvailable = Convert.ToInt32(sellingReader["Fixed_Count"].ToString());
                            auctionAvailable = Convert.ToInt32(sellingReader["Open_Count"].ToString()); 
                            sealedAvailable = Convert.ToInt32(sellingReader["Sealed_Count"].ToString());
                            auctionDuration = Convert.ToInt32(sellingReader["auctionDuration"].ToString());
                            txtFixedPrice.Text = sellingReader["productPrice"].ToString();
                            txtStartPrice.Text = sellingReader["startingPrice"].ToString();
                            txtStock.Text = sellingReader["productStock"].ToString();
                        }
                    }

                    //selling format - fixed
                    if (fixedAvailable > 0)
                    {
                        if(chkSellOption.Items.FindByValue("Fixed Price") != null)
                        {
                            chkSellOption.Items.FindByValue("Fixed Price").Selected = true;
                        }
                    }

                    //selling format - auction
                    if(auctionAvailable > 0)
                    {
                        if (chkSellOption.Items.FindByValue("Open Bid Auction") != null)
                        {
                            chkSellOption.Items.FindByValue("Open Bid Auction").Selected = true;
                        }
                    }

                    //selling format - sealed
                    if (sealedAvailable > 0)
                    {
                        if (chkSellOption.Items.FindByValue("Sealed Bid Auction") != null)
                        {
                            chkSellOption.Items.FindByValue("Sealed Bid Auction").Selected = true;
                        }
                    }

                    //auction duration
                    if(auctionDuration > 0)
                    {
                        if (ddlDuration.Items.FindByValue(auctionDuration.ToString()) != null)
                        {
                            ddlDuration.Items.FindByValue(auctionDuration.ToString()).Selected = true;
                        }
                    }
                    
                }
                catch (NullReferenceException ex)
                {
                    HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                    if (control != null)
                    {
                        control.Style.Add("display", "block");
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = ex.Message.ToString();
                    }

                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        void uploadFiles()
        {
            StringBuilder sb = new StringBuilder();

            if (txtUploadPhoto.HasFile)
            {
                try
                {
                    sb.AppendFormat(" Uploading file: {0}", txtUploadPhoto.FileName);

                    //saving the file
                    txtUploadPhoto.SaveAs("<c:\\SaveDirectory>" + txtUploadPhoto.FileName);

                    //Showing the file information
                    sb.AppendFormat("<br/> Save As: {0}", txtUploadPhoto.PostedFile.FileName);
                    sb.AppendFormat("<br/> File type: {0}", txtUploadPhoto.PostedFile.ContentType);
                    sb.AppendFormat("<br/> File length: {0}", txtUploadPhoto.PostedFile.ContentLength);
                    sb.AppendFormat("<br/> File name: {0}", txtUploadPhoto.PostedFile.FileName);

                }
                catch (Exception ex)
                {
                    sb.Append("<br/> Error <br/>");
                    sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
                }
            }
            else
            {
                lblMessage.Attributes.CssStyle.Add("display", "block");
                lblMessage.Text = sb.ToString();
            }
        }
    }
}