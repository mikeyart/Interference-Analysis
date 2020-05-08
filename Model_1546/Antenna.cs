using System;
using System.Data;
using System.Device.Location;
using System.IO;
using System.Linq;


namespace Model_1546
{
    public class Antenna
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

        public static double GetHorAten(int azimuth, int tilt)
        {
            
                string filepath = @"B:\Antenna.csv";
                DataTable dt = ConvertCSVtoDataTable(filepath);
                DataRow[] H_Ang = dt.Select(String.Format("H_Ang = '{0}'", azimuth));
            if (tilt < 2)
            {
                tilt = 2;
                foreach (DataRow row in H_Ang)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(x => x.ColumnName).Where(n => n.Contains(String.Format("H_Aten_{0}", tilt).ToString())).ToArray();
                    var H_Atenuation = row[String.Format("{0}", columnNames)].ToString();
                    double H_Aten = double.Parse(H_Atenuation);
                    return H_Aten;
                }
            }
            else
            {
                foreach (DataRow row in H_Ang)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(x => x.ColumnName).Where(n => n.Contains(String.Format("H_Aten_{0}", tilt).ToString())).ToArray();
                    var H_Atenuation = row[String.Format("{0}", columnNames)].ToString();
                    double H_Aten = double.Parse(H_Atenuation);
                    return H_Aten;
                }
            }
            return 0;
            
        }

        public static double GetVertAten(double latTx,double lonTx, double latRx, double lonRx, double hRx, int tilt)
        {
            double C, c, sinalfa, a, b, cosc, alfa, epsilon, pi2;

            string filepath = @"B:\Antenna.csv";
            DataTable dt = ConvertCSVtoDataTable(filepath);

            var nCoord = new GeoCoordinate(latRx, lonRx);
            var eCoord = new GeoCoordinate(49.50555556, 29.88638889);
            double d = Math.Round(nCoord.GetDistanceTo(eCoord) / 1000, 1);
            pi2 = 90;
            latTx *= (Math.PI / 180);
            lonTx *= (Math.PI / 180);
            latRx *= (Math.PI / 180);
            lonRx *= (Math.PI / 180);
            pi2 *= (Math.PI / 180);

            C = lonRx - lonTx;
            a = pi2 - latRx;
            b= pi2 - latTx;
            cosc = Math.Cos(a) * Math.Cos(b) + Math.Sin(a) * Math.Sin(b) * Math.Cos(C);
            c = Math.Acos(cosc);
            
            sinalfa = (6371 + hRx/1000) / d * Math.Sin(c);
            alfa = Math.Asin(sinalfa);
            epsilon = alfa - pi2;
            DataRow[] V_Ang = dt.Select(String.Format("V_Ang = '{0}'", Math.Round(epsilon, 0)));

            if (tilt < 2)
            {
                tilt = 2;
                foreach (DataRow row in V_Ang)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                            .Select(x => x.ColumnName).Where(n => n.Contains(String.Format("V_Aten_{0}", tilt).ToString())).ToArray();
                    var V_Atenuation = row[String.Format("{0}", columnNames)].ToString();
                    double V_Aten = double.Parse(V_Atenuation);
                    return V_Aten;
                }
            }
            else
            {
                foreach (DataRow row in V_Ang)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                            .Select(x => x.ColumnName).Where(n => n.Contains(String.Format("V_Aten_{0}", tilt).ToString())).ToArray();
                    var V_Atenuation = row[String.Format("{0}", columnNames)].ToString();
                    double V_Aten = double.Parse(V_Atenuation);
                    return V_Aten;
                }
            }
            return 0;
        }

        public static double GetGain(int tilt)
        {
            string filepath = @"B:\Antenna.csv";
            DataTable dt = ConvertCSVtoDataTable(filepath);
            DataRow[] H_Ang = dt.Select("H_Ang = 0");

            if (tilt < 2)
            {
                tilt = 2;
                foreach (DataRow row in H_Ang)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(x => x.ColumnName).Where(n => n.Contains(String.Format("Gain_{0}", tilt).ToString())).ToArray();
                    var g = row[String.Format("{0}", columnNames)].ToString();
                    double gain = double.Parse(g);
                    return gain;
                }
            }
            else
            {
                foreach (DataRow row in H_Ang)
                {
                    string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(x => x.ColumnName).Where(n => n.Contains(String.Format("Gain_{0}", tilt).ToString())).ToArray();
                    var g = row[String.Format("{0}", columnNames)].ToString();
                    double gain = double.Parse(g);
                    return gain;
                }
            }
            return 0;

        }
    }
}
