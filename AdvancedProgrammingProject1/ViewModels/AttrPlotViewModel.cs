using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace AdvancedProgrammingProject1
{
	public class AttrPlotViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public MainControllerModel Model { get; }

		public PlotModel MyModel { get; private set; }

		public AttrPlotViewModel(MainControllerModel model)
		{
			this.Model = model;
			model.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
			MyModel = new PlotModel { Title = "Example 1" };
			MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
