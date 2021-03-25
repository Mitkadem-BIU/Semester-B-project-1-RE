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
using System.IO;
using Microsoft.Win32;

namespace AdvancedProgrammingProject1

{
	public partial class MainWindow : Window
	{
		MainControllerViewModel vm;
		public MainWindow()
		{
			InitializeComponent();
			vm = new MainControllerViewModel(new MainControllerModel());
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
		}
		private void BtnUploadXMLFile_Click(object sender, RoutedEventArgs e)
		{
            OpenFileDialog UploadXMLFileDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"
            };

			if (UploadXMLFileDialog.ShowDialog() == true)
				xmlName.Text = UploadXMLFileDialog.FileName;
		}

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
			/*
			 * I wanted to make this button do the reading of both files, but couldn't manage to do it correctly so far.
			 * You can go ahead and try if you wish, but as long as you load the XML first and then the CSV it shouldn't
			 * be a problem right now.
			 */ 
			NewWindow mnw = new NewWindow();
			mnw.Owner = this;
			this.Hide(); // not required if using the child events below
			mnw.ShowDialog();
		}
    }
}