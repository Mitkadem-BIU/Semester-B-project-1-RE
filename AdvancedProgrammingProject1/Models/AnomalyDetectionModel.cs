using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    public class AnomalyDetectionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainControllerModel Model { get; }
        public AttrPlotModel AttrModel { get; }
        public PlotModel AnomalyPlotModel { get; private set; }
        private ScatterSeries normalPoints;
        private ScatterSeries anomalyPoints;
        public DataTable Table
        {
            get { return Model.CSVTable; }
        }
        string anomalAttr;
        string anomalPear;
        List<int> errTimes;
        List<AnomalyReport> algoResults;
        Dictionary<string, LineSeries> attrLines;
        public List<AnomalyReport> AlgoResults
        {
            get { return algoResults; }
            set { algoResults = value; }
        }
        public string AnomalPear
        {
            get { return anomalPear; }
            set
            {
                anomalPear = value;
                UpdatePlot();
            }
        }
        public string AnomalAttr
        {
            get { return anomalAttr; }
            set
            {
                anomalAttr = value;
            }
        }
        public double AP_Time
        {
            get { return AttrModel.AP_Time; }
            set { AttrModel.AP_Time = value; }
        }

        public string AttrToPlot
        {
            get { return AttrModel.AttrToPlot; }
        }

        public string PearsonAttrToPlot
        {
            get { return AttrModel.PearsonAttrToPlot; }
        }

        public string Csv
        {
            get { return Model.Csv; }
            set { Model.Csv = value; }
        }

        public string AlgoName
        {
            get { return Model.AlgoName; }
            set
            {
                Model.AlgoName = value;
                LoadDLL();
                UpdatePlot();
            }
        }

        public AnomalyDetectionModel(MainControllerModel model, AttrPlotModel aModel)
        {
            Model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };

            AttrModel = aModel;
            aModel.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
            algoResults = new List<AnomalyReport>();
            errTimes = new List<int>();
            AnomalyPlotModel = new PlotModel { };

            AnomalyPlotModel.Series.Add(new LineSeries()); // here we should add the LineSeries Coming from the Algorithm
            AnomalyPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
            });
            AnomalyPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
            });
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            // I'm pretty sure the view should be fixed
            //if (propertyName == "AP_Row")
            //{
            //    RowChanged();
            //}
            // if (propertyName == "AttrToPlot")
            //    UpdatePlot();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdatePlot()
        {
            normalPoints = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerFill = OxyColors.LightGray,
            };
            anomalyPoints = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerFill = OxyColors.IndianRed,
            };
            if (anomalAttr != null)
            {
                
                for (int i = 0; i < Model.Columns[anomalAttr].Count; i++)
                {
                    if (errTimes.Contains(i))
                        anomalyPoints.Points.Add(new ScatterPoint(Model.Columns[anomalAttr][i], Model.Columns[anomalPear][i]));
                    else
                        normalPoints.Points.Add(new ScatterPoint(Model.Columns[anomalAttr][i], Model.Columns[anomalPear][i]));
                }
            }

            /* foreach (double keyTime in AttrModel.AttrData.Keys)
            {
                if (AnomalAttr != null)
                {
                    if (Model.Time - 30 > keyTime) // change this to anomaly points. should include all times
                        
                    else if (Model.Time >= keyTime)
                        
                }
            } */
            AnomalyPlotModel.Series.Clear();
            AnomalyPlotModel.Series.Add(normalPoints);
            AnomalyPlotModel.Series.Add(anomalyPoints);
            try
            {
                AnomalyPlotModel.Series.Add(attrLines[$"{AnomalAttr} - {AnomalPear}"]);
            } catch (KeyNotFoundException) { }
            AnomalyPlotModel.InvalidatePlot(true);
        }

        public void LoadDLL()
        {
            var dllFile = new System.IO.FileInfo(AlgoName);
            Assembly dllAssembly = Assembly.LoadFile(dllFile.FullName);
           // string fileName = System.IO.Path.GetFileName(dllFile.Name);

            var type = dllAssembly.GetType("RegressionLineAlgorithm.RegLine");
            dynamic obj = Activator.CreateInstance(type);
            var learn = type.GetMethod("Learn");
            learn.Invoke(obj, new object[] { Model.LearnCsvTable });
            var detect = type.GetMethod("DetectTime");
            var anomaly = detect.Invoke(obj, new object[] { Model.CSVTable });
            // AnomalyTimes((List<long>)anomaly);
            algoResults.Clear();
            errTimes.Clear();
            int last = -1;
            foreach (var item in (List<Tuple<long, string, string>>)anomaly)
            {
                algoResults.Add(new AnomalyReport(item.Item2, item.Item3, (double)item.Item1 / 10));
                if ((int)item.Item1 != last)
                    errTimes.Add((int)item.Item1);
                last = (int)item.Item1;
            }

            var valsToLines = type.GetMethod("ValuesToLine");
            var lines = valsToLines.Invoke(obj, new object[] { });
            attrLines = (Dictionary<string, LineSeries>)lines;
        }
    }
}
