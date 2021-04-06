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
		public MainControllerModel Model { get; }
        public PlotModel PlotModel { get; private set; }
		public List<string> AP_FlightAttrNames//
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
			get { return PlotModel.Series[0] as LineSeries; }
		}
		public AttrPlotModel(MainControllerModel model)
        {
            Model = model;
			model.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("AP_" + e.PropertyName);
			};
			PlotModel = new PlotModel { };
			PlotModel.Series.Add(new LineSeries());
			PlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Bottom,
				Maximum = 30,
				Minimum = 0,
				Title = "time [s]",
				MajorStep = 10,
			});
			PlotModel.Axes.Add(new LinearAxis
			{
				Position = AxisPosition.Left,
			});
		}
		private void AttrChanged()
		{
			PlotModel.Series.Clear();
			PlotModel.Series.Add(new LineSeries());
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			if (propertyName == "AP_Row")
			{
				try
				{
					Console.WriteLine(AP_Row[attr]);
					DataPoint newPoint = new DataPoint(AP_Time, Double.Parse((string)AP_Row[attr]));
					LS.Points.Add(newPoint); // (time, data)
					double minTime = LS.Points[0].X;
					if (newPoint.X - minTime > 30)
						LS.Points.RemoveAt(0);
					PlotModel.Axes[0].Minimum = minTime;
					PlotModel.Axes[0].Maximum = Math.Max(minTime + 30, newPoint.X);
					PlotModel.InvalidatePlot(true);
				}
				catch (ArgumentNullException) { }
			}
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
				covarr[i] = (x[i] - x.Average()) * (y[i] - y.Average());
			return covarr.Average();
		}

		public double Pearson(List<double> x, List<double> y)
		{
			return Covariance(x, y) / Math.Sqrt(Variance(x) * Variance(y));
		}
	}
}
