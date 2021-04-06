using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
	public class AttrPlotModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		string attr;
		string pearAttr;
		Dictionary<string, LineSeries> attrLinePoints;
		Dictionary<string, Dictionary<double, double>> attrData;
		/* public DataTable AP_CSVTable
        {
			get { return Model.CSVTable; }
		} */
		
		public MainControllerModel Model { get; }
		public PlotModel SelfPlotModel { get; private set; }
		public PlotModel PearsonPlotModel { get; private set; }
		public PlotModel LinearRegPlotModel { get; private set; }
		public List<string> AP_FlightAttrNames
		{
			get { return Model.FlightAttrNames; }
		}

		public DataRow AP_Row
		{
			get { return Model.Row; }
		}

		public double AP_Time
		{
			get { return Model.Time; }
		}

		public string AttrToPlot
		{
			get { return attr; }
			set
			{
				attr = value;
				AttrChanged();
			}
		}

		public LineSeries LS
		{
			get { return SelfPlotModel.Series[0] as LineSeries; }
		}
		public AttrPlotModel(MainControllerModel model)
		{
			Model = model;
			model.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("AP_" + e.PropertyName);
			};
			attrLinePoints = new Dictionary<string, LineSeries>();
			attrData = new Dictionary<string, Dictionary<double, double>>();

			SelfPlotModel = new PlotModel { };
			SelfPlotModel.Series.Add(new LineSeries());
			SelfPlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Maximum = 30,
				Minimum = 0,
				Title = "time [s]",
				MajorStep = 10,
			});
			SelfPlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Left,
			});

			PearsonPlotModel = new PlotModel { };
			PearsonPlotModel.Series.Add(new LineSeries());
			PearsonPlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Maximum = 30,
				Minimum = 0,
				Title = "time [s]",
				MajorStep = 10,
			});
			PearsonPlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Left,
			});

			LinearRegPlotModel = new PlotModel { };
			LinearRegPlotModel.Series.Add(new LineSeries());
			LinearRegPlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Bottom,
			});
			LinearRegPlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Left,
			});
		}
		private void AttrChanged()
		{
			SelfPlotModel.Series.Clear();
			SelfPlotModel.Series.Add(new LineSeries());
			PearsonPlotModel.Series.Clear();
			PearsonPlotModel.Series.Add(new LineSeries());
			LinearRegPlotModel.Series.Clear();
			LinearRegPlotModel.Series.Add(new LineSeries());
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			if (propertyName == "AP_Row")
			{
				try { Row_Changed(); }
				catch (ArgumentNullException) { }
			}
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public string GetMostSimilar()
        {
			// attrData[attr]
			Dictionary<string, double> pvals = new Dictionary<string, double>();
            foreach(string attrName in attrData.Keys)
            {
				if (attrName != attr)
				{
					pvals[attrName] = Math.Abs(Pearson(attrData[attr].Values.ToList(), attrData[attrName].Values.ToList()));
					if (Double.IsNaN(pvals[attrName]))
						pvals[attrName] = 0;
				}
            }
			return pvals.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // key of max value
        }


		public void Row_Changed()
		{
			DataPoint newPoint;
			double minTime;
			try { minTime = attrLinePoints[attr].Points[0].X; }
			catch (KeyNotFoundException)
			{
				// initialize
				minTime = 0;
				foreach (string attrName in AP_FlightAttrNames)
				{
					attrLinePoints[attrName] = new LineSeries();
					attrData[attrName] = new Dictionary<double, double>();
				}
			}

			// add DataPoints
			foreach (string attrName in AP_FlightAttrNames)
			{
				newPoint = new DataPoint(AP_Time, Double.Parse((string)AP_Row[attrName]));
				if (!attrLinePoints[attrName].Points.Contains(newPoint))
					attrLinePoints[attrName].Points.Add(newPoint);
				if (!attrData[attrName].ContainsKey(AP_Time))
					attrData[attrName].Add(AP_Time, Double.Parse((string)AP_Row[attrName]));
				/* while (AP_Time - minTime > 30)
				{
					attrLinePoints[attrName].Points.RemoveAt(0);
					// attrData[attrName].Remove(minTime);
					minTime = attrLinePoints[attrName].Points[0].X;
				} */
			}

			// Set Plots
			SelfPlotModel.Axes[0].Minimum = Math.Max(0, AP_Time - 30);
			SelfPlotModel.Axes[0].Maximum = Math.Max(30, AP_Time);
			SelfPlotModel.Series.Clear();
			SelfPlotModel.Series.Add(attrLinePoints[attr]);
			SelfPlotModel.InvalidatePlot(true);

			pearAttr = GetMostSimilar();
			PearsonPlotModel.Axes[0].Minimum = Math.Max(0, AP_Time - 30);
			PearsonPlotModel.Axes[0].Maximum = Math.Max(30, AP_Time);
			PearsonPlotModel.Series.Clear();
			PearsonPlotModel.Series.Add(attrLinePoints[pearAttr]);
			PearsonPlotModel.Axes[1].Title = pearAttr;
			PearsonPlotModel.InvalidatePlot(true);

			LinearRegPlotModel.Series.Clear();
			LinearRegPlotModel.Series.Add(LinearReg(attrData[attr].Values.ToList(), attrData[pearAttr].Values.ToList()));
			LinearRegPlotModel.Axes[0].Title = attr;
			LinearRegPlotModel.Axes[1].Title = pearAttr;
			LinearRegPlotModel.InvalidatePlot(true);
		}

		public double Variance(List<double> x)
		{
			double sqsum = 0;
			foreach (double val in x)
			{
				sqsum += val * val;
			}
			return sqsum / x.Count() - x.Average() * x.Average();
		}

		public double Covariance(List<double> x, List<double> y)
		{
			List<double> covarr = new List<double>();
			for (int i = 0; i < x.Count(); i++)
				covarr.Add((x[i] - x.Average()) * (y[i] - y.Average()));
			return covarr.Average();
		}

		public double Pearson(List<double> x, List<double> y)
		{
			return Covariance(x, y) / Math.Sqrt(Variance(x) * Variance(y));
		}

		public LineSeries LinearReg(List<double> x, List<double> y)
		{
			double a = Covariance(x, y) / Variance(x);
			double b = y.Average() - a * x.Average();
			LineSeries LS = new LineSeries();
			for (double i = x.Min(); i < x.Max(); i++)
				LS.Points.Add(new DataPoint(i, a * i + b));
			return LS;
		}
	}
}
