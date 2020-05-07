using System;
using System.Collections.Generic;
using System.Linq;


namespace Model_1546
{
    public static class MyEnumerable
    {
        public static IEnumerable<double> OneRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 1)
            {
                yield return i;
            }
        }

        public static IEnumerable<double> FiveRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 5)
            {
                yield return i;
            }
        }

        public static IEnumerable<double> TenRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 10)
            {
                yield return i;
            }
        }

        public static IEnumerable<double> TwentyFiveRange(int start, int count)
        {
            for (int i = start; i < start + count; i += 25)
            {
                yield return i;
            }
        }
    }

    public class Distance_Interpolation
    {
        public static double DistanceInterpolation(double distance, string path, int time, double height, int freq, string option43, double angle)
        {
            List<double> D1 = MyEnumerable.OneRange(1, 19).ToList();
            List<double> D2 = MyEnumerable.FiveRange(20, 80).ToList();
            List<double> D3 = MyEnumerable.TenRange(100, 100).ToList();
            List<double> D4 = MyEnumerable.TwentyFiveRange(200, 825).ToList();

            var Distances = D1.Concat(D2)
                              .Concat(D3)
                              .Concat(D4)
                              .ToList();
            double E_inf, E_sup, E;
            double d_inf, d_sup;
            int i;

             if (Distances.Contains(distance))
                 return Height_Interpolation.HeightInterpolation(distance, path, time, height, freq, option43, angle);
             else
             {
                Distances.Add(distance);
                Distances.Sort();
                i = Distances.IndexOf(distance);
                 d_inf = Distances[i - 1];
                 d_sup = Distances[i + 1];

                 E_inf = Height_Interpolation.HeightInterpolation(d_inf, path, time, height, freq, option43, angle);
                 E_sup = Height_Interpolation.HeightInterpolation(d_sup, path, time, height, freq, option43, angle);
                 E = E_inf + (E_sup - E_inf) * Math.Log10(distance * 1.0 / d_inf * 1.0) / Math.Log10(d_sup * 1.0 / d_inf * 1.0);
                 return E;
             }             
        }

    }
}
