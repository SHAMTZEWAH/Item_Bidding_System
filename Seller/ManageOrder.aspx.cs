using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ManageOrder : System.Web.UI.Page, IPostBackEventHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int subStoreCount = 0;
            if (!IsPostBack)
            {
                
            }
            //subStoreCount = getSubStoreCount()
            //while loop:
            //addBtnControl()

            //Event for create substore button
            btnCreateStore.Attributes["onclick"] = ClientScript.GetPostBackEventReference(this, "clickDiv");
        }

        //Event for create substore button
        protected void btnCreateStore_Click()
        {
            //getSubStoreCount()
            //addBtnControl();
        }

        //Post back event for create substore button
        #region IPostBackEventHandler Members
        public void RaisePostBackEvent(string eventArgument)
        {

            if (!string.IsNullOrEmpty(eventArgument))
            {

                if (eventArgument == "clickDiv")
                {
                    btnCreateStore_Click();
                }
            }
        }

        #endregion

        //Create substore button
        void addBtnControl(int subStoreCount)
        {
            Button btnSubStore = new Button();
            btnSubStore.ID = "btnSubStore";
            btnSubStore.Text = subStoreCount.ToString();
            btnSubStore.Click += (sender, e) =>
            {
                //what you want to do when click
                btnSubStore_Click(sender, e);
            };
            SubStoreCon.Controls.Add(btnSubStore);
        }
        int getSubStoreCount()
        {
            return 0;
        }

        //Each substore event
        void btnSubStore_Click(Object sender, EventArgs e)
        {

        }

    }
}