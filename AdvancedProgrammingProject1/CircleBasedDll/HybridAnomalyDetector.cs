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
        public static List<AnomalyReport> LearnAndDetect(DataTable csvTable1, DataTable csvTable2)
        {
            TimeSeries ts1 = new TimeSeries(csvTable1);
            SimpleAnomalyDetector.LearnNormal(ts1);
            TimeSeries ts2 = new TimeSeries(csvTable2);
            List<AnomalyReport> anomaly_reports = new List<AnomalyReport>();
            List<CorrelatedFeatures> cf = SimpleAnomalyDetector.GetNormalModel();
            for (int i = 0; i < cf.Count; i++)
            {
                if (cf[i].IsHybrid())
                {
                    string feature_a = cf[i].GetFeature1();
                    string feature_b = cf[i].GetFeature2();
                    cf[i].SetThreshold(cf[i].GetThreshold() * 1.1);
                    cf[i].GetMinCircle().SetRadius(cf[i].GetThreshold());
                    for (int j = 0; j < ts2.GetFeatures().Count; j++)
                    {
                        for (int k = j + 1; k < ts2.GetFeatures().Count; k++)
                        {
                            if (feature_a == ts2.GetFeatures()[ts2.GetFeatures().Keys.ElementAt(j)] && feature_b == ts2.GetFeatures()[ts2.GetFeatures().Keys.ElementAt(k)])
                            {
                                double[] x = SimpleAnomalyDetector.ListToArray(ts2.GetTable(), j);
                                double[] y = SimpleAnomalyDetector.ListToArray(ts2.GetTable(), k);

                                for (int l = 0; l < ts2.GetTable().Count; l++)
                                {
                                    Point pl = new Point(SimpleAnomalyDetector.GetAnArrayOfPoints(x, y, ts2)[l].GetX(), SimpleAnomalyDetector.GetAnArrayOfPoints(x, y, ts2)[l].GetY());
                                    if (!minCircle.IsPointInsideCircle(cf[i].GetMinCircle(), pl))
                                    {
                                        AnomalyReport anomaly_report = new AnomalyReport((feature_a + "-" + feature_b), l + 1);
                                        anomaly_reports.Add(anomaly_report);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            SimpleAnomalyDetector.SetNormalModel(cf);
            return anomaly_reports;
        }

        public static Dictionary<string, LineSeries> ValuesToCircle()
        {
            Dictionary<string, LineSeries> values = new Dictionary<string, LineSeries>();
            List<CorrelatedFeatures> cf = SimpleAnomalyDetector.GetNormalModel();
            
            for (int i = 0; i < cf.Count; i++)
            {
                LineSeries Ls = new LineSeries();
                for (double t = 0;  t < 2*Math.PI + 0.1; t+=0.3)
                {
                    double x = cf[i].GetMinCircle().GetRadius() * Math.Cos(t) + cf[i].GetMinCircle().GetCenter().GetX();
                    double y = cf[i].GetMinCircle().GetRadius() * Math.Sin(t) + cf[i].GetMinCircle().GetCenter().GetY();
                    Ls.Points.Add(new DataPoint(x, y));
                }
                values.Add(cf[i].GetFeature1() + "-" + cf[i].GetFeature2(), Ls);
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
                values.Add(cf[i].GetFeature2() + "-" + cf[i].GetFeature1(), Ls);
            }
            return values;
        }
    }
}
