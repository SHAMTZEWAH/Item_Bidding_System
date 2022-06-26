using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.General
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlConnection con;
            if (!IsPostBack)
            {
                try
                {

                    if (Request.QueryString["category"].ToString() != null)
                    {
                        //show category product when the user click on the category bar below the search bar
                        loadContentAsync(Request.QueryString["category"].ToString());
                        
                    }
                    else if (Request.QueryString["keyword"].ToString() != null) //searching keyword (on search bar)
                    {
                        //show product based on the keyword
                        loadContentAsync(Request.QueryString["keyword"].ToString());
                    }
                    else
                    {
                        loadHotProduct();
                    }

                }
                catch (NullReferenceException ex)
                {
                    //Show Hot product
                    loadHotProduct();
                }
            }
            
        }

        void loadHotProduct()
        {
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //apply string query
            SqlCommand cmdRetrieve;

            //execute query
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand();
                cmdRetrieve.CommandType = CommandType.StoredProcedure;
                cmdRetrieve.CommandText = "SelectAllProducts"; //need to change to "pr_HotProduct"
                cmdRetrieve.Parameters.AddWithValue("@name","test2"); //need to remove
                cmdRetrieve.Connection = con;
                Repeater1.DataSource = cmdRetrieve.ExecuteReader();
                Repeater1.DataBind();
            }
            catch (NullReferenceException exDB)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = exDB.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        } 

        async Task<DataTable> loadSPAsync(string filterDetails, string sp)
        {
            string username = "";
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            //create connectionString
            SqlConnection con = new SqlConnection();
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            //apply string query
            SqlCommand cmdRetrieve;

            //wait for the connection string to create 
            await Task.Run( () => {
                con.ConnectionString = strCon;
            });

            //execute query
            try
            {
                con.Open();

                //wait for the username to have value 
                await Task.Run(() =>
                {
                    if (User.Identity.Name != null)
                    {
                        username = User.Identity.Name;
                    }
                    else
                    {
                        username = "";
                    }
                });
                
                con.Open();
                cmdRetrieve = new SqlCommand();
                

                //wait for the cmdRetrieve is ready
                await Task.Run(() =>
                {
                    cmdRetrieve.CommandType = CommandType.StoredProcedure;
                    cmdRetrieve.CommandText = sp;
                    cmdRetrieve.Connection = con;
                    cmdRetrieve.Parameters.AddWithValue("@name", username);
                    if (Request.QueryString["category"].ToString() != null)
                    {
                        cmdRetrieve.Parameters.AddWithValue("@category", filterDetails);
                    }
                    else if (Request.QueryString["keyword"].ToString() != null)
                    {
                        cmdRetrieve.Parameters.AddWithValue("@filter", filterDetails);
                    }
                });

                //wait for the dataTable to be fill before return
                await Task.Run(() =>
                {
                    adapter.SelectCommand = cmdRetrieve;
                    adapter.Fill(dataTable);
                });
                
            }
            catch (NullReferenceException ex)
            {
                Label lblFooter = (Label)Repeater1.FindControl("lblNoData");
                if (lblFooter != null)
                {
                    lblFooter.Visible = true;
                    lblFooter.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dataTable;
        }

        async Task loadContentAsync(string filterDetails)
        {
            //async task assign in var
            var checkCategory = loadSPAsync(filterDetails, "pr_filterCategory");
            var checkName = loadSPAsync(filterDetails, "pr_filterName");
            var checkBrand = loadSPAsync(filterDetails, "pr_filterBrand");
            var checkBusinessName = loadSPAsync(filterDetails, "pr_filterBusinessName");
            var checkModel = loadSPAsync(filterDetails, "pr_filterModel");
            var checkType = loadSPAsync(filterDetails, "pr_filterType");

            //check which task finish first 
            DataTable resultTable = new DataTable();
            var filterTasks = new List<Task> { checkCategory, checkName, checkBrand, checkBusinessName, checkModel, checkType };
            while (filterTasks.Count > 0)
            {
                //check if any dataTable has data
                Task finishedTask = await Task.WhenAny(filterTasks);
                if (finishedTask == checkCategory)
                {
                    if(checkCategory.Result.Rows.Count > 0)
                    {
                        resultTable.Merge(checkCategory.Result);
                    }
                }
                else if (finishedTask == checkName)
                {
                    if (checkName.Result.Rows.Count > 0)
                    {
                        resultTable.Merge(checkName.Result);
                    }
                }
                else if (finishedTask == checkBrand)
                {
                    if (checkBrand.Result.Rows.Count > 0)
                    {
                        resultTable.Merge(checkBrand.Result);
                    }
                }
                else if (finishedTask == checkBusinessName)
                {
                    if (checkBusinessName.Result.Rows.Count > 0)
                    {
                        resultTable.Merge(checkBusinessName.Result);
                    }
                }
                else if (finishedTask == checkModel)
                {
                    if (checkModel.Result.Rows.Count > 0)
                    {
                        resultTable.Merge(checkModel.Result);
                    }
                }
                else if (finishedTask == checkType)
                {
                    if (checkType.Result.Rows.Count > 0)
                    {
                        resultTable.Merge(checkType.Result);
                    }
                }
                filterTasks.Remove(finishedTask);
            }

            //assign dataset to the repeater
            Repeater1.DataSource = resultTable;
            Repeater1.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}