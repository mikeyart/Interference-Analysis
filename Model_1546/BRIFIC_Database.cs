using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Model_1546
{
    public class BRIFIC_Database
    {

        public static List<string> GetNameTx()
        {
            List<string> Name = new List<string>();
            List<double> Longitude = new List<double>();

            List<double> Height = new List<double>();
            List<double> Power = new List<double>();
            string name;

            var con = new SQLiteConnection();
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox1"] as System.Windows.Forms.TextBox;
            string source = t.Text;
            con.ConnectionString = String.Format(@"Data Source = {0} ; Version=3", source);
            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT site_name FROM fmtv_terra WHERE adm = 'UKR' AND notice_typ = 'GT1' AND tv_chan BETWEEN 50 AND 53 AND lat_deg BETWEEN 45 AND 49 AND long_dec BETWEEN 26 AND 32";

            cmd.Prepare();

            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                name = rdr.GetString(0);
                Name.Add(name);
            }
            return Name;
        }

        public static List<double> GetLatTx()
        {
            List<double> Latitude = new List<double>();
            double lat;

            var con = new SQLiteConnection();
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox1"] as System.Windows.Forms.TextBox;
            string source = t.Text;
            con.ConnectionString = String.Format(@"Data Source = {0} ; Version=3", source);
            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT lat_dec FROM fmtv_terra WHERE adm = 'UKR' AND notice_typ = 'GT1' AND tv_chan BETWEEN 50 AND 53 AND lat_deg BETWEEN 45 AND 49 AND long_dec BETWEEN 26 AND 32";

            cmd.Prepare();

            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                lat = rdr.GetDouble(0);
                Latitude.Add(lat);

            }
            return Latitude;
        }

        public static List<double> GetLonTx()
        {
            List<double> Longitude = new List<double>();
            double lon;

            var con = new SQLiteConnection();
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox1"] as System.Windows.Forms.TextBox;
            string source = t.Text;
            con.ConnectionString = String.Format(@"Data Source = {0} ; Version=3", source);
            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT long_dec FROM fmtv_terra WHERE adm = 'UKR' AND notice_typ = 'GT1' AND tv_chan BETWEEN 50 AND 53 AND lat_deg BETWEEN 45 AND 49 AND long_dec BETWEEN 26 AND 32";

            cmd.Prepare();

            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                lon = rdr.GetDouble(0);
                Longitude.Add(lon);

            }
            return Longitude;
        }


        public static List<double> GetHeightTx()
        {
            List<double> Height = new List<double>();
            double height;

            var con = new SQLiteConnection();
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox1"] as System.Windows.Forms.TextBox;
            string source = t.Text;
            con.ConnectionString = String.Format(@"Data Source = {0} ; Version=3", source);
            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT hgt_agl FROM fmtv_terra WHERE adm = 'UKR' AND notice_typ = 'GT1' AND tv_chan BETWEEN 50 AND 53 AND lat_deg BETWEEN 45 AND 49 AND long_dec BETWEEN 26 AND 32";

            cmd.Prepare();

            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                height = rdr.GetDouble(0);
                Height.Add(height);

            }
            return Height;
        }

        public static List<double> GetPowerTx()
        {
            List<double> Power = new List<double>();
            double power;

            var con = new SQLiteConnection();
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox1"] as System.Windows.Forms.TextBox;
            string source = t.Text;
            con.ConnectionString = String.Format(@"Data Source = {0} ; Version=3", source);
            con.Open();

            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "SELECT erp_dbw FROM fmtv_terra WHERE adm = 'UKR' AND notice_typ = 'GT1' AND tv_chan BETWEEN 50 AND 53 AND lat_deg BETWEEN 45 AND 49 AND long_dec BETWEEN 26 AND 32";

            cmd.Prepare();

            SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                power = rdr.GetDouble(0);
                Power.Add(power);

            }
            return Power;
        }
    }
}
