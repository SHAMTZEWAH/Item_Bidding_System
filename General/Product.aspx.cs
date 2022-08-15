using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlConnection con;
            
            PageAsyncTask pageAsyncTask = new PageAsyncTask(executeAllMethodAsync);
            Page.RegisterAsyncTask(pageAsyncTask);
            //Method to invoke the registered async-methods immedietly and not after the PreRender-Event
            Page.ExecuteRegisteredAsyncTasks();
        }

        async Task executeAllMethodAsync()
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["selection"].ToString() != null)
                    {
                        //show category product when the user click on the category bar below the search bar
                        await loadContentAsync(Request.QueryString["selection"].ToString());

                    }
                    else if (Request.QueryString["keyword"].ToString() != null) //searching keyword (on search bar)
                    {
                        //show product based on the keyword
                        await loadContentAsync(Request.QueryString["keyword"].ToString());
                    }
                    else
                    {
                        await loadHotProductAsync();
                    }


                }
                catch (NullReferenceException ex)
                {
                    //Show Hot product
                    await loadHotProductAsync(); 
                }
                finally
                {
                    ctrl_IndexChangedHandler("");
                }
            }
        }
        void ctrl_IndexChangedHandler(string value) //not used
        {
            //ViewState[];
            //var filterControl = new Control();
            //filterControl = Page.Master.FindControl("SideMenu");
            //filterControl = filterControl.FindControl("SideMenuFilter.ascx");
            //DropDownList ddlSelect = (DropDownList)(filterControl as SideMenuFilter).NamingContainer.FindControl("radioSelect");
            //// change index
            //if (ViewState["selection"] != null)
            //{
            //    ddlSelect.SelectedValue = ViewState["selection"].ToString();
            //}
        }

        async Task loadHotProductAsync()
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;

            if (con.State.ToString() == "Open")
            {
                con.Close();
            }

            //execute query
            await Task.Run(() =>
            {
                try
                {

                    con.Open();
                    cmdRetrieve = new SqlCommand();
                    cmdRetrieve.CommandType = CommandType.StoredProcedure;
                    cmdRetrieve.CommandText = "pr_HotProduct"; //need to change to "pr_HotProduct"
                    cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name); //need to remove
                    cmdRetrieve.Connection = con;
                    Repeater1.DataSource = cmdRetrieve.ExecuteReader();
                    Repeater1.DataBind();
                }
                catch (NullReferenceException exDB)
                {
                    Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
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
            }); 
        } 

        async Task<DataTable> loadSPAsync(string filterDetails, string sp)
        {
            string username = "";
            string[] category = { };
            string minPrice = "";
            string maxPrice = "";
            string[] state = { };
            string[] sellingFormat = { };
            string selectedSellingFormat = "";
            string selection = "";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            //create connectionString
            SqlConnection con = new SqlConnection();
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            //apply string query
            SqlCommand cmdRetrieve;
            string queryWithoutMinAndMax1 = "SELECT ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock--, ISNULL(allProduct.yourBid,0) AS yourBid " +
                "FROM ProductPhoto INNER JOIN " +
                "Product ON ProductPhoto.productId = Product.productId " +
                "FULL JOIN BidTable ON Product.productId = BidTable.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON Product.productId = FixedPriceProduct.productId FULL JOIN " +
                "AuctionProduct ON AuctionProduct.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId INNER JOIN " +
                "Address ON Address.accId = Account.accId " +
                "WHERE(Product.addDateTime >= DateAdd(day, 1, @secondDate) AND Product.addDateTime < DateAdd(month, 1, @startDate)) AND ProductPhoto.photoStatus = 'Main' AND " +
                "ProductDetails.productCategory LIKE '%' + @category + '%' AND FixedPriceProduct.productPrice >= @minprice AND FixedPriceProduct.productPrice <= @maxprice AND Address.state LIKE '%' + @state + '%' AND " +
                "AuctionProduct.productId IS @sellingformat " +
                "GROUP BY ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock, addDateTime " +
                "ORDER BY Product.addDateTime DESC";
            string queryWithoutMinAndMax = "ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock " +
                "FROM ProductPhoto INNER JOIN " +
                "Product ON ProductPhoto.productId = Product.productId " +
                "FULL JOIN BidTable ON Product.productId = BidTable.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN " +
                "FixedPriceProduct ON Product.productId = FixedPriceProduct.productId LEFT JOIN " +
                "AuctionProduct ON AuctionProduct.productId = Product.productId LEFT JOIN " +
                "OrderProduct ON OrderProduct.productId = Product.productId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId INNER JOIN " +
                "Address ON Address.accId = Account.accId " +
                "WHERE(Product.addDateTime >= DateAdd(day, 1, @secondDate) AND Product.addDateTime < DateAdd(month, 1, @startDate)) AND ProductPhoto.photoStatus = 'Main' AND " +
                "AuctionProduct.productId IS @sellingformat AND FixedPriceProduct.productPrice >= @minprice AND FixedPriceProduct.productPrice <= @maxprice AND ";
            string query = "";
            
            if (con.State.ToString() == "Open")
            {
                con.Close();
            }

            //wait for the connection string to create 
            con.ConnectionString = strCon;

            //execute query
            try
            {
                con.Open();

                //wait for the username to have value 
                await Task.Run(() =>
                {
                    try
                    {
                        username = User.Identity.Name;
                    }
                    catch(Exception ex)
                    {
                        username = "";
                    }
                });

                //wait for the category to have value
                await Task.Run(() =>
                {
                    try
                    {
                        string qValue = Request.QueryString["category"];
                        category = qValue.Split(',');
                    }
                    catch (Exception ex)
                    {
                        category = new string[1] { "" };
                    }
                });

                //wait for the min price to have value
                await Task.Run(() =>
                {
                    try
                    {
                        minPrice = Request.QueryString["minPrice"];
                    }
                    catch (Exception ex)
                    {
                        minPrice = "";
                    }
                });

                //wait for the max price to have value
                await Task.Run(() =>
                {
                    try
                    {
                        maxPrice = Request.QueryString["maxPrice"];
                    }
                    catch (Exception ex)
                    {
                        maxPrice = "";
                    }
                });

                //wait for the state to have value
                await Task.Run(() =>
                {
                    try
                    {
                        string qValue = Request.QueryString["state"];
                        state = qValue.Split(',');
                    }
                    catch (Exception ex)
                    {
                        state = new string[1] { "" };
                    }
                });

                //wait for the state to have value
                await Task.Run(() =>
                {
                    try
                    {
                        string qValue = Request.QueryString["option"];
                        sellingFormat = qValue.Split(',');
                    }
                    catch (Exception ex)
                    {
                        sellingFormat = new string[1] {""};
                    }
                });

                //wait for the selection to have value
                await Task.Run(() =>
                {
                    try
                    {
                        selection = Request.QueryString["selection"];
                    }
                    catch (Exception ex)
                    {
                        selection = "";
                    }
                });

                //if no min and max
                //if have min and max
                //if have min, but no max
                //if have max, but no min
                //if Hots
                //if Newly added
                //if Auction Product is NULL (no check & fixed product only)
                //if Auction Product is NOT NULL (have check & auction product only)
                //@secondDate ('08/02/2022'), @startDate ('08/01/2022'), @category, @minprice, @maxprice, @state, @sellingformat, selection

                //min and max price
                query = queryWithoutMinAndMax;
                if (string.IsNullOrEmpty(minPrice))
                {           
                    minPrice = "0";
                }
                if (string.IsNullOrEmpty(maxPrice))
                {
                    maxPrice = "1000000";
                }

                //Auction product is null or not null (selling format)
                int count = 0;
                foreach (string opt in sellingFormat) //OpenAuction
                {
                    if (opt == "SealedBidAuction" || opt == "OpenBidAuction")
                    {
                        selectedSellingFormat = "NOT NULL";
                        break;
                    }
                    count++;
                }
                if (count > 1)
                {
                    selectedSellingFormat = "NULL";
                }

                //product category
                //Address.state LIKE '%' + @state + '%' AND ";
                for(int i=0; i<category.Length; i++)
                {
                    query += "ProductDetails.productCategory LIKE '%' + @category"+(i+1)+"'%' AND ";
                }

                //address state
                for (int i = 0; i < state.Length; i++)
                {
                    query += "Address.state LIKE '%' + @state" + (i + 1) + "'%' AND ";
                }

                query.TrimEnd(new char[] {'A','N','D'});
                query += "GROUP BY ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock, addDateTime ";

                //selection
                cmdRetrieve = new SqlCommand(query, con);
                if (selection == "NewlyAdded")
                {
                    query += "ORDER BY Product.addDateTime DESC";
                }
                else if (selection == "Hots")
                {
                    query += "ORDER BY COUNT(OrderProduct.productId) DESC";
                }

                //wait for the cmdRetrieve is ready
                await Task.Run(() =>
                {
                    int monthInt = DateTime.Now.Month;
                    string month = "";
                    if (monthInt <= 9)
                    {
                        month += ("0" + monthInt.ToString());
                    }
                    else
                    {
                        month = monthInt.ToString();
                    }
                    cmdRetrieve.Parameters.AddWithValue("@name", username);
                    cmdRetrieve.Parameters.AddWithValue("@secondDate", (month+"/02/2022"));
                    cmdRetrieve.Parameters.AddWithValue("@startDate",(month+"/01/2022"));
                    cmdRetrieve.Parameters.AddWithValue("@minprice", minPrice);
                    cmdRetrieve.Parameters.AddWithValue("@maxprice", maxPrice);
                    cmdRetrieve.Parameters.AddWithValue("@sellingformat", sellingFormat);
                    for (int i=0; i<category.Length; i++)
                    {
                        cmdRetrieve.Parameters.AddWithValue("@category"+(i+1), category);
                    }
                    for (int i = 0; i < state.Length; i++)
                    {
                        cmdRetrieve.Parameters.AddWithValue("@state"+(i+1), state);
                    }
                });

                //wait for the dataTable to be fill before return
                adapter.SelectCommand = cmdRetrieve;
                adapter.Fill(dataTable);

                
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
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
            return dataTable;
        }

        async Task loadContentAsync(string filterDetails)
        {
            //async task assign in var
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();


            //var checkCategory = loadSPAsync(filterDetails, "pr_filterCategory");
            //var checkName = loadSPAsync(filterDetails, "pr_filterName");
            //var checkBrand = loadSPAsync(filterDetails, "pr_filterBrand");
            //var checkBusinessName = loadSPAsync(filterDetails, "pr_filterBusinessName");
            //var checkModel = loadSPAsync(filterDetails, "pr_filterModel");
            //var checkType = loadSPAsync(filterDetails, "pr_filterType");
            var getProduct = loadSPAsync(filterDetails, "");

            stopWatch.Stop();
            Label1.Text = stopWatch.Elapsed.TotalSeconds.ToString();

            //check which task finish first 
            DataTable resultTable = new DataTable();

            var filterTasks = new List<Task> { getProduct };
            while(filterTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(filterTasks);
                if(finishedTask == getProduct)
                {
                    //assign dataset to the repeater
                    Repeater1.DataSource = resultTable;
                    Repeater1.DataBind();
                }
            }
            //var filterTasks = new List<Task> { checkCategory, checkName, checkBrand, checkBusinessName, checkModel, checkType };
            //while (filterTasks.Count > 0)
            //{
            //    //check if any dataTable has data
            //    Task finishedTask = await Task.WhenAny(filterTasks);
            //    if (finishedTask == checkCategory)
            //    {
            //        if(checkCategory.Result.Rows.Count > 0)
            //        {
            //            resultTable.Merge(checkCategory.Result);
            //        }
            //    }
            //    else if (finishedTask == checkName)
            //    {
            //        if (checkName.Result.Rows.Count > 0)
            //        {
            //            resultTable.Merge(checkName.Result);
            //        }
            //    }
            //    else if (finishedTask == checkBrand)
            //    {
            //        if (checkBrand.Result.Rows.Count > 0)
            //        {
            //            resultTable.Merge(checkBrand.Result);
            //        }
            //    }
            //    else if (finishedTask == checkBusinessName)
            //    {
            //        if (checkBusinessName.Result.Rows.Count > 0)
            //        {
            //            resultTable.Merge(checkBusinessName.Result);
            //        }
            //    }
            //    else if (finishedTask == checkModel)
            //    {
            //        if (checkModel.Result.Rows.Count > 0)
            //        {
            //            resultTable.Merge(checkModel.Result);
            //        }
            //    }
            //    else if (finishedTask == checkType)
            //    {
            //        if (checkType.Result.Rows.Count > 0)
            //        {
            //            resultTable.Merge(checkType.Result);
            //        }
            //    }
            //    filterTasks.Remove(finishedTask);
            //}

            

            //// create a UniqueConstraint instance and set its columns that should make up
            //// that uniqueness constraint - in your case, that would be a set of *three*
            //// columns, obviously! Adapt to your needs!
            //UniqueConstraint resultUnique =
            //   new UniqueConstraint(new DataColumn[] { resultTable.Columns["productId"] });

            //// add unique constraint to the list of constraints for your DataTable
            //resultTable.Constraints.Add(resultUnique);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}