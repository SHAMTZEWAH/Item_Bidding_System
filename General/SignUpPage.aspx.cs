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

        int getPointCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(pointId) FROM Voucher";

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

        void createUserAcc(string username, string email, string phoneNo)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO Account(accId, username, email, phoneNo, createDateTime, accStatus, accBalance, userId) " +
                "VALUES(@id, @name, @email, @phone, CONVERT(DATETIME, GETDATE(), 120), @status, @balance, @userId)";
            
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
                Session["accId"] = accId;

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@id", accId);
                cmdRetrieve.Parameters.AddWithValue("@name", username);
                cmdRetrieve.Parameters.AddWithValue("@email", email);
                cmdRetrieve.Parameters.AddWithValue("@phone", phoneNo);
                cmdRetrieve.Parameters.AddWithValue("@status", "Unconfirm");
                cmdRetrieve.Parameters.AddWithValue("@balance", 0.00);
                cmdRetrieve.Parameters.AddWithValue("@userId", userId);
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

        void createVoucherStake()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO Voucher(pointId, point, accId, pointStake, pointReward) " +
                "VALUES(@pointId, 0, @accId, 0, 0)";

            //execute
            try
            {
                string accId = "";
                int pointId = 0;

                //get acc id
                if (String.IsNullOrEmpty(Session["accId"].ToString()))
                {
                    accId = Session["accId"].ToString();
                }

                //get point id
                pointId = getPointCount();


                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@pointId", pointId);
                cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                cmdRetrieve.ExecuteNonQuery();
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



        protected void CreateUserWizard1_CreatedUser1(object sender, EventArgs e)
        {
            //send email to the user
            sendEmail("Tzewah1234@gmail.com", "Create Account Confirmation", "You have created your account successfully. Please click on the link here for confirmation! ");

            //get username and password text from the user wizard
            CreateUserWizardStep step = (CreateUserWizardStep)(sender as CreateUserWizard).FindControl("CreateUserWizardStep1");
            TextBox username = (TextBox)step.ContentTemplateContainer.FindControl("UserName");
            //TextBox password = (TextBox)step.ContentTemplateContainer.FindControl("Password");

            //create a token in cookie
            HttpCookie loginCookie = new HttpCookie("loginToken");
            loginCookie.Value = "Login Success";
            loginCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(loginCookie);

            loginCookie = new HttpCookie("username");
            loginCookie.Value = username.Text;
            loginCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(loginCookie);


            TextBox email = (TextBox)step.ContentTemplateContainer.FindControl("Email");
            TextBox phoneNo = (TextBox)step.ContentTemplateContainer.FindControl("PhoneNo");
            createUserAcc(username.Text, email.Text, phoneNo.Text);

            string wizardUsername = (sender as CreateUserWizard).UserName;
            Roles.AddUserToRole(wizardUsername, "Customer");

            //create Voucher staking data
            createVoucherStake();

            //string alertMsg = "[!] Error in sending email. Please try again.";
            //string script = "<script type=\"text/javascript\">alert('" + "created user" + "');</script>";
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);

            //set cookie authentication
            //FormsAuthentication.SetAuthCookie(username.Text, true);
            //FormsAuthentication.RedirectFromLoginPage(username.Text, true);
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //check email whether is exist alr
            try
            {
                string username = Membership.GetUserNameByEmail(args.Value.ToString());
                if (username.Equals(null) == true) //if username is not empty, means email exist, not valid email
                {
                    args.IsValid = false; //trigger validator
                }
                else //username is empty, means email doesnt exist, valid email
                {
                    args.IsValid = true; //does not trigger validator
                }
            }
            catch(Exception ex)
            {
                args.IsValid = true;
            }
            
        }

        protected void Email_TextChanged(object sender, EventArgs e)
        {

            
            
        }

        bool checkEmailExistance(string email)
        {
            bool exist = false;
            //execute

            try
            {
                //Get username by email
                string username = Membership.GetUserNameByEmail(email);

                //check for email existance
                if (username != String.Empty)
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

            return exist;
        }

        protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
        {

        }
    }
}