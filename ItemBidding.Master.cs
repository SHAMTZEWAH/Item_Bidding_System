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

            
            if(path.Contains("General") != false && path.Contains("Product.aspx") != true)
            {
                //sidemenu width = 0
                
            }

            string ctrlPathMenu = PageControl_TopLogin(path);
            if (ctrlPathMenu != string.Empty)
            {
                ctrl_top = Page.LoadControl(ctrlPathMenu);
                TopLoginMenu.Controls.Clear();
                TopLoginMenu.Controls.Add(ctrl_top);
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
        }

        string PageControl_TopLogin(string path)
        {
            string ctrlPath = "";
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
                if ((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    ctrlPath = "/TopMenu.ascx";
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