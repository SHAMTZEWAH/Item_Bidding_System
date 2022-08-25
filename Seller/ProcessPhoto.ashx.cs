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
            string id = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryAuctionProduct = "SELECT productPhoto FROM TempPhoto WHERE productPhoto IS NOT NULL AND id = @id";
            string queryEditProduct = "SELECT productPhoto FROM ProductPhoto WHERE productPhoto IS NOT NULL AND productId = @productId AND productPhotoId = @id";
            try
            {
                con.Open();
                if(context.Request.QueryString["prodId"] != null) //determine whether it is from edit product page (as productId has been available in db)
                {
                    var prodId = context.Request.QueryString["prodId"];
                    cmdRetrieve = new SqlCommand(queryEditProduct, con);
                    cmdRetrieve.Parameters.AddWithValue("@productId", prodId);
                }
                else
                {
                    cmdRetrieve = new SqlCommand(queryAuctionProduct, con);
                }
                if (context.Request.QueryString["photoId"] != null)
                {
                    id = context.Request.QueryString["photoId"];
                    cmdRetrieve.Parameters.AddWithValue("@id", id);
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
                                //context.Response.BinaryWrite(new byte[0]);
                                //context.Response.BinaryWrite();
                                //HttpContext.Current.ApplicationInstance.CompleteRequest();
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