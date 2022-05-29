using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System
{
    public partial class ItemBidding : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control ctrl;
            string path = Request.Url.LocalPath.ToString();
            //TopCategory.InnerText = path;

            //side menu user control
            if (path.Contains("General") == false)
            {
                ctrl = Page.LoadControl("/SideMenuUser.ascx"); //if user
                if (path.Contains("Admin") == true)
                {
                    ctrl = Page.LoadControl("/SideMenuAdmin.ascx");
                }
                else if (path.Contains("Seller") == true)
                {
                    ctrl = Page.LoadControl("/SideMenuSeller.ascx");
                }
                TopCategory.Controls.Clear();
                TopCategory.Controls.Add(ctrl);
            }

        }
    }   
}