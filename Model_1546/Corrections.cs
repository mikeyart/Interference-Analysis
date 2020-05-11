using System;


namespace Model_1546
{
    public class Corrections
    {
        public static double TerrainClearanceAngleCorrectionTx(double angle, int freq)
        {
            double v_prim, v, Jv, Jv_prim, corr;

            if (angle < 0)
                return 0;
            else
            {
                v_prim = 0.036 * Math.Sqrt(freq);
                v = 0.065 * angle * Math.Sqrt(freq);
                Jv = 6.9 + 20.0 * Math.Log10(v - 0.1 + Math.Sqrt(Math.Pow(v - 0.1, 2) + 1));
                Jv_prim = 6.9 + 20.0 * Math.Log10(v_prim - 0.1 + Math.Sqrt(Math.Pow(v_prim - 0.1, 2) + 1));
                corr = Jv_prim - Jv;
                return corr;
            }

        }

        private static double TerrainClearanceAngleCorrectionRx(double angle, int freq)
        {
            double v_prim, v, Jv, Jv_prim,corr;

            if (angle < 0.55)
                return 0;
            else
            {
                if (angle > 40)
                    angle = 40;
                v_prim = 0.036 * Math.Sqrt(freq);
                v = 0.065 * angle * Math.Sqrt(freq);
                Jv = 6.9 + 20.0 * Math.Log10(v - 0.1 + Math.Sqrt(Math.Pow(v - 0.1, 2) + 1));
                Jv_prim = 6.9 + 20.0 * Math.Log10(v_prim - 0.1 + Math.Sqrt(Math.Pow(v_prim - 0.1, 2) + 1));
                corr = Jv_prim - Jv;
                return corr;
            }
        }

        private static double Ang( double value)
        {
            double angle = value + 90;
            return (angle + value) % 360;
        }

        public static double GetC_h1(int freq, double h)
        {
            double Teta_eff, v, k_v, Jv, correction;

            Teta_eff = Ang(Math.Atan(-1 * h / 9000.0));
            if (freq == 2000)
            {
                k_v = 6.0;
                return k_v;
            }
            else if (freq == 600)
            {
                k_v = 3.31;
                return k_v;
            }
            else if (freq == 100)
            {
                k_v = 1.35;

                v = k_v * Teta_eff;

                if (v <= -0.7806)
                {
                    Jv = 0;
                    return Jv;
                }
                else
                {
                    Jv = 6.9 + 20 * Math.Log10(v - 0.1 + Math.Sqrt(Math.Pow(v - 0.1, 2) + 1));
                    correction = 6.03 - Jv;
                    return correction;
                }
            }
            else { return 0; }
        }

        private static double rec_corr(double distance, string path, int time, double height, int freq, string option43, double angle, bool use_rTCA, double rTCA)
        {
            double fsr, rTCA_correction;

            fsr = Frequency_Interpolation.FrequencyInterpolation(distance, path, time, height, freq, option43, angle);
            if (use_rTCA == false)
                return fsr;
            else
            {
                rTCA_correction = TerrainClearanceAngleCorrectionRx(rTCA, freq);
                return fsr + rTCA_correction;
            }
        }

        public static double fsl(double distance, int time, double height, int freq, string option43, double angle, bool use_rTCA, double rTCA, double power)
        {
            double E;

            E = rec_corr(distance, "land", time, height, freq, option43, angle, use_rTCA, rTCA);
            E = E + (power - 30);
            return E;
        }
    }
}
