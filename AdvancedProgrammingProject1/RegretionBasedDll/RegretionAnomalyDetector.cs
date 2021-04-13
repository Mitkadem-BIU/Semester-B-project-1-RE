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
        public static double dynamicThreshold = 0.9;
        public static List<CorrelatedFeatures> cf=new List<CorrelatedFeatures>();
        public RegretionAnomalyDetector() { }
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

        public static List<AnomalyReport> LearnAndDetect(DataTable csvTable1, DataTable csvTable2)
        {
            TimeSeries ts1 = new TimeSeries(csvTable1);
            for (int i = 0; i < ts1.GetTable().Count; i++)
            {
                string feature_a, feature_b;
                double features_pearson;
                Line features_line;
                double features_dev_max;
                for (int j = i + 1; j < ts1.GetTable().Count; j++)
                {
                    double[] x = ListToArray(ts1.GetTable(), i);
                    double[] y = ListToArray(ts1.GetTable(), j);
                    features_pearson = anomaly_detection_util.Pearson(x, y, ts1.GetTable().Count);
                    if (Math.Abs(features_pearson) >= dynamicThreshold)
                    {
                        features_line = anomaly_detection_util.Linear_reg(GetAnArrayOfPoints(x, y, ts1), ts1.GetTable().Count);
                        Point p = new Point(GetAnArrayOfPoints(x, y, ts1)[0].GetX(), GetAnArrayOfPoints(x, y, ts1)[0].GetY());
                        features_dev_max = anomaly_detection_util.Dev(p, features_line);
                        for (int l = 1; l < ts1.GetTable().Count; l++)
                        {
                            Point pl = new Point(GetAnArrayOfPoints(x, y, ts1)[l].GetX(), GetAnArrayOfPoints(x, y, ts1)[l].GetY());
                            if (features_dev_max < anomaly_detection_util.Dev(pl, features_line))
                            {
                                features_dev_max = anomaly_detection_util.Dev(pl, features_line);
                            }
                        }
                        feature_a = ts1.GetFeatures()[ts1.GetFeatures().Keys.ElementAt(i)];
                        feature_b = ts1.GetFeatures()[ts1.GetFeatures().Keys.ElementAt(j)];
                        cf.Add(new CorrelatedFeatures(feature_a, feature_b, x.Max(),x.Min(), features_pearson, features_dev_max, features_line, null, false));

                    }
                }
            }

            TimeSeries ts2 = new TimeSeries(csvTable2);
            List<AnomalyReport> anomaly_reports = new List<AnomalyReport>();
            for (int i = 0; i < cf.Count; i++)
            {
                    string feature_a = cf[i].GetFeature1();
                    string feature_b = cf[i].GetFeature2();
                    for (int j = 0; j < ts2.GetFeatures().Count; j++)
                    {
                        for (int k = j + 1; k < ts2.GetFeatures().Count; k++)
                        {
                            if (feature_a == ts2.GetFeatures()[ts2.GetFeatures().Keys.ElementAt(j)] && feature_b == ts2.GetFeatures()[ts2.GetFeatures().Keys.ElementAt(k)])
                            {
                                double[] x = ListToArray(ts2.GetTable(), j);
                                double[] y = ListToArray(ts2.GetTable(), k);
                                for (int l = 0; l < ts2.GetTable().Count; l++)
                                {
                                    Point pl = new Point(GetAnArrayOfPoints(x, y, ts2)[l].GetX(), GetAnArrayOfPoints(x, y, ts2)[l].GetY());
                                    double checkedDev = anomaly_detection_util.Dev(pl, cf[i].GetLin_Reg());
                                    if (cf[i].GetThreshold() * 1.1 < checkedDev)
                                    {
                                        anomaly_reports.Add(new AnomalyReport((feature_a + "-" + feature_b), l + 1));
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
                values.Add(cf[i].GetFeature1() + "-" + cf[i].GetFeature2(), Ls);
            }
            for (int i = 0; i < cf.Count; i++)
            {
                LineSeries Ls = new LineSeries();
                Ls.Points.Add(new DataPoint(cf[i].GetLin_Reg().F(cf[i].GetMaxXFeatures()), cf[i].GetMaxXFeatures()));
                Ls.Points.Add(new DataPoint(cf[i].GetLin_Reg().F(cf[i].GetMinXFeatures()), cf[i].GetMinXFeatures()));
                values.Add(cf[i].GetFeature2() + "-" + cf[i].GetFeature1(), Ls);
            }
            return values;
        }
    }
}
