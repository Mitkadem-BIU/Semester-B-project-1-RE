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
    /// Interaction logic for ControlBarView.xaml
    /// </summary>
    public partial class ControlBarView : UserControl
    {
        MainControllerViewModel vm;

        public ControlBarView() : this(new MainControllerViewModel(new MainControllerModel())) { }

        public ControlBarView(MainControllerViewModel viewModel)
        {
            InitializeComponent();
            vm = viewModel;
            DataContext = vm;
        }

        private void timeScroller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_Stop == false)
                vm.VM_Stop = true;
        }

        private void BtnMoveWayBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMoveBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_Stop == true)
                vm.VM_Stop = false;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMoveForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnMoveWayForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnIncreaseSpeed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDecreaseSpeed_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}