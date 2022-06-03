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
            Control ctrl_top;
            Control ctrl_category;
            string path = Request.Url.LocalPath.ToString();
            //TopCategory.InnerText = path;

            

            //side menu user control
            if (path.Contains("General") == false)
            {
                //top menu (login part)
                ctrl_top = Page.LoadControl("/TopMenu.ascx");
                TopLoginMenu.Controls.Clear();
                TopLoginMenu.Controls.Add(ctrl_top);

                //side menu user control
                ctrl = Page.LoadControl("/SideMenuUser.ascx"); //if user
                if (path.Contains("Admin") == true)
                {
                    ctrl = Page.LoadControl("/SideMenuAdmin.ascx");
                }
                else if (path.Contains("Seller") == true)
                {
                    ctrl = Page.LoadControl("/SideMenuSeller.ascx");
                }
                else if (path.Contains("User") == true)
                {
                    //top menu category
                    ctrl_category = Page.LoadControl("/TopMenuCategory.ascx");
                    TopCategory.Controls.Clear();
                    TopCategory.Controls.Add(ctrl_category);
                }
                SideMenu.Controls.Clear();
                SideMenu.Controls.Add(ctrl);
            }
            else if (path.Contains("Product.aspx") == true)
            {
                //top menu category
                ctrl_category = Page.LoadControl("/TopMenuCategory.ascx");
                TopCategory.Controls.Clear();
                TopCategory.Controls.Add(ctrl_category);

                //top menu (login part)
                ctrl_top = Page.LoadControl("/TopMenu.ascx");
                TopLoginMenu.Controls.Clear();
                TopLoginMenu.Controls.Add(ctrl_top);

                //side menu user control
                ctrl = Page.LoadControl("/SideMenuFilter.ascx");
                SideMenu.Controls.Clear();
                SideMenu.Controls.Add(ctrl);
            }
            else
            {
                //top menu category
                ctrl_category = Page.LoadControl("/TopMenuCategory.ascx");
                TopCategory.Controls.Clear();
                TopCategory.Controls.Add(ctrl_category);

                //top menu (login part)
                ctrl_top = Page.LoadControl("/TopMenuGeneral.ascx");
                TopLoginMenu.Controls.Clear();
                TopLoginMenu.Controls.Add(ctrl_top);
            }
        }
    }   
}