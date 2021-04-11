using Microsoft.Win32;
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

namespace AdvancedProgrammingProject1.Views
{
    /// <summary>
    /// Interaction logic for AnomalyDetection.xaml
    /// </summary>
    public partial class AnomalyDetection : UserControl
    {
        AnomalyDetectionViewModel vm;

        public AnomalyDetection()
        {
            vm = (Application.Current as App).ADVM;
            InitializeComponent();
            DataContext = vm;
        }

        private void BtnUploadCSVFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog UploadCSVFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (UploadCSVFileDialog.ShowDialog() == true)
                csvName.Text = UploadCSVFileDialog.FileName;
            csvName1.Text = System.IO.Path.GetFileName(csvName.Text);
        }

        private void BtnUploadPlugin_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog UploadDLLFileDialog = new OpenFileDialog
            {
                Filter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*"
            };

            if (UploadDLLFileDialog.ShowDialog() == true)
                algoName.Text = UploadDLLFileDialog.FileName;
            algoName1.Text = System.IO.Path.GetFileName(csvName.Text);
        }
    }
}
