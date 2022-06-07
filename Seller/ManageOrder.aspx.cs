using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.Seller
{
    public partial class ManageOrder : System.Web.UI.Page
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
        }

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

        void btnSubStore_Click(Object sender, EventArgs e)
        {

        }

        protected void btnCreateStore_Click(object sender, EventArgs e)
        {
            //subStoreCount = getSubStoreCount()
            //pop up for rename substore?
            //addBtnControl()
        }
    }
}