using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                Console.WriteLine("Welcome to Google crawler");
                RegisterAsyncTask(new PageAsyncTask(mainMethod));
            }

        }

        private static async Task mainMethod()
        {
            //await crawlURL("Web Crawler", 1);
            await crawlGoogleShopURL("Bandai Figuarts Zero One Piece Armor Oden", 1);
            //await crawlAmazonPriceasync();

            foreach (var result in urlList)
            {
                Console.WriteLine(result.Title);
                Console.WriteLine(result.Url);
                Console.Write("\n");
            }
            //await crawlPriceasync();
        }

        private static async Task crawlURL(string keyword, int n_pages)
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

        private static async Task crawlGoogleShopURL(string keyword, int n_pages)
        {
            productList = new List<CrawlProduct>();

            //use google bot indexing to get top 3 websites (exclude ads)
            for (var i = 1; i <= n_pages; i++)
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

                foreach (var tag in nodes)
                {
                    var result = new CrawlProduct();

                    var priceList = tag.SelectNodes("//div[@class='sh-dgr__content']");
                    var contentURL = priceList[2].Descendants("span").FirstOrDefault().InnerText;

                    var loc = new Uri(contentURL).Host;
                    result.Price = loc;

                    result.Name = tag.Descendants("h4").FirstOrDefault().InnerText;

                    productList.Add(result);
                }
            }
        }

        private static async Task crawlAmazonPriceasync()
        {
            //based on the urlList, crawl the price one by one
            try
            {
                if(urlList.Count > 0)
                {
                    foreach(CrawlResult crawlURL in urlList)
                    {
                        var url = crawlURL.Url;
                        HtmlWeb web = new HtmlWeb();
                        web.UserAgent = "user-agent=Mozilla/5.0" +
                            " (Windows NT 10.0; Win64; x64)" +
                            " AppleWebKit/537.36 (KHTML, like Gecko)" +
                            " Chrome/74.0.3729.169 Safari/537.36";
                        //https://www.google.com/search?q=Bandai+Figuarts+Zero+One+Piece+Armor+Oden&sxsrf=ALiCzsYyiFTdXQRGV0rqxUk77jEgyNf-Rg:1661397666445&source=lnms&tbm=shop&sa=X&ved=2ahUKEwiswfTNhOH5AhXg_DgGHZwiDZMQ_AUoAnoECAEQBA&biw=1229&bih=577&dpr=1.56
                        //https://www.google.com/search?q=Bandai+Figuarts+Zero+One+Piece+Armor+Oden&sxsrf=ALiCzsZsRknFQEbPkMmMzD_KwpAimsL2Hg:1661397668071&source=lnms&sa=X&ved=0ahUKEwjesdfOhOH5AhV2xjgGHRT2AZMQ_AUIqwkoAA&biw=1229&bih=577&dpr=1.56
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
                }
            }
            catch(Exception ex)
            {
                
            }

            //save the crawled price into the database

            //list of products

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