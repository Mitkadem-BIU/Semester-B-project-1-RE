using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdvancedProgrammingProject1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // properties of view models and the model
        public MainControllerModel Model { get; internal set; }
        public MainControllerViewModel MainVM { get; internal set; }
        public AttrPlotViewModel APVM { get; internal set; }
        public ControlBarViewModel CBVM { get; internal set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Model = new MainControllerModel();
            MainVM = new MainControllerViewModel(Model);
            APVM = new AttrPlotViewModel(Model.AP);
            // Create main application window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
