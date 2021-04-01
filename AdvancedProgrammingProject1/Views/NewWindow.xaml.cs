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

namespace AdvancedProgrammingProject1
{
	/// <summary>
	/// Interaction logic for NewWindow.xaml
	/// </summary>
	public partial class NewWindow : Window
	{
		MainControllerViewModel vm;
		// FGModel fg;
		public NewWindow() : this(new MainControllerViewModel(new MainControllerModel())) { }

		public NewWindow(MainControllerViewModel viewModel)
		{
			InitializeComponent();
			vm = viewModel;
			DataContext = vm;
			vm.Run();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			Application.Current.Shutdown();
		}
    }
}
