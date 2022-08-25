using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Item_Bidding_System.Admin
{
    /// <summary>
    /// Summary description for ProcessPhoto
    /// </summary>
    public class ProcessPhoto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryAccPhoto = "SELECT productPhoto FROM ProductPhoto WHERE productPhoto = 'Main' AND productId = @productId";
            try
            {
                con.Open();
                if (context.Request.QueryString["prodId"] != null)
                {
                    var prodId = context.Request.QueryString["prodId"];

                    cmdRetrieve = new SqlCommand(queryAccPhoto, con);
                    cmdRetrieve.Parameters.AddWithValue("@productId", prodId);
                    SqlDataReader reader = cmdRetrieve.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                if (reader["productPhoto"] != null)
                                {
                                    context.Response.BinaryWrite((byte[])reader["productPhoto"]);
                                }

                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Redirect("/ErrorPage.aspx?message=" + ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}