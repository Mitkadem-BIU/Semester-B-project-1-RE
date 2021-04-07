﻿using System;
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

		public double VM_PearVal
		{
			get { return Model.PearVal; }
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
