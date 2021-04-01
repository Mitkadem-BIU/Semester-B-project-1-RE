using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
	public class FGModel : INotifyPropertyChanged
	{
		MainControllerModel mainControllerModel;
		public event PropertyChangedEventHandler PropertyChanged;
		TcpClient myClient;
		public bool isConnected = false;

		public DataRow FG_Row
		{
			get { return mainControllerModel.Row; }
			set { mainControllerModel.Row = value; }
		}

		private void WriteRow(DataRow row)
		{
			string command = string.Join(",", row.ItemArray);
			Write(command);
		}

		public FGModel(MainControllerModel model)
		{
			this.mainControllerModel = model;
			mainControllerModel.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("FG_" + e.PropertyName);
			};
		}
		public void Connect(string ip, int port)
		{
			myClient = new TcpClient(ip, port);
			isConnected = true;
		}

		public void Write(string command)
		{
			byte[] buffer = Encoding.ASCII.GetBytes(command + "\n");

			try
			{
				NetworkStream stream = this.myClient.GetStream();
				stream.Flush();
				stream.Write(buffer, 0, buffer.Length);
				Console.WriteLine("enter write scope");
			}
			catch
			{

			}
		}

		public void Disconnect()
		{
			if (isConnected)
			{
				this.myClient.Close();
				isConnected = false;
			}
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			if (propertyName == "FG_Row")
				WriteRow(FG_Row);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
