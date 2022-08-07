using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class CreateProduct : System.Web.UI.Page
    {
        //private DataSet dtSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                createPhotoTable();
                assignSubStoreOption();
            }
            foreach (RepeaterItem item in Repeater1.Items)
            {
                Button button = item.FindControl("btnRemoveImg") as Button;
                ScriptManager.GetCurrent(Page).RegisterPostBackControl(button);
            }

        }

        void assignSubStoreOption()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT SubStore.subStoreId, SubStore.subStoreName " +
                "FROM SubStore INNER JOIN " +
                "Seller ON SubStore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                ddlSubStore.DataSource = cmdRetrieve.ExecuteReader();
                ddlSubStore.DataBind();
                ddlSubStore.DataTextField = "subStoreName";
                ddlSubStore.DataValueField = "subStoreId";
                ddlSubStore.DataBind();
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
            finally
            {
                ddlSubStore.Items.Insert(0, new ListItem("--Select Category--", "-1"));
                con.Close();
                con.Dispose();
            }
        }//ok

        string getProductDetailsId()
        {
            string productDetailsId = "";
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(productDetailsId) FROM ProductDetails";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();

                if (count + 1 > 9)
                {
                    productDetailsId = "pd_0" + (count + 1);
                }
                else
                {
                    productDetailsId = "pd_00" + (count + 1);
                }
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return productDetailsId;
        } //ok

        string getProductId()
        {
            string productId = "";
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(productId) FROM Product";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();

                if (count + 1 > 9)
                {
                    productId = "p_0" + (count + 1);
                }
                else
                {
                    productId = "p_00" + (count + 1);
                }
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return productId;
        }//ok

        string getProductPhotoId()
        {
            string productPhotoId = "";
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(productPhotoId) FROM ProductPhoto";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                count = (int)cmdRetrieve.ExecuteScalar();

                if (count + 1 > 9)
                {
                    productPhotoId = "ppi_0" + (count + 1);
                }
                else
                {
                    productPhotoId = "ppi_00" + (count + 1);
                }
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return productPhotoId;
        }//ok

        string getPhotoStatus(string productId)
        {
            string status = "";
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(productPhotoId) FROM ProductPhoto WHERE ProductPhoto.productId = @productPhotoId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                count = (int)cmdRetrieve.ExecuteScalar();

                if (count > 0)
                {
                    status = "Sub";
                }
                else
                {
                    status = "Main";
                }
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return status;
        }//ok

        //to store the photo temporarily before update into database
        void createPhotoTable()
        {
            DataTable dt = new DataTable("Photo");
            DataColumn col;

            //create photoURL column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "photoURL";
            col.Caption = "photoURL";
            col.ReadOnly = false;
            col.Unique = true;
            dt.Columns.Add(col);

            //create photo column
            col = new DataColumn();
            col.DataType = typeof(byte[]);
            col.ColumnName = "photo";
            col.Caption = "photo";
            col.ReadOnly = false;
            col.Unique = true;
            dt.Columns.Add(col);

            //create data set
            dtSet = new DataSet();

            //assign data table into data set
            dtSet.Tables.Add(dt);
        }

        //to get the photo URL from data table
        string[] getAllProductPhotoURL()
        {
            string[] productPhotoURLContainer = { };
            DataTable dt;

            dt = dtSet.Tables["Photo"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].Field<string>("photoURL") != null)
                {
                    productPhotoURLContainer[i] = dt.Rows[i].Field<string>("photoURL");
                }
            }

            return productPhotoURLContainer;
        }

        //to get the photo from data table
        byte[,] getAllProductPhoto()
        {
            byte[,] productPhotoContainer = { { } };
            DataTable dt;

            dt = dtSet.Tables["Photo"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].Field<byte[]>("photo") != null)
                {
                    byte[] productPhoto = dt.Rows[i].Field<byte[]>("photo");
                    for (int j = 0; j < productPhoto.Length; j++)
                    {
                        productPhotoContainer[i, j] = productPhoto[j];
                    }

                }
            }

            return productPhotoContainer;
        }

        protected void btnSubmitURL_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow row;

            if (dtSet.Tables["Photo"] != null)
            {
                dt = dtSet.Tables["Photo"];
                if (txtInsertURL.Text != null)
                {
                    row = dt.NewRow();
                    row["photoURL"] = txtInsertURL.Text;
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                    txtInsertURL.Text = "";
                }
            }

            //data bind the updated version of data table
            Repeater1.DataSource = dtSet.Tables["Photo"];
            Repeater1.DataBind();

            //Response.Redirect("ProcessPhoto.ashx?action=create");
        }

        protected void btnSubmitPhoto_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow row;

            if (txtUploadPhoto.HasFiles)
            {
                //get the posted file 
                foreach (HttpPostedFile postedFile in txtUploadPhoto.PostedFiles)
                {
                    string filename = Path.GetFileName(postedFile.FileName);
                    string contentType = postedFile.ContentType;
                    using (Stream fs = postedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            //serialise the photo
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);

                            //assign into data table 
                            dt = dtSet.Tables["Photo"];
                            row = dt.NewRow();
                            row["photoURL"] = bytes;
                            dt.Rows.Add(row);
                            dt.AcceptChanges();
                            //dispose the photo inside file upload
                            txtUploadPhoto.Dispose();
                        }
                    }
                }
            }

            //update the updated version of data table
            Repeater1.DataSource = dtSet.Tables["Photo"];
            Repeater1.DataBind();

            //Response.Redirect("ProcessPhoto.ashx?action=create");
        }

        protected void btnRemoveImg_Click(object sender, EventArgs e)
        {
            DataTable dt;
            
            //get current row image
            var btnRemove = (Button)sender;
            var imgBtn = (System.Web.UI.WebControls.Image)btnRemove.NamingContainer.FindControl("Image1");

            //get data table
            dt = dtSet.Tables["Photo"];

            for (int i=0; i<dt.Rows.Count; i++)
            {
                try
                {
                    //get data table url
                    var photoURL = (string)dt.Rows[i].Field<string>(imgBtn.ImageUrl);
                    if (photoURL == imgBtn.ImageUrl)
                    {
                        dt.Rows[i].Delete();
                    }
                    var photoByte = (byte[])dt.Rows[i].Field<byte[]>(imgBtn.ImageUrl);
                    var photo = (string)convertPhotoIntoURL(photoByte);
                    if (photo == imgBtn.ImageUrl)
                    {
                        dt.Rows[i].Delete();
                    }
                }
                catch(Exception ex)
                {
                    continue;
                }
                
            }
            dt.AcceptChanges();

            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string[] sellingFormat = { };
            int count = 0;

            //create product details in database
            //retrieve value from multiple checkbox
            for (int i = 0; i < chkSellOption.Items.Count; i++)
            {
                if (chkSellOption.Items[i].Selected == true)
                {
                    sellingFormat[count] = chkSellOption.Items[i].Value;
                    count++;
                }
            }
            insertAllDataToDB(sellingFormat);

            //update the product details into the database

        }
        void displayErrorMsg(Exception ex)
        {
            HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
            if (control != null)
            {
                control.Style.Add("display", "block");
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = ex.Message.ToString();
            }
        }//ok

        void createProductDetailsToDB(string productDetailsId)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryProductDetails = "INSERT INTO ProductDetails(productDetailsId, productName, productCategory, productType, productBrand, productModel) " +
                "VALUES(@productDetailsId, @productName, @productCategory, @productType, @productBrand, @productModel)";

            try
            {
                cmdRetrieve = new SqlCommand(queryProductDetails, con);
                cmdRetrieve.Parameters.AddWithValue("@productDetailsId", productDetailsId);
                cmdRetrieve.Parameters.AddWithValue("@productName", txtProdName.Text);
                cmdRetrieve.Parameters.AddWithValue("productCategory", ddlProdCategory.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@productType", txtType.Text);
                cmdRetrieve.Parameters.AddWithValue("@productBrand", txtBrand.Text);
                cmdRetrieve.Parameters.AddWithValue("@productModel", txtModel.Text);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }//ok

        void createProductToDB(string productId, string subStoreId, string productDetailsId) //ok, created subStoreId, created current productDetailsId
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryProduct = "INSERT INTO Product(productId, productStock, addDateTime, productStatus, productDesc, subStoreId, productDetailsId) " +
                 "VALUES(@productId, 1, CONVERT(DATETIME, GETDATE(), 120), 'Available', @desc, @subStoreId, @productDetailsId)";

            try
            {
                cmdRetrieve = new SqlCommand(queryProduct, con);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.Parameters.AddWithValue("@desc", txtDesc.Text);
                cmdRetrieve.Parameters.AddWithValue("@subStoreId", subStoreId); //created subStoreId
                cmdRetrieve.Parameters.AddWithValue("@productDetailsId", productDetailsId);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        void createProductPhotoToDB(string productId, byte[] bytes, string photoURL)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryProductPhoto = "INSERT INTO ProductPhoto(productPhotoId, productPhotoURL, productPhoto, photoStatus, productId) " +
                "VALUES(@productPhotoId, @productPhotoURL, @productPhoto, @status, @productId)";

            try
            {
                cmdRetrieve = new SqlCommand(queryProductPhoto, con);
                cmdRetrieve.Parameters.AddWithValue("@productPhotoId", getProductPhotoId());
                cmdRetrieve.Parameters.AddWithValue("productPhotoURL", photoURL);
                cmdRetrieve.Parameters.AddWithValue("@productPhoto", bytes);
                cmdRetrieve.Parameters.AddWithValue("@status", getPhotoStatus(productId));
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        } //ok, created current productId

        void createFixedPriceProductToDB(string productId)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryFixedPriceProduct = "INSERT INTO FixedPriceProduct(productId, productPrice) " +
                "VALUES(@productId, @productPrice)";

            try
            {
                cmdRetrieve = new SqlCommand(queryFixedPriceProduct, con);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.Parameters.AddWithValue("@productPrice", txtFixedPrice.Text);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }//ok, created current productId

        void createAuctionProductToDB(string productId) //ok, created current productId
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryAuctionProduct = "INSERT INTO AuctionProduct(productId, reservePrice, startingPrice, acceptedBid, auctionDuration) " +
                "VALUES(@productId, @reservePrice, @startPrice, NULL, @auctionDuration)";

            try
            {
                cmdRetrieve = new SqlCommand(queryAuctionProduct, con);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.Parameters.AddWithValue("@reservePrice", txtReservePrice.Text);
                cmdRetrieve.Parameters.AddWithValue("@startPrice", txtStartPrice.Text);
                cmdRetrieve.Parameters.AddWithValue("@auctionDuration", ddlDuration.SelectedValue);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        void insertAllDataToDB(string[] sellingFormat)
        {
            string productDetailsId = "";
            string productId = "";
            string subStoreId = "";

            string productPhotoId = "";
            string productPhotoURL = "";
            string photoStatus = "";

            byte[] productPhoto = { };

            int flag = 0; //signal for the first photo has been retrieve
            StringBuilder sb = new StringBuilder();
            DataTable dt;


            //execute
            try
            {
                //get productDetailsId
                productDetailsId = getProductDetailsId();

                //get productId 
                productId = getProductId();

                //get subStoreId
                subStoreId = ddlSubStore.SelectedValue;

                //ProductDetails, Product, ProductPhoto, FixedPriceProduct, AuctionProduct
                createProductDetailsToDB(productDetailsId);
                createProductToDB(productId, subStoreId, productDetailsId);

                //get data table
                dt = dtSet.Tables["Photo"];

                //get photoURL or photo
                foreach(DataRow dr in dt.Rows)
                {
                    try
                    {
                        productPhotoURL = (string)dr["photoURL"];
                        if (productPhotoURL != null)
                        {
                            //insert the product photo into the database
                            createProductPhotoToDB(productId, null, productPhotoURL);
                        }
                        else
                        {
                            productPhoto = (byte[])dr["photo"];
                            if (productPhoto.Length != 0)
                            {
                                createProductPhotoToDB(productId, productPhoto, String.Empty);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
                    
                }
               
                //insert the fixed price product into DB
                createFixedPriceProductToDB(productId);

                foreach (string option in sellingFormat)
                {
                    if (option.Equals("OpenBidAuction") || option.Equals("SealedBidAuction"))
                    {
                        createAuctionProductToDB(productId);
                    }
                }


            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
        } //update all the product

        string convertPhotoIntoURL(byte[] imgBytes)
        {
            StringBuilder imgURL = new StringBuilder();

            // If you want convert to a bitmap file
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap MyBitmap = (Bitmap)tc.ConvertFrom(imgBytes);

            imgURL = imgURL.Append(Convert.ToBase64String(imgBytes));
            imgURL.Replace(imgURL.ToString(), String.Format("data:image/Bmp;base64,{0}\"", imgURL));

            return imgURL.ToString();
        }//ok
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var repeater = (Repeater)sender;
            var imageHolder = (System.Web.UI.WebControls.Image)repeater.NamingContainer.FindControl("Image1");
            DataTable dt;

            //get existing value from repeater1 and assign into the data table
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                dt = dtSet.Tables["Photo"];
                foreach (DataRow dr in dt.Rows)
                {
                    byte[] imgBytes = (byte[])dr["photo"];
                    if (imgBytes.Length != 0)
                    {
                        //Set the source with data:image/bmp
                        imageHolder.ImageUrl = convertPhotoIntoURL(imgBytes);
                    }
                    else
                    {
                        string url = (string)dr["photoURL"];
                        if (url != String.Empty)
                        {
                            //Set the source with url
                            imageHolder.ImageUrl = url;
                        }
                    }
                }

            }

            //add new value into the data table


            //data bind the new data table into the repeater1
        }

    }
}