using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Repeater1.DataSource = SqlDataSource1;
            if (!IsPostBack)
            {
                //Repeater1.DataBind();
               
            }

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "select")
            {
                var index = e.Item.ItemIndex;
                Label lblProdName =  (Label)Repeater1.Items[index].FindControl("lblName");
                string prodName = lblProdName.Text;
                Response.Redirect("/General/ProductDetails.aspx?prodName=" + prodName + "&category="+"&keyword=");
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                var index = e.Item.ItemIndex;
                Label lblProdName = (Label)Repeater2.Items[index].FindControl("lblName2");
                string prodName = lblProdName.Text;
                Response.Redirect("/General/ProductDetails.aspx?prodName=" + prodName);
            }
        }

        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Image1_1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Image2_1_Click(object sender, ImageClickEventArgs e)
        {

        }


        // div onclick ->
        // the product name is obtained from data bound ->
        // save at cookies / somewhere ->
        // retrieve from the js / c# and redirect with query string


        //today's hot - based on sales (first 10 items)

        //today's new added - retrieve today's date => display first 10 items
    }
}