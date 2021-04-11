using AdvancedProgrammingProject1.Views;
using Microsoft.Win32;
using System;
using System.Windows;

namespace AdvancedProgrammingProject1

{
    public partial class MainWindow : Window
    {
        public MainControllerViewModel vm;

        public MainControllerViewModel VM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            vm = (Application.Current as App).MainVM;
            DataContext = vm;
        }

        private void BtnUploadCSVFile_Click(object sender, RoutedEventArgs e) // learning file - sent straight to the anomaly detector
        {
            if (xmlName.Text == "")
            {
                MessageBox.Show("You should upload xml file first", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.No);
                return;
            }
            OpenFileDialog UploadCSVFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (UploadCSVFileDialog.ShowDialog() == true)
            {
                LearnCsvName.Text = UploadCSVFileDialog.FileName;
                LearnCsvName1.Text = System.IO.Path.GetFileName(LearnCsvName.Text);
            }
        }

        private void BtnUploadCSVAnomaly_Click(object sender, RoutedEventArgs e) // this is the file to inspect
        {
            if (xmlName.Text == "")
            {
                MessageBox.Show("You should upload xml file first", "Error Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.No);
                return;
            }
            OpenFileDialog UploadCSVFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (UploadCSVFileDialog.ShowDialog() == true)
            {
                csvName.Text = UploadCSVFileDialog.FileName;
                csvName1.Text = System.IO.Path.GetFileName(csvName.Text);
            }
        }

        private void BtnUploadXMLFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog UploadXMLFileDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            };

            if (UploadXMLFileDialog.ShowDialog() == true)
            {
                xmlName.Text = UploadXMLFileDialog.FileName;
                xmlName1.Text = System.IO.Path.GetFileName(xmlName.Text);
            }
        }
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.Connect();
                save.Content = "Connected!";
                save.IsEnabled = false;
            } catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.No);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            /*
			 * I wanted to make this button do the reading of both files, but couldn't manage to do it correctly so far.
			 * You can go ahead and try if you wish, but as long as you load the XML first and then the CSV it shouldn't
			 * be a problem right now.
			 */
            if (!vm.VM_IsConnected)
                try
                {
                    vm.Connect();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error Message", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.No);
                }
            else
            {
                vm.VM_Stop = false;
                NewWindow mnw = new NewWindow(vm);
                mnw.Show();
                this.Close();
            }
            // mnw.Owner = this;
            // this.Hide(); // not required if using the child events below
            // mnw.ShowDialog();
            // base.OnClosed(e);
            // Application.Current.Shutdown();
        }

    }
}