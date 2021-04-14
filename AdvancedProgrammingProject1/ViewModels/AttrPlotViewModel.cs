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

		public AttrPlotModel Model { get; }
		public string VM_AttrToPlot
		{
			get { return Model.AttrToPlot; }
			set { Model.AttrToPlot = value; }
		}
		public string VM_PearsonAttrToPlot
		{
			get { return Model.PearsonAttrToPlot; }
		}
		public string VM_AlgoName
		{
			get { return Model.AlgoName; }
			set { Model.AlgoName = value; }
		}
		public double VM_PearVal
		{
			get { return Model.PearVal; }
			set { Model.PearVal = value; }
		}
		public LineSeries VM_LS
		{
			get { return Model.LS; }
		}

		public PlotModel VM_SelfPlotModel
		{
			get { return Model.SelfPlotModel; }
		}

		public PlotModel VM_PearsonPlotModel
		{
			get { return Model.PearsonPlotModel; }
		}
		public PlotModel VM_LinearRegPlotModel
		{
			get { return Model.LinearRegPlotModel; }
		}
		public DataRow VM_Row
		{
			get { return Model.AP_Row; }
		}

		public List<string> VM_FlightAttrNames
		{
			get { return Model.AP_FlightAttrNames; }
		}

		public AttrPlotViewModel(AttrPlotModel model)
		{
			this.Model = model;
			model.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
