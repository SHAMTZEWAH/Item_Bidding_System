using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Admin
{
    public partial class UpdateWebCrawler : System.Web.UI.Page
    {
        //private static List<CrawlResult> urlList;
        private static List<CrawlProduct> productList;
        private static List<string> productIdList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(mainMethod));
                
            }

        }

        //get all product name
        private List<string> getAllKeyword()
        {
            List<string> keyword = new List<string>();

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT keyword, productId FROM ProductDetails INNER JOIN Product ON ProductDetails.productDetailsId = Product.productDetailsId";

            //execute
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        keyword.Add(reader["keyword"].ToString().Replace(",", "+"));
                        productIdList.Add(reader["productId"].ToString());
                    }
                }
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

            return keyword;
        }

        private async Task mainMethod()
        {
            int countProductId = 0;
            productList = new List<CrawlProduct>();
            productIdList = new List<string>();

            //get all keyword
            List<string> keywordList = getAllKeyword();

            foreach(string keyword in keywordList)
            {
                //crawl Google Shopping reference price
                await crawlGoogleShopURL(keyword, 1);
                Task.Delay(100).Wait();

                //crawl IPrice website reference price
                await crawlIPriceasync(keyword, 1);
                Task.Delay(100).Wait();

                //update the data if available, else add data
                foreach (var product in productList)
                {
                    //if available
                    if (checkProductInfo(product.Name) > 0)
                    {
                        updateProductRef(product.Price, product.Name);
                    }
                    else
                    {
                        insertProductRef(product.Price, product.Name, productIdList[countProductId]);
                    }
                }
                

                //clear all the data in product list
                productList.Clear();

                //move to the next product id that might be insert
                countProductId++;
            }

            //closure of updating the info
            string alertMsg = "Updated successfully!";
            string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            Response.Redirect("~/ManageProduct.aspx");
        }

        private async Task crawlIPriceasync(string keyword, int n_pages)
        {
            List<string> keywordList = keyword.Split('+').ToList();

            //use google bot indexing to get top 3 websites (exclude ads)
            for (var i = 1; i <= n_pages; i++)
            {
                try
                {
                    //[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)?
                    var url = "https://iprice.my/search/?term=" + keyword;
                    HtmlWeb web = new HtmlWeb();
                    web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59";

                    //load the url
                    var htmlDoc = web.Load(url);

                    //get the node of the html element in Google
                    HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'pu kF oT cM iq iU iV uu')]");

                    //get the child node
                    foreach (var tag in nodes)
                    {
                        try
                        {
                            var result = new CrawlProduct();
                            var contentList = tag.Descendants("a").FirstOrDefault().Descendants("span").ElementAtOrDefault(1);


                            //get name
                            int flag = 0;
                            result.Name = contentList.Descendants("span").FirstOrDefault().InnerText;
                            foreach (string aKey in keywordList)
                            {
                                if (!result.Name.Contains(aKey))
                                {
                                    flag = 1;
                                    break;
                                }
                            }

                            //if keyword is not all match
                            if (flag > 0)
                            {
                                continue;
                            }

                            //get price
                            var resultOfURL = contentList.Descendants("div").FirstOrDefault().InnerText.Split('R');
                            resultOfURL = resultOfURL.Where(x => !string.IsNullOrEmpty(x.Trim())).ToArray();
                            result.Price = "R" + resultOfURL[0];
                            productList.Add(result);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private async Task crawlGoogleShopURL(string keyword, int n_pages)
        {
            List<string> keywordList = keyword.Split('+').ToList();

            //use google bot indexing to get top 3 websites (exclude ads)
            for (var i = 1; i <= n_pages; i++)
            {
                try
                {
                    //[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)?
                    var url = "http://www.google.com/search?q=" + keyword + "price " + RegionInfo.CurrentRegion.EnglishName + " &num=100&start=" + ((i - 1) * 10) + "&tbm=shop".ToString();
                    HtmlWeb web = new HtmlWeb();
                    web.UserAgent = "user-agent=Mozilla/5.0" +
                        " (Windows NT 10.0; Win64; x64)" +
                        " AppleWebKit/537.36 (KHTML, like Gecko)" +
                        " Chrome/74.0.3729.169 Safari/537.36";

                    //load the url
                    var htmlDoc = web.Load(url);

                    //get the node of the html element in Google
                    HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='i0X6df']");

                    //get the child node
                    foreach (var tag in nodes)
                    {
                        try
                        {
                            var result = new CrawlProduct();

                            //get name
                            int flag = 0;
                            result.Name = tag.Descendants("h4").FirstOrDefault().InnerText;
                            foreach (string aKey in keywordList)
                            {
                                if (!result.Name.Contains(aKey))
                                {
                                    flag = 1;
                                    break;
                                }
                            }

                            //if keyword is not all match
                            if (flag > 0)
                            {
                                continue;
                            }

                            //get price
                            var resultOfURL = tag.Descendants("span").ToList();

                            foreach (var price in resultOfURL)
                            {
                                try
                                {
                                    var someClass = price.Attributes["class"].DeEntitizeValue;
                                    if (someClass.Equals("a8Pemb OFFNJ"))
                                    {
                                        var rawPrice = price.InnerText.Split('+');
                                        result.Price = rawPrice[0];
                                        break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }

                            }

                            productList.Add(result);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        //check whether the product info is available
        private int checkProductInfo(string productName)
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(ProductInfo.productInfoId) FROM ProductInfo WHERE productName = @productName";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@productName", productName);
                count = (int)cmdRetrieve.ExecuteScalar();

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

            return count;
        }

        private int getProductInfoCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(ProductInfo.productInfoId) FROM ProductInfo";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();

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

            return count;
        }

        private void insertProductRef(string price, string productName, string productId)
        {
            string productInfoId = "";
            //get all the product

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO ProductInfo(productInfoId, productName, price, productId) VALUES (@productInfoId, @productName, @price, @productId)";

            //execute
            try
            {
                //get productInfo count
                int count = getProductInfoCount();

                //get productInfo Id
                if (count + 1 > 9)
                {
                    productInfoId = "pi_0" + (count + 1);
                }
                else
                {
                    productInfoId = "pi_00" + (count + 1);
                }

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@productInfoId", productInfoId);
                cmdRetrieve.Parameters.AddWithValue("@productName", productName);
                cmdRetrieve.Parameters.AddWithValue("@price", Convert.ToDouble(price.Replace("RM", "").Trim()));
                cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                cmdRetrieve.ExecuteNonQuery();

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
        }

        private void updateProductRef(string price, string productName)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "UPDATE ProductInfo SET price = @price WHERE productName = @productName";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@productName", productName);
                cmdRetrieve.Parameters.AddWithValue("@price", String.Format("{0:0.##}", price));
                cmdRetrieve.ExecuteNonQuery();

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
        }

        public class CrawlResult
        {
            public string Url { get; set; }
            public string Title { get; set; }

        }

        public class CrawlProduct
        {
            public string Name { get; set; }

            public string Price { get; set; }
        }
    }
}