﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                

                if (Request.QueryString["selection"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);
                    qs.Set("selection", radioSelect.SelectedValue);

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
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
            string allQuery = "";
            try
            { 
                foreach(ListItem item in chkBoxCategory.Items)
                {
                    if(item.Selected == true)
                    {
                        selectedCategory.Add(item.Value.ToString());
                    }
                }

                if (Request.QueryString["selection"] != null)
                {
                    var uri = new Uri(Request.Url.AbsoluteUri.ToString());
                    var qs = HttpUtility.ParseQueryString(uri.Query);
                    qs.Remove("category");
                    foreach (string item in selectedCategory)
                    {
                        qs.Add("category", item);
                    }

                    //build a new uri with diff param value
                    var uriBuilder = new UriBuilder(uri); //build a new uri obj with original value
                    uriBuilder.Query = qs.ToString(); //assign the query with new param
                    var newUri = uriBuilder.Uri.ToString();
                    Response.Redirect(newUri);
                }
                else if (Request.RawUrl.ToString().Contains("?"))
                {
                    foreach (string item in selectedCategory)
                    {
                        allQuery = "&category=" + item;
                    }
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {
                    foreach (string item in selectedCategory)
                    {
                        allQuery = "?category=" + item;
                    }
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
            catch (NullReferenceException)
            {
                if (Request.RawUrl.ToString().Contains("?"))
                {
                    foreach (string item in selectedCategory)
                    {
                        allQuery = "&category=" + item;
                    }
                    Response.Redirect(Request.RawUrl + allQuery);
                }
                else
                {   
                    foreach (string item in selectedCategory)
                    {
                        allQuery = "?category=" + item;
                    }
                    Response.Redirect(Request.RawUrl + allQuery);
                }
            }
        }

        protected void txtMinPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
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
            Response.Redirect(Request.RawUrl + "?minPrice=" + txtMaxPrice.Text);
        }

        protected void chkBoxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("/General/Product.aspx?state=" + Request.RawUrl);
        }

        protected void chkBoxSellOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("/General/Product.aspx?option=" + Request.RawUrl);
        }
    }
}