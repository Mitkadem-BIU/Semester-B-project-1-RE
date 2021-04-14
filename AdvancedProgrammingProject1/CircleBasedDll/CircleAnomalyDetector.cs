using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AnomalyDetectionDll
{
    public class CircleAnomalyDetector
    {
        static double dynamicThreshold = 0;
        static List<CorrelatedFeatures> cf = new List<CorrelatedFeatures>();
        public static double[] ListToArray(List<List<double>> v, double n)
        {
            double[] array = new double[v.Count];
            for (int i = 0; i <v.Count; i++)
            {
                array[i] = v[i].IndexOf(n);
            }
            return array;
        }

        public static Point[] GetAnArrayOfPoints(double[] x, double[] y, TimeSeries ts)
        {
            Point[] points = new Point[ts.GetTable().Count];
            for (int i = 0; i < ts.GetTable().Count; i++)
            {
                points[i] = new Point(x[i], y[i]);
            }
            return points;
        }

        public static List<CorrelatedFeatures> GetNormalModel()
        {
            return cf;
        }

        public static void SetNormalModel(List<CorrelatedFeatures> CF)
        {
            cf = CF;
        }

        public static void LearnNormal(DataTable csvTable)
        {
            TimeSeries ts = new TimeSeries(csvTable);
            for (int i = 0; i < ts.GetTable()[0].Count; i++)
            {
                string feature_a, feature_b;
                double features_pearson;
                Line features_line;
                for (int j = i + 1; j < ts.GetTable()[0].Count; j++)
                {
                    double[] x = ListToArray(ts.GetTable(), i);
                    double[] y = ListToArray(ts.GetTable(), j);
                    features_pearson = anomaly_detection_util.Pearson(x, y, ts.GetTable().Count);
                    /*if (Math.Abs(features_pearson) > 0.5 && Math.Abs(features_pearson) < dynamicThreshold)*/
                    if(Math.Abs(features_pearson) >= dynamicThreshold)
                    {
                        features_line = anomaly_detection_util.Linear_reg(GetAnArrayOfPoints(x, y, ts), ts.GetTable().Count);
                        feature_a = ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(i)];
                        feature_b = ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(j)];
                        Circle c = minCircle.FindMinCircle(GetAnArrayOfPoints(x, y, ts), ts.GetTable().Count);
                        cf.Add(new CorrelatedFeatures(feature_a, feature_b, features_pearson, c.GetRadius(), features_line, c, true));
                    }
                }
            }
        }
    }
}
