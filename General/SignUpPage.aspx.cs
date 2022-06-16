using System;
using System.Collections.Generic;
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

            //set cookie authentication
            //FormsAuthentication.SetAuthCookie(username.Text, true);
            //FormsAuthentication.RedirectFromLoginPage(username.Text, true);
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
    }
}