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
        private DataSet dtSet;
        protected void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack)
            {
                //Session["displayPhoto"] = 0;
                assignSubStoreOption();
                Panel1.Visible = false;
                txtStock.Visible = true;
                lblStock.Visible = true;
            }
        }

        //to store the photo temporarily before update into database
        void createPhotoTable()
        {
            DataTable dt = new DataTable("Photo");
            DataColumn col;

            //create photoURL column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "id";
            col.Caption = "id";
            col.ReadOnly = false;
            col.Unique = true;
            dt.Columns.Add(col);

            //create photoURL column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productPhotoURL";
            col.Caption = "photoURL";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            //create photo column
            col = new DataColumn();
            col.DataType = typeof(byte[]);
            col.ColumnName = "productPhoto";
            col.Caption = "photo";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            ////create data set
            dtSet = new DataSet();

            //assign data table into data set
            dtSet.Tables.Add(dt);
        }

        void createTable(string tableName)
        {
            DataTable dt = new DataTable(tableName);
            DataColumn col;

            //create photoURL column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "id";
            col.Caption = "id";
            col.ReadOnly = false;
            col.Unique = true;
            dt.Columns.Add(col);

            //create photoURL column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productPhotoURL";
            col.Caption = "photoURL";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            //create photo column
            col = new DataColumn();
            col.DataType = typeof(byte[]);
            col.ColumnName = "productPhoto";
            col.Caption = "photo";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            ////create data set
            dtSet = new DataSet();

            //assign data table into data set
            dtSet.Tables.Add(dt);
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
                ddlSubStore.Items.Insert(0, new ListItem("--Select SubStore--", "-1"));
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
            string query = "SELECT COUNT(productPhotoId) FROM ProductPhoto WHERE ProductPhoto.productId = @productId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
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

        //retrieve photo and assign back to the data table
        void getAllPhoto()
        {

            //get data table
            DataTable dt = dtSet.Tables["Photo"];

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT id,productPhoto, productPhotoURL FROM TempPhoto";
             
            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["id"] = reader["id"];
                        row["productPhoto"] = reader["productPhoto"];
                        row["productPhotoURL"] = reader["productPhotoURL"];
                        dt.Rows.Add(row);
                        dt.AcceptChanges();
                    }
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
        }

        //remove the photo
        void removePhoto(string id)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "DELETE FROM TempPhoto WHERE id = @id";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@id", id);
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

        //remove all photo
        void removeAllPhoto()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "DELETE FROM TempPhoto";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
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

        protected void btnSubmitURL_Click(object sender, EventArgs e) //not all column field is initialised, ok?
        {
            createPhotoTable();
            DataTable dt = dtSet.Tables["Photo"];

            //get data from the gvStore
            try
            {

                //insert photo into database
                createPhotoToTempDB(txtInsertURL.Text, null);

                //retrieve data
                getAllPhoto();

                //set session to determine whether to show the productPhoto (byte)
                Session["displayPhoto"] = 0;

                //data bind repeater
                DataList1.DataSource = dt;
                DataList1.DataBind();

                //empty the url textbox
                txtInsertURL.Text = string.Empty;
            }
            catch(Exception ex)
            {
                displayErrorMsg(ex);
            }

            //Response.Redirect("ProcessPhoto.ashx?action=create");
        }

        protected void btnSubmitPhoto_Click(object sender, EventArgs e)
        {
            createPhotoTable();
            DataTable dt = dtSet.Tables["Photo"];

            //get data from the gvStore
            try
            {

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

                                //insert photo into database
                                createPhotoToTempDB(null, bytes);
                            }
                        }
                    }
                }

                //retrieve data
                getAllPhoto();

                //display the product photo 
                Session["displayPhoto"] = 1;

                //update the updated version of data table
                DataList1.DataSource = dt;
                DataList1.DataBind();

                //dispose the photo inside file upload
                txtUploadPhoto.Dispose();
            }
            catch (Exception ex)
            {
                displayErrorMsg(ex);
            }
        }

        protected void btnRemoveImg1_Click(object sender, EventArgs e)
        {
            createPhotoTable();
            DataTable dt = dtSet.Tables["Photo"];

            //get current row item
            var btnRemove = (Button)sender;
            var img1 = (System.Web.UI.WebControls.Image)btnRemove.NamingContainer.FindControl("Image1");
            var hfRow = (HiddenField)btnRemove.NamingContainer.FindControl("hfRow");

            //get data from the gvStore
            try
            {
                //delete the selected photo
                removePhoto(hfRow.Value);

                //retrieve the remaining photo
                getAllPhoto();

                //Bind the data with repeater
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                displayErrorMsg(ex);
            }
        }

        protected void btnRemoveImg2_Click(object sender, EventArgs e)
        {
            createPhotoTable();
            DataTable dt = dtSet.Tables["Photo"];

            byte[] bytes = { };

            //get current row item
            var btnRemove = (Button)sender;
            var img2 = (System.Web.UI.WebControls.Image)btnRemove.NamingContainer.FindControl("Image2");
            var hfRow = (HiddenField)btnRemove.NamingContainer.FindControl("hfRow");
            

            //get data from the gvStore
            try
            {
                //delete the selected photo
                removePhoto(hfRow.Value);

                //retrieve the remaining photo
                getAllPhoto();

                //Bind the data with repeater
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                displayErrorMsg(ex);
            }
            
        }//no row used, ok?

        protected void btnConfirm_Click(object sender, EventArgs e) //ok?
        {
            List<string> formatList = new List<string>();
            string[] sellingFormat = { };

            //retrieve value from multiple checkbox
            for (int i = 0; i < chkSellOption.Items.Count; i++)
            {
                if (chkSellOption.Items[i].Selected == true)
                {
                    formatList.Add(chkSellOption.Items[i].Value);
                }
            }
            sellingFormat = formatList.ToArray<string>();

            //create product details in database
            insertAllDataToDB(sellingFormat);

            //delete the temporary table
            removeAllPhoto();

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

        void createPhotoToTempDB(string productPhotoURL, byte[] prouductPhoto)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryURL = "INSERT INTO TempPhoto(productPhotoURL) " +
                 "VALUES(@productPhotoURL)";

            string queryPhoto = "INSERT INTO TempPhoto(productPhoto) " +
                 "VALUES(@productPhoto)";

            //execute
            try
            {
                con.Open();
                if(productPhotoURL != null)
                {
                    cmdRetrieve = new SqlCommand(queryURL, con);
                    cmdRetrieve.Parameters.AddWithValue("@productPhotoURL", productPhotoURL);
                }
                else
                {
                    cmdRetrieve = new SqlCommand(queryPhoto, con);
                    cmdRetrieve.Parameters.AddWithValue("@productPhoto", prouductPhoto);
                }
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
                con.Open();
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
                con.Open();
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
            string queryProductPhotoURL = "INSERT INTO ProductPhoto(productPhotoId, productPhotoURL, photoStatus, productId) " +
                "VALUES(@productPhotoId, @productPhotoURL, @status, @productId)";
            string queryProductPhoto = "INSERT INTO ProductPhoto(productPhotoId, productPhoto, photoStatus, productId) " +
                "VALUES(@productPhotoId, @productPhoto, @status, @productId)";

            try
            {
                string productPhotoId = getProductPhotoId();
                string status = getPhotoStatus(productId);
                con.Open();
                if(bytes == null)
                {
                    cmdRetrieve = new SqlCommand(queryProductPhotoURL, con);
                    cmdRetrieve.Parameters.AddWithValue("productPhotoURL", photoURL);
                }
                else
                {
                    cmdRetrieve = new SqlCommand(queryProductPhoto, con);
                    cmdRetrieve.Parameters.AddWithValue("@productPhoto", bytes);
                }
                
                cmdRetrieve.Parameters.AddWithValue("@productPhotoId", productPhotoId);
                cmdRetrieve.Parameters.AddWithValue("@status", status);
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
                con.Open();
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
                con.Open();
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

            //create data table
            createPhotoTable();
            DataTable dt = dtSet.Tables["Photo"];

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

                //get photoURL or photo
                getAllPhoto();
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        productPhotoURL = (string)dr["productPhotoURL"];
                        if (productPhotoURL != null)
                        {
                            //insert the product photo into the database
                            createProductPhotoToDB(productId, null, productPhotoURL);
                        }
                        else
                        {
                            productPhoto = (byte[])dr["productPhoto"];
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

                //empty all text field
                txtProdName.Text = "";
                ddlProdCategory.SelectedValue = "-1";
                txtType.Text = "";
                txtBrand.Text = "";
                txtModel.Text = "";
                createTable("EmptyTable");
                DataList1.DataSource = dtSet.Tables["EmptyTable"];
                DataList1.DataBind();
                ddlSubStore.SelectedValue = "-1";
                txtDesc.Text = "";
                chkSellOption.ClearSelection();
                ddlDuration.ClearSelection();
                txtFixedPrice.Text = "";
                txtStartPrice.Text = "";
                txtReservePrice.Text = "";
                txtStock.Text = "";
                stockRow.Visible = true;
                UpdatePanel2.Visible = false;

            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
        } //update all the product

        protected void chkSellOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int visibleStock = 0;

            foreach (ListItem item in chkSellOption.Items)
            {
                if (item.Selected && (item.Value == "OpenBidAuction" || item.Value == "SealedBidAuction"))
                {
                    Panel1.Visible = true;
                    stockRow.Visible = false;
                    break;
                }
                else
                {
                    visibleStock++;
                }

            }
            if(visibleStock == 3)
            {
                Panel1.Visible = false;
                stockRow.Visible = true;
            }
            UpdatePanel2.Update();
        }

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

        byte[] convertURLToPhoto(string url)
        {
            StringBuilder imgURL = new StringBuilder();
            byte[] imgBytes = { };

            //based on url, get image

            //convert to the byte[]

            // If you want convert url to bitmap file 
            imgURL = imgURL.Append(url);
            imgURL.Replace("data:image/Bmp;base64,\"", "");
            imgBytes = Convert.FromBase64String(imgURL.ToString());
            
            return imgBytes;
        }
        protected void DataList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        //retrieve data and assign back to the data table
        string[] getAllProductPhotoURL()
        {
            string[] productPhotoURLContainer = { };
            int count = 0;
            DataTable dt = dtSet.Tables["Product"];

            //from the gvStore, get the URL



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

        protected void UpdatePanel1_DataBinding(object sender, EventArgs e)
        {

        }

    }
}