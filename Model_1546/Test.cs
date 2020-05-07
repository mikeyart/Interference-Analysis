using SRTM;
using SRTM.Sources.NASA;
using SRTM.Sources.USGS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model_1546
{
    public class Test
    {
        public static double ReadTerrain(double latitude, double longitude)
        {
            var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_USGS", new USGSSource());

            //var credentials = new NetworkCredential("mikeyart", "Prince5498");
            //var srtmData = new SRTMData(@"C:\Users\Ciclicci\Desktop\SRTM_NASA", new NASASource(credentials));

            int? elevation1 = srtmData.GetElevation(latitude, longitude);
            double elevation = Convert.ToDouble(elevation1);

            return elevation;
        }

        public static double Bearing(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 *= (Math.PI / 180);
            lon1 *= (Math.PI / 180);
            lat2 *= (Math.PI / 180);
            lon2 *= (Math.PI / 180);
            double y = lon2 - lon1;

            double dX = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(y);
            double dY = Math.Cos(lat2) * Math.Sin(y);

            double Azimuth = Math.Atan2(dX, dY);

            double bearing = Azimuth * 180 / Math.PI;
            return bearing;
        }

        public static double coords(double lat, double lon, double distance, double theta)
        {
            double lat2, lon2, angdist, height;
            lat *= (Math.PI / 180);
            lon *= (Math.PI / 180);

            theta *= (Math.PI / 180);
            angdist = distance / 6371;
            lat2 = Math.Asin(Math.Sin(lat) * Math.Cos(angdist) + Math.Cos(lat) * Math.Sin(angdist) * Math.Cos(theta));

            double forAtana = Math.Sin(theta) * Math.Sin(angdist) * Math.Cos(lat);
            double forAtanb = Math.Cos(angdist) - Math.Sin(lat) * Math.Sin(lat2);

            lon2 = lon + Math.Atan2(forAtana, forAtanb);

            double finalLat = lat2 * 180 / Math.PI;
            double finalLon = lon2 * 180 / Math.PI;

            height = ReadTerrain(finalLat, finalLon);
            return height;
        }


        public static double TCA()
        {
            double bearing = Bearing(47.341232, 29.426484, 47.064436, 28.456821);
            Dictionary<double,double> h1 = new Dictionary<double,double>();
            double height = 0;
            for (double dist = 0; dist < 15; dist += 0.1)
            {
                height = coords(47.341232, 29.426484, dist, bearing);
                h1.Add(dist, height);
            }
            double max = h1.Values.Max();
            var distance = Math.Round(h1.FirstOrDefault(x => x.Value == max).Key,2);
            double ang1 = Math.Atan2(distance*1000, max) * 180 / Math.PI;
            double tca = Math.Round(90 - ang1,1);
            return tca;
        }

    }
}