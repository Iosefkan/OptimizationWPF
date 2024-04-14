using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.Tasks
{
    class SecondTask : ITask
    {
        public double G { get; set; } = 2;
        public double A { get; set; } = 1;
        public double V { get; set; } = 2;
        public double S { get; set; } = 1500;
        public double Alpha { get; set; } = 1;
        public double Beta { get; set; } = 1;
        public double Mu { get; set; } = 1;
        public double T1From { get; set; } = -3;
        public double T1To { get; set; } = 3;
        public double T2From { get; set; } = -3;
        public double T2To { get; set; } = 6;
        public double TDiff { get; set; } = 2;
        public double Equation(double T1, double T2)
        {
            double res = Alpha * (G * Mu * (Math.Pow(T2 - T1, V) + Math.Pow(Beta * A - T1, V)));
            return res;
        }
        public bool Validate()
        {
            if (Alpha < 0 || Alpha > 1)
                return false;
            if (Beta < 0 || Beta > 1)
                return false;
            if (Mu < 0 || Mu > 1)
                return false;
            if (T1To < -273 || T1From < -273 || T1From > T1To)
                return false;
            if (T2To < -273 || T2From < -273 || T2From > T2To)
                return false;
            if (A < 0 || G < 0 || S < 0 || V < 0)
                return false;
            return true;
        }
    }
}
