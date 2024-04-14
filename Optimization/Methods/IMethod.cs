using Optimization.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.Methods
{
    interface IMethod
    {
        public double Epsilon { get; set; }
        public ExtremumType Extremum { get; set; }
        public (double, double, double, double) Solve();
    }
}
