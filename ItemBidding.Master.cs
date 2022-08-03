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
            string path = Request.Url.LocalPath.ToString(); //get current path of the page locate at (~/General/Home.aspx)
            //TopCategory.InnerText = path;

            
            if(path.Contains("General") != false && path.Contains("Product.aspx") != true)
            {
                //sidemenu width = 0
                
            }

            string ctrlPathMenu = PageControl_TopLogin(path);
            if (ctrlPathMenu != string.Empty)
            {
                ctrl_top = Page.LoadControl(ctrlPathMenu); //load the top menu user control and pass to the ctrl_top 
                TopLoginMenu.Controls.Clear();
                TopLoginMenu.Controls.Add(ctrl_top); //ctrl_top user control pass back to the row display part
            }

            string ctrlPathCategory = PageControl_TopCategory(path);
            if(ctrlPathCategory != string.Empty)
            {
                ctrl_category = Page.LoadControl(ctrlPathCategory);
                TopCategory.Controls.Clear();
                TopCategory.Controls.Add(ctrl_category);

            }

            string ctrlPathSideMenu = PageControl_SideMenu(path);
            if(ctrlPathSideMenu != string.Empty)
            {
                ctrl = Page.LoadControl(ctrlPathSideMenu);
                SideMenu.Controls.Clear();
                SideMenu.Controls.Add(ctrl);
            }

            //get the user control
            //pass indexChangedhandler to it
            //if (ctrlPathSideMenu.Contains("Filter") == true)
            //{
            //    UserControl filterControl = (UserControl)Page.LoadControl(ctrlPathSideMenu);
            //    if (filterControl is SideMenuFilter)
            //    {
            //        (filterControl as SideMenuFilter).IndexChangedHandler += new SideMenuFilter.OnIndexChanged(ctrl_IndexChangedHandler);
            //    }
            //}
        }

        string PageControl_TopLogin(string path)
        {
            string ctrlPath = "";
            string[] roles = Roles.GetRolesForUser();

            if (path.Contains("General") == false)
            {
                ctrlPath = "/TopMenu.ascx";
            }
            else if (path.Contains("Product.aspx") == true)
            {
                ctrlPath = "/TopMenu.ascx";
            }
            else 
            {
                //if the current user is logged in
                if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ctrlPath = "/TopMenu.ascx";
                }
                else if (roles.Contains("Seller") == true)
                {
                    ctrlPath = "/TopMenuSeller.ascx";
                }
                else
                {
                    ctrlPath = "/TopMenuGeneral.ascx";
                }
            }
            return ctrlPath;
        }

        string PageControl_TopCategory(string path)
        {
            string ctrlPath = "";
            if (path.Contains("General") == false)
            {
                if(path.Contains("User") == true)
                {
                    ctrlPath = "/TopMenuCategory.ascx";
                } 
            }
            else if (path.Contains("Product.aspx") == true)
            {
                ctrlPath = "/TopMenuCategory.ascx";
            }
            else 
            {
                ctrlPath = "/TopMenuCategory.ascx";

                SideMenu.Attributes.CssStyle.Add("display", "none");
                mainContent.Attributes.CssStyle.Add("width", "100%");
            }
            return ctrlPath;
        }

        string PageControl_SideMenu(string path)
        {
            string ctrlPath = "";
            if (path.Contains("General") == false)
            {
                if (path.Contains("Admin") == true)
                {
                    ctrlPath = "/SideMenuAdmin.ascx";
                }
                else if (path.Contains("Seller") == true && path.Contains("Registration") == false)
                {
                    ctrlPath = "/SideMenuSeller.ascx";
                }
                else if (path.Contains("User") == true)
                {
                    ctrlPath = "/SideMenuUser.ascx";
                }
            }
            else if (path.Contains("Product.aspx") == true)
            {
                ctrlPath = "/SideMenuFilter.ascx";
            }
            return ctrlPath;
        }
    }   
}