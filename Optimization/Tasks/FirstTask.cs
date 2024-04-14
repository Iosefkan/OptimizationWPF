using SciChart.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Optimization.Tasks
{
    class FirstTask : ITask
    {
        public double A { get; set; } = 1;
        public double G { get; set; } = 1;
        public double S { get; set; } = 1000;
        public double T1From { get; set; } = -18;
        public double T2From { get; set; } = -8;
        public double T1To { get; set; } = 7;
        public double T2To { get; set; } = 8;
        public double TDiff { get; set; } = 1;
        public double Alpha { get; set; } = 1;
        public double Beta { get; set; } = 1;
        public double Mu { get; set; } = 1;
        public double Delta { get; set; } = 1;
        public double Equation(double T1, double T2)
        {
            double res = Alpha * G * (Math.Pow(T2 - Beta * A, 2) + Mu * Math.Pow(Math.Exp(T1 + T2), 2) + Delta * (T2 - T1));
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
            if (Delta < 0 || Delta > 1)
                return false;
            if (T1To < -273 || T1From < -273 || T1From > T1To)
                return false;
            if (T2To < -273 || T2From < -273 || T2From > T2To)
                return false;
            if (A < 0 || G < 0 || S < 0) 
                return false;
            return true;
        }
    }
}
