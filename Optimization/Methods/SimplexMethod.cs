using Optimization.Tasks;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.Methods
{
    
    internal class SimplexMethod : IMethod
    {
        private class Point2D
        {
            public double X { get; set; }
            public double Y { get; set; }
        }
        public ExtremumType Extremum { get; set; } = ExtremumType.Minimum;
        public ITask? Task { get; set; }
        public double Epsilon { get; set; }
        private int n { get; set; } = 2;
        public int N { get; set; }
        public SimplexMethod() 
        {
            N = n + 1;
        }
        public (double, double, double, double) Solve()
        {
            if (Task is null)
                throw new Exception();
            double T1 = 0, T2 = 0, compQuan = 0, res = 0;
            if (Extremum == ExtremumType.Minimum)
            {
                //(T1, T2, Res) = MinSolve();
            }
            else
            {
                //(T1, T2, Res) = MaxSolve();
            }
            return (T1, T2, compQuan, res);
        }
        private (double a, double b) GetLineCoef(Point2D p1, Point2D p2)
        {
            double a = (p2.Y - p1.Y) / (p2.X - p1.X);
            double b = p1.Y - a * p1.X;
            return (a, b);
        }
        private Point2D GetSymmetricPoint(Point2D point, double a, double b)
        {
            var symPoint = new Point2D();
            var midPoint = new Point2D();
            double coefA = -(1 / a);
            double coefB = point.Y - coefA * point.X;
            midPoint.X = (coefB - b) / (a - coefA);
            midPoint.Y = coefA * midPoint.X + coefB;
            symPoint.X = 2 * midPoint.X - point.X;
            symPoint.Y = 2 * midPoint.Y - point.Y;
            return symPoint;
        }
    }
}
