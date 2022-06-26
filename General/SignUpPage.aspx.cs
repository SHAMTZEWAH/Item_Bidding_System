using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class SignUpPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            //send email to the user
            sendEmail("Tzewah1234@gmail.com", "Create Account Confirmation", "You have created your account successfully. Please click on the link here for confirmation! ");

            TextBox username = (TextBox)CreateUserWizard1.FindControl("UserName");
            TextBox password = (TextBox)CreateUserWizard1.FindControl("Password");

            //create a token in cookie
            HttpCookie loginCookie = new HttpCookie("loginToken");
            loginCookie.Value = "Login Success";
            loginCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(loginCookie);

            loginCookie = new HttpCookie("username");
            loginCookie.Value = username.Text;
            loginCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(loginCookie);


            TextBox email = (TextBox)CreateUserWizard1.FindControl("Email");
            TextBox phoneNo = (TextBox)CreateUserWizard1.FindControl("PhoneNo");
            createUserAcc(username.Text, email.Text, phoneNo.Text);

            //set cookie authentication
            //FormsAuthentication.SetAuthCookie(username.Text, true);
            //FormsAuthentication.RedirectFromLoginPage(username.Text, true);
        }

        bool checkEmailExistance(string email)
        {
            bool exist = false;
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(Account.accId) FROM Account WHERE Account.email = @email";

            //execute
            int count = 0;
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();
                if (count > 0)
                {
                    exist = true;
                }

            }
            catch (NullReferenceException ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return exist;
        }
        int getAccCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(Account.accId) FROM Account";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();
                
            }
            catch (NullReferenceException ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        Guid getUserId(string email)
        {
            Guid userId = new Guid();

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT aspnet_Membership.UserId FROM aspnet_Membership WHERE aspnet_Membership.Email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@email", email);
                userId = (Guid)cmdRetrieve.ExecuteScalar();

            }
            catch (NullReferenceException ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return userId;
        }

        void createUserAcc(string username, string email, string phoneNo)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO Account(accId, username, email, phoneNo, createDateTime, accPhotoURL, accStatus, accBalance, userId, addressId) " +
                "VALUES(@id, @name, @email, @phone, CONVERT(DATETIME, GETDATE(), 120), @photo, @status, @balance, @userId, @addressId)";
            
            //execute
            try
            {
                //get acc id
                string accId = "";
                int accCount = getAccCount();
                if(accCount + 1 > 9)
                {
                    accId = "a_0" + (accCount+1);
                }
                else
                {
                    accId = "a_00" + (accCount + 1);
                }

                //get userId
                Guid userId;
                userId = getUserId(email);

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@id", accId);
                cmdRetrieve.Parameters.AddWithValue("@name", username);
                cmdRetrieve.Parameters.AddWithValue("@email", email);
                cmdRetrieve.Parameters.AddWithValue("@phone", phoneNo);
                cmdRetrieve.Parameters.AddWithValue("@photo", "");
                cmdRetrieve.Parameters.AddWithValue("@status", "Unconfirm");
                cmdRetrieve.Parameters.AddWithValue("@balance", 0.00);
                cmdRetrieve.Parameters.AddWithValue("@userId", userId);
                cmdRetrieve.Parameters.AddWithValue("@addressId", "");
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                string alertMsg = "[!] The action is unable to complete: "+ ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        void sendEmail(String email, String subject, String body)
        {
            //Send Mail
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("eauctionforeveryone@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = "<div>" + body + "</div>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential()
                        {
                            UserName = "eauctionforeveryone@gmail.com",
                            Password = "Eauction1234."
                        };
                        smtp.EnableSsl = true;
                        smtp.Send(mail);

                    }
                }
            }
            catch (Exception ex)
            {
                string alertMsg = "[!] Error in sending email. Please try again.";
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            sendEmail("Tzewah1234@gmail.com", "Create Account Confirmation", "You have created your account successfully. Please click on the link here for confirmation! ");

        }

        protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
        {
            var control = (Control)sender;
            TextBox txtEmail = (TextBox)control.NamingContainer.FindControl("Email");
            bool emailExist = checkEmailExistance(txtEmail.Text);
            if(emailExist == true)
            {
                CreateUserWizard1.InstructionText = "Duplicate email address. Please use another email.";
                e.Cancel = true;
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }
    }
}