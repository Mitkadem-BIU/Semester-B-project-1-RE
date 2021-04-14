using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace AdvancedProgrammingProject1
{
    public class AttrPlotModel : INotifyPropertyChanged
    {
        private string attr;
        private string pearAttr;
        private double pearVal;

        private Dictionary<string, Dictionary<double, double>> attrData;
        private Dictionary<string, LineSeries> attrLinePoints;

        private ScatterSeries oldPoints;
        private ScatterSeries newPoints;

        public string AlgoName
        {
            get { return Model.AlgoName; }
            set
            {
                Model.AlgoName = value;
                NotifyPropertyChanged("AlgoName");
            }
        }

        public Dictionary<string, Dictionary<double, double>> FullAttrData
        {
            get { return attrData; }
        }
        public Dictionary<double, double> AttrData
        {
            get
            {
                try
                {
                    return attrData[attr];
                } catch (ArgumentNullException)
                {
                    return new Dictionary<double, double>();
                }
                
            }
        }

        public Dictionary<double, double> PearData
        {
            get
            {
                try
                {
                    return attrData[pearAttr];
                }
                catch (ArgumentNullException)
                {
                    return new Dictionary<double, double>();
                }

            }
        }
        public AttrPlotModel(MainControllerModel model)
        {
            Model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("AP_" + e.PropertyName);
            };
            attrLinePoints = new Dictionary<string, LineSeries>();
            attrData = new Dictionary<string, Dictionary<double, double>>();

            SelfPlotModel = new PlotModel { };
            SelfPlotModel.Series.Add(new LineSeries());
            SelfPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                Maximum = 30,
                Minimum = 0,
                Title = "time [s]",
                MajorStep = 10,
            });
            SelfPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
            });

            PearsonPlotModel = new PlotModel { };
            PearsonPlotModel.Series.Add(new LineSeries());
            PearsonPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                Maximum = 30,
                Minimum = 0,
                Title = "time [s]",
                MajorStep = 10,
            });
            PearsonPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
            });

            LinearRegPlotModel = new PlotModel { };
            
            LinearRegPlotModel.Series.Add(new LineSeries());
            LinearRegPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
            });
            LinearRegPlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
            });
            // attr = AP_FlightAttrNames[0];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public DataTable AP_CSVTable
        {
            get { return Model.CSVTable; }
        }

        public List<string> AP_FlightAttrNames
        {
            get { return Model.FlightAttrNames; }
        }

        public bool AP_JumpFlag
        {
            get { return Model.JumpFlag; }
        }

        public DataRow AP_Row
        {
            get { return Model.Row; }
        }

        public double AP_Time
        {
            get { return Model.Time; }
            set { Model.Time = value; }
        }

        public string AttrToPlot
        {
            get { return attr; }
            set
            {
                attr = value;
                AttrChanged();
                NotifyPropertyChanged("AttrToPlot");
            }
        }

        public PlotModel LinearRegPlotModel { get; private set; }
        public LineSeries LS
        {
            get { return SelfPlotModel.Series[0] as LineSeries; }
        }

        public MainControllerModel Model { get; }
        public string PearsonAttrToPlot
        {
            get { return pearAttr; }
            set
            {
                pearAttr = value;
                NotifyPropertyChanged("PearsonAttrToPlot");
            }
        }

        public PlotModel PearsonPlotModel { get; private set; }
        public double PearVal
        {
            get { return pearVal; }
            set
            {
                pearVal = value;
                NotifyPropertyChanged("PearVal");
            }
        }

        public PlotModel SelfPlotModel { get; private set; }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (propertyName == "AP_Row")
            {
                RowChanged();
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void AttrChanged()
        {
            SelfPlotModel.Series.Clear();
            SelfPlotModel.Series.Add(new LineSeries());
            PearsonPlotModel.Series.Clear();
            PearsonPlotModel.Series.Add(new LineSeries());
            LinearRegPlotModel.Series.Clear();
            LinearRegPlotModel.Series.Add(new LineSeries());
        }

        public void RowChanged()
        {
            DataPoint newPoint;
            double minTime;
            try { minTime = attrLinePoints[attr].Points[0].X; }
            /* catch (ArgumentNullException)
            {
                // attr = AP_FlightAttrNames[0];
                minTime = attrLinePoints[AP_FlightAttrNames[0]].Points[0].X;
            } */
            catch (Exception exp)
            {
                if (exp is ArgumentNullException || exp is KeyNotFoundException)
                {
                    // initialize
                    foreach (string attrName in AP_FlightAttrNames)
                    {
                        attrLinePoints[attrName] = new LineSeries();
                        attrData[attrName] = new Dictionary<double, double>();
                    }
                } else
                {
                    throw;
                }
            }
            TimeJump();
            UpdateLinePoints();

            // add DataPoints
            foreach (string attrName in AP_FlightAttrNames)
            {
                newPoint = new DataPoint(AP_Time, Double.Parse((string)AP_Row[attrName]));
                if (!attrLinePoints[attrName].Points.Contains(newPoint))
                    attrLinePoints[attrName].Points.Add(newPoint);
                if (!attrData[attrName].ContainsKey(AP_Time))
                    attrData[attrName].Add(AP_Time, Double.Parse((string)AP_Row[attrName]));
            }

            UpdatePlots();
        }

        public void TimeJump()
        {
            List<double> keysList;
            try
            {
                keysList = attrData[attr].Keys.ToList();
            } catch (ArgumentNullException)
            {
                keysList = attrData[AP_FlightAttrNames[0]].Keys.ToList();
            }
            for (int i = 1; i < keysList.Count(); i++)
            {
                if (keysList[i] - keysList[i - 1] > 0.2)
                    JumpForward(i - 1);
                if (keysList[i] - keysList[i - 1] < 0)
                    JumpBackward(i);
            }
        }

        public void JumpBackward(double time) // remove all excess rows
        {
            try
            {
                foreach (double timeKey in attrData[attr].Keys)
                {
                    if (timeKey > time)
                    {
                        foreach (string attrName in AP_FlightAttrNames)
                            attrData[attrName].Remove(timeKey);
                    }
                }
            } catch (InvalidOperationException)
            {

            }

        }

        public void JumpForward(double time) // add all missing rows
        {
            for (double iTime = time; iTime < AP_Time; iTime += 0.1)
                foreach (string attrName in AP_FlightAttrNames)
                {
                    object val = AP_CSVTable.Rows[(int)(10 * iTime)][attrName];
                    try
                    {
                        attrData[attrName].Add(iTime, Double.Parse((string)val));
                    }
                    catch (ArgumentException) { return; }
                }
        }

        public void UpdateLinePoints()
        {
            // take all points up to 30 seconds backwards
            DataPoint newPoint;
            foreach (string attrName in AP_FlightAttrNames)
            {
                attrLinePoints[attrName] = new LineSeries();
                for (double iTime = Math.Max(0, AP_Time - 30); iTime < AP_Time; iTime += 0.1)
                {
                    object val = AP_CSVTable.Rows[(int)(10 * iTime)][attrName];
                    newPoint = new DataPoint(iTime, Double.Parse((string)val));
                    attrLinePoints[attrName].Points.Add(newPoint);
                }
            }
        }

        public void UpdatePlots()
        {
            if (string.IsNullOrEmpty(attr))
                return;
            double minTime = attrLinePoints[attr].Points[0].X; // ***
            SelfPlotModel.Axes[0].Minimum = Math.Max(minTime, AP_Time - 30);
            SelfPlotModel.Axes[0].Maximum = Math.Max(30, AP_Time);
            SelfPlotModel.Series.Clear();
            SelfPlotModel.Series.Add(attrLinePoints[attr]);
            SelfPlotModel.InvalidatePlot(true);

            PearsonAttrToPlot = GetMostSimilar();
            PearsonPlotModel.Axes[0].Minimum = Math.Max(minTime, AP_Time - 30);
            PearsonPlotModel.Axes[0].Maximum = Math.Max(30, AP_Time);
            PearsonPlotModel.Series.Clear();
            PearsonPlotModel.Series.Add(attrLinePoints[pearAttr]);
            PearsonPlotModel.Axes[1].Title = pearAttr;
            PearsonPlotModel.InvalidatePlot(true);

            LinearRegPlotModel.Series.Clear();
            if (AP_Time < 0.5)
            {
                LinearRegPlotModel.Series.Add(new LineSeries());
            } else
            {
                LinearRegPlotModel.Series.Add(StatisticMethods.LinearReg(UptoNow(attr), UptoNow(pearAttr)));
                oldPoints = new ScatterSeries()
                {
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 2,
                    MarkerFill = OxyColors.LightGray,
                };
                newPoints = new ScatterSeries()
                {
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 2,
                    MarkerFill = OxyColors.IndianRed,
                };
                FillScatterPoints();
                LinearRegPlotModel.Series.Add(oldPoints);
                LinearRegPlotModel.Series.Add(newPoints);

            }
            LinearRegPlotModel.Axes[0].Title = attr;
            LinearRegPlotModel.Axes[1].Title = pearAttr;
            LinearRegPlotModel.InvalidatePlot(true);
        }

        public List<double> UptoNow(string attrName)
        {
            List<double> upt = new List<double>();
            foreach (double keyTime in attrData[attrName].Keys)
            {
                if (keyTime <= AP_Time)
                    upt.Add(attrData[attrName][keyTime]);
            }
            return upt;
        }

        public void FillScatterPoints()
        {
            foreach (double keyTime in attrData[attr].Keys)
            {
                if (AP_Time - 30 > keyTime) // old point
                    oldPoints.Points.Add(new ScatterPoint(attrData[attr][keyTime], attrData[pearAttr][keyTime]));
                else if (AP_Time >= keyTime) // new but not future
                    newPoints.Points.Add(new ScatterPoint(attrData[attr][keyTime], attrData[pearAttr][keyTime]));
            }
        }
        public string GetMostSimilar()
        {
            Dictionary<string, double> pvals = new Dictionary<string, double>();
            foreach (string attrName in attrData.Keys)
            {
                if (attrName != attr)
                {
                    pvals[attrName] = Math.Abs(Pearson(attr, attrName));
                    if (Double.IsNaN(pvals[attrName]))
                        pvals[attrName] = 0;
                }
            }
            string keyMax = pvals.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // key of max value
            PearVal = Pearson(attr, keyMax);
            return keyMax;
        }

        public double Pearson(string x, string y)
        {
            return StatisticMethods.Pearson(attrData[x].Values.ToList(), attrData[y].Values.ToList());
        }
    }
}