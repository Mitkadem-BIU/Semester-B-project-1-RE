using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    class StatisticMethods
    {
        public static double Variance(List<double> x)
        {
            double sqsum = 0;
            foreach (double val in x)
            {
                sqsum += val * val;
            }
            return sqsum / x.Count() - x.Average() * x.Average();
        }

        public static double Pearson(List<double> x, List<double> y)
        {
            return Covariance(x, y) / Math.Sqrt(Variance(x) * Variance(y));
        }

        public static LineSeries LinearReg(List<double> x, List<double> y)
        {
            double a = Covariance(x, y) / Variance(x);
            double b = y.Average() - a * x.Average();
            LineSeries LS = new LineSeries();
            double lowerLim = Math.Min(x.Min() * 0.8, x.Min() * 1.2);
            double upperLim = Math.Max(x.Max() * 0.8, x.Max() * 1.2);
            for (double i = lowerLim; i <= upperLim; i += 0.01)
                LS.Points.Add(new DataPoint(i, a * i + b));
            return LS;
        }

        public static double Covariance(List<double> x, List<double> y)
        {
            List<double> covarr = new List<double>();
            for (int i = 0; i < x.Count(); i++)
                covarr.Add((x[i] - x.Average()) * (y[i] - y.Average()));
            return covarr.Average();
        }
    }
}
