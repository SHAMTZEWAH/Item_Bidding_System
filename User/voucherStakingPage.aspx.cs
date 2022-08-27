using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace Item_Bidding_System.User
{
	public partial class voucherStakingPage : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {

            showPoint();
            showStakePoint();
            showTotalPoint();

            //calculate percentage
            //100percent = 10000token
            //addwith previous value
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string retrieveStakePoint = "Select pointStake From Voucher Where accId = @accId";
            SqlCommand retrieveStakePointCmd = new SqlCommand(retrieveStakePoint, con);
            retrieveStakePointCmd.Parameters.AddWithValue("@accId", getAccId());
            int stakePoint = Convert.ToInt32(retrieveStakePointCmd.ExecuteScalar().ToString());

            con.Close();

            con.Open();

            string retrieveTotalPoint = "Select point From TokenPool";
            SqlCommand retrieveTotalPointCmd = new SqlCommand(retrieveTotalPoint, con);
            int totalPoint = Convert.ToInt32(retrieveTotalPointCmd.ExecuteScalar().ToString());

            con.Close();

            int reward = calculateReward(stakePoint, totalPoint);
            pointReward.Text = reward.ToString();

            con.Open();

            string retrieveRewardPoint = "Select reward from TokenPool";
            SqlCommand retrieveRewardPointCmd = new SqlCommand(retrieveRewardPoint, con);
            int rewardPoint = Convert.ToInt32(retrieveRewardPointCmd.ExecuteScalar().ToString());
            label1.Text = rewardPoint.ToString();

            con.Close();

            int stakePercentage = calculateStakeRate(totalPoint, rewardPoint);
            rate.Text = stakePercentage.ToString() + "%";

            //String foo = DateTime.Now.ToString("HH:mm");
            //label2.Text = foo.ToString();

            TimeSpan hourMinute;
            hourMinute = DateTime.Now.TimeOfDay;
            Boolean status = true;
            if (hourMinute >= new TimeSpan(12, 15, 0))
            {
                con.Open();
                string insertReward = "Update Voucher Set pointReward = pointReward + @pointReward Where accId = @accId";
                SqlCommand insertRewardCmd = new SqlCommand(insertReward, con);
                insertRewardCmd.Parameters.AddWithValue("@pointReward", reward);
                insertRewardCmd.Parameters.AddWithValue("@accId", 1);
                insertRewardCmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                string retrieveReward = "Select pointReward From Voucher Where accId = @accId";
                SqlCommand retrieveRewardCmd = new SqlCommand(retrieveReward, con);
                retrieveRewardCmd.Parameters.AddWithValue("@accId", 1);
                label2.Text = retrieveRewardCmd.ExecuteScalar().ToString();
                con.Close();
            }

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
            string query = "SELECT accId FROM Account WHERE username = @name AND email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                accId = (string)cmdRetrieve.ExecuteScalar();

            }
            catch (NullReferenceException ex)
            {
                string alertMsg = "[!] The action is unable to complete: " + ex.ToString();
                string script = "<script type=\"text/javascript\">alert('" + alertMsg + "');</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", script);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return accId;
        }

        protected void stakeBtn_Click(object sender, EventArgs e)
        {
            string accId = getAccId();

            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);

            con.Open();//retieve points to compare
            string retrievePoint = "Select point from Voucher Where accId = @accId";
            SqlCommand cmd1 = new SqlCommand(retrievePoint, con);
            cmd1.Parameters.AddWithValue("@accId", accId);
            int tokenCompare = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
            con.Close();

            if (Regex.IsMatch(tokenStake.Text, @"\d"))
            {

                if (Convert.ToInt32(tokenStake.Text) <= tokenCompare)
                {

                    con.Open();
                    int storeToken = Convert.ToInt32(tokenStake.Text);
                    string updatePoint = "Update Voucher set point = point - @point";
                    SqlCommand cmd2 = new SqlCommand(updatePoint, con);
                    cmd2.Parameters.AddWithValue("@point", storeToken);
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string pointStake = "Update Voucher set pointStake = pointStake + @pointStake where accId = @accId";
                    SqlCommand pointStakeCommand = new SqlCommand(pointStake, con);
                    pointStakeCommand.Parameters.AddWithValue("@pointStake", storeToken);
                    pointStakeCommand.Parameters.AddWithValue("@accId", accId);
                    pointStakeCommand.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    string tokenPool = "Update TokenPool Set point = point + @point";
                    SqlCommand cmd4 = new SqlCommand(tokenPool, con);
                    cmd4.Parameters.AddWithValue("@point", storeToken);
                    cmd4.ExecuteNonQuery();
                    con.Close();


                    showPoint();
                    showStakePoint();
                    showTotalPoint();
                    Response.Redirect("voucherStakingPage.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Insufficient of token')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid input, Please enter number only')</script>");
            }
        }

        protected void showPoint()
        {
            string accId = getAccId();

            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string selectPoint = "Select point from Voucher Where accId = @accId";
            SqlCommand cmd1 = new SqlCommand(selectPoint, con);
            cmd1.Parameters.AddWithValue("@accId", accId);
            string tokenDisplay = cmd1.ExecuteScalar().ToString();

            token.Text = tokenDisplay;
            con.Close();

        }

        protected void showStakePoint()
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();
            string displayStakeToken = "Select pointStake from Voucher Where accId = @accId";
            SqlCommand displayStakeTokenCmd = new SqlCommand(displayStakeToken, con);
            displayStakeTokenCmd.Parameters.AddWithValue("@accId", accId);
            stakeValue.Text = (displayStakeTokenCmd.ExecuteScalar()).ToString();
            con.Close();

        }

        protected void showTotalPoint()
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();
            string displayTotalToken = "Select point from TokenPool";
            SqlCommand cmd5 = new SqlCommand(displayTotalToken, con);

            string totalTokenDisplay = cmd5.ExecuteScalar().ToString();
            tokenCirculate.Text = totalTokenDisplay;

            con.Close();
        }

        protected int calculateReward(int one, int two)
        {
            double parameterOne = (double)one;
            double parameterTwo = (double)two;
            double totalPercentage = (parameterOne * 100) / parameterTwo;

            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();

            string retrieveRewardPoint = "Select reward from TokenPool";
            SqlCommand retrieveRewardPointCmd = new SqlCommand(retrieveRewardPoint, con);
            int reward = Convert.ToInt32(retrieveRewardPointCmd.ExecuteScalar().ToString());
            int totalReward = Convert.ToInt32((totalPercentage * reward) / 100);

            return totalReward;
        }

        protected int calculateStakeRate(int one, int two)
        {
            int stakePercentage = (one * 100) / two;
            return stakePercentage;
        }

        protected void Button1_Click(object sender, EventArgs e)//claim
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();
            string retrieveReward = "Select pointReward From Voucher Where accId = @accId";
            SqlCommand retrieveRewardCmd = new SqlCommand(retrieveReward, con);
            retrieveRewardCmd.Parameters.AddWithValue("@accId", accId);
            int pointReward = Convert.ToInt32(retrieveRewardCmd.ExecuteScalar());
            con.Close();

            con.Open();
            string insertReward = "Update Voucher Set pointReward = @pointReward Where accId = @accId";
            SqlCommand insertRewardCmd = new SqlCommand(insertReward, con);
            insertRewardCmd.Parameters.AddWithValue("@pointReward", 0);
            insertRewardCmd.Parameters.AddWithValue("@accId", accId);
            insertRewardCmd.ExecuteNonQuery();
            con.Close();



            con.Open();
            string updatePoint = "Update point Set point = point + @point Where accId = @accId";
            SqlCommand updatePointCmd = new SqlCommand(updatePoint, con);
            updatePointCmd.Parameters.AddWithValue("@point", pointReward);
            updatePointCmd.Parameters.AddWithValue("@accId", accId);
            updatePointCmd.ExecuteNonQuery();
            con.Close();

            Response.Redirect("voucherStakingPage.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)//restake
        {
            string accId = getAccId();
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            con = new SqlConnection(strCon);
            con.Open();
            string retrieveReward = "Select pointReward From Voucher Where accId = @accId";
            SqlCommand retrieveRewardCmd = new SqlCommand(retrieveReward, con);
            retrieveRewardCmd.Parameters.AddWithValue("@accId", accId);
            int pointReward = Convert.ToInt32(retrieveRewardCmd.ExecuteScalar());
            con.Close();

            con.Open();
            string insertReward = "Update Voucher Set pointReward = @pointReward Where accId = @accId";
            SqlCommand insertRewardCmd = new SqlCommand(insertReward, con);
            insertRewardCmd.Parameters.AddWithValue("@pointReward", 0);
            insertRewardCmd.Parameters.AddWithValue("@accId", accId);
            insertRewardCmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            string updatePointStake = "Update pointStake Set pointStake = pointStake + @pointStake Where accId = @accId";
            SqlCommand updatePointStakeCmd = new SqlCommand(updatePointStake, con);
            updatePointStakeCmd.Parameters.AddWithValue("@pointStake", pointReward);
            updatePointStakeCmd.Parameters.AddWithValue("@accId", accId);
            updatePointStakeCmd.ExecuteNonQuery();
            con.Close();

            showStakePoint();

        }
    }
}