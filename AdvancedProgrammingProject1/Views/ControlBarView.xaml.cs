using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        ControlBarViewModel vm;
        //int lenght;
        public ControlBarView()
        {
            vm = (Application.Current as App).CBVM;
            InitializeComponent();
            DataContext = vm;
            slide.Maximum = vm.VM_CSVTable.Rows.Count;
            slide.Width = 500;
            slide.TickFrequency = 50;
            slide.IsSnapToTickEnabled = true;
        }

        private void TimeScroller_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void BtnMoveWayBack_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_LineCounter - 500 > 0)
            {
                    vm.VM_LineCounter -= 500;
            }
            else
            {
                vm.VM_LineCounter = 0;
            }
        }

        private void BtnMoveBack_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_LineCounter - 100 > 0)
            {
                vm.VM_LineCounter -= 100;
            }
            else
            {
                vm.VM_LineCounter = 0;
            }
        }
        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_Pause == false)
                vm.VM_Pause = true;
        }
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_Pause == true)
                vm.VM_Pause = false;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {

            vm.VM_Stop = true;
            var myWindow = Window.GetWindow(this);
            myWindow.Close(); 
        }

        private void BtnMoveForward_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_LineCounter + 100 < vm.VM_CSVTable.Rows.Count)
            {
                vm.VM_LineCounter += 100;
            }
            else
            {
                vm.VM_LineCounter = vm.VM_CSVTable.Rows.Count-1;
            }
        }

        private void BtnMoveWayForward_Click(object sender, RoutedEventArgs e)
        {
            if (vm.VM_LineCounter + 500 < vm.VM_CSVTable.Rows.Count)
            {
                vm.VM_LineCounter += 500;
            }
            else
            {
                vm.VM_LineCounter = vm.VM_CSVTable.Rows.Count-1;
            }
        }

        private void BtnIncreaseSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (100 / vm.VM_Speed > 40)
            { vm.VM_Speed = (vm.VM_Speed * 2); }
            speed.Text = vm.VM_Speed.ToString();
        }

        private void BtnDecreaseSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (100 / vm.VM_Speed < 250)
            { vm.VM_Speed = (vm.VM_Speed / 2); }
            speed.Text = vm.VM_Speed.ToString();
        }
    }
}