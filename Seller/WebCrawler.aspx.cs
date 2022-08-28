using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Net;

namespace Item_Bidding_System.Admin
{
    public partial class WebCrawler : System.Web.UI.Page
    {
        private static List<CrawlResult> urlList;
        private static List<CrawlProduct> productList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["prodName"].ToString()) && !string.IsNullOrEmpty(Request.QueryString["prodId"].ToString()))
                {
                    Session["prodId"] = Request.QueryString["prodId"].ToString();
                    RegisterAsyncTask(new PageAsyncTask(mainMethod));
                }
            }

        }

        private string getKeyword(string productName)
        {
            string keyword = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT keyword FROM ProductDetails WHERE productName = @name";

            //execute
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", productName);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        keyword = reader["keyword"].ToString().Replace(",","+");
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
            //get productName
            string productName = Request.QueryString["prodName"].ToString();

            //get keyword
            string keyword = getKeyword(productName);

            //await crawlURL("Web Crawler", 1); //1 represents the number of pages
            //await crawlShopeeasync(keyword, 1);
            //string url = "https://shopee.com.my/search?keyword=" + keyword;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "none", "<script src='scrape.js' type='text/javascript'>scrape("+url+")</script>", false);

            //create new product list
            productList = new List<CrawlProduct>();

            //crawl Google Shopping reference price
            await crawlGoogleShopURL(keyword, 1);
            Task.Delay(100).Wait();

            //crawl IPrice website reference price
            await crawlIPriceasync(keyword, 1);
            Task.Delay(100).Wait();

            insertProductRef();
            Response.Redirect("~/Seller/CreateProduct.aspx");
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
                        catch(Exception ex)
                        {
                            continue;
                        }
                    }
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
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

        private void insertProductRef()
        {
            string productInfoId = "";
            string productId = "";
            //get all the product

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO ProductInfo(productInfoId, productName, price, productId) VALUES(@productInfoId, @productName, @price, @productId)";

            //execute
            try
            {
                //get productInfo count
                int count = getProductInfoCount();

                //get product id
                if (!string.IsNullOrEmpty(Session["prodId"].ToString()))
                {
                    productId = Session["prodId"].ToString();
                }

                con.Open();
                foreach (var product in productList)
                {
                    try
                    {
                        //get productInfo Id
                        if (count + 1 > 9)
                        {
                            productInfoId = "pi_0" + (count + 1);
                        }
                        else
                        {
                            productInfoId = "pi_00" + (count + 1);
                        }

                        cmdRetrieve = new SqlCommand(query, con);
                        cmdRetrieve.Parameters.AddWithValue("@productInfoId", productInfoId);
                        cmdRetrieve.Parameters.AddWithValue("@productName", product.Name);
                        cmdRetrieve.Parameters.AddWithValue("@price", Convert.ToDouble(product.Price.Replace("RM", "").Trim()));
                        cmdRetrieve.Parameters.AddWithValue("@productId", productId);
                        cmdRetrieve.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }

                    count++;
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

        /*
         
         
         private async Task crawlURL(string keyword, int n_pages)
        {
            //get keywords
            //var keyword = "Bandai Oden Statue";

            urlList = new List<CrawlResult>();

            //use google bot indexing to get top 3 websites (exclude ads)
            for (var i = 1; i <= n_pages; i++)
            {
                //[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)?
                var url = "http://www.google.com/search?q=" + keyword + "price " + RegionInfo.CurrentRegion.EnglishName + " &num=10&start=" + ((i - 1) * 10).ToString();
                HtmlWeb web = new HtmlWeb();
                web.UserAgent = "user-agent=Mozilla/5.0" +
                    " (Windows NT 10.0; Win64; x64)" +
                    " AppleWebKit/537.36 (KHTML, like Gecko)" +
                    " Chrome/74.0.3729.169 Safari/537.36";

                //load the url
                var htmlDoc = web.Load(url);

                //get the node of the html element in Google
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='yuRUbf']");

                foreach (var tag in nodes)
                {
                    var result = new CrawlResult();

                    var contentURL = tag.Descendants("a").FirstOrDefault().Attributes["href"].Value;
                    var loc = new Uri(contentURL).Host;
                    result.Url = loc;
                    result.Title = tag.Descendants("h3").FirstOrDefault().InnerText;

                    urlList.Add(result);
                }
            }



            //store all the website URL into the urlList

        }
         
         private async Task crawlShopeeasync(string keyword, int n_pages)
        {
            productList = new List<CrawlProduct>();
            List<string> keywordList = keyword.Split('+').ToList();
            keyword = keyword.Replace("+","%20");

            //use google bot indexing to get top 3 websites (exclude ads)
            for (var i = 1; i <= n_pages; i++)
            {
            //[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)?
                var url = "https://shopee.com.my/search?keyword=" + keyword;
                HtmlWeb web = new HtmlWeb();
                web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 Edg/91.0.864.59";

                //load the url
                var htmlDoc = web.Load(url);

                //get the node of the html element in Google
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='KMyn8J']");

                //get the child node
                foreach (var tag in nodes)
                {
                    var result = new CrawlProduct();

                    //get name
                    int flag = 0;
                    result.Name = tag.Descendants("div").FirstOrDefault().InnerText;
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
                    var resultOfURL = tag.Descendants("div").ElementAtOrDefault(2).InnerText;
                    result.Price = resultOfURL;
                    productList.Add(result);
                }
            }
        }

        private async Task crawlAmazonPriceasync(string keyword, int n_pages)
        {
            productList = new List<CrawlProduct>();
            List<string> keywordList = keyword.Split('+').ToList();

            //based on the urlList, crawl the price one by one
            for (var i = 1; i <= n_pages; i++)
            {
            //[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)?
                var url = "https://www.amazon.com/s?k=" + keyword + "&crid=IQYIOMKAYK3D&prefix="+keyword+"%2Caps%2C306&ref=nb_sb_noss_1";
                HtmlWeb web = new HtmlWeb();
                web.UserAgent = "user-agent=Mozilla/5.0" +
                    " (Windows NT 10.0; Win64; x64)" +
                    " AppleWebKit/537.36 (KHTML, like Gecko)" +
                    " Chrome/74.0.3729.169 Safari/537.36";

                //load the url
                var htmlDoc = web.Load(url);

                //get the node of the html element in Google
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='a-section']");

                foreach (var tag in nodes)
                {
                    var result = new CrawlResult();

                    var contentURL = tag.Descendants("a").FirstOrDefault().Attributes["href"].Value;
                    var loc = new Uri(contentURL).Host;
                    result.Url = loc;
                    result.Title = tag.Descendants("h3").FirstOrDefault().InnerText;

                    urlList.Add(result);
                }

                //save the crawled price into the database

                //list of products
            }
        }
         */
    }
}