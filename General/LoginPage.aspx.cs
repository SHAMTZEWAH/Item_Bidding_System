using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            TextBox username = (TextBox)Login1.FindControl("UserName");

            if (Login1.RememberMeSet)
            {
                FormsAuthentication.SetAuthCookie(username.Text, true);
                FormsAuthentication.RedirectFromLoginPage(username.Text, true);
            }
            else //when the user does not choose remember me 
            {
                var authTicket = new FormsAuthenticationTicket(1, 
                    username.Text, 
                    DateTime.Now, 
                    DateTime.Now.AddDays(1), 
                    true,  
                    "Application Specific data for this user.");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                    encryptedTicket)
                {
                    HttpOnly = true,
                    Secure = FormsAuthentication.RequireSSL,
                    Path = FormsAuthentication.FormsCookiePath,
                    Domain = FormsAuthentication.CookieDomain,
                    Expires = authTicket.Expiration
                };
                Response.Cookies.Set(cookie);
                // Do not use FormsAuthentication.SetAuthCookie or RedirectFromLoginPage
                // if you create own FormsAuthenticationTicket.

                //direct to the url
                if (Request.QueryString["ReturnURL"] != null)
                {
                    Response.Redirect(Request.QueryString["ReturnURL"]);
                }
                else
                {
                    Response.Redirect("/General/Home.aspx");
                }
            } 
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //TextBox username = (TextBox)Login1.FindControl("UserName");
            //TextBox password = (TextBox)Login1.FindControl("Password");

            //if (Membership.ValidateUser(username.Text, password.Text))
            //    FormsAuthentication.RedirectFromLoginPage(username.Text, Request.Cookies["loginToken"]!=null);
            //else
            //    lblError.Text = "Login failed. Please check your user name and password and try again.";
        }

        protected void Login1_LoginError(object sender, EventArgs e)
        {
            Login1.FailureText = "Your login attempt was not successful. <br /> Please try again.";
        }
    }
}