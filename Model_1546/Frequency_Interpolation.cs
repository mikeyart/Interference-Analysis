using System;
using System.Linq;

namespace Model_1546
{
    public class Frequency_Interpolation
    {
        public static double FrequencyInterpolation(double distance, string path, int time, double height, int freq, string option43, double angle)
        {
            int[] Frequencies = new int[] { 100, 600, 2000 };
            int f_inf, f_sup;
            double E_inf, E_sup,E;

            if (Frequencies.Contains(freq))
                return Distance_Interpolation.DistanceInterpolation(distance, path, time, height, freq, option43, angle);
            else
            {
                Array.Resize(ref Frequencies, Frequencies.Length + 1);
                Frequencies[Frequencies.GetUpperBound(0)] = freq;
                Array.Sort(Frequencies);
                int i = Array.IndexOf(Frequencies, freq);
                f_inf = Frequencies[i - 1];
                f_sup = Frequencies[i + 1];
                E_inf = Distance_Interpolation.DistanceInterpolation(distance, path, time, height, f_inf, option43, angle);
                E_sup = Distance_Interpolation.DistanceInterpolation(distance, path, time, height, f_sup, option43, angle);
                E = E_inf + (E_sup - E_inf) * Math.Log10(freq * 1.0 / f_inf * 1.0) / Math.Log10(f_sup * 1.0 / f_inf * 1.0);
                return E;
            }
        }


        
    }
}
