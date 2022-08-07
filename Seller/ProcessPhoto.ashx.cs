using System;
using System.Collections.Generic;
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
            if(context.Request.QueryString["action"] == "create") //retreive from datatable
            {

            }
            else if(context.Request.QueryString["action"] == "edit") //retrieve from database
            {

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