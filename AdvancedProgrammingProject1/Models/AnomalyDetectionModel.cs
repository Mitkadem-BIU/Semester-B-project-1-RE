using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public string AttrToPlot
        {
            get { return AttrModel.AttrToPlot; }
        }

        public double AP_Time
        {
            get { return AttrModel.AP_Time; }
            set { AttrModel.AP_Time = value; }
        }

        public string PearsonAttrToPlot
        {
            get { return AttrModel.PearsonAttrToPlot; }
            // set { AttrModel}
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
            foreach (double keyTime in AttrModel.AttrData.Keys)
            {
                if (Model.Time - 30 > keyTime) // change this to anomaly points. should include all times
                    anomalyPoints.Points.Add(new ScatterPoint(AttrModel.AttrData[keyTime], AttrModel.PearData[keyTime]));
                else if (Model.Time >= keyTime)
                    normalPoints.Points.Add(new ScatterPoint(AttrModel.AttrData[keyTime], AttrModel.PearData[keyTime]));
            }
            AnomalyPlotModel.Series.Add(normalPoints);
            AnomalyPlotModel.Series.Add(anomalyPoints);
            AnomalyPlotModel.InvalidatePlot(true);
        }

        public void LoadDLL()
        {
            var dllFile = new FileInfo(AlgoName);
            Assembly dllAssembly = Assembly.LoadFile(dllFile.FullName);
            var type = dllAssembly.GetType("RegretionBasedDll.RegretionAnomalyDetector");
            dynamic obj = Activator.CreateInstance(type);

            //this is the way to dynamically load a dll file and using its methods
            //string dllFile = @"C:\Users\User\Desktop\מסמכי אוניברסיטה\שנה ב- סמסטר ב\תכנות מתקדם 2\AdvancedProgrammingProject1\AdvancedProgrammingProject1\RegretionBasedDll\bin\Debug\netcoreapp3.1\RegretionBasedDll.dll";
            /*var assembly = Assembly.LoadFile(*//*dllFile*//*AlgoName);
            var type = assembly.GetType("RegretionBasedDll.RegretionAnomalyDetector");
            dynamic obj = Activator.CreateInstance(type);
            var method = type.GetMethod("LearnAndDetect");
            var result = method.Invoke(obj, new object[] { Model.CSVTable });*/


            // var method = type.GetMethod("LearnAndDetect");
            // var result = method.Invoke(obj, new object[] { Model.CSVTable });

            // Object dllInstance = (Object)dllAssembly.CreateInstance("AnomalyDetectionDll.RegretionAnomalyDetector");
            /* dll = dllInstance;
             object[] argslearn = new object[] { (object)trainLines };
             maxCorralatedFreatures = (List<Tuple<int, int>>)dll.GetType().GetMethod("learnNormal").Invoke(dll, argslearn);*/
        }
    }
}
