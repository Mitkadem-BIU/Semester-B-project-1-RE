using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace AnomalyDetectionDll
{
    public class RegretionAnomalyDetector
    {
        public static double dynamicThreshold = 0.3;
        public static List<CorrelatedFeatures> cf = new List<CorrelatedFeatures>();
        public RegretionAnomalyDetector()
        {
            // Console.WriteLine("constructor");
        }
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

        public static void Learn(DataTable csvTable)
        {
            TimeSeries ts = new TimeSeries(csvTable);
            for (int i = 0; i < ts.GetTable()[0].Count; i++)
            {
                string feature_a, feature_b;
                double features_pearson;
                Line features_line;
                double features_dev_max;
                for (int j = i + 1; j < ts.GetTable()[0].Count; j++)
                {
                    double[] x = ListToArray(ts.GetTable(), i);
                    double[] y = ListToArray(ts.GetTable(), j);
                    features_pearson = anomaly_detection_util.Pearson(x, y, ts.GetTable().Count);
                    if (Math.Abs(features_pearson) >= dynamicThreshold)
                    {
                        features_line = anomaly_detection_util.Linear_reg(GetAnArrayOfPoints(x, y, ts), ts.GetTable().Count);
                        Point p = new Point(GetAnArrayOfPoints(x, y, ts)[0].GetX(), GetAnArrayOfPoints(x, y, ts)[0].GetY());
                        features_dev_max = anomaly_detection_util.Dev(p, features_line);
                        for (int l = 1; l < ts.GetTable()[0].Count; l++)
                        {
                            Point pl = new Point(GetAnArrayOfPoints(x, y, ts)[l].GetX(), GetAnArrayOfPoints(x, y, ts)[l].GetY());
                            double checkedDev = anomaly_detection_util.Dev(pl, features_line);
                            if (features_dev_max < checkedDev)
                            {
                                features_dev_max = checkedDev;
                            }
                        }
                        feature_a = ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(i)];
                        feature_b = ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(j)];
                        cf.Add(new CorrelatedFeatures(feature_a, feature_b, x.Max(),x.Min(), features_pearson, features_dev_max, features_line, null, false));
                    }
                }
            }
        }
        public static List<long> DetectTime(DataTable csvTable)
        {
            List<AnomalyReport> anomalyReports = Detect(csvTable);
            List<long> times = new List<long>();
            foreach (var ar in anomalyReports)
                times.Add(ar.TimeStep);

            return times;
        }

        public static List<AnomalyReport> Detect(DataTable csvTable)
        {
            TimeSeries ts = new TimeSeries(csvTable);
            List<AnomalyReport> anomaly_reports = new List<AnomalyReport>();
            for (int i = 0; i < cf.Count; i++)
            {
                string feature_a = cf[i].GetFeature1();
                string feature_b = cf[i].GetFeature2();
                for (int j = 0; j < ts.GetFeatures().Count; j++)
                {
                    for (int k = j + 1; k < ts.GetFeatures().Count; k++)
                    {
                        if (feature_a == ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(j)] && feature_b == ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(k)])
                        {
                            double[] x = ListToArray(ts.GetTable(), j);
                            double[] y = ListToArray(ts.GetTable(), k);
                            for (int l = 0; l < ts.GetTable().Count; l++)
                            {
                                Point pl = new Point(GetAnArrayOfPoints(x, y, ts)[l].GetX(), GetAnArrayOfPoints(x, y, ts)[l].GetY());
                                double checkedDev = anomaly_detection_util.Dev(pl, cf[i].GetLin_Reg());
                                if (cf[i].GetThreshold() < checkedDev)
                                {
                                    anomaly_reports.Add(new AnomalyReport((feature_a + " - " + feature_b), l + 1));
                                }
                            }
                        }
                    }
                }
            }
            return anomaly_reports;
        }

        public static Dictionary<string, LineSeries> ValuesToLine()
        {
            Dictionary<string, LineSeries> values = new Dictionary<string, LineSeries>();
            for (int i = 0; i < cf.Count; i++)
            {
                LineSeries Ls = new LineSeries();
                Ls.Points.Add(new DataPoint(cf[i].GetMaxXFeatures(), cf[i].GetLin_Reg().F(cf[i].GetMaxXFeatures())));
                Ls.Points.Add(new DataPoint(cf[i].GetMinXFeatures(), cf[i].GetLin_Reg().F(cf[i].GetMinXFeatures())));
                values.Add(cf[i].GetFeature1() + " - " + cf[i].GetFeature2(), Ls);
            }
            for (int i = 0; i < cf.Count; i++)
            {
                LineSeries Ls = new LineSeries();
                Ls.Points.Add(new DataPoint(cf[i].GetLin_Reg().F(cf[i].GetMaxXFeatures()), cf[i].GetMaxXFeatures()));
                Ls.Points.Add(new DataPoint(cf[i].GetLin_Reg().F(cf[i].GetMinXFeatures()), cf[i].GetMinXFeatures()));
                values.Add(cf[i].GetFeature2() + " - " + cf[i].GetFeature1(), Ls);
            }
            return values;
        }
    }
}
