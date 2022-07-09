using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Admin
{
    public partial class ManageAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
            
        }

        void loadData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId, Account.username, Account.email, aspnet_Roles.RoleName " +
                "FROM Account INNER JOIN " +
                "aspnet_UsersInRoles ON Account.userId = aspnet_UsersInRoles.UserId INNER JOIN " +
                "aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId " +
                "ORDER BY Account.createDateTime";
            string queryFilter = "SELECT Account.accId, Account.username, Account.email, aspnet_Roles.RoleName " +
                "FROM Account INNER JOIN " +
                "aspnet_UsersInRoles ON Account.userId = aspnet_UsersInRoles.UserId INNER JOIN " +
                "aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId " +
                "ORDER BY @filter";

            //execute
            try
            {
                con.Open();
                if(Request.QueryString["filter"] != null)
                {
                    cmdRetrieve = new SqlCommand(queryFilter, con);
                    cmdRetrieve.Parameters.AddWithValue("@filter",Request.QueryString["filter"].ToString());
                  
                }
                else
                {
                   
                    cmdRetrieve = new SqlCommand(query, con);
                }
                
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                userGrid.DataSource = reader;
                userGrid.DataBind();
            }
            catch (NullReferenceException ex)
            {
                lblNoData.Visible = true;
                lblNoData.Text = ex.Message.ToString();

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "accId" ||
                RadioButtonList1.SelectedValue == "username" ||
                RadioButtonList1.SelectedValue == "email" ||
                RadioButtonList1.SelectedValue == "RoleName")
            {
                Response.Redirect("/Admin/ManageAccount.aspx?filter=" + RadioButtonList1.SelectedValue);
            }
        }

        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlRoles = (DropDownList)sender;
            int index = ddlRoles.SelectedIndex;
            string value = ddlRoles.SelectedValue;
            this.Session["roleName"] = value;
        }

        protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void userGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void userGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void userGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void userGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void userGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void roleSubmitBtn_Click(object sender, EventArgs e)
        {
            Roles.CreateRole(txtRoles.Text);
            txtRoles.Text = "";
        }

        protected void btnFlag_Click(object sender, EventArgs e)
        {
            //update account status
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "UPDATE Account SET Account.accStatus = 'Flagged' WHERE Account.username = @name AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name); //wrong
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email); //wrong
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                lblNoData.Visible = true;
                lblNoData.Text = ex.Message.ToString();

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void btnUnflag_Click(object sender, EventArgs e)
        {
            //update account status
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "UPDATE Account SET Account.accStatus = 'Active' WHERE Account.username = @name AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name); //wrong
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email); //wrong
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                lblNoData.Visible = true;
                lblNoData.Text = ex.Message.ToString();

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}