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
        private DataSet dtSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadData();
            }
            
        }

        //to store the photo temporarily before update into database

        void createAccountTable()
        {
            DataTable dt = new DataTable("Account");
            DataColumn dtColumn;

            //create datatable
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "accId";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "username";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "email";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = true;
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "sellerStatus";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "RoleName";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
            dt.Columns.Add(dtColumn);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dt.Columns["accId"];
            dt.PrimaryKey = PrimaryKeyColumns;

            //create data set
            dtSet = new DataSet();

            //assign data table into data set
            dtSet.Tables.Add(dt);
        }

        void loadData()
        {
            createAccountTable();
            DataTable dt = dtSet.Tables["Account"];
            DataColumn dtColumn;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId, Account.username, Account.email, Seller.sellerStatus, aspnet_Roles.RoleName " +
                "FROM Account INNER JOIN " +
                "aspnet_UsersInRoles ON Account.userId = aspnet_UsersInRoles.UserId INNER JOIN " +
                "aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId FULL JOIN " +
                "Seller ON Seller.accId = Account.accId " +
                "ORDER BY Account.createDateTime";
            string queryFilter = "SELECT Account.accId, Account.username, Account.email, Seller.sellerStatus, aspnet_Roles.RoleName " +
                "FROM Account INNER JOIN " +
                "aspnet_UsersInRoles ON Account.userId = aspnet_UsersInRoles.UserId INNER JOIN " +
                "aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId FULL JOIN " +
                "Seller ON Seller.accId = Account.accId " +
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
                SqlDataReader reader = cmdRetrieve.ExecuteReader();

                //remove multiple role acc
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow row = dt.NewRow();
                        try
                        { 
                            row["accId"] = reader["accId"];
                            row["username"] = reader["username"];
                            row["email"] = reader["email"];
                            row["sellerStatus"] = reader["sellerStatus"];
                            row["RoleName"] = reader["RoleName"];
                            dt.Rows.Add(row);
                        }
                        catch (Exception ex)
                        {
                            //get duplicate role
                            string duplicateRole = reader["RoleName"].ToString();
                            //var duplicates = dt.AsEnumerable().GroupBy(r => r[0]).Where(gr => gr.Count() > 1).ToList();
                            
                            //replace the role into the current existing row accId
                            if(duplicateRole != "Customer")
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i].Field<string>("accId") == reader["accId"].ToString())
                                    {
                                        dt.Rows[i].SetField("RoleName", duplicateRole);
                                    }
                                }
                                dt.AcceptChanges();
                            }
                        }
                        
                    }
                }

                userGrid.DataSource = dt;
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
        string createSellerId()
        {
            string sellerId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(sellerId) FROM Seller";
            int count = 0;

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();

                if(count+1 > 9)
                {
                    sellerId = "s_0" + (count + 1);
                }
                else
                {
                    sellerId = "s_00" + (count + 1);
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

            return sellerId;
        }
        string getAccId()
        {
            string accId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT accId FROM Account WHERE Account.username = @username AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        accId = (string)reader["accId"];
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
            return accId;
        }

        int getSubStoreCount()
        {
            int subStoreCount = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(subStoreId) FROM SubStore";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                subStoreCount = (int)cmdRetrieve.ExecuteScalar();
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
            return subStoreCount;
        }

        void createSellerData()
        {
            string accId = "";
            int countSeller = 0;
            string sellerId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(sellerId) FROM Seller INNER JOIN " +
                "Account ON Seller.accId = Account.accId " +
                "WHERE Account.username = @username AND Account.email = @email";
            string queryInsertSellerData = "INSERT INTO Seller(sellerId, limitPayout, merchantId, businessName, sellerStatus, accId) " +
                "VALUES(@sellerId, 10000.00, NULL, @businessName, 'Confirm', @accId)";
            string queryActivateSeller = "UPDATE Seller SET sellerStatus = @status WHERE accId = @accId";


            //execute
            try
            {

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                countSeller = (int)cmdRetrieve.ExecuteScalar();
                accId = getAccId();
                sellerId = createSellerId();

                if (countSeller < 1)
                {
                    //if dont have previous record, create new data
                    cmdRetrieve = new SqlCommand(queryInsertSellerData, con);
                    cmdRetrieve.Parameters.AddWithValue("@sellerId", sellerId);
                    cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                    cmdRetrieve.Parameters.AddWithValue("@businessname", (User.Identity.Name + " Sdn. Bhd."));
                    cmdRetrieve.ExecuteNonQuery();

                    //create subStore
                    createSubStoreData(sellerId);
                }
                else
                {
                    //if have previous record, activate it
                    cmdRetrieve = new SqlCommand(queryActivateSeller, con);
                    cmdRetrieve.Parameters.AddWithValue("@accId", accId);
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

        void createSubStoreData(string sellerId)
        {
            int subStoreCount = getSubStoreCount();
            string subStoreId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO SubStore(subStoreId, subStoreName, subStoreDescription, subStoreStatus, createDateTime, sellerId) " +
                "VALUES(@subStoreId, @subStoreName, NULL, 'Active', CONVERT(DATETIME, GETDATE(), 120), @sellerId)";

            //execute
            try
            {
                

                if (subStoreCount + 1 > 9)
                {
                    subStoreId = "ss_0" + (subStoreCount+1);
                }
                else
                {
                    subStoreId = "ss_00" + (subStoreCount+1);
                }

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@subStoreId", subStoreId);
                cmdRetrieve.Parameters.AddWithValue("@subStoreName", ("SubStore" + (subStoreCount+1)));
                cmdRetrieve.Parameters.AddWithValue("@sellerId", sellerId);
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

            //get the current row index 
            int index = ((GridViewRow)(ddlRoles.NamingContainer)).RowIndex;

            try 
            {
                string[] roles = Roles.GetRolesForUser(usernameControl.Text);

                foreach (string role in roles)
                {
                    if (role == "Seller")
                    {
                        //deactivate all of the product own by this user
                        deactivateProduct(usernameControl.Text, emailControl.Text);
                    }
                    if (role != ddlRoles.SelectedValue)
                    {
                        Roles.RemoveUserFromRole(usernameControl.Text, role);
                    }
                }

                //update to the hidden field
                hiddenFieldRole.Value = ddlRoles.SelectedValue;

                //update database
                Roles.AddUserToRole(usernameControl.Text, ddlRoles.SelectedValue);

                //create seller data if new role is seller
                if (ddlRoles.SelectedValue == "Seller")
                {
                    createSellerData();
                }
                loadData();
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