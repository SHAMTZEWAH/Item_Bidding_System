using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System
{
    public partial class SideMenuUser : System.Web.UI.UserControl
    {
        private DataSet dtSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();

        }
        
        void loadData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlDataAdapter adapter;
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId, Account.username, Account.accPhotoURL, Account.accPhoto " +
                "FROM Account WHERE Account.username = @name AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdRetrieve;

                dtSet = new DataSet();
                adapter.Fill(dtSet, "Account");
                DataList1.DataSource = dtSet.Tables["Account"];
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("/ErrorPage.aspx?Message="+ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}