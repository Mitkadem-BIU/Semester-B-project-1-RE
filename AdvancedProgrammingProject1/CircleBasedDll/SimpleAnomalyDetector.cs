using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircleBasedDll
{
    public class SimpleAnomalyDetector
    {
        static double dynamicThreshold = 0.9;
        static List<CorrelatedFeatures> cf;
        public SimpleAnomalyDetector() { }
        ~SimpleAnomalyDetector() { cf.Clear(); }

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

        public static void LearnNormal(TimeSeries ts)
        {
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
                        for (int l = 1; l < ts.GetTable().Count; l++)
                        {
                            Point pl = new Point(GetAnArrayOfPoints(x, y, ts)[l].GetX(), GetAnArrayOfPoints(x, y, ts)[l].GetY());
                            if (features_dev_max < anomaly_detection_util.Dev(pl, features_line))
                            {
                                features_dev_max = anomaly_detection_util.Dev(pl, features_line);
                            }
                        }
                        feature_a = ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(i)];
                        feature_b = ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(j)];
                        cf.Add(new CorrelatedFeatures(feature_a, feature_b, features_pearson, features_dev_max, features_line, null, false));
                    }
                    else if (Math.Abs(features_pearson) > 0.5 && Math.Abs(features_pearson) < dynamicThreshold)
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

        public static List<AnomalyReport> Detect(TimeSeries ts){
            List<AnomalyReport> anomaly_reports = new List<AnomalyReport>();
            for (int i = 0; i< cf.Count; i++){
                if(!cf[i].IsHybrid()){
                    string feature_a = cf[i].GetFeature1();
                    string feature_b = cf[i].GetFeature2(); 
                    for (int j = 0; j<ts.GetFeatures().Count; j++){
                        for (int k = j+1; k< ts.GetFeatures().Count; k++){
                            if(feature_a== ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(j)] && feature_b==ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(k)])
                            {
                                double[] x = ListToArray(ts.GetTable(), j);
                                double[] y = ListToArray(ts.GetTable(), k);
                                for (int l = 0; l< ts.GetTable().Count; l++){
                                Point pl=new Point(GetAnArrayOfPoints(x, y, ts)[l].GetX(), GetAnArrayOfPoints(x, y, ts)[l].GetY());
                                double checkedDev = anomaly_detection_util.Dev(pl, cf[i].GetLin_Reg());
                                if(cf[i].GetThreshold()*1.1<checkedDev){
                                    anomaly_reports.Add(new AnomalyReport((feature_a + "-" + feature_b),l+1));
                                }
                            }                 
                        }
                    }
                }
            }
        }
        return anomaly_reports;
        }
    }
}
