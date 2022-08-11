using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Item_Bidding_System.Seller
{
    /// <summary>
    /// Summary description for ProcessPhoto (Temporarily not used)
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
            string queryAuctionProduct = "SELECT productPhoto FROM TempPhoto WHERE productPhoto IS NOT NULL";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(queryAuctionProduct, con);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            if(reader["productPhoto"] != null)
                            {
                                context.Response.BinaryWrite((byte[])reader["productPhoto"]);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            //context.Response.BinaryWrite(new byte[0]);
                            //context.Response.BinaryWrite();
                            //HttpContext.Current.ApplicationInstance.CompleteRequest();
                            continue;
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

            //if (context.Request.QueryString["action"] == "create") //retreive from datatable
            //{

            //}
            //else if(context.Request.QueryString["action"] == "edit") //retrieve from database
            //{

            //}

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