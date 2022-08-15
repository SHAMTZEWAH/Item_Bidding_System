using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Admin
{
    public partial class ManageComplaint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Manually register the event-handling method for the   
            // CheckedChanged event of the CheckBox control.
            List<CheckBox> checkBox1;

            if (!IsPostBack)
            {
                loadComplaintDetails();
                checkBox1 = checkBoxContainer();
                //retrieve all the checkbox which is checked
                foreach (CheckBox checkbox in checkBox1)
                {
                    displayToggle(checkbox);
                }
                
            }
            //checkBox1.CheckedChanged += new EventHandler(this.CheckBox1_CheckedChanged);
        }

        List<CheckBox> checkBoxContainer()
        {
            List<CheckBox> chkBoxContainer = new List<CheckBox>();
            CheckBox chk = new CheckBox();

            foreach (GridViewRow row in userGrid.Rows)
            {
                for(int i=0; i<row.Cells.Count; i++)
                {
                    Control control = row.Cells[i].FindControl("CheckBox1");
                    Control controlStatus = row.Cells[i].FindControl("lblStatusContent");
                    if (control is CheckBox)
                    {
                        chk = control as CheckBox;
                    }
                    if(controlStatus is Label)
                    {
                        Label ctrlStatus = controlStatus as Label;
                        if (ctrlStatus.Text == "Resolved")
                        {
                            chkBoxContainer.Add(chk);
                            break;
                        }
                    } 
                }
            }

            return chkBoxContainer;
        }

        void loadComplaintDetails()
        {
            //string sellerId = "";
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;
            //string getSellerId = "SELECT sellerId FROM Seller INNER JOIN Account ON Account.accId = Seller.accId WHERE Account.username = @username AND Account.email = @email";
            string query = "SELECT ComplaintReport.complaintId, ComplaintReport.complaintTitle, ComplaintReport.complaintDateTime, ComplaintReport.description, ProductDetails.productName, Account.username, ComplaintReport.reportStatus " +
                "FROM ComplaintReport INNER JOIN " +
                "Product ON Product.productId = ComplaintReport.productId INNER JOIN " +
                "ProductDetails ON ProductDetails.productDetailsId = Product.productDetailsId INNER JOIN " +
                "Account ON Account.accId = ComplaintReport.accId INNER JOIN " +
                "SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN " +
                "Seller ON Seller.sellerId = SubStore.sellerId "; //
            string filterDate = "ORDER BY ComplaintReport.complaintDateTime DESC";
            string filterTitle = "ORDER BY ComplaintReport.complaintTitle";
            string filterProductName = "ORDER BY ProductDetails.productName";
            string filterUsername = "ORDER BY Account.username";

            //execute query
            try
            {
                con.Open();
                //get seller id
                //cmdRetrieve = new SqlCommand(getSellerId, con);
                //cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                //cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                //sellerId = (string)cmdRetrieve.ExecuteScalar();
                if(Request.QueryString["filter"] == "complaintDateTime")
                {
                    query = query + filterDate;
                    RadioButtonList1.SelectedValue = "complaintDateTime";
                }
                else if (Request.QueryString["filter"] == "complaintTitle")
                {
                    query = query + filterTitle;
                    RadioButtonList1.SelectedValue = "complaintTitle";
                }
                else if (Request.QueryString["filter"] == "productName")
                {
                    query = query + filterProductName;
                    RadioButtonList1.SelectedValue = "productName";
                }
                else if (Request.QueryString["filter"] == "username")
                {
                    query = query + filterUsername;
                    RadioButtonList1.SelectedValue = "username";
                }

                //execute data
                cmdRetrieve = new SqlCommand(query, con);
                userGrid.DataSource = cmdRetrieve.ExecuteReader();
                userGrid.DataBind();
            }
            catch (NullReferenceException exDB)
            {
                Label lblFooter = (Label)userGrid.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = exDB.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //when user choose to filter the product
        protected void RadioButtonList1_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Response.Redirect("ManageComplaint.aspx?filter="+ RadioButtonList1.SelectedValue);
        }

        void updateComplaintStatus(string complaintId, string status)
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;
            //string getSellerId = "SELECT sellerId FROM Seller INNER JOIN Account ON Account.accId = Seller.accId WHERE Account.username = @username AND Account.email = @email";
            string query = "UPDATE ComplaintReport SET reportStatus = @status WHERE complaintId = @complaintId"; //

            //execute query
            try
            {
                //execute query
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@status", status);
                cmdRetrieve.Parameters.AddWithValue("@complaintId", complaintId);
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

        void displayToggle(Control sender)
        {
            var chkBox = (CheckBox)sender;

            //add new class
            string sliderFocus = "slider-focus"; //just some decoration purpose in shadow
            string sliderChecked = "slider-checked"; //when the slider is checked
            //string sliderBeforeChecked = "slider-before-checked";

            //get the span element
            var control = chkBox.Parent.Controls.OfType<HtmlGenericControl>().LastOrDefault(); //the flag is always place at the end of the column

            string classes = ((HtmlGenericControl)control).Attributes["class"];

            //get status
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            Label statusControl = (Label)grvRow.FindControl("lblStatusContent");
            string status = statusControl.Text;

            //change toggle
            if (status == "Resolved")
            {
                //toggle round button move to right
                string script = "<script type=\"text/javascript\">" +
                    "document.querySelector('.slider').style.setProperty('--transformValue', '26px');" +
                    "</script>";

                //RegisterStartupScript vs RegisterClientScriptBlock
                //one is run before end of form tag, another one is after start of form tag
                //run the script to make css transformation (toggle go left or right)
                ClientScript.RegisterStartupScript(this.GetType(), "transform", script);
                chkBox.Checked = true;
                classes += (classes == "") ? sliderFocus : " " + sliderFocus; //add into the class string
                classes += (classes == "") ? sliderChecked : " " + sliderChecked;
            }
            else
            {
                chkBox.Checked = false;
                classes = classes.Replace(sliderFocus, "");
                classes = classes.Replace(sliderChecked, "");
            }
            control.Attributes.Add("class", classes); //add the class attribute back to the control
        }

        void updateStatusText(Object sender, string complaintStatus)
        {
            var chkBox = (CheckBox)sender;
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;

            Label lblReportStatus = (Label)grvRow.FindControl("lblStatusContent");
            lblReportStatus.Text = complaintStatus;
        }

        //the main method to load the data (status) and toggle
        void updateComplaintStatusUI(Object sender)
        {
            var chkBox = (CheckBox)sender;
            string complaintId = "";
            string complaintStatus = "";

            //get complaint id
            GridViewRow grvRow = (GridViewRow)chkBox.NamingContainer;
            Label complaintIdControl = (Label)grvRow.FindControl("lblComplaintIdContent");
            complaintId = complaintIdControl.Text;

            try
            {
                if (chkBox.Checked == true)
                {
                    complaintStatus = "Resolved";
                }
                else
                {
                    complaintStatus = "Pending";
                }
                //update into database
                updateComplaintStatus(complaintId, complaintStatus);

                //update the label status text
                updateStatusText(sender, complaintStatus);

                //update the toggle (at left or right)
                displayToggle((Control)sender);
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
            //CheckBox checkbox = (CheckBox)sender;
            //if (checkbox.Checked)
            //{
            //    ViewState[checkbox.UniqueID] = true;
            //}
            //else
            //{
            //    ViewState.Remove(checkbox.UniqueID);
            //}
            updateComplaintStatusUI(sender);
        }

        /*Useless function*/
        protected void userGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void userGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void userGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void userGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void userGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            // Checking the RowType of the Row
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ////get complaint status
            //    //var lblComplaintStatus = (Label)e.Row.FindControl("lblStatusContent");
            //    //string status = lblComplaintStatus.Text;

            //    //get toggle status
            //    //var chkBoxStatus = (CheckBox)e.Row.FindControl("CheckBox1");
            //    //displayToggle(chkBoxStatus);
            //    //Session["chkBox" + e.Row.RowIndex] = chkBoxStatus.Checked;
            //}
        }

        protected void chkAccAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_Load(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_DataBinding(object sender, EventArgs e)
        {
            //CheckBox checkbox = (CheckBox)sender;
            //checkbox.Checked = ViewState[checkbox.UniqueID] != null;
        }

        
    }
}