using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System
{
    public partial class SideMenuFilter : System.Web.UI.UserControl
    {
        //Delegate function (not used)
        public delegate void OnIndexChanged(string strValue);

        //Event Declaration (not used)
        public event OnIndexChanged IndexChangedHandler;

         

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkSession();
            }
            
        }

        //session purpose is to restore the value of check or uncheck
        void checkSession()
        {
            List<string> category = (List<string>)Session["selectedCategory"];
            List<string> state = (List<string>)Session["addrState"];
            List<string> sellOption = (List<string>)Session["selectedCategory"];

            try
            {
                if (!string.IsNullOrEmpty(Session["selection"].ToString()))
                {
                    radioSelect.SelectedValue = Session["selection"].ToString();
                }
            }
            catch (Exception ex)
            {
            }

            int count = 0;
            if (category?.Any()==true)
            {
                string cat = "";
                for (int i=0; i<chkBoxCategory.Items.Count && category.Count > count; i++)
                {
                    cat = category.ElementAt<string>(count);
                    if (chkBoxCategory.Items[i].Value == cat) //if it is match
                    {
                        count++;
                        chkBoxCategory.Items[i].Selected = true;
                    }
                }
                
            }

            try
            {
                if (!string.IsNullOrEmpty(Session["minPrice"].ToString()))
                {
                    txtMinPrice.Text = Session["minPrice"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                if (!string.IsNullOrEmpty(Session["maxPrice"].ToString()))
                {
                    txtMaxPrice.Text = Session["maxPrice"].ToString();
                }
            }
            catch(Exception ex)
            {

            }

            count = 0;
            if (state?.Any() == true)
            {
                string aState = "";
                for (int i = 0; i < chkBoxState.Items.Count && state.Count > count; i++)
                {
                    aState = state.ElementAt<string>(count);
                    if (chkBoxState.Items[i].Value == aState) //if it is match
                    {
                        count++;
                        chkBoxState.Items[i].Selected = true;
                    }
                }
            }

            count = 0;
            if (sellOption?.Any() == true)
            {
                string opt = "";
                for (int i = 0; i < chkBoxSellOption.Items.Count && sellOption.Count > count; i++)
                {
                    opt = sellOption.ElementAt<string>(count);
                    if (chkBoxSellOption.Items[i].Value == opt) //if it is match
                    {
                        count++;
                        chkBoxSellOption.Items[i].Selected = true;
                    }
                }
            }
        }

        protected void radioSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get current url
            //add url to it
            // Check if event is null
            //if (IndexChangedHandler != null)
            //    IndexChangedHandler(string.Empty);


            try
            {
                Session["selection"] = radioSelect.SelectedValue;

                if (Request.QueryString["selection"] != null)
                {
                    //get the uri
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());

                    //seperate all the key and value into pair
                    var qs = HttpUtility.ParseQueryString(uri.Query);

                    //based on the key and assign the value
                    qs.Set("selection", radioSelect.SelectedValue);

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString(); //convert it into URL
                    if (ViewState["selection"] != null)
                    {
                        radioSelect.SelectedValue = ViewState["selection"].ToString();
                    }
                    ViewState["selection"] = radioSelect.SelectedValue;
                    Response.Redirect(newUri);
                }
                else if (Request.RawUrl.ToString().Contains("?"))
                {
                    Response.Redirect(Request.RawUrl + "&selection=" + radioSelect.SelectedValue);
                }
                else
                {
                    Response.Redirect(Request.RawUrl + "?selection=" + radioSelect.SelectedValue);
                }
            }
            catch(NullReferenceException)
            {
                if (Request.RawUrl.ToString().Contains("?"))
                {
                    Response.Redirect(Request.RawUrl + "&selection=" + radioSelect.SelectedValue);
                }
                else
                {
                    Response.Redirect(Request.RawUrl + "?selection=" + radioSelect.SelectedValue);
                }
            }
            
            
        }

        protected void chkBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> selectedCategory = new List<string>();
            StringBuilder allQuery = new StringBuilder();
            try
            {
                
                foreach (ListItem item in chkBoxCategory.Items)
                {
                    if(item.Selected == true)
                    {
                        selectedCategory.Add(item.Value.ToString());
                    }
                }

                Session["selectedCategory"] = selectedCategory;

                if (Request.QueryString["category"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);

                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",","",allQuery.Length-1,1);
                    qs.Set("category", allQuery.ToString());

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
                    Response.Redirect(newUri);
                }
                else if (Request.RawUrl.ToString().Contains("?"))
                {
                    allQuery.Append("&category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    allQuery.Append("?category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
            catch (NullReferenceException)
            {
                if (Request.RawUrl.ToString().Contains("?"))
                {
                    allQuery.Append("&category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    allQuery.Append("?category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
        }

        protected void txtMinPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["minPrice"] = txtMinPrice.Text;
                if (Request.QueryString["minPrice"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);
                    qs.Set("minPrice", txtMinPrice.Text);

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
                    Response.Redirect(newUri);
                }
                else
                {
                    Response.Redirect(Request.RawUrl + "?minPrice=" + txtMinPrice.Text);
                }
            }
            catch(NullReferenceException ex)
            {
                Response.Redirect(Request.RawUrl + "?minPrice=" + txtMinPrice.Text);
            }
            
               
        }

        protected void txtMaxPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["maxPrice"] = txtMaxPrice.Text;
                if (Request.QueryString["maxPrice"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);
                    qs.Set("maxPrice", txtMaxPrice.Text);

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
                    Response.Redirect(newUri);
                }
                else
                {
                    Response.Redirect(Request.RawUrl + "?maxPrice=" + txtMaxPrice.Text);
                }
            }
            catch (NullReferenceException ex)
            {
                Response.Redirect(Request.RawUrl + "?maxPrice=" + txtMaxPrice.Text);
            }

            
            //Response.Redirect(Request.RawUrl + "?maxPrice=" + txtMaxPrice.Text);
        }

        protected void chkBoxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> state = new List<string>();
            Session["AddrState"] = state;

            List<string> selectedCategory = new List<string>();
            StringBuilder allQuery = new StringBuilder();
            try
            {

                foreach (ListItem item in chkBoxCategory.Items)
                {
                    if (item.Selected == true)
                    {
                        selectedCategory.Add(item.Value.ToString());
                    }
                }

                Session["selectedCategory"] = selectedCategory;

                if (Request.QueryString["category"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);

                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    qs.Set("category", allQuery.ToString());

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
                    Response.Redirect(newUri);
                }
                else if (Request.RawUrl.ToString().Contains("?"))
                {
                    allQuery.Append("&category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    allQuery.Append("?category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
            catch (NullReferenceException)
            {
                if (Request.RawUrl.ToString().Contains("?"))
                {
                    allQuery.Append("&category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    allQuery.Append("?category=");
                    foreach (string item in selectedCategory)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }

            //Response.Redirect("/General/Product.aspx?state=" + Request.RawUrl);
        }

        protected void chkBoxSellOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> selectedSellOption = new List<string>();
            StringBuilder allQuery = new StringBuilder();
            try
            {

                foreach (ListItem item in chkBoxSellOption.Items)
                {
                    if (item.Selected == true)
                    {
                        selectedSellOption.Add(item.Value.ToString());
                    }
                }

                Session["selectedCategory"] = selectedSellOption;

                if (Request.QueryString["category"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);

                    foreach (string item in selectedSellOption)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    qs.Set("category", allQuery.ToString());

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
                    Response.Redirect(newUri);
                }
                else if (Request.RawUrl.ToString().Contains("?"))
                {
                    allQuery.Append("&category=");
                    foreach (string item in selectedSellOption)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    allQuery.Append("?category=");
                    foreach (string item in selectedSellOption)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
            catch (NullReferenceException)
            {
                if (Request.RawUrl.ToString().Contains("?"))
                {
                    allQuery.Append("&category=");
                    foreach (string item in selectedSellOption)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    allQuery.Append("?category=");
                    foreach (string item in selectedSellOption)
                    {
                        allQuery.Append(item + ",");
                    }
                    allQuery.Replace(",", "", allQuery.Length - 1, 1);
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
            //Response.Redirect("/General/Product.aspx?option=" + Request.RawUrl);
        }
    }

}