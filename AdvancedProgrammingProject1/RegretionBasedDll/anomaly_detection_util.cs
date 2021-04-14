using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDetectionDll
{
    public class anomaly_detection_util
    {
        public static double Avg(double[] x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i];
            }
            return sum / size;
        }

        public static double Var(double[] x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += Math.Pow(x[i] - Avg(x, size), 2);
            }
            return sum / size;
        }

        public static double Cov(double[] x, double[] y, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];

            }
            return (sum / size) - Avg(x, size) * Avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        public static double Pearson(double[] x, double[] y, int size)
        {
            return Cov(x, y, size) / (Math.Sqrt(Var(x, size)) * Math.Sqrt(Var(y, size)));
        }

        // performs a linear regression and returns the line equation
        public static Line Linear_reg(Point[] points, int size)
        {

            double[] x = new double[size];
            double[] y = new double[size];

            for (int i = 0; i < size; i++)
            {
                x[i] = points[i].GetX();
                y[i] = points[i].GetY();
            }

            double a = Cov(x, y, size) / Var(x, size);
            double b = Avg(y, size) - a * Avg(x, size);

            return new Line(a, b);
        }

        // returns the deviation between point p and the line equation of the points
        public static double Dev(Point p, Point[] points, int size)
        {
            Line l = Linear_reg(points, size);
            return Math.Abs(l.F(p.GetX()) - p.GetY());
        }

        // returns the deviation between point p and the line
        public static double Dev(Point p, Line l)
        {
            return Math.Abs(l.F(p.GetX()) - p.GetY());
        }
    }
}
