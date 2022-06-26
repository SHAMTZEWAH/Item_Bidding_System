using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.User
{
    public partial class Watchlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getConnection();
            }
        }

        void getConnection()
        {
            string username = ""; 

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);


            //prepare command
            SqlCommand cmdRetrieve;
            cmdRetrieve = new SqlCommand();
            cmdRetrieve.CommandType = CommandType.StoredProcedure;
            cmdRetrieve.CommandText = "SelectAllProducts";

            try
            {
                //get username 
                username = User.Identity.Name; 

                //execute query
                con.Open();
                cmdRetrieve.Connection = con;
                cmdRetrieve.Parameters.AddWithValue("@name", username);
                Repeater1.DataSource = cmdRetrieve.ExecuteReader();
                Repeater1.DataBind();
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
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Repeater1.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblNoData");
                    lblFooter.Visible = true;
                }
            }
        }

        void Page_Error()
        {
            Response.Write("<div>Sorry for the error</div>");
            Response.Write(Server.GetLastError().Message + "<br />");
            Server.ClearError();


        }
    }
}