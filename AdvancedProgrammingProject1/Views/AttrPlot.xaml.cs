using OxyPlot.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdvancedProgrammingProject1
{
	/// <summary>
	/// Interaction logic for AttrPlot.xaml
	/// </summary>
	public partial class AttrPlot : UserControl
	{
		AttrPlotViewModel vm;
        
        public AttrPlot()
		{
			vm = (Application.Current as App).APVM;
			InitializeComponent();
			DataContext = vm;

			List<AttrItem> items = new List<AttrItem>();
			foreach (var item in vm.Model.FlightAttrNames)
				items.Add(new AttrItem() { Title = item, Completion = 15 });
			lbAttrList.ItemsSource = items;
		}

		private void LbAttrList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			vm.AttrToPlot = (lbAttrList.SelectedItem as AttrItem).Title;
			vm.PlotModel.Axes[1].Title = vm.AttrToPlot;
		   // Console.WriteLine($"current selection: {(lbAttrList.SelectedItem as AttrItem).Title}");
		}
	}

	public class AttrItem
	{
		public string Title { get; set; }
		public int Completion { get; set; }
	}
}
