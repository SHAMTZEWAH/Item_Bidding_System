using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadContent(Request.QueryString["prodName"]!=null?Request.QueryString["prodName"]:"");
            }

        }

        void DataList_errorMsg(Exception ex)
        {
            Label lblFooter = (Label)DataList1.FindControl("lblNoData");
            if (lblFooter != null)
            {
                lblFooter.Visible = true;
                lblFooter.Text = ex.Message.ToString();
            }
        }

        void FormView_errorMsg(Exception ex)
        {
            Label lblFooter = (Label)FormView1.FindControl("lblNoData2");
            if (lblFooter != null)
            {
                lblFooter.Visible = true;
                lblFooter.Text = ex.Message.ToString();
            }
        }

        void alertMsg()
        {
            string alertMsg = "[!] The action is unable to complete: An error is encounted.";
            string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
        }
        private DateTime endTime(Object sender)
        {
            var ticker = (Control)sender;

            //get bid started time
            string time = (DataList1.Items[0].FindControl("lblAddTime") as Label).Text;
            if (!string.IsNullOrEmpty(time))
            {
                Session["endTime"] = time;
            }
            else
            {
                time = Session["endTime"].ToString();
            }

            //convert time to DateTime data type
            DateTime addDateTime = Convert.ToDateTime(time);

            //get bid duration left
            string durationTxt = (DataList1.Items[0].FindControl("lblDuration") as Label).Text;
            if (!string.IsNullOrEmpty(durationTxt))
            {
                Session["bidDurationLeft"] = durationTxt;
            }
            else
            {
                durationTxt = Session["bidDurationLeft"].ToString();
            }

            int duration = Convert.ToInt32(durationTxt);
            DateTime endDateTime = addDateTime.AddDays(duration);
            return endDateTime;
        }

        int countComplaint()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(ComplaintReport.complaintId) FROM ComplaintReport INNER JOIN Product ON ComplaintReport.productId = Product.productId INNER JOIN " +
                "ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId WHERE ProductDetails.productName = @prodName";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodName", Request.QueryString["prodName"].ToString());
                count = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                FormView_errorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        string getAccId()
        {
            string accId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId FROM Account WHERE Account.username = @name";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                accId = (string)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return accId;
        }

        int getBidCount()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(BidTable.bidId) FROM BidTable INNER JOIN " +
                "Product ON BidTable.productId = Product.productId INNER JOIN " +
                "ProductDetails ON ProductDetails.productDetailsId = Product.productDetailsId " +
                "WHERE ProductDetails.productName = @prodName";

            string prodName = "";
            if (Request.QueryString["prodName"].ToString() != null)
            {
                prodName = Request.QueryString["prodName"].ToString();
            }

            int count = 0;
            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodName", prodName);
                count = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return count;
        }

        string getProdId()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT Product.productId FROM Product, ProductDetails WHERE Product.productDetailsId = ProductDetails.productDetailsId AND ProductDetails.productName = @prodName";

            //execute
            string prodId = "";
            string prodName = "";
            try
            {
                con.Open();
                prodName = Request.QueryString["prodName"];
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodName", prodName);
                prodId = (string)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return prodId;
        }

        string getWatchStatus(string prodId, string accId, string typeOfButton)
        {
            string watchStatus = "Favourite";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            //use prodid to get watchProductId
            string query = "SELECT watchStatus FROM WatchlistProduct INNER JOIN Watchlist " +
                "ON WatchlistProduct.watchProductId = Watchlist.watchProductId " +
                "WHERE WatchlistProduct.productId = @prodId AND " +
                "Watchlist.accId = @accId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("prodId", prodId);
                cmdRetrieve.Parameters.AddWithValue("accId", accId);
                SqlDataReader reader = cmdRetrieve.ExecuteReader();

                //Add To Watchlist
                //if Favourite is found, return null
                //if Bidded is found, return null
                //if nothing is found, return "Favourite"

                //Offer Bid
                //if Favourite is found, update status and return null
                //if Bidded is found, return null
                //if nothing is found, return "Bidded"
                if (reader.HasRows)
                {
                    if(typeOfButton == "AddToWatchlist")
                    {
                        watchStatus = "";
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if ((string)reader["watchStatus"]=="Favourite")
                            {
                                query = "UPDATE WatchlistProduct SET watchStatus = 'Bidded' " +
                                    "WHERE WatchlistProduct.watchProductId = " +
                                    "(SELECT WatchlistProduct.watchProductId " +
                                    "FROM Watchlist, WatchlistProduct " +
                                    "WHERE Watchlist.watchProductId = WatchlistProduct.watchProductId AND WatchlistProduct.productId = @prodId AND Watchlist.accId = @accId)";

                                if(con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                    con.Open();
                                } 
                                cmdRetrieve = new SqlCommand(query, con);
                                cmdRetrieve.Parameters.AddWithValue("@watchStatus", "Bidded");
                                cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
                                cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                                cmdRetrieve.ExecuteNonQuery();
                                watchStatus = "";
                            }
                            else if ((string)reader["watchStatus"]=="Bidded")
                            {
                                watchStatus = "";
                            }
                            else
                            {
                                watchStatus = "Bidded";
                            }
                        }
                    }
                }
 
                
            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return watchStatus;
        }

        int getWatchlistProductCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(WatchlistProduct.watchProductId) FROM WatchlistProduct";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();

            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        int getWatchlistCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(Watchlist.watchlistId) FROM Watchlist";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();

            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        string getWPId()
        {
            //get wp id (wp = watchlist product)
            int wpCount = getWatchlistProductCount();
            string wpId = "";

            if (wpCount + 1 > 9)
            {
                wpId = "wp_0" + (wpCount + 1);
            }
            else
            {
                wpId = "wp_00" + (wpCount + 1);
            }

            return wpId;
        }

        string getWId()
        {
            //get w id (w = watchlist)
            int wCount = getWatchlistCount();
            string wId = "";

            if (wCount + 1 > 9)
            {
                wId = "w_0" + (wCount + 1);
            }
            else
            {
                wId = "w_00" + (wCount + 1);
            }
            return wId;
        }

        //The main function

        private void loadContent(string prodName)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand("pr_productDetails", con);
                cmdRetrieve.Parameters.AddWithValue("prodName", prodName);
                cmdRetrieve.CommandType = CommandType.StoredProcedure;
                DataList1.DataSource = cmdRetrieve.ExecuteReader();
                DataList1.DataBind();
            }
            catch (Exception ex)
            {
                DataList_errorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Reference to Data list item
                DataListItem item = e.Item;

                //Reference the controls
                string prodDesc = (item.FindControl("lblProdDesc") as Label).Text;

                //assign to the real product desc
                lblDesc.Text = prodDesc;

                //assign value to the price recommendation label
                Label label = (item.FindControl("lblRecommendPrice") as Label);
                string maxBidText = (item.FindControl("lblCurrentBid") as Label).Text.ToString();
                double maxBid = 0.0;
                if (!string.IsNullOrEmpty(maxBidText))
                {
                    maxBid = Convert.ToDouble(String.Format("{0:0.00}", Decimal.Parse(maxBidText)));
                    priceRecommendation(label, maxBid);
                }
            }
        }

        protected void Timer1_Tick(Object sender, EventArgs args)
        {
            DateTime endDateTime = endTime(sender);
            DateTime currentTime = DateTime.Now;

            TimeSpan span = endDateTime.Subtract(currentTime);
            span = TimeSpan.FromSeconds(span.TotalSeconds - 1);

            //assign value
            var ticker = (Control)sender;
            Label lblTimeLeft = (Label)ticker.NamingContainer.FindControl("lblTimeLeft");
            lblTimeLeft.Text = span.Days + "day(s) " + span.Hours + "h " + span.Minutes + "m " + span.Seconds + "s";
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO ComplaintReport(complaintId, complaintTitle, complaintDateTime, description, productId, reportStatus, accId) " +
                "VALUES(@id, @title, @dateTime, @desc, @prodId, @status, @accId)";

            int getCountComplaint = countComplaint();
            string complaintId = "";

            //get complaint id
            if(getCountComplaint+1 > 9)
            {
                complaintId = "cr_0" + (getCountComplaint+1);
            }
            else
            {
                complaintId = "cr_00" + (getCountComplaint+1);
            }

            //execute
            try
            {
                string title = "";
                string desc = "";
                string prodId = "";
                var button = (Control)sender;

                TextBox txtDesc = (TextBox)button.NamingContainer.FindControl("txtDesc");
                desc = txtDesc.Text.ToString();

                TextBox txtTitle = (TextBox)button.NamingContainer.FindControl("txtTitle");
                title = txtTitle.Text.ToString();

                Label lblProdId = (Label)button.NamingContainer.FindControl("lblProdId");
                prodId = lblProdId.Text.ToString();

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@id",complaintId);
                cmdRetrieve.Parameters.AddWithValue("@title", title);
                cmdRetrieve.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmdRetrieve.Parameters.AddWithValue("@desc", desc);
                cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
                cmdRetrieve.Parameters.AddWithValue("@status", "Pending");
                cmdRetrieve.Parameters.AddWithValue("@accId", getAccId());
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void FormView1_DataBound(Object sender, EventArgs e)
        {
             //Check the RowType to where the Control is placed
             if (FormView1.Row.RowType == DataControlRowType.DataRow)
             {
                    //Just Changed the index of cells based on your requirement
                Label lbl = (Label)FormView1.Row.Cells[0].FindControl("lblAccName");
                try
                {
                    if(Membership.GetUser().UserName != null)
                    {
                        lbl.Text = Membership.GetUser().UserName;
                    }
                }
                catch(NullReferenceException ex)
                {
                    Label lblFooter = (Label)FormView1.FindControl("lblNoData2");
                    if (lblFooter != null)
                    {
                        lblFooter.Visible = true;
                        lblFooter.Text = ex.Message.ToString();
                    }
                }

             }
            
        }

        //label for price recommendation
        private void priceRecommendation(Label label, double maxBid)
        {
            //get current max bid price

            //if-else condition
            if(maxBid >= 0.01 && maxBid < 1)
            {
                maxBid = maxBid + 0.05;
            }
            if(maxBid >= 1 && maxBid < 5)
            {
                maxBid = maxBid + 0.25;
            }
            else if(maxBid >=5 && maxBid < 25)
            {
                maxBid = maxBid + 0.50;
            }
            else if(maxBid >= 25 && maxBid < 100)
            {
                maxBid = maxBid + 1.00;
            }
            else if (maxBid >= 100 & maxBid < 250)
            {
                maxBid = maxBid + 2.50;
            }
            else if (maxBid >= 250 & maxBid < 500)
            {
                maxBid = maxBid + 5.00;
            }
            else if (maxBid >= 500 & maxBid < 1000)
            {
                maxBid = maxBid + 10.00;
            }
            else if (maxBid >= 1000 & maxBid < 2500)
            {
                maxBid = maxBid + 25.00;
            }
            else if (maxBid >= 2500 & maxBid < 5000)
            {
                maxBid = maxBid + 50.00;
            }
            else
            {
                maxBid = maxBid + 100.00;
            }

            //assign to the label
            label.Text = String.Format("{0:0.##}",maxBid.ToString());
        }

        //save offer bid into database

        private void offerBid(string type, string price)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO BidTable(bidId, bidPrice, bidDateTime, bidType, accId, productId) " +
                "VALUES(@id, @price, @dateTime, @type, @accId, @prodId)";
            string query2 = "INSERT INTO WatchlistProduct(watchProductId, addDateTime, watchStatus, productId) " +
                "VALUES(@wpId, CONVERT(DATETIME, GETDATE(), 120), @watchStatus, @prodId)";
            string query3 = "INSERT INTO Watchlist(watchlistId, accId, watchProductId) " +
                "VALUES(@wId, @accId, @wpId)";


            //execute
            try
            {
                //query 1
                //get bid id
                int getCountBidTable = getBidCount();
                string bidId = "";
                if (getCountBidTable + 1 > 9)
                {
                    bidId = "b_0" + (getCountBidTable + 1);
                }
                else
                {
                    bidId = "b_00" + (getCountBidTable + 1);
                }

                //get accId
                string accId = getAccId();
                if (accId == null)
                {
                    alertMsg();
                    Response.Redirect("~/General/LoginPage.aspx");
                }

                //get prodId
                string prodId = getProdId();
                if (prodId == null)
                {
                    alertMsg();
                    Response.Redirect("~/General/Home.aspx");
                }

                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@id", bidId);
                cmdRetrieve.Parameters.AddWithValue("@price", Convert.ToDouble(String.Format("{0:0.##}", price)));
                cmdRetrieve.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmdRetrieve.Parameters.AddWithValue("@type", type);
                cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
                cmdRetrieve.ExecuteNonQuery();

                //query 2
                //get watchstatus to check if the watchlist is available
                string watchStatus = getWatchStatus(prodId, accId, "Bid");
                if (watchStatus != null)
                {
                    //get wpId
                    string wpId = getWPId();
                    cmdRetrieve = new SqlCommand(query2, con);
                    cmdRetrieve.Parameters.AddWithValue("@wpId", wpId);
                    cmdRetrieve.Parameters.AddWithValue("@watchStatus", watchStatus);
                    cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
                    cmdRetrieve.ExecuteNonQuery();

                    //query 3
                    //get wId
                    string wId = getWId();
                    cmdRetrieve = new SqlCommand(query3, con);
                    cmdRetrieve.Parameters.AddWithValue("@wId",wId);
                    cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                    cmdRetrieve.Parameters.AddWithValue("@wpId", wpId);
                    cmdRetrieve.ExecuteNonQuery();
                }
            }
            catch (NullReferenceException ex)
            {
                DataList_errorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //place bid
        protected void btnPlaceBid_Click(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            var txtBid = ctrl.NamingContainer.FindControl("txtBid") as TextBox;
            var currentBid = ctrl.NamingContainer.FindControl("lblRecommendPrice") as Label;
            Label label = (DataList1.Items[0].FindControl("lblRecommendPrice") as Label);
            Label lblMaxBid = (DataList1.Items[0].FindControl("lblCurrentBid") as Label);

            bool match = Regex.IsMatch(txtBid.Text, @"^\d+(?:\.\d{2})?$");

            try
            {
                if (!string.IsNullOrEmpty(txtBid.Text) && Convert.ToDouble(txtBid.Text) >= Convert.ToDouble(currentBid.Text) && match)
                {
                    offerBid("Open", txtBid.Text);

                    (ctrl.NamingContainer.FindControl("lblCurrentBid") as Label).Text = txtBid.Text;
                    txtBid.Text = "";
                    priceRecommendation(label, Convert.ToDouble(String.Format("{0:0.##}",lblMaxBid.Text)));
                }
                else
                {
                    string alertMsg = "[!] Please enter amount of bid more than current bid in digit.";
                    string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
            catch(Exception ex)
            {
                string alertMsg = "[!] Please enter amount of bid more than current bid in digit.";
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            
            
        }

        //make offer
        protected void btnMakeOffer_Click(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            var txtBid = ctrl.NamingContainer.FindControl("txtMakeOffer") as TextBox;
            var currentBid = ctrl.NamingContainer.FindControl("lblRecommendPrice") as Label;
            bool match = Regex.IsMatch(txtBid.Text, @"^\d+(?:\.\d{2})?$");
            Label label = (DataList1.Items[0].FindControl("lblRecommendPrice") as Label);
            Label lblMaxBid = (DataList1.Items[0].FindControl("lblCurrentBid") as Label);

            try
            {
                if (!string.IsNullOrEmpty(txtBid.Text) && Convert.ToDouble(txtBid.Text) >= Convert.ToDouble(currentBid.Text) && match && checkPrivateOffer() <= 0)
                {
                    offerBid("Private", txtBid.Text);
                    txtBid.Text = "";
                    priceRecommendation(label, Convert.ToDouble(String.Format("{0:0.##}", lblMaxBid.Text)));
                }
                else
                {
                    string alertMsg = "[!] Please enter amount of bid more than current bid in digit.";
                    string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
                }
            }
            catch(Exception ex)
            {
                string alertMsg = "[!] Please enter amount of bid more than current bid in digit.";
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }   
        }

        int checkPrivateOffer()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(bidId) FROM BidTable WHERE BidTable.accId = @accId AND BidTable.productId = @prodId" +
                "";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@prodId", getProdId());
                cmdRetrieve.Parameters.AddWithValue("@accId", getAccId());
                count = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (Exception ex)
            {
                DataList_errorMsg(ex);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return count;
        }

        //add to watchlist
        protected void btnAddToWatchlist_Click(object sender, EventArgs e)
        {
            //get prod id
            string prodId = getProdId();

            //get acc id
            string accId = getAccId();

            //check if there is record in the database
            //get watchStatus
            string watchStatus = getWatchStatus(prodId, accId, "AddToWatchlist");

            if(watchStatus != null)
            {
                //get wp id (wp = watchlist product)
                string wpId = getWPId();

                //get w id (w = watchlist)
                string wId = getWId();

                //create connection
                SqlConnection con;
                string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(strCon);

                //prepare command 
                SqlCommand cmdRetrieve;
                string query = "INSERT INTO WatchlistProduct(watchProductId, addDateTime, watchStatus, productId) " +
                    "VALUES(@wpId, CONVERT(DATETIME, GETDATE(), 120), @watchStatus, @prodId)";
                string query2 = "INSERT INTO Watchlist(watchlistId, accId, watchProductId) " +
                    "VALUES(@wId, @accId, @wpId)";

                //execute
                try
                {
                    con.Open();
                    cmdRetrieve = new SqlCommand(query, con);
                    cmdRetrieve.Parameters.AddWithValue("@wpId", wpId);
                    cmdRetrieve.Parameters.AddWithValue("@watchStatus", watchStatus);
                    cmdRetrieve.Parameters.AddWithValue("@prodId", prodId);
                    cmdRetrieve.ExecuteNonQuery();

                    //query 2 execution
                    cmdRetrieve = new SqlCommand(query2, con);
                    cmdRetrieve.Parameters.AddWithValue("@wId", wId);
                    cmdRetrieve.Parameters.AddWithValue("@accId", accId);
                    cmdRetrieve.Parameters.AddWithValue("@wpId", wpId);
                    cmdRetrieve.ExecuteNonQuery();
                }

                catch (NullReferenceException ex)
                {
                    DataList_errorMsg(ex);
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            
        }

        //add to cart
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {

        }

        //validation purpose
        protected void txtBid_TextChanged(object sender, EventArgs e)
        {
            var ctrl = (TextBox)sender;
            bool match = Regex.IsMatch(ctrl.Text, @"^\d+(?:\.\d{2})?$");
            if (!match)
            {
                string alertMsg = "[!] Please enter amount of bid more than current bid in digit.";
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
        }

        protected void txtMakeOffer_TextChanged(object sender, EventArgs e)
        {
            var ctrl = (TextBox)sender;
            bool match = Regex.IsMatch(ctrl.Text, @"^\d+(?:\.\d{2})?$");

            if (!match)
            {
                string alertMsg = "[!] Please enter amount of bid more than current bid in digit.";
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
        }

    }

    public class JSAuthenticationUse
    {
        private const string MyScript = "var authenticate='{0}'";

        public static string GetMyScript()
        {
            return string.Format(MyScript, HttpContext.Current.Request.IsAuthenticated);
        }
    }
}