using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
   
        public ControlBarView()
        {
            vm = (Application.Current as App).CBVM;
            InitializeComponent();
            DataContext = vm;
            slide.Maximum = vm.VM_CSVTable.Rows.Count;
            slide.Width = 500;
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            vm.VM_LineCounter = vm.VM_SlideValue;
            vm.VM_ValueChanged = false;
            vm.VM_JumpFlag = true;
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            vm.VM_ValueChanged = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void BtnMoveWayBack_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_LineCounter = Math.Max(0, vm.VM_LineCounter - 500);
            vm.VM_JumpFlag = true;
        }

        private void BtnMoveBack_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_LineCounter = Math.Max(0, vm.VM_LineCounter - 100);
            vm.VM_JumpFlag = true;
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
            // TODO: since you close up the program, you should first close up all resources
            // I suggest you alert the MainConroller when this window is being pressed
            // and it will be in charge of closing up the program.
            vm.VM_Stop = true;
            var myWindow = Window.GetWindow(this);
            myWindow.Close(); 
        }

        private void BtnMoveForward_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_LineCounter = Math.Min(vm.VM_CSVTable.Rows.Count, vm.VM_LineCounter + 100);
            vm.VM_JumpFlag = true;
        }

        private void BtnMoveWayForward_Click(object sender, RoutedEventArgs e)
        {
            vm.VM_LineCounter = Math.Min(vm.VM_CSVTable.Rows.Count, vm.VM_LineCounter + 500);
            vm.VM_JumpFlag = true;
        }

        // TODO: the speeds are not correct, I'm sure of it.
        private void BtnIncreaseSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (100 / vm.VM_Speed > 40) // minimal speed: 100 / 40 != 1/4
                vm.VM_Speed *= 2;
            speed.Text = vm.VM_Speed.ToString();
        }

        private void BtnDecreaseSpeed_Click(object sender, RoutedEventArgs e)
        {
            if (100 / vm.VM_Speed < 250) // maximal speed: 100 / 250 != 4
                vm.VM_Speed /= 2;
            speed.Text = vm.VM_Speed.ToString();
        }
        private void BtnUploadCSVFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog UploadCSVFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (UploadCSVFileDialog.ShowDialog() == true)
                csvName.Text = UploadCSVFileDialog.FileName;
        }
    }
}