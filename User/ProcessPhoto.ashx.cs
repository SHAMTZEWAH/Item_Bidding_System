using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Item_Bidding_System.User
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
            string queryAccPhoto = "SELECT accPhoto FROM Account WHERE accPhoto IS NOT NULL AND accId = @accId";
            try
            {
                con.Open();
                if (context.Request.QueryString["accId"] != null)
                {
                    var accId = context.Request.QueryString["accId"];

                    cmdRetrieve = new SqlCommand(queryAccPhoto, con);
                    cmdRetrieve.Parameters.AddWithValue("@accId",accId);
                    SqlDataReader reader = cmdRetrieve.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                if (reader["productPhoto"] != null)
                                {
                                    context.Response.BinaryWrite((byte[])reader["accPhoto"]);
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
            catch (NullReferenceException ex)
            {
                context.Response.Redirect("/ErrorPage.aspx");
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
                return true;
            }
        }
    }
}