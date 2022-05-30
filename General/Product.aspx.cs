using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
            //Response.Write(Request.Url);
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["category"].ToString() != null)
                    {
                        //string connection
                        Response.Write(Request.QueryString["category"].ToString());
                        //con = new SqlConnection();
                    }
                    else if (Request.QueryString["keyword"].ToString() != null)
                    {
                        Response.Write(Request.QueryString["keyword"].ToString());
                    }

                }
            }
            catch(NullReferenceException ex)
            {
                Response.Redirect("/ErrorPage.aspx");
            }
            
        }
    }
}