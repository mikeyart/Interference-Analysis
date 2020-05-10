using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Model_1546
{
   
    public class Station_Add
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(';');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(';');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < rows.Count(); i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }

        public static string[] GetName()
        {
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox2"] as System.Windows.Forms.TextBox;
            string filepath = t.Text;
            DataTable dt = ConvertCSVtoDataTable(filepath);

            string[] name = dt.AsEnumerable().Select(s => s.Field<string>("Site_Name")).ToArray<string>();
            return name;
        }
        public static double[] GetLatitude()
        {
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox2"] as System.Windows.Forms.TextBox;
            string filepath = t.Text;
            DataTable dt = ConvertCSVtoDataTable(filepath);

            string[] latitude = dt.AsEnumerable().Select(s => s.Field<string>("Latitude")).ToArray<string>();
            double[] lat = Array.ConvertAll(latitude, s => double.Parse(s));
            return lat;
        }

        public static double[] GetLongitude()
        {
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox2"] as System.Windows.Forms.TextBox;
            string filepath = t.Text;
            DataTable dt = ConvertCSVtoDataTable(filepath);

            string[] longitude = dt.AsEnumerable().Select(s => s.Field<string>("Longitude")).ToArray<string>();
            double[] lon = Array.ConvertAll(longitude, s => double.Parse(s));
            return lon;
        }

        public static double[] GetHeight()
        {
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox2"] as System.Windows.Forms.TextBox;
            string filepath = t.Text;
            DataTable dt = ConvertCSVtoDataTable(filepath);

            string[] height = dt.AsEnumerable().Select(s => s.Field<string>("Height")).ToArray<string>();
            double[] h = Array.ConvertAll(height, s => double.Parse(s));
            return h;
        }

        public static int[] GetAzimuth()
        {
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox2"] as System.Windows.Forms.TextBox;
            string filepath = t.Text;
            DataTable dt = ConvertCSVtoDataTable(filepath);

            string[] azimuth = dt.AsEnumerable().Select(s => s.Field<string>("Azimuth")).ToArray<string>();
            int[] az = Array.ConvertAll(azimuth, s => int.Parse(s));
            return az;
        }

        public static int[] GetTilt()
        {
            System.Windows.Forms.TextBox t = Application.OpenForms["Program"].Controls["textbox2"] as System.Windows.Forms.TextBox;
            string filepath = t.Text;
            DataTable dt = ConvertCSVtoDataTable(filepath);

            string[] elevation = dt.AsEnumerable().Select(s => s.Field<string>("Tilt")).ToArray<string>();
            int[] tilt = Array.ConvertAll(elevation, s => int.Parse(s));
            return tilt;
        }
    }




}
    






   