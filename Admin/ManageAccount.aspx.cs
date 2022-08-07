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

namespace Item_Bidding_System.Admin
{
    public partial class ManageAccount : System.Web.UI.Page
    {
        //private DataSet dtSet;

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

                //get suitable query
                if(Request.QueryString["filter"] != null)
                {
                    cmdRetrieve = new SqlCommand(queryFilter, con);
                    cmdRetrieve.Parameters.AddWithValue("@filter",Request.QueryString["filter"].ToString());
                  
                }
                else
                {
                    cmdRetrieve = new SqlCommand(query, con);
                }

                //execute query 
                userGrid.DataSource = cmdRetrieve.ExecuteReader();
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

        string[] getSubStoreId(string username, string email)
        {
            string[] subStoreIdList = { };
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT SubStore.subStoreId " +
                "FROM SubStore INNER JOIN " +
                "Seller ON SubStore.sellerId = Seller.sellerId INNER JOIN " +
                "Account ON Account.accId = Seller.accId " +
                "WHERE Account.username = @username AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", username);
                cmdRetrieve.Parameters.AddWithValue("@email", email);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subStoreIdList[count] = reader["subStoreId"].ToString();
                        ++count;
                    }
                }
                
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
            return subStoreIdList;
        }

        void deactivateProduct(string username, string email)
        {
            //deactivate the product


            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "UPDATE Product SET productStatus = @status WHERE subStoreId = @subStoreId";

            //execute
            try
            {
                con.Open();
                string[] subStoreList = getSubStoreId(username, email);

                foreach (string subStore in subStoreList)
                {
                    cmdRetrieve = new SqlCommand(query, con);
                    cmdRetrieve.Parameters.AddWithValue("@subStoreId", subStore);
                    cmdRetrieve.ExecuteNonQuery();
                }
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

        protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //get hidden field that store role
                var hfRole = (HiddenField)e.Row.FindControl("hfRole");

                //assign roles into the ddlRoles
                var ddlRoles = e.Row.FindControl("ddlRoles") as DropDownList;
                ddlRoles.DataSource = Roles.GetAllRoles();
                ddlRoles.DataBind();

                //update the selected ddlRoles value
                ddlRoles.SelectedValue = hfRole.Value;
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

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = dtSet.Tables["Account"];

            //get accId control
            //var accIdControl = (Label)userGrid.NamingContainer.FindControl("accId");

            //get username control
            var usernameControl = ((GridViewRow)((Control)sender).NamingContainer).FindControl("username") as Label;

            //get email control
            var emailControl = ((GridViewRow)((Control)sender).NamingContainer).FindControl("email") as Label;

            //get hidden field
            var hiddenFieldRole = ((GridViewRow)((Control)sender).NamingContainer).FindControl("hfRole") as HiddenField;

            //get selected role from (ddlRoles)
            var ddlRoles = (DropDownList)sender;


            try 
            {
                string[] roles = Roles.GetRolesForUser(usernameControl.Text);
                foreach (string role in roles)
                {
                    if(role == "Seller")
                    {
                        //deactivate all of the product own by this user
                        deactivateProduct(usernameControl.Text, emailControl.Text);
                    }
                }

                //update to the hidden field
                hiddenFieldRole.Value = ddlRoles.SelectedValue;

                //update database
                Roles.AddUserToRole(usernameControl.Text, ddlRoles.SelectedValue);
            }
            catch(Exception ex)
            {
                lblNoData.Visible = true;
                lblNoData.Text = ex.Message.ToString();
            }
            
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


        /*Useless Function*/
        protected void chkHeader_CheckedChanged(object sender, EventArgs e)
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

        
    }
}