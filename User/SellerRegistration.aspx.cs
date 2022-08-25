using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.User
{
    public partial class SellerRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadUserData();
                loadCountry();
            }
            loadState();
            loadCity();
        }

        void loadCountry()
        {
            List<string> country = new List<string>();
            country.Add("Malaysia");
            ddlCountry.DataSource = country;
            ddlCountry.DataBind();
        }

        void loadState()
        {
            List<string> state = new List<string>();
            if(ddlCountry.SelectedValue == "Malaysia")
            {
                state.Add("Johor");
                state.Add("Kedah");
                state.Add("Kelantan");
                state.Add("Melaka");
                state.Add("Negeri Sembilan");
                state.Add("Pahang");
                state.Add("Penang");
                state.Add("Perak");
                state.Add("Perlis");
                state.Add("Sabah");
                state.Add("Sarawak");
                state.Add("Selangor");
                state.Add("Terengganu");
                state.Add("Kuala Lumpur");
                state.Add("Labuan");
                state.Add("Putrajaya");
            }
            ddlState.DataSource = state;
            ddlState.DataBind();
        }

        void loadCity()
        {
            List<string> city = new List<string>();
            if (ddlState.SelectedValue == "Johor")
            {
                
                city.Add("Ayer Baloi");
                city.Add("Ayer Hitam");
                city.Add("Ayer Tawar 2");
                city.Add("Bandar Penawar");
                city.Add("Bandar Tenggara");
                city.Add("Batu Anam");
                city.Add("Batu Pahat");
                city.Add("Bekok");
                city.Add("Benut");
                city.Add("Bukit Gambir");
                city.Add("Bukit Pasir");
                city.Add("Chaah");
                city.Add("Endau");
                city.Add("Gelang Patah");
                city.Add("Gerisek");
                city.Add("Gugusan Taib Andak");
                city.Add("Jementah");
                city.Add("Johor Bahru");
                city.Add("Kahang");
                city.Add("Kluang");
                city.Add("Kota Tinggi");
                city.Add("Kukup");
                city.Add("Kulai");
                city.Add("Labis");
                city.Add("Layang - Layang");
                city.Add("Masai");
                city.Add("Mersing");
                city.Add("Muar");
                city.Add("Nusajaya");
                city.Add("Pagoh");
                city.Add("Paloh");
                city.Add("Panchor");
                city.Add("Parit Jawa");
                city.Add("Parit Raja");
                city.Add("Parit Sulong");
                city.Add("Pasir Gudang");
                city.Add("Pekan Nenas");
                city.Add("Pengerang");
                city.Add("Pontian");
                city.Add("Pulau Satu");
                city.Add("Rengam");
                city.Add("Rengit");
                city.Add("Segamat");
                city.Add("Semerah");
                city.Add("Senai");
                city.Add("Senggarang");
                city.Add("Seri Gading");
                city.Add("Seri Medan");
                city.Add("Simpang Rengam");
                city.Add("Sungai Mati");
                city.Add("Tangkak");
                city.Add("Ulu Tiram");
                city.Add("Yong Peng");
            }
            else if (ddlState.SelectedValue == "Kedah")
            {
                
                city.Add("Alor Setar");
                city.Add("Ayer Hitam");
                city.Add("Baling");
                city.Add("Bandar Baharu");
                city.Add("Bedong");
                city.Add("Bukit Kayu Hitam");
                city.Add("Changloon");
                city.Add("Gurun");
                city.Add("Jeniang");
                city.Add("Jitra");
                city.Add("Karangan");
                city.Add("Kepala Batas");
                city.Add("Kodiang");
                city.Add("Kota Kuala Muda");
                city.Add("Kota Sarang Semut");
                city.Add("Kuala Kedah");
                city.Add("Kuala Ketil");
                city.Add("Kuala Nerang");
                city.Add("Kuala Pegang");
                city.Add("Kulim");
                city.Add("Kupang");
                city.Add("Langgar");
                city.Add("Langkawi");
                city.Add("Lunas");
                city.Add("Merbok");
                city.Add("Padang Serai");
                city.Add("Pendang");
                city.Add("Pokok Sena");
                city.Add("Serdang");
                city.Add("Sik");
                city.Add("Simpang Empat");
                city.Add("Sungai Petani");
                city.Add("Universiti Utara Malaysia");
                city.Add("Yan");
            }
            else if (ddlState.SelectedValue == "Kelantan")
            {
                
                city.Add("Ayer Lanas");
                city.Add("Bachok");
                city.Add("Cherang Ruku");
                city.Add("Dabong");
                city.Add("Gua Musang");
                city.Add("Jeli");
                city.Add("Kem Desa Pahlawan");
                city.Add("Ketereh");
                city.Add("Kota Bharu");
                city.Add("Kuala Balah");
                city.Add("Kuala Krai");
                city.Add("Machang");
                city.Add("Melor");
                city.Add("Pasir Mas");
                city.Add("Pasir Puteh");
                city.Add("Pulai Chondong");
                city.Add("Rantau Panjang");
                city.Add("Selising");
                city.Add("Tanah Merah");
                city.Add("Temangan");
                city.Add("Tumpat");
                city.Add("Wakaf Bharu");
            }
            else if (ddlState.SelectedValue == "Melaka")
            {
                
                city.Add("Alor Gajah");
                city.Add("Asahan");
                city.Add("Ayer Keroh");
                city.Add("Bemban");
                city.Add("Durian Tunggal");
                city.Add("Jasin");
                city.Add("Kem Trendak");
                city.Add("Kuala Sungai Baru");
                city.Add("Lubok China");
                city.Add("Masjid Tanah");
                city.Add("Malacca City");
                city.Add("Merlimau");
                city.Add("Selandar");
                city.Add("Sungai Rambai");
                city.Add("Sungai Udang");
                city.Add("Tanjong Kling");
            }
            else if (ddlState.SelectedValue == "Negeri Sembilan")
            {
                city.Add("Bahau");
                city.Add("Bandar Enstek");
                city.Add("Bandar Seri Jempol");
                city.Add("Batu Kikir");
                city.Add("Gemas");
                city.Add("Gemencheh");
                city.Add("Johol");
                city.Add("Kota");
                city.Add("Kuala Klawang");
                city.Add("Kuala Pilah");
                city.Add("Labu");
                city.Add("Linggi");
                city.Add("Mantin");
                city.Add("Nilai");
                city.Add("Nilai");
                city.Add("Port Dickson");
                city.Add("Pusat Bandar Palong");
                city.Add("Rantau");
                city.Add("Rembau");
                city.Add("Rompin");
                city.Add("Seremban");
                city.Add("Si Rusa");
                city.Add("Simpang Durian");
                city.Add("Simpang Pertang");
                city.Add("Tampin");
                city.Add("Tanjong Ipoh");
            }
            else if (ddlState.SelectedValue == "Pahang")
            {
                
                city.Add("Balok");
                city.Add("Bandar Bera");
                city.Add("Bandar Pusat Jengka");
                city.Add("Bandar Tun Abdul Razak");
                city.Add("Benta");
                city.Add("Bentong");
                city.Add("Brinchang");
                city.Add("Bukit Fraser");
                city.Add("Bukit Goh");
                city.Add("Bukit Kuin");
                city.Add("Chenor");
                city.Add("Tasik Chini");
                city.Add("Damak");
                city.Add("Dong");
                city.Add("Gambang");
                city.Add("Genting Highlands");
                city.Add("Jerantut");
                city.Add("Karak");
                city.Add("Kemayan");
                city.Add("Kuala Krau");
                city.Add("Kuala Lipis");
                city.Add("Kuala Rompin");
                city.Add("Kuantan");
                city.Add("Lanchang");
                city.Add("Lurah Bilut");
                city.Add("Maran");
                city.Add("Mentakab");
                city.Add("Muadzam Shah");
                city.Add("Padang Tengku");
                city.Add("Pekan");
                city.Add("Raub");
                city.Add("Ringlet");
                city.Add("Sega");
                city.Add("Sungai Koyan");
                city.Add("Sungai Lembing");
                city.Add("Tanah Rata");
                city.Add("Temerloh");
                city.Add("Triang");

            }
            else if (ddlState.SelectedValue == "Penang")
            {
                
                city.Add("Ayer Itam");
                city.Add("Balik Pulau");
                city.Add("Batu Ferringhi");
                city.Add("Batu Maung");
                city.Add("Bayan Lepas");
                city.Add("Bukit Mertajam");
                city.Add("Butterworth");
                city.Add("Gelugor");
                city.Add("Jelutong");
                city.Add("Kepala Batas");
                city.Add("Kubang Semang");
                city.Add("Nibong Tebal");
                city.Add("Penaga");
                city.Add("Penang Hill");
                city.Add("Perai");
                city.Add("Permatang Pauh");
                city.Add("Pulau Pinang");
                city.Add("Simpang Ampat");
                city.Add("Sungai Jawi");
                city.Add("Tanjung Bungah");
                city.Add("Tasek Gelugor");
                city.Add("USM Pulau Pinang");
            }
            else if (ddlState.SelectedValue == "Perak")
            {
                
                city.Add("Ayer Tawar");
                city.Add("Bagan Datoh");
                city.Add("Bagan Serai");
                city.Add("Bandar Seri Iskandar");
                city.Add("Batu Gajah");
                city.Add("Batu Kurau");
                city.Add("Behrang Stesen");
                city.Add("Bidor");
                city.Add("Bota");
                city.Add("Bruas");
                city.Add("Changkat Jering");
                city.Add("Changkat Keruing");
                city.Add("Chemor");
                city.Add("Chenderiang");
                city.Add("Chenderong Balai");
                city.Add("Chikus");
                city.Add("Enggor");
                city.Add("Gerik");
                city.Add("Gopeng");
                city.Add("Hutan Melintang");
                city.Add("Intan");
                city.Add("Ipoh");
                city.Add("Jeram");
                city.Add("Kampar");
                city.Add("Kampung Gajah");
                city.Add("Kampung Kepayang");
                city.Add("Kamunting");
                city.Add("Kuala Kangsar");
                city.Add("Kuala Kurau");
                city.Add("Kuala Sepetang");
                city.Add("Lambor Kanan");
                city.Add("Langkap");
                city.Add("Lenggong");
                city.Add("Lumut");
                city.Add("Malim Nawar");
                city.Add("Manong");
                city.Add("Matang");
                city.Add("Padang Rengas");
                city.Add("Pangkor");
                city.Add("Pantai Remis");
                city.Add("Parit");
                city.Add("Parit Buntar");
                city.Add("Pengkalan Hulu");
                city.Add("Pusing");
                city.Add("Rantau Panjang");
                city.Add("Sauk");
                city.Add("Selama");
                city.Add("Selekoh");
                city.Add("Seri Manjong");
                city.Add("Simpang");
                city.Add("Simpang Ampat Semanggol");
                city.Add("Sitiawan");
                city.Add("Slim River");
                city.Add("Sungai Siput");
                city.Add("Sungai Sumun");
                city.Add("Sungkai");
                city.Add("Taiping");
                city.Add("Tanjong Malim");
                city.Add("Tanjong Piandang");
                city.Add("Tanjong Rambutan");
                city.Add("Tanjong Tualang");
                city.Add("Tapah");
                city.Add("Tapah Road");
                city.Add("Teluk Intan");
                city.Add("Temoh");
                city.Add("TLDM Lumut");
                city.Add("Trolak");
                city.Add("Trong");
                city.Add("Tronoh");
                city.Add("Ulu Bernam");
                city.Add("Ulu Kinta");
            }
            else if (ddlState.SelectedValue == "Perlis")
            {

                city.Add("Arau");
                city.Add("Kaki Bukit");
                city.Add("Kangar");
                city.Add("Kuala Perlis");
                city.Add("Padang Besar");
                city.Add("Simpang Ampat");
            }
            else if (ddlState.SelectedValue == "Sabah")
            {
                city.Add("Beaufort");
                city.Add("Beluran");
                city.Add("Beverly");
                city.Add("Bongawan");
                city.Add("Inanam");
                city.Add("Keningau");
                city.Add("Kota Belud");
                city.Add("Kota Kinabalu");
                city.Add("Kota Kinabatangan");
                city.Add("Kota Marudu");
                city.Add("Kuala Penyu");
                city.Add("Kudat");
                city.Add("Kunak");
                city.Add("Lahad Datu");
                city.Add("Likas");
                city.Add("Membakut");
                city.Add("Menumbok");
                city.Add("Nabawan");
                city.Add("Pamol");
                city.Add("Papar");
                city.Add("Penampang");
                city.Add("Putatan");
                city.Add("Ranau");
                city.Add("Sandakan");
                city.Add("Semporna");
                city.Add("Sipitang");
                city.Add("Tambunan");
                city.Add("Tamparuli");
                city.Add("Tanjung Aru");
                city.Add("Tawau");
                city.Add("Tenghilan");
                city.Add("Tenom");
                city.Add("Tuaran");
            }
            else if (ddlState.SelectedValue == "Sarawak")
            {
                city.Add("Asajaya");
                city.Add("Balingian");
                city.Add("Baram");
                city.Add("Bau");
                city.Add("Bekenu");
                city.Add("Belaga");
                city.Add("Belawai");
                city.Add("Betong");
                city.Add("Bintangor");
                city.Add("Bintulu");
                city.Add("Dalat");
                city.Add("Daro");
                city.Add("Debak");
                city.Add("Engkilili");
                city.Add("Julau");
                city.Add("Kabong");
                city.Add("Kanowit");
                city.Add("Kapit");
                city.Add("Kota Samarahan");
                city.Add("Kuching");
                city.Add("Lawas");
                city.Add("Limbang");
                city.Add("Lingga");
                city.Add("Long Lama");
                city.Add("Lubok Antu");
                city.Add("Lundu");
                city.Add("Lutong");
                city.Add("Matu");
                city.Add("Miri");
                city.Add("Mukah");
                city.Add("Nanga Medamit");
                city.Add("Niah");
                city.Add("Pusa");
                city.Add("Roban");
                city.Add("Saratok");
                city.Add("Sarikei");
                city.Add("Sebauh");
                city.Add("Sebuyau");
                city.Add("Serian");
                city.Add("Sibu");
                city.Add("Siburan");
                city.Add("Simunjan");
                city.Add("Song");
                city.Add("Spaoh");
                city.Add("Sri Aman");
                city.Add("Sundar");
                city.Add("Tatau");
            }
            else if (ddlState.SelectedValue == "Selangor")
            {
                city.Add("Ampang");
                city.Add("Bandar Baru Bangi");
                city.Add("Bandar Puncak Alam");
                city.Add("Banting");
                city.Add("Batang Kali");
                city.Add("Batu Arang");
                city.Add("Batu Caves");
                city.Add("Beranang");
                city.Add("Bestari Jaya");
                city.Add("Bukit Rotan");
                city.Add("Cheras");
                city.Add("Cyberjaya");
                city.Add("Dengkil");
                city.Add("Hulu Langat");
                city.Add("Jenjarom");
                city.Add("Jeram");
                city.Add("Kajang");
                city.Add("Kapar");
                city.Add("Kerling");
                city.Add("Klang");
                city.Add("KLIA");
                city.Add("Kuala Kubu Baru");
                city.Add("Kuala Selangor");
                city.Add("Kluang");
                city.Add("Pelabuhan Klang");
                city.Add("Petaling Jaya");
                city.Add("Puchong");
                city.Add("Pulau Carey");
                city.Add("Pulau Indah");
                city.Add("Pulau Ketam");
                city.Add("Rasa");
                city.Add("Rawang");
                city.Add("Sabak Bernam");
                city.Add("Sekinchan");
                city.Add("Semenyih");
                city.Add("Sepang");
                city.Add("Serdang");
                city.Add("Serendah");
                city.Add("Seri Kembangan");
                city.Add("Shah Alam");
                city.Add("Subang Jaya");
                city.Add("Sungai Ayer Tawar");
                city.Add("Sungai Besar");
                city.Add("Sungai Buloh");
                city.Add("Sungai Pelek");
                city.Add("Tanjong Karang");
                city.Add("Tanjong Sepat");
                city.Add("Telok Panglima Garang");
            }
            else if (ddlState.SelectedValue == "Terengganu")
            {
                city.Add("Ajil");
                city.Add("Al Muktatfi Billah Shah");
                city.Add("Ayer Puteh");
                city.Add("Bukit Besi");
                city.Add("Bukit Payong");
                city.Add("Ceneh");
                city.Add("Chalok");
                city.Add("Cukai");
                city.Add("Dungun");
                city.Add("Jerteh");
                city.Add("Kampung Raja");
                city.Add("Kemasek");
                city.Add("Kerteh");
                city.Add("Ketengah Jaya");
                city.Add("Kijal");
                city.Add("Kuala Berang");
                city.Add("Kuala Besut");
                city.Add("Kuala Terengganu");
                city.Add("Marang");
                city.Add("Paka");
                city.Add("Permaisuri");
                city.Add("Sungai Tong");
            }
            else if (ddlState.SelectedValue == "Kuala Lumpur")
            {
                city.Add("Ampang");
                city.Add("Batu Caves");
                city.Add("Cheras");
                city.Add("Damansara");
                city.Add("Gombak");
                city.Add("Hulu Kelang");
                city.Add("Kepong");
                city.Add("Kuala Lumpur");
                city.Add("Petaling");
                city.Add("Petaling Jaya");
                city.Add("Sentul");
                city.Add("Setapak");
                city.Add("Sungai Besi");
            }
            else if (ddlState.SelectedValue == "Labuan")
            {
                city.Add("Labuan");
            }
            else if (ddlState.SelectedValue == "Putrajaya")
            {
                city.Add("Putrajaya");
            }
            ddlCity.DataSource = city;
            ddlCity.DataBind();
        }
        void loadUserData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            SqlCommand cmdGetUserId;
            //name, email, phoneNo, address
            string query = "SELECT Account.username, Account.email, Account.phoneNo, Address.poscode, Address.street " +
                "FROM Account INNER JOIN " +
                "Address ON Account.accId = Address.accId " +
                "WHERE Account.userId = @userId";
            
            //get user id
            string queryGetUserId = "SELECT aspnet_Users.UserId FROM aspnet_Users INNER JOIN aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId WHERE aspnet_Users.Username = @name AND aspnet_Membership.Email = @emailC";

            //execute
            try
            {
                con.Open();
                cmdGetUserId = new SqlCommand(queryGetUserId, con);
                cmdGetUserId.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdGetUserId.Parameters.AddWithValue("@emailC", Membership.GetUser().Email);

                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdGetUserId.ExecuteScalar());
                SqlDataReader reader = cmdRetrieve.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtName.Text = reader["username"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        PhoneNo.Text = reader["phoneNo"].ToString();
                        txtZip.Text = reader["poscode"].ToString();
                        txtAddress.Text = reader["street"].ToString();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        int getSellerCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(sellerId) FROM Seller";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        int getAddressCount()
        {
            int count = 0;

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "SELECT COUNT(addresId) FROM Address";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                count = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //uploadFiles();
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            SqlCommand cmdGetUserId;
            SqlCommand cmdGetAccId;

            //get user id
            string queryGetUserId = "SELECT aspnet_Users.UserId FROM aspnet_Users WHERE aspnet_Users.Username = @name AND aspnet_Users.Email = @emailC";
            string queryGetAccId = "SELECT Account.accId FROM Account WHERE userId = @userId";
            string querySeller = "INSERT INTO Seller(sellerId, limitPayout, merchantId, businessName, sellerStatus, accId) " +
                "VALUES(@sellerId, @payout, @merchantId, @businessName, @status, @accId)";
            string queryAddress = "INSERT INTO Address(addressId, country, state, city, poscode, street, defAddress, accId) " +
                "VALUES(@addressId, @country, @state, @city, @poscode, @street, 'Yes', @accId)";

            //execute
            try
            {
                con.Open();

                //get user id 
                cmdGetUserId = new SqlCommand(queryGetUserId, con);
                cmdGetUserId.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdGetUserId.Parameters.AddWithValue("@email", Membership.GetUser().Email);

                //get acc id
                cmdGetAccId = new SqlCommand(queryGetAccId, con);
                cmdGetAccId.Parameters.AddWithValue("@userId",cmdGetUserId.ExecuteScalar());

                //get seller id
                int sellerCount = getSellerCount();
                string sellerId = "";
                if(sellerCount + 1 > 9)
                {
                    sellerId = "s_0" + (sellerCount+1);
                }
                else
                {
                    sellerId = "s_00" + (sellerCount + 1);
                }

                //INSERT INTO Seller data
                cmdRetrieve = new SqlCommand(querySeller, con);
                cmdRetrieve.Parameters.AddWithValue("@sellerId", sellerId);
                cmdRetrieve.Parameters.AddWithValue("@payout", 10000.00);
                cmdRetrieve.Parameters.AddWithValue("@merchantId", txtMerchant.Text);
                cmdRetrieve.Parameters.AddWithValue("@businessName", txtBusiness.Text);
                cmdRetrieve.Parameters.AddWithValue("@status","Unconfirm");
                cmdRetrieve.Parameters.AddWithValue("@accId", cmdGetAccId.ExecuteScalar());
                cmdRetrieve.ExecuteNonQuery();

                //INSERT INTO address data
                int addressCount = getAddressCount();
                string addressId = "";
                if (addressCount + 1 > 9)
                {
                    addressId = "ad_0" + (addressCount + 1);
                }
                else
                {
                    addressId = "ad_00" + (addressCount + 1);
                }
                if (ViewState["clickedDef"] == null) //default havent been chosen
                {
                    cmdRetrieve = new SqlCommand(queryAddress, con);
                    cmdRetrieve.Parameters.AddWithValue("@addressId", addressId);
                    cmdRetrieve.Parameters.AddWithValue("@country", ddlCountry.SelectedValue);
                    cmdRetrieve.Parameters.AddWithValue("@state", ddlState.SelectedValue);
                    cmdRetrieve.Parameters.AddWithValue("@city", ddlCity.SelectedValue);
                    cmdRetrieve.Parameters.AddWithValue("@poscode", txtZip.Text);
                    cmdRetrieve.Parameters.AddWithValue("@street", txtAddress.Text);
                    cmdRetrieve.Parameters.AddWithValue("@accId", cmdGetAccId.ExecuteScalar());
                    cmdRetrieve.ExecuteNonQuery();
                }
                
            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        protected void BtnDefAddress_Click(object sender, EventArgs e)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            SqlCommand cmdGetUserId;
            SqlCommand cmdGetAccId;

            //get user id
            string queryGetUserId = "SELECT aspnet_Users.UserId FROM aspnet_Users WHERE aspnet_Users.Username = @name AND aspnet_Users.Email = @emailC";
            string queryGetAccId = "SELECT Account.accId FROM Account WHERE userId = @userId";
            string queryGetAddress = "SELECT * FROM Address WHERE Address.accId = @accId";

            //execute
            try
            {
                con.Open();

                //get user id
                cmdGetUserId = new SqlCommand(queryGetUserId, con);
                cmdGetUserId.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdGetUserId.Parameters.AddWithValue("@email", Membership.GetUser().Email);

                //get acc id
                cmdGetAccId = new SqlCommand(queryGetAccId, con);
                cmdGetAccId.Parameters.AddWithValue("@userId", cmdGetUserId.ExecuteScalar());

                //get address
                cmdRetrieve = new SqlCommand(queryGetAddress, con);
                cmdRetrieve.Parameters.AddWithValue("@accId",cmdGetAccId.ExecuteScalar());
                SqlDataReader reader = cmdRetrieve.ExecuteReader();

                //select address based on addr obtain
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (ddlCountry.Items.FindByValue(reader["country"].ToString()) != null)
                        {
                            ddlCountry.Items.FindByValue(reader["country"].ToString()).Selected = true;
                        }
                        if (ddlState.Items.FindByValue(reader["state"].ToString()) != null)
                        {
                            ddlState.Items.FindByValue(reader["state"].ToString()).Selected = true;
                        }
                        if (ddlCity.Items.FindByValue(reader["city"].ToString()) != null)
                        {
                            ddlCity.Items.FindByValue(reader["city"].ToString()).Selected = true;
                        }
                        txtZip.Text = reader["poscode"].ToString();
                        txtAddress.Text = reader["street"].ToString();
                    }
                }

            }
            catch (NullReferenceException ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }

            }
            finally
            {
                ViewState["clickedDef"] = true;
                con.Close();
                con.Dispose();
            }
        }
    }
}