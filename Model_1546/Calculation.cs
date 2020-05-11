using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_1546
{
    public class Calculation
    {
        public static void Interference()
        {
            double Distance, Heff, TCA, HorAten, VertAten, Gain, E, Powgain, Pow;
            double SumPow = 0;
            double aux = 0;

            var Doc = new Document();
            var doc = new Document();


            double[] LatRx = Station_Add.GetLatRx();
            double[] LongRx = Station_Add.GetLonRx();
            List<double> LatTx = BRIFIC_Database.GetLatTx();
            List<double> LongTx = BRIFIC_Database.GetLonTx();
            List<double> PowerTx = BRIFIC_Database.GetPowerTx();

            List<double> Power = new List<double>();
            IEnumerable<double> Top;

            double[] HeightRx = Station_Add.GetHeightRx();
            List<double> HeightTx = BRIFIC_Database.GetHeightTx();

            string[] NameRx = Station_Add.GetNameRx();
            List<string> NameTx = BRIFIC_Database.GetNameTx();

            int[] Azimuth = Station_Add.GetAzimuthRx();
            int[] Tilt = Station_Add.GetTiltRx();

            Output.WriteHeaders();

            for (int i = 0; i < NameRx.Length; i++)
            {
                var style = new Style();
                for (int j = 0; j < NameTx.Count; j++)
                {
                    var nCoord = new GeoCoordinate(LatRx[i], LongRx[i]);
                    var eCoord = new GeoCoordinate(LatTx[j], LongTx[j]);

                    Distance = Math.Round(nCoord.GetDistanceTo(eCoord) / 1000, 1);
                    Heff = Calculate_Field.Heff(LatTx[j], LongTx[j], 15, HeightTx[j]);
                    TCA = Calculate_Field.TCA(LatTx[j], LongTx[j], LatRx[i], LongRx[i]);
                    HorAten = Antenna.GetHorAten(Azimuth[i], Tilt[i]);
                    VertAten = Antenna.GetVertAten(LatTx[j], LongTx[j], LatRx[i], LongRx[i], HeightRx[i], Tilt[i]);
                    Gain = Antenna.GetGain(Tilt[i]) - HorAten - VertAten;

                    E = Calculate_Field.CalculateField(Distance, 10, 700, Heff, "a", 0, false, TCA, PowerTx[j], 40);
                    Powgain = E - 20 * Math.Log10(0.7) - 137.2 + Math.Round(Gain, 2);
                    Power.Add(Powgain);
                    Pow = E - 20 * Math.Log10(0.7) - 137.2;
                    Output.WriteCSV(NameTx[j], NameRx[i], LatTx[j], LongTx[j], Distance, E, Gain, HorAten, VertAten, Powgain, Pow);
                }

                Top = Power.OrderByDescending(x => x).Take(10);
                foreach (var item in Top)
                {
                    aux += Math.Pow(10, item / 10);

                }
                SumPow = 10 * Math.Log10(aux);
                Power.Clear();
                Output.GetStyle(SumPow, style);
                Output.WriteDoc(Doc, NameRx[i], LatRx[i], LongRx[i], style);
                style = null;
            }
            Output.WriteKML(Doc);
        }
    }
}
