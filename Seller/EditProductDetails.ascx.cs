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
    public partial class EditProductDetails : System.Web.UI.UserControl
    {
        private DataSet dtSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //createDataTable();
                Session["prodName"] = Request.QueryString["prodName"];
                assignSubStoreOption();
                loadData();
            }

        }

        //retrieve photo and assign back to the data table
        void getAllPhoto(string productId)
        {
            //get data table
            DataTable dt = dtSet.Tables["Photo"];

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT productPhotoId, productPhoto, productPhotoURL FROM ProductPhoto WHERE (productPhoto IS NOT NULL OR productPhotoURL IS NOT NULL) AND productId = @productId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@productId",productId);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["productPhotoId"] = reader["productPhotoId"];
                        if(reader["productPhoto"] == DBNull.Value && reader["productPhotoURL"] == DBNull.Value)
                        {
                            continue;
                        }
                        row["productPhoto"] = reader["productPhoto"];
                        row["productPhotoURL"] = reader["productPhotoURL"];
                        row["productId"] = productId;
                        dt.Rows.Add(row);
                    }
                }
                dt.AcceptChanges();

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

        void createDataTable()
        {
            DataTable dt = new DataTable("Photo");
            DataTable dt1 = new DataTable("Product");
            DataColumn col;

            //create prodId column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productId";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create prodDetailsId column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productDetailsId";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create prodName column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productName";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create prodCategory col
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productCategory";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create prodType col
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productType";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create prodBrand col
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productBrand";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create prodModel col
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productModel";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create subStoreId col
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "subStoreId";
            col.Caption = "subStoreId";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create desc col
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productDesc";
            col.Caption = "prodDesc";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create chkSellOption col
            col = new DataColumn();
            col.DataType = typeof(string[]);
            col.ColumnName = "sellOption";
            col.Caption = "sellOption";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create duration col
            col = new DataColumn();
            col.DataType = typeof(int);
            col.ColumnName = "auctionDuration";
            col.Caption = "auctionDuration";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create fixed price col
            col = new DataColumn();
            col.DataType = typeof(double);
            col.ColumnName = "fixedPrice";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create start price col
            col = new DataColumn();
            col.DataType = typeof(double);
            col.ColumnName = "startPrice";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create reserve price col
            col = new DataColumn();
            col.DataType = typeof(double);
            col.ColumnName = "reservePrice";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create stock col
            col = new DataColumn();
            col.DataType = typeof(int);
            col.ColumnName = "productStock";
            col.ReadOnly = false;
            col.Unique = true;
            dt1.Columns.Add(col);

            //create id column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productPhotoId";
            col.Caption = "productPhotoId";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            //create photoURL column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productPhotoURL";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            //create photo column
            col = new DataColumn();
            col.DataType = typeof(byte[]);
            col.ColumnName = "productPhoto";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            //create photo column
            col = new DataColumn();
            col.DataType = typeof(string);
            col.ColumnName = "productId";
            col.ReadOnly = false;
            col.Unique = false;
            dt.Columns.Add(col);

            // Make id column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dt1.Columns["productId"];
            dt1.PrimaryKey = PrimaryKeyColumns;

            //create data set
            dtSet = new DataSet();

            //assign data table into data set
            dtSet.Tables.Add(dt);
            dtSet.Tables.Add(dt1);

            //create a new row for the data table (dt1)
            DataRow dr;
            dr = dt1.NewRow();
            try
            {
                string prodName = Request.QueryString["prodName"];
                dr["productName"] = prodName;
                dr["productDetailsId"] = getProductDetailsId(prodName);
                dr["productId"] = getProductId();
                if (!dr.IsNull(0))
                {
                    dt1.Rows.Add(dr);
                }
                dr.AcceptChanges();
            }
            catch (Exception ex)
            {
                displayErrorMsg(ex);
            }
        }//query string is needed to edit product details

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
                ddlSubStore.Items.Insert(0, new ListItem("--Select Category--", "-1"));
                con.Close();
                con.Dispose();
            }
        }//ok

        string getProductDetailsId(string prodName)
        {
            string productDetailsId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT productDetailsId FROM ProductDetails WHERE productName = @prodName";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodName", prodName);
                productDetailsId = (string)cmdRetrieve.ExecuteScalar();
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
            string query = "SELECT productId FROM Product WHERE productDetailsId = @prodDetailsId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodDetailsId", getProductDetailsId(Request.QueryString["prodName"]));
                productId = (string)cmdRetrieve.ExecuteScalar();

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

        void updatePhotoStatus(string id, string status)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "UPDATE ProductPhoto SET photoStatus = @status WHERE productPhotoId = @id";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@id", id);
                cmdRetrieve.Parameters.AddWithValue("@status", status);
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

        //remove the photo
        void removePhoto(string photoId)
        {
            string status = "";
            string photoId2 = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryRemove = "UPDATE ProductPhoto SET productPhoto = NULL, productPhotoURL = NULL, photoStatus = 'Sub' WHERE productPhotoId = @id";
            string queryGetPhoto = "SELECT productPhotoId FROM ProductPhoto WHERE productId = @prodId AND (productPhoto IS NOT NULL OR productPhotoURL IS NOT NULL)";

            //execute
            try
            {
                con.Open();

                //get photo status
                status = getPhotoStatus(getProductId());

                //remove the product photo from the DB
                cmdRetrieve = new SqlCommand(queryRemove, con);
                cmdRetrieve.Parameters.AddWithValue("@id", photoId);
                cmdRetrieve.ExecuteNonQuery();

                if (status == "Main")
                {
                    //get second photo
                    cmdRetrieve = new SqlCommand(queryGetPhoto, con);
                    cmdRetrieve.Parameters.AddWithValue("@prodId",getProductId());
                    SqlDataReader reader = cmdRetrieve.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            photoId2 = (string)reader["productPhotoId"];
                            break;
                        }                    
                    }

                    //update second photo status to 'Main'
                    updatePhotoStatus(photoId2, "Main");
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

        protected void btnSubmitURL_Click(object sender, EventArgs e)
        {
            createDataTable();
            DataTable dt = dtSet.Tables["Photo"];

            string productId = getProductId();

            //get data from the gvStore
            try
            {
                //create photo database
                createProductPhotoToDB(productId, null, txtInsertURL.Text);

                //get all photo
                getAllPhoto(productId);

                //data bind the updated version of data table
                DataList1.DataSource = dt;
                DataList1.DataBind();

                //empty the url textbox
                txtInsertURL.Text = string.Empty;
            }
            catch (Exception ex)
            {
                displayErrorMsg(ex);
            }  
        }

        protected void btnSubmitPhoto_Click(object sender, EventArgs e)
        {
            createDataTable();
            DataTable dt = dtSet.Tables["Photo"];

            byte[] bytes = { };
            string productId = getProductId();

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
                                bytes = br.ReadBytes((Int32)fs.Length);

                                //insert photo into database
                                createProductPhotoToDB(getProductId(), bytes, null);
                                Array.Clear(bytes, 0, bytes.Length);

                            }
                        }
                    }
                }
                

                //retrieve data
                getAllPhoto(productId);

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
            createDataTable();
            DataTable dt = dtSet.Tables["Photo"];

            //get current row item
            var btnRemove = (Button)sender;
            var hfRow = (HiddenField)btnRemove.NamingContainer.FindControl("hfRow");
            string productId = getProductId();

            //get data from the gvStore
            try
            {
                //delete the selected photo
                removePhoto(hfRow.Value);

                //retrieve the remaining photo
                getAllPhoto(productId);

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
            createDataTable();
            DataTable dt = dtSet.Tables["Photo"];

            byte[] bytes = { };

            //get current row item
            var btnRemove = (Button)sender;
            var hfRow = (HiddenField)btnRemove.NamingContainer.FindControl("hfRow");
            string productId = getProductId();

            //get data from the gvStore
            try
            {
                //delete the selected photo
                removePhoto(hfRow.Value);

                //retrieve the remaining photo
                getAllPhoto(productId);

                //Bind the data with repeater
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                displayErrorMsg(ex);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
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

            //update the product details into the database
            updateAllDataToDB(sellingFormat);
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

        void loadData()
        {
            string path = Request.Url.LocalPath.ToString();
            string prodName = "";
            string prodId = "";
            int auctionAvailable = 0;
            int fixedAvailable = 0;
            int sealedAvailable = 0;
            int auctionDuration = 0;

            createDataTable();
            DataTable dt = dtSet.Tables["Photo"];
            DataRow dr;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Product.productId, ProductDetails.productName, ProductDetails.productCategory, ProductDetails.productType, ProductDetails.productBrand, ProductDetails.productModel, Product.productDesc, ProductPhoto.productPhotoId, ProductPhoto.productPhotoURL, ProductPhoto.productPhoto, SubStore.subStoreId FROM " +
                "ProductDetails INNER JOIN " +
                "Product ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "ProductPhoto ON ProductPhoto.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                "Account ON Seller.accId = Account.accId " +
                "WHERE Account.username = @name AND Account.email = @email AND ProductDetails.productName = @prodName";
            string queryAuction = "SELECT DISTINCT FixedPriceProduct.productId AS Fixed_Prod_ID, COALESCE(OpenAuctionProduct.productId,'0') AS Open_Auction_ID, COALESCE(SealedAuctionProduct.productId,'0') AS Sealed_Auction_ID, COALESCE(AuctionProduct.auctionDuration,'0') AS Auction_Duration, FixedPriceProduct.productPrice, COALESCE(AuctionProduct.startingPrice,'0') AS Start_Price, Product.productStock " +
                "FROM ProductDetails INNER JOIN " +
                "Product ON ProductDetails.productDetailsId = Product.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON FixedPriceProduct.productId = Product.productId FULL JOIN " +
                "OpenAuctionProduct ON OpenAuctionProduct.productId = Product.productId FULL JOIN " +
                "SealedAuctionProduct ON SealedAuctionProduct.productId = Product.productId FULL JOIN " +
                "AuctionProduct ON AuctionProduct.productId = Product.productId " +
                "WHERE Product.productId = @prodId ";
           //execute
           try
           {
                con.Open();

                //assign product name, category, brand, model, productPhotoURL parameter into the query
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);

                //get product name value
                if (Request.QueryString["prodName"] != null)
                {
                    prodName = Request.QueryString["prodName"].ToString();
                }

                cmdRetrieve.Parameters.AddWithValue("@prodName", prodName);

                //execute query
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
                        ddlSubStore.SelectedValue = reader["subStoreId"].ToString();

                        //assign photo into data table 
                        dr = dt.NewRow();
                        dr["productPhotoId"] = reader["productPhotoId"];
                        if(reader["productPhotoURL"] == DBNull.Value && reader["productPhoto"] == DBNull.Value)
                        {
                            continue;
                        }
                        if(reader["productPhotoURL"] != DBNull.Value)
                        {
                            string url = reader["productPhotoURL"].ToString();
                            dr["productPhotoURL"] = reader["productPhotoURL"];
                        }
                        else
                        {
                            dr["productPhoto"] = reader["productPhoto"];
                        }
                        dr["productId"] = prodId;
                        dt.Rows.Add(dr);

                    }
                }
                dt.AcceptChanges();

                //bind with repeater
                DataList1.DataSource = dt;
                DataList1.DataBind();
                reader.Close();

                //prepare second query
                //selling format, selling end date, fixed price, start price, stock parameter into the second query
               
               cmdRetrieve = new SqlCommand(queryAuction, con);
               cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
               SqlDataReader sellingReader = cmdRetrieve.ExecuteReader();

               if (sellingReader.HasRows)
               {
                    while (sellingReader.Read())
                    {
                        fixedAvailable = sellingReader["Fixed_Prod_ID"].ToString().Length>2 ? 1 : 0;
                        auctionAvailable = sellingReader["Open_Auction_ID"].ToString().Length>2 ? 1 : 0;
                        sealedAvailable = sellingReader["Sealed_Auction_ID"].ToString().Length>2 ? 1 : 0;
                        auctionDuration = (int)sellingReader["Auction_Duration"];
                        txtFixedPrice.Text = sellingReader["productPrice"].ToString();
                        txtStartPrice.Text = sellingReader["Start_Price"].ToString();
                        txtStock.Text = sellingReader["productStock"].ToString();
                    }
               }

               //selling format - fixed
               if (fixedAvailable > 0)
               {
                    if (chkSellOption.Items.FindByValue("FixedPrice") != null)
                    {
                        chkSellOption.Items.FindByValue("FixedPrice").Selected = true;
                    }
               }

               //selling format - auction
               if (auctionAvailable > 0)
               {
                    if (chkSellOption.Items.FindByValue("OpenBidAuction") != null)
                    {
                        chkSellOption.Items.FindByValue("OpenBidAuction").Selected = true;
                    }
               }

               //selling format - sealed
               if (sealedAvailable > 0)
               {
                    if (chkSellOption.Items.FindByValue("SealedBidAuction") != null)
                    {
                        chkSellOption.Items.FindByValue("SealedBidAuction").Selected = true;
                    }
               }

               //auction duration
               if (auctionDuration > 0)
               {
                    if (ddlDuration.Items.FindByValue(auctionDuration.ToString()) != null)
                    {
                         ddlDuration.Items.FindByValue(auctionDuration.ToString()).Selected = true;
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
            
        }//ok

        void updateProductDetailsToDB(string productDetailsId)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryProductDetails = "UPDATE ProductDetails SET productName = @productName, productCategory = @productCategory, productType = @productType, productBrand = @productBrand, productModel = @productModel WHERE productDetailsId = @productDetailsId";

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

        void updateProductToDB(string productId, string productDetailsId) //ok
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryProduct = "UPDATE Product SET productDesc = @desc, subStoreId = @subStoreId WHERE productId = @productId AND productDetailsId = @productDetailsId";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(queryProduct, con);
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.Parameters.AddWithValue("@desc", txtDesc.Text);
                cmdRetrieve.Parameters.AddWithValue("@subStoreId", ddlSubStore.SelectedValue); //created subStoreId
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
                if (bytes == null)
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

        void updateFixedPriceProductToDB(string productId)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryFixedPriceProduct = "UPDATE FixedPriceProduct SET productPrice = @productPrice WHERE productId = @productId";

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

        void updateAuctionProductToDB(string productId) //ok, created current productId
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryAuctionProduct = "UPDATE AuctionProduct SET reservePrice = @reservePrice, startingPrice = @startPrice, auctionDuration = @auctionDuration WHERE productId = @productId";

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
        void updateAllDataToDB(string[] sellingFormat)
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
                string prodName = Request.QueryString["prodName"];
                productDetailsId = getProductDetailsId(prodName);

                //get productId 
                productId = getProductId();

                //get subStoreId
                subStoreId = ddlSubStore.SelectedValue;

                //ProductDetails, Product, ProductPhoto, FixedPriceProduct, AuctionProduct
                updateProductDetailsToDB(productDetailsId);
                updateProductToDB(productId, productDetailsId);

                //get photoURL or photo
                //foreach (DataRow dr in dt.Rows)
                //{
                //    try
                //    {
                //        productPhotoURL = (string)dr["photoURL"];
                //        if (productPhotoURL != null)
                //        {
                //            //insert the product photo into the database
                //            updateProductPhotoToDB(productId, null, productPhotoURL);
                //        }
                //        else
                //        {
                //            productPhoto = (byte[])dr["photo"];
                //            if (productPhoto.Length != 0)
                //            {
                //                updateProductPhotoToDB(productId, productPhoto, String.Empty);
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        continue;
                //    }

                //}

                //insert the fixed price product into DB
                updateFixedPriceProductToDB(productId);

                foreach (string option in sellingFormat)
                {
                    if (option.Equals("OpenBidAuction") || option.Equals("SealedBidAuction"))
                    {
                        updateAuctionProductToDB(productId);
                    }
                }


            }
            catch (NullReferenceException ex)
            {
                displayErrorMsg(ex);

            }
        } //update all the product

        /*Useless function*/
        string convertPhotoIntoURL(byte[] imgBytes)
        {
            StringBuilder imgURL = new StringBuilder();

            // If you want convert to a bitmap file
            TypeConverter tc = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap MyBitmap = (Bitmap)tc.ConvertFrom(imgBytes);

            imgURL = imgURL.Append(Convert.ToBase64String(imgBytes));
            imgURL.Replace(imgURL.ToString(), String.Format("data:image/Bmp;base64,{0}\"", imgURL));

            return imgURL.ToString();
        }
        protected void DataList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }
        protected void txtProdName_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodName"] = txtProdName.Text;
            //dr.AcceptChanges();
        }

        protected void ddlProdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodCategory"] = ddlProdCategory.SelectedValue;
            //dr.AcceptChanges();
        }

        protected void txtType_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodType"] = txtType.Text;
            //dr.AcceptChanges();
        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodBrand"] = txtBrand.Text;
            //dr.AcceptChanges();
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodModel"] = txtModel.Text;
            //dr.AcceptChanges();
        }

        protected void ddlSubStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["subStoreId"] = ddlSubStore.SelectedValue;
            //dr.AcceptChanges();
        }

        protected void txtDesc_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodDesc"] = txtDesc.Text;
            //dr.AcceptChanges();
        }

        protected void chkSellOption_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["auctionDuration"] = ddlDuration.SelectedValue;
            //dr.AcceptChanges();
        }

        protected void txtFixedPrice_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["fixedPrice"] = txtFixedPrice.Text;
            //dr.AcceptChanges();
        }

        protected void txtStartPrice_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["startPrice"] = txtStartPrice.Text;
            //dr.AcceptChanges();
        }

        protected void txtReservePrice_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["reservePrice"] = txtReservePrice.Text;
            //dr.AcceptChanges();
        }

        protected void txtStock_TextChanged(object sender, EventArgs e)
        {
            //DataRow dr;
            //DataTable dt = dtSet.Tables["Product"];

            ////get the first row of data table (the data table of "Product" has one row only)
            //dr = dt.Rows[0];

            ////update the data table "prodName" col value
            //dr["prodStock"] = txtStock.Text;
            //dr.AcceptChanges();
        }

        void updateProductPhotoToDB(string productId, byte[] bytes, string photoURL)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryProductPhoto = "UPDATE ProductPhoto SET productPhotoURL = @productPhotoURL, productPhoto = @productPhoto, photoStatus = @status WHERE productId = @productId";

            try
            {
                cmdRetrieve = new SqlCommand(queryProductPhoto, con);
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
        } //ok
    }
}