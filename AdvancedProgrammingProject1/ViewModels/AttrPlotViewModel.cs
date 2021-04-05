using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Series;

namespace AdvancedProgrammingProject1
{
	public class AttrPlotViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		string attr;

		public MainControllerModel Model { get; }
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
			get { return Model.PlotModel.Series[0] as LineSeries; }
        }

		private void AttrChanged()
		{
			Model.PlotModel.Series.Clear();
			Model.PlotModel.Series.Add(new LineSeries());
		}

		public PlotModel PlotModel
		{
			get { return Model.PlotModel; }
		}
		public DataRow VM_Row
		{
			get { return Model.Row; }
		}

		public AttrPlotViewModel(MainControllerModel model)
		{
			this.Model = model;
			model.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			if (propertyName == "VM_Row")
            {
				try
				{
					Console.WriteLine(Model.Row[attr]);
					DataPoint newPoint = new DataPoint(Model.Time, Double.Parse((string)Model.Row[attr]));
					LS.Points.Add(newPoint); // (time, data)
					double minTime = LS.Points[0].X;
					if (newPoint.X - minTime > 30)
						LS.Points.RemoveAt(0);
					Model.PlotModel.Axes[0].Minimum = minTime;
					Model.PlotModel.Axes[0].Maximum = Math.Max(minTime + 30, newPoint.X);
					Model.PlotModel.InvalidatePlot(true);
				}
				catch (ArgumentNullException) { }
			}
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
