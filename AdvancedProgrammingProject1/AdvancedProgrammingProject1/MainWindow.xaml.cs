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
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnUploadCSVFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog UploadCSVFileDialog = new OpenFileDialog();
			if (UploadCSVFileDialog.ShowDialog() == true)
				txtEditor1.Text = System.IO.Path.GetFileNameWithoutExtension(UploadCSVFileDialog.FileName);
				
		}
		private void btnUploadXMLFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog UploadXMLFileDialog = new OpenFileDialog();
			if (UploadXMLFileDialog.ShowDialog() == true)
				txtEditor2.Text = System.IO.Path.GetFileNameWithoutExtension(UploadXMLFileDialog.FileName);

		}
	}
}