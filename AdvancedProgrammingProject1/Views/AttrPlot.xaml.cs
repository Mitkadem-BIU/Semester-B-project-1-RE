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

			List<string> items = new List<string>();
			foreach (var item in vm.VM_FlightAttrNames)
				items.Add(item);
			lbAttrList.ItemsSource = items;
		}

		private void LbAttrList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			vm.VM_AttrToPlot = (lbAttrList.SelectedItem as string);
			vm.VM_SelfPlotModel.Axes[1].Title = vm.VM_AttrToPlot;

			// fix this
			testBox.Text = vm.VM_AttrToPlot;

		}
	}
}
