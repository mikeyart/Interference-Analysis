using System;
using System.Linq;
using F = Model_1546.Field_Strength;
using TCA = Model_1546.Corrections;


namespace Model_1546
{
    public class Height_Interpolation
    {
        public static double HeightInterpolation(double distance, string path, int time, double height, int freq, string option_43, double angle)
        {
            F field = new F();
            double[] Heights = new double[] { 10, 20, 37.5, 75, 150, 300, 600, 1200 };
            double h_inf, h_sup, E_inf, E_sup, x1, x2, E, E_10, E_20,C_1020,C_h1neg10,E_zero;
            string E1, E2, E3;

            //Transmitting antenna height 1 in range 10...3000 meters
            if (height >=10 && height <3000)
            {
                if (!Heights.Contains(height))
                {
                    Array.Resize(ref Heights, Heights.Length + 1);
                    Heights[Heights.GetUpperBound(0)] = height;
                    Array.Sort(Heights);
                    int i = Array.IndexOf(Heights, height);
                    h_inf = Heights[i - 1];
                    h_sup = Heights[i + 1];
                    E1 = field.GetTabFieldStrength(distance, path, time, h_inf, freq);
                    E2 = field.GetTabFieldStrength(distance, path, time, h_sup, freq);

                    E_inf = Convert.ToDouble(E1);
                    E_sup = Convert.ToDouble(E2);

                    x1 = Math.Log10(height / h_inf);
                    x2 = Math.Log10(h_sup / h_inf);

                    E = E_inf + (E_sup - E_inf) * (x1 / x2);
                    return E;
                }
                else
                {
                    E3 = field.GetTabFieldStrength(distance, path, time, height, freq);
                    E = Convert.ToDouble(E3);
                    return E;
                }
            }

            //Transmitting antenna height 1 in range 0...10 meters
            else if (height >=0 && height <10)
            {
                E_10 = HeightInterpolation(distance, path, time, height = 10, freq, option_43 = "a",  angle);
                E_20 = HeightInterpolation(distance, path, time, height = 20, freq, option_43 = "a",  angle);

                C_1020 = E_10 - E_20;
                C_h1neg10 = TCA.GetC_h1(freq, -10);
                E_zero = E_10 + 0.5 * (C_1020 + C_h1neg10);
                E = E_zero + 0.1 * height * (E_10 - E_zero);
                return E;
            }

            //Negative effective height
            else
            {
                if (option_43 == "a")
                {
                    E = HeightInterpolation(distance, path, time, height = 0, freq, option_43 = "a", angle);
                    return E + TCA.TerrainClearanceAngleCorrectionTx(angle, freq);
                }

                else if (option_43 == "b")
                {
                    E = HeightInterpolation(distance, path, time, height = 0, freq, option_43 = "b",angle);
                    return E + TCA.GetC_h1(freq, height);
                }
                else { return 0; } 
            }
        }
    }
}
   


