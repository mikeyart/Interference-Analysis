using System;
using System.IO;
using System.Linq;
using System.Data;

namespace Model_1546
{
    public class Field_Strength
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

        public static double getMaxFieldStrength(int distance, string path, int time)
        {
            double e_fs = 106.9 - 20 * Math.Log10(distance);
            return e_fs;
        }

        public string GetTabFieldStrength(double distance, string path, int time, double height, int freq)
        {
            string filename = "Tabulated.csv";
            string path_file = Path.Combine(Environment.CurrentDirectory, filename);
            DataTable dt = ConvertCSVtoDataTable(path_file);

            DataRow[] Rows = dt.Select(String.Format("Frequency = '{0}' AND Path = '{1}' AND Time = '{2}' AND Values = '{3}'", freq, path, time, distance));

            foreach (DataRow row in Rows)
            {
                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                         .Select(x => x.ColumnName).Where(n => n.Contains(height.ToString())).ToArray();
                var name = row[String.Format("{0}", columnNames)].ToString();
                return name;
            }
            return null;
        }
    }
}
