using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Item_Bidding_System.User
{
    public partial class AccountManagement : System.Web.UI.Page
    {
        private DataSet dtSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                assignData();
            }
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
            if (ddlCountry.SelectedValue == "Malaysia")
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

        void assignData()
        {
            loadData();
            try
            {
                foreach (DataRow dr in dtSet.Tables["Account"].Rows)
                {
                    txtUsername.Text = dr["username"].ToString();
                    txtEmail.Text = dr["email"].ToString();

                    if (dr["phoneNo"] != DBNull.Value)
                    {
                        txtPhoneNo.Text = (string)dr["phoneNo"];
                    }
                    if (dr["gender"] != DBNull.Value)
                    {
                        radioGender.SelectedValue = (string)dr["gender"];
                    }
                    if (dr["dateOfBirth"] != DBNull.Value)
                    {
                        Calendar1.SelectedDate = (DateTime)dr["dateOfBirth"];
                    }
                }
            }
            catch(Exception ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
            
        }
        void loadData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlDataAdapter adapter;
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId, Account.username, Account.email, Account.phoneNo, Account.gender, Account.dateOfBirth, Account.accPhotoURL, Account.accPhoto, Address.country, Address.state, Address.city, Address.poscode, Address.street " +
                "FROM Account INNER JOIN " +
                "Address ON Account.accId = Address.accId " +
                "WHERE Account.username = @name AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdRetrieve;

                dtSet = new DataSet();
                adapter.Fill(dtSet, "Account");
                foreach(DataRow dr in dtSet.Tables["Account"].Rows)
                {
                    ddlCountry.SelectedValue = (string)dr["country"];
                    loadState();
                    ddlState.SelectedValue = (string)dr["state"];
                    loadCity();
                    ddlCity.SelectedValue = (string)dr["city"];
                    txtZip.Text = (string)dr["poscode"];
                    txtAddress.Text = (string)dr["street"];
                }
                DataList1.DataSource = dtSet.Tables["Account"];
                DataList1.DataBind();
                
            }
            catch (Exception ex)
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
        string getAccId()
        {
            string accId = "";

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlDataAdapter adapter;
            SqlCommand cmdRetrieve;
            string query = "SELECT Account.accId " +
                "FROM Account WHERE Account.username = @name AND Account.email = @email";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                accId = (string)cmdRetrieve.ExecuteScalar();
            }
            catch (Exception ex)
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

            return accId;
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
            string query = "SELECT COUNT(addressId) FROM Address";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.ExecuteNonQuery();
                count = (int)cmdRetrieve.ExecuteScalar();
            }
            catch (Exception ex)
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

        string getAddressId()
        {
            int count = getAddressCount();
            string addressId = "";
            if (count + 1 > 9)
            {
                addressId = "ad_0" + (count+1);
            }
            else
            {
                addressId = "ad_00" + (count + 1);
            }
            return addressId;
        }
        void createAddressData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "INSERT INTO Address(addressId, country, state, city, poscode, street, defAddress, accId) " +
                "VALUES(@addressId, @country, @state, @city, @poscode, @street, 'Yes', @accId)";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@addressId", getAddressId());
                cmdRetrieve.Parameters.AddWithValue("@country", ddlCountry.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@state",ddlState.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@city",ddlCity.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@poscode",txtZip.Text);
                cmdRetrieve.Parameters.AddWithValue("@street",txtAddress.Text);
                cmdRetrieve.Parameters.AddWithValue("@accId",getAccId());
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        void updateAddressData()
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string query = "UPDATE Address SET country = @country, state = @state, city = @city, poscode = @poscode, street = @street WHERE accId = @accId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@country", ddlCountry.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@state", ddlState.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@city", ddlCity.SelectedValue);
                cmdRetrieve.Parameters.AddWithValue("@poscode", txtZip.Text);
                cmdRetrieve.Parameters.AddWithValue("@street",txtAddress.Text);
                cmdRetrieve.Parameters.AddWithValue("@accId",getAccId());
                cmdRetrieve.ExecuteNonQuery();
            }
            catch (Exception ex)
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
        void updateData()
        {
            byte[] bytes = { };

            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            //get user id
            string queryGetUserId = "SELECT aspnet_Users.UserId FROM aspnet_Users INNER JOIN aspnet_Membership ON aspnet_Users.UserId = aspnet_Membership.UserId WHERE aspnet_Users.Username = @name AND aspnet_Membership.Email = @emailC";
            //Account table
            string query = "UPDATE Account SET Account.username = @name, Account.email = @email, Account.phoneNo = @phoneNo, Account.accPhotoURL=@photoURL, Account.accPhoto=@photo, Account.gender = @gender, Account.dateOfBirth = @dateBirth WHERE Account.username = @name2 AND Account.userId = @userId";
            //Membership table
            string queryMember = "UPDATE aspnet_Membership SET Email = @email, LoweredEmail = @lowerEmail WHERE UserId = @userId";
            //Users table
            string queryUsers = "UPDATE aspnet_Users SET aspnet_Users.Username = @name, aspnet_Users.LoweredUserName = @lowerName WHERE aspnet_Users.UserId = @userId";
            //Address table
            string queryCheckAddress = "SELECT COUNT(addressId) FROM Address WHERE accId = @accId";

            //execute
            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(queryCheckAddress, con);
                cmdRetrieve.Parameters.AddWithValue("@accId",getAccId());

                if((int)cmdRetrieve.ExecuteScalar() > 0)
                {
                    updateAddressData();
                }
                else
                {
                    createAddressData();
                }

                SqlCommand cmdRetrieveUserId = new SqlCommand(queryGetUserId, con);
                cmdRetrieveUserId.Parameters.AddWithValue("@name", User.Identity.Name);
                cmdRetrieveUserId.Parameters.AddWithValue("@emailC", Membership.GetUser().Email);
                
                //Account table
                cmdRetrieve = new SqlCommand(query, con);
                cmdRetrieve.Parameters.AddWithValue("@name", txtUsername.Text);
                cmdRetrieve.Parameters.AddWithValue("@email", txtEmail.Text);
                cmdRetrieve.Parameters.AddWithValue("@phoneNo", txtPhoneNo.Text);
                cmdRetrieve.Parameters.AddWithValue("@photoURL", txtInsertURL.Text);

                //get the posted file 
                if (txtUploadPhoto.HasFiles)
                {
                    foreach (HttpPostedFile postedFile in txtUploadPhoto.PostedFiles)
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string contentType = postedFile.ContentType;
                        using (Stream fs = postedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                //serialise the photo
                                bytes = br.ReadBytes((Int32)fs.Length);

                            }
                        }
                    }
                }    

                cmdRetrieve.Parameters.AddWithValue("@photo", bytes);
                cmdRetrieve.Parameters.AddWithValue("@gender", radioGender.SelectedValue);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                cmdRetrieve.Parameters.AddWithValue("@dateBirth", Calendar1.SelectedDate.ToShortDateString());
                cmdRetrieve.Parameters.AddWithValue("@name2", User.Identity.Name);
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdRetrieveUserId.ExecuteScalar());
                cmdRetrieve.ExecuteNonQuery();

                //Membership table
                cmdRetrieve = new SqlCommand(queryMember, con);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                cmdRetrieve.Parameters.AddWithValue("@lowerEmail", Membership.GetUser().Email.ToLower());
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdRetrieveUserId.ExecuteScalar());
                cmdRetrieve.ExecuteNonQuery();

                //Users table
                cmdRetrieve = new SqlCommand(queryUsers, con);
                cmdRetrieve.Parameters.AddWithValue("@name", Membership.GetUser().UserName);
                cmdRetrieve.Parameters.AddWithValue("@lowerName", Membership.GetUser().UserName.ToLower());
                cmdRetrieve.Parameters.AddWithValue("@userId", cmdRetrieveUserId.ExecuteScalar());
                cmdRetrieve.ExecuteNonQuery();

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

        void insertPhoto(string photoURL, byte[] bytes)
        {
            //create connection
            SqlConnection con;
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con = new SqlConnection(strCon);

            //prepare command 
            SqlCommand cmdRetrieve;
            string queryPhoto = "UPDATE Account SET accPhotoURL = @accPhotoURL, accPhoto = @accPhoto WHERE email = @email AND username = @username";

            try
            {
                con.Open();
                cmdRetrieve = new SqlCommand(queryPhoto, con);
                cmdRetrieve.Parameters.AddWithValue("@email", Membership.GetUser().Email);
                cmdRetrieve.Parameters.AddWithValue("@username", User.Identity.Name);
                if (bytes == null)
                {
                    cmdRetrieve.Parameters.AddWithValue("@accPhotoURL", photoURL);
                    cmdRetrieve.Parameters.AddWithValue("@accPhoto", new byte[] { });
                }
                else
                {
                    cmdRetrieve.Parameters.AddWithValue("@accPhotoURL", DBNull.Value);
                    cmdRetrieve.Parameters.AddWithValue("@accPhoto", bytes);
                }   
                cmdRetrieve.ExecuteNonQuery();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            updateData();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
        }

        protected void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnSubmitPhoto_Click(object sender, EventArgs e)
        {
            byte[] bytes = { };

            try
            {
                if (txtUploadPhoto.HasFiles)
                {
                    //get the posted file 
                    foreach (HttpPostedFile postedFile in txtUploadPhoto.PostedFiles)
                    {
                        string filename = Path.GetFileName(postedFile.FileName);
                        string contentType = postedFile.ContentType;
                        using (Stream fs = postedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                //serialise the photo
                                bytes = br.ReadBytes((Int32)fs.Length);

                                //insert photo into database
                                insertPhoto(null, bytes);
                                Array.Clear(bytes, 0, bytes.Length);

                            }
                        }
                    }
                }
                assignData();
            }
            catch (Exception ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }

        protected void btnSubmitURL_Click(object sender, EventArgs e)
        {
            //get data from the gvStore
            try
            {
                //create photo database
                insertPhoto(txtInsertURL.Text, null);

                //get all Details
                assignData();

                //empty the url textbox
                txtInsertURL.Text = string.Empty;
            }
            catch (Exception ex)
            {
                HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
                if (control != null)
                {
                    control.Style.Add("display", "block");
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = ex.Message.ToString();
                }
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadState();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCity();
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnDefAddress_Click(object sender, EventArgs e)
        {

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //void getAllPhoto()
        //{

        //    //get data table
        //    DataTable dt = dtSet.Tables["Account"];

        //    try
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            //dr["accPhotoURL"]
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        HtmlControl control = (HtmlControl)Page.FindControl("errorMsg");
        //        if (control != null)
        //        {
        //            control.Style.Add("display", "block");
        //            lblErrorMsg.Visible = true;
        //            lblErrorMsg.Text = ex.Message.ToString();
        //        }
        //    }
        //}
    }
}