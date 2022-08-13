using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.User
{
    public partial class AccountManagement : System.Web.UI.Page
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
            string query = "SELECT Account.accId, Account.username, Account.email, Account.phoneNo, Account.gender, Account.dateOfBirth, Account.accPhotoURL, Account.accPhoto " +
                "FROM Account WHERE Account.username = @name AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtUsername.Text = reader["username"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        hfRowAccId.Value = (string)reader["accId"];
                        if (reader["phoneNo"] != DBNull.Value)
                        {
                            txtPhoneNo.Text = (string)reader["phoneNo"];
                        }
                        if (reader["gender"] != DBNull.Value)
                        {
                            radioGender.SelectedValue = (string)reader["gender"];
                        }
                        if (reader["dateOfBirth"] != DBNull.Value)
                        {
                            Calendar1.SelectedDate = (DateTime)reader["dateOfBirth"];
                        }
                        if(reader["accPhotoURL"] == DBNull.Value && reader["accPhoto"] == DBNull.Value)
                        {
                            continue;
                        }
                        else if (reader["accPhotoURL"] != DBNull.Value)
                        {
                            Image1.ImageUrl = (string)reader["accPhotoURL"];
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        void insertData()
        {
            byte[] bytes = { };

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            //get user id
            string queryGetUserId = "SELECT aspnet_Users.UserId FROM aspnet_Users WHERE aspnet_Users.Username = @name AND aspnet_Users.Email = @emailC";
            //Account table
            string query = "UPDATE Account SET Account.username = @name, Account.email = @email, Account.phoneNo = @phoneNo, Account.accPhotoURL=@photoURL, Account.accPhoto=@photo, Account.gender = @gender, Account.dateOfBirth = @dateBirth WHERE Account.username = @name2 AND Account.userId = @userId";
            //Membership table
            string queryMember = "UPDATE aspnet_Membership SET Email = @email, LoweredEmail = @lowerEmail WHERE UserId = @userId";
            //Users table
            string queryUsers = "UPDATE aspnet_Users SET aspnet_Users.Username = @name, aspnet_Users.LoweredUserName = @lowerName WHERE aspnet_Users.UserId = @userId";
            
            //execute
            try
            {
                con.Open();
                SqlCommand cmdRetrieveUserId = new SqlCommand(queryGetUserId, con);
                cmdRetrieveUserId.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdRetrieveUserId.Parameters.AddWithValue("@emailC", Membership.GetUser().Email);
                
                //Account table
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", txtUsername.Text);
                cmdRetrieve.Parameters.AddWithValue("@email", txtEmail.Text);
                cmdRetrieve.Parameters.AddWithValue("@phoneNo", txtPhoneNo.Text);
                cmdRetrieve.Parameters.AddWithValue("@photoURL", txtImageUrl.Text);

                //get the posted file 
                if (txtUploadPhoto.HasFiles)
                {
                    foreach (HttpPostedFile postedFile in txtUploadPhoto.PostedFiles)
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string contentType = postedFile.ContentType;
                        using (Stream fs = postedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                //serialise the photo
                                bytes = br.ReadBytes((Int32)fs.Length);

                            }
                        }
                    }
                }    

                cmdRetrieve.Parameters.AddWithValue("@photo", bytes);
                cmdRetrieve.Parameters.AddWithValue("@gender", radioGender.SelectedValue);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                cmdRetrieve.Parameters.AddWithValue("@dateBirth", Calendar1.SelectedDate.ToShortDateString());
                cmdRetrieve.Parameters.AddWithValue("@name2", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdRetrieveUserId.ExecuteScalar());
                cmdRetrieve.ExecuteNonQuery();

                //Membership table
                cmdRetrieve = new SqlCommand(queryMember, con);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                cmdRetrieve.Parameters.AddWithValue("@lowerEmail", Membership.GetUser().Email.ToLower());
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdRetrieveUserId.ExecuteScalar());

                //Users table
                cmdRetrieve = new SqlCommand(queryUsers, con);
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdRetrieveUserId.ExecuteScalar());
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            insertData();
        }

        protected void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmitPhoto_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitURL_Click(object sender, EventArgs e)
        {

        }
    }
}