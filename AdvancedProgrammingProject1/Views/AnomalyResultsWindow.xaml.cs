﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdvancedProgrammingProject1.Views
{
	/// <summary>
	/// Interaction logic for AnomalyResultsWindow.xaml
	/// </summary>
	public partial class AnomalyResultsWindow : Window
	{
		AnomalyDetectionViewModel vm;
		List<double> algoResult;

		public AnomalyResultsWindow()
		{
			vm = (Application.Current as App).ADVM;
			InitializeComponent();
			DataContext = vm;
			algoResult = new List<double>();
			FillWindow();
		}

		private void FillWindow()
        {
			List<AttrItem> items = new List<AttrItem>();
			foreach (var item in algoResult)
				items.Add(new AttrItem() { Title = "title", Time = item });
			lbTimesList.ItemsSource = items;
		}

		private void lbTimesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			vm.VM_AP_Time = (lbTimesList.SelectedItem as AttrItem).Time;
		}
	}
}
