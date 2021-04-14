using System;
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
		// List<double> algoResult;

		public AnomalyResultsWindow()
		{
			vm = (Application.Current as App).ADVM;
			InitializeComponent();
			DataContext = vm;
			// algoResult = new List<double>();
			FillWindow();
		}

		private void FillWindow()
        {
			List<PlotItem> items = new List<PlotItem>();
			// double last = -1;
			foreach (AnomalyReport item in vm.VM_AlgoResults)
            {
				// if (item > last)
                // {
					int seconds = (int)(item.TimeStep - 60 * ((int)(item.TimeStep / 60)));
					int minutes = (int)(item.TimeStep / 60);

					items.Add(new PlotItem() { Title = $"{item.F1}, {item.F2}:\t{minutes}:{seconds}",
													Time = item.TimeStep, F1 = item.F1, F2 = item.F2});
				// }
				// last = item;
			}
			lbTimesList.ItemsSource = items;
		}

		private void lbTimesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			vm.VM_AP_Time = (lbTimesList.SelectedItem as PlotItem).Time;
			vm.VM_AnomalAttr = (lbTimesList.SelectedItem as PlotItem).F1;
			vm.VM_AnomalPear = (lbTimesList.SelectedItem as PlotItem).F2;
		}

		public class PlotItem
		{
			public string Title { get; set; }
			public double Time { get; set; }
			public string F1 { get; set; }
			public string F2 { get; set; }
		}
	}
}
