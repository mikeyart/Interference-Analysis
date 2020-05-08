using CsvHelper;
using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_1546
{
    public class CsvFields
    {
        public string NameTx { get; set; }
        public string NameRx { get; set; }
        public double LatTx { get; set; }
        public double LonTx { get; set; }
        public double Distance { get; set; }
        public double Strength { get; set; }
        public double Gain { get; set; }
        public double Hor_Aten { get; set; }
        public double Vert_Aten { get; set; }
        public double Power_Gain { get; set; }
        public double Power { get; set; }
    }
    public class Output
    {
        public static void WriteHeaders()
        {
            string strFilePath = @"C:\Users\Ciclicci\Desktop\Output\Output.csv";
            string strSeperator = ";";
            StringBuilder sbOutput = new StringBuilder();

            string[][] inaOutput = new string[][]{
                new string[]{"NameTx", "NameRx", "LatTx", "LonTx", "Distance", "E", "Gain", "Hor_Aten", "Vert_Aten", "Power_Gain", "Power"},
                                        };
            int ilength = inaOutput.GetLength(0);
            for (int i = 0; i < ilength; i++)
                sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));

            // Create and write the csv file
            File.WriteAllText(strFilePath, sbOutput.ToString());
        }
        public static void WriteCSV(string nameTx, string nameRx, double latTx, double lonTx, double distance, double E, double gain, double HorAten, double VertAten, double powerGain, double power)
        {
            var data = new[]
            {
                new CsvFields {NameTx = nameTx, NameRx = nameRx, LatTx = latTx, LonTx = lonTx, Distance = distance, Strength = E, Gain = gain, Hor_Aten = HorAten, Vert_Aten = VertAten, Power_Gain = powerGain, Power = power  }
            };

            var mem = new MemoryStream();
            var writer = new StreamWriter(mem);
            var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture);

            csvWriter.Configuration.Delimiter = ";";
            csvWriter.Configuration.HasHeaderRecord = false;
            csvWriter.Configuration.AutoMap<CsvFields>();

            csvWriter.WriteRecords(data);

            writer.Flush();
            var result = Encoding.UTF8.GetString(mem.ToArray());
            File.AppendAllText(@"C:\Users\Ciclicci\Desktop\Output\Output.csv", result);
        }

        public static void WriteDoc(Document doc, string nameRx, double latRx, double longRx, Style style)
        {
            Point point = new Point();
            point.Coordinate = new Vector(latRx, longRx);

            Placemark placemark = new Placemark();
            placemark.Name = nameRx;
            placemark.Geometry = point;
            placemark.StyleUrl = new Uri(style.Id.ToString(), UriKind.Relative);
            doc.AddFeature(placemark);
        }

        public static Style Green()
        {
            var style = new Style();
            style.Id = "GreenLabel";
            style.Label = new LabelStyle();
            style.Label.Color = new Color32(255, 0, 255, 0);
            return style;
        }

        public static Style Red()
        {
            var style = new Style();
            style.Id = "RedLabel";
            style.Label = new LabelStyle();
            style.Icon.Color = new Color32(255, 0, 0, 255);
            return style;
        }

        public static void GetStyle(double E, Style style)
        {
            if (E < -110)
                style = Green();
            else
                style = Red();
        }

        public static void WriteKML(Document doc)
        {
            Kml kml = new Kml();
            kml.Feature = doc;
            Serializer serializer = new Serializer();
            serializer.Serialize(kml);
            Console.WriteLine(serializer.Xml);
            File.AppendAllText(@"C:\Users\Ciclicci\Desktop\Output\Output.kml", serializer.Xml.ToString());

        }

    }
}
