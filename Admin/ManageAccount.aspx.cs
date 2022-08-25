using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Admin
{
    public partial class ManageAccount : System.Web.UI.Page
    {
        private DataSet dtSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<CheckBox> checkBox1;
            if (!IsPostBack)
            {
                loadData();
                checkBox1 = checkBoxContainer();
                //retrieve all the checkbox which is checked
                foreach (CheckBox checkbox in checkBox1)
                {
                    GridViewRow gvr = (GridViewRow)checkbox.NamingContainer;
                    displayToggle(checkbox, gvr.RowIndex);
                }
                try
                {
                    if(Request.QueryString["filter"].ToString() != null)
                    {
                        RadioButtonList1.SelectedValue = Request.QueryString["filter"].ToString();
                    }
                }
                catch(Exception ex)
                {
                    lblNoData.Visible = true;
                    lblNoData.Text = ex.Message.ToString();
                }
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
            dtColumn.ColumnName = "accStatus";
            dtColumn.ReadOnly = false;
            dtColumn.Unique = false;
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

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId, Account.username, Account.email, Account.accStatus, Seller.sellerStatus, aspnet_Roles.RoleName " +
                "FROM Account INNER JOIN " +
                "aspnet_UsersInRoles ON Account.userId = aspnet_UsersInRoles.UserId INNER JOIN " +
                "aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId FULL JOIN " +
                "Seller ON Seller.accId = Account.accId " +
                "ORDER BY Account.createDateTime";
            string queryFilter = "SELECT Account.accId, Account.username, Account.email, Account.accStatus, Seller.sellerStatus, aspnet_Roles.RoleName " +
                "FROM Account INNER JOIN " +
                "aspnet_UsersInRoles ON Account.userId = aspnet_UsersInRoles.UserId INNER JOIN " +
                "aspnet_Roles ON aspnet_Roles.RoleId = aspnet_UsersInRoles.RoleId FULL JOIN " +
                "Seller ON Seller.accId = Account.accId " +
                "ORDER BY " +
                "CASE @filter " +
                "WHEN 'accId' THEN Account.accId " +
                "WHEN 'username' THEN Account.username " +
                "WHEN 'email' THEN Account.email " +
                "WHEN 'RoleName' THEN aspnet_Roles.RoleName " +
                "END";

            //execute
            try
            {
                con.Open();

                //get suitable query
                if(Request.QueryString["filter"] != null)
                {
                    cmdRetrieve = new SqlCommand(queryFilter, con);
                    string someStr = Request.QueryString["filter"].ToString();
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
                            row["accStatus"] = reader["accStatus"];
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
                            }
                        }
                        
                    }
                }
                dt.AcceptChanges();
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

        /*Toggle function*/
        List<CheckBox> checkBoxContainer()
        {
            List<CheckBox> chkBoxContainer = new List<CheckBox>();
            CheckBox chk = new CheckBox();

            foreach (GridViewRow row in userGrid.Rows)
            {
                Control control = row.FindControl("CheckBox1");
                Control controlStatus = row.FindControl("lblStatusContent");
                if (control is CheckBox)
                {
                    chk = control as CheckBox;
                }
                if (controlStatus is Label)
                {
                    Label ctrlStatus = controlStatus as Label;
                    if (ctrlStatus.Text.Trim().Equals("Flagged"))
                    {
                        chkBoxContainer.Add(chk);
                    }
                }
            }

            return chkBoxContainer;
        }

        void updateAccStatus(string accId, string status)
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;
            //string getSellerId = "SELECT sellerId FROM Seller INNER JOIN Account ON Account.accId = Seller.accId WHERE Account.username = @username AND Account.email = @email";
            string query = "UPDATE Account SET accStatus = @status WHERE accId = @accId"; //

            //execute query
            try
            {
                //execute query
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@status", status);
                cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException exDB)
            {
                if (lblNoData != null)
                {
                    lblNoData.Visible = true;
                    lblNoData.Text = exDB.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        void displayToggle(Control sender, int rowNo)
        {
            var chkBox = (CheckBox)sender;

            //add new class
            string sliderFocus = "slider-focus"; //just some decoration purpose in shadow
            string sliderChecked = "slider-checked"; //when the slider is checked
            //string sliderBeforeChecked = "slider-before-checked";

            //get the span element
            //var control = chkBox.Parent.Controls.OfType<HtmlGenericControl>().LastOrDefault(); //the flag is always place at the end of the column
            var control = userGrid.Rows[rowNo].FindControl("btnToggleRound") as HtmlGenericControl;

            string classes = ((HtmlGenericControl)control).Attributes["class"];
            string dataStyle = ((HtmlGenericControl)control).Attributes["style"];
            //get status
            //GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            //Label statusControl = (Label)grvRow.FindControl("lblStatusContent");
            Label statusControl = userGrid.Rows[rowNo].FindControl("lblStatusContent") as Label;
            string status = statusControl.Text;

            //change toggle
            if (status.Trim().Equals("Flagged"))
            {
                //toggle round button move to right
                //string script = "<script type=\"text/javascript\">" +
                //    "document.querySelector('.slider').style.setProperty('--transformValue', '26px');" +
                //    "</script>";

                //RegisterStartupScript vs RegisterClientScriptBlock
                //one is run before end of form tag, another one is after start of form tag
                //run the script to make css transformation (toggle go left or right)
                //ClientScript.RegisterStartupScript(this.GetType(), "transform", script);
                chkBox.Checked = true;
                dataStyle = dataStyle.Replace("--transformValue:0px;", "--transformValue:26px;");
                classes += (classes == "") ? sliderFocus : " " + sliderFocus; //add into the class string
                classes += (classes == "") ? sliderChecked : " " + sliderChecked;
            }
            else
            {
                chkBox.Checked = false;
                dataStyle = dataStyle.Replace("--transformValue:26px;", "--transformValue:0px;");
                classes = classes.Replace(sliderFocus, "");
                classes = classes.Replace(sliderChecked, "");
            }
            control.Attributes.Add("class", classes); //add the class attribute back to the control
            control.Attributes.Add("style", dataStyle);
        }

        void updateStatusText(Object sender, string complaintStatus, int rowNo)
        {
            //var chkBox = (CheckBox)sender;
            //GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            //Label lblReportStatus = (Label)grvRow.FindControl("lblStatusContent");

            Label lblReportStatus = userGrid.Rows[rowNo].FindControl("lblStatusContent") as Label;
            lblReportStatus.Text = complaintStatus;
        }

        //the main method to load the data (status) and toggle
        void updateAccStatusUI(Object sender, int rowNo)
        {
            var chkBox = (CheckBox)sender;
            string accId = "";
            string accStatus = "";

            //get complaint id
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            Label accIdControl = (Label)grvRow.FindControl("accId");
            accId = accIdControl.Text;

            try
            {
                if (chkBox.Checked == true)
                {
                    accStatus = "Flagged";
                }
                else
                {
                    accStatus = "Unflagged";
                }
                //update into database
                updateAccStatus(accId, accStatus);

                //update the label status text
                updateStatusText(sender, accStatus, rowNo);

                //update the toggle (at left or right)
                displayToggle((Control)sender, rowNo);
            }
            catch (Exception ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var chkBox = (CheckBox)sender;
            var hfRowNo = chkBox.NamingContainer.FindControl("hfRowNo") as HiddenField;
            updateAccStatusUI(sender,Convert.ToInt32(hfRowNo.Value));
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