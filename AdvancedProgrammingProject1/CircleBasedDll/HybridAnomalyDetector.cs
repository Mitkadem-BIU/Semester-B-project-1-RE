using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AnomalyDetectionDll
{
    public class HybridAnomalyDetector 
    {
        public HybridAnomalyDetector() { }
        public static void Learn(DataTable csvTable)
        {
            CircleAnomalyDetector.LearnNormal(csvTable);
        }

        public static List<AnomalyReport> Detect(DataTable csvTable)
        {
            TimeSeries ts = new TimeSeries(csvTable);
            List<AnomalyReport> anomaly_reports = new List<AnomalyReport>();
            List<CorrelatedFeatures> cf = CircleAnomalyDetector.GetNormalModel();
            for (int i = 0; i < cf.Count; i++)
            {
                    string feature_a = cf[i].GetFeature1();
                    string feature_b = cf[i].GetFeature2();
                    cf[i].SetThreshold(cf[i].GetThreshold() * 1.1);
                    cf[i].GetMinCircle().SetRadius(cf[i].GetThreshold());
                    for (int j = 0; j < ts.GetFeatures().Count; j++)
                    {
                        for (int k = j + 1; k < ts.GetFeatures().Count; k++)
                        {
                            if (feature_a == ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(j)] && feature_b == ts.GetFeatures()[ts.GetFeatures().Keys.ElementAt(k)])
                            {
                                double[] x = CircleAnomalyDetector.ListToArray(ts.GetTable(), j);
                                double[] y = CircleAnomalyDetector.ListToArray(ts.GetTable(), k);

                                for (int l = 0; l < ts.GetTable().Count; l++)
                                {
                                    Point pl = new Point(CircleAnomalyDetector.GetAnArrayOfPoints(x, y, ts)[l].GetX(), CircleAnomalyDetector.GetAnArrayOfPoints(x, y, ts)[l].GetY());
                                    if (!minCircle.IsPointInsideCircle(cf[i].GetMinCircle(), pl))
                                    {
                                        AnomalyReport anomaly_report = new AnomalyReport((feature_a + " - " + feature_b), l + 1);
                                        anomaly_reports.Add(anomaly_report);
                                    }
                                }
                            }
                        }
                    }
            }
            CircleAnomalyDetector.SetNormalModel(cf);
            return anomaly_reports;
        }

        public static Dictionary<string, LineSeries> ValuesToCircle()
        {
            Dictionary<string, LineSeries> values = new Dictionary<string, LineSeries>();
            List<CorrelatedFeatures> cf = CircleAnomalyDetector.GetNormalModel();
            
            for (int i = 0; i < cf.Count; i++)
            {
                LineSeries Ls = new LineSeries();
                for (double t = 0;  t < 2*Math.PI + 0.1; t+=0.3)
                {
                    double x = cf[i].GetMinCircle().GetRadius() * Math.Cos(t) + cf[i].GetMinCircle().GetCenter().GetX();
                    double y = cf[i].GetMinCircle().GetRadius() * Math.Sin(t) + cf[i].GetMinCircle().GetCenter().GetY();
                    Ls.Points.Add(new DataPoint(x, y));
                }
                values.Add(cf[i].GetFeature1() + " - " + cf[i].GetFeature2(), Ls);
            }

            for (int i = 0; i < cf.Count; i++)
            {
                LineSeries Ls = new LineSeries();
                for (double t = 0; t < 2 * Math.PI + 0.1; t += 0.3)
                {
                    double x = cf[i].GetMinCircle().GetRadius() * Math.Cos(t) + cf[i].GetMinCircle().GetCenter().GetX();
                    double y = cf[i].GetMinCircle().GetRadius() * Math.Sin(t) + cf[i].GetMinCircle().GetCenter().GetY();
                    Ls.Points.Add(new DataPoint(y, x));
                }
                values.Add(cf[i].GetFeature2() + " - " + cf[i].GetFeature1(), Ls);
            }
            return values;
        }
    }
}
