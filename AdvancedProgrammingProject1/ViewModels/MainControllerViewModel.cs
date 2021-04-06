using System;
using System.ComponentModel;
using System.Data;
using System.Net.Sockets;


namespace AdvancedProgrammingProject1
{
	public class MainControllerViewModel : INotifyPropertyChanged
	{
		private MainControllerModel model;
		public event PropertyChangedEventHandler PropertyChanged;

		public MainControllerModel Model
		{
			get { return model; }
		}
		public string VM_Csv
		{
			get { return model.Csv; }
			set { model.Csv = value; }
		}

		public string VM_Xml
		{
			get { return model.Xml; }
			set { model.Xml = value; }
		}

		public string VM_IP
		{
			get { return model.IP; }
			set { model.IP = value; }
		}

		public int VM_Port
		{
			get { return model.Port; }
			set { model.Port = value; }
		}

		public bool VM_Stop
		{
			get { return model.Stop; }
			set { model.Stop = value; }
		}

		public double VM_Altimeter
		{
			get { return model.Altimeter; }
			set { model.Altimeter = value; }
		}
		public double VM_AirSpeed
		{
			get { return model.AirSpeed; }
			set { model.AirSpeed = value; }
		}
		public double VM_Altitude
		{
			get { return model.Altitude; }
			set { model.Altitude = value; }
		}
		public double VM_Roll
		{
			get { return model.Roll; }
			set { model.Roll = value; }
		}
		public double VM_Pitch
		{
			get { return model.Pitch; }
			set { model.Pitch = value; }
		}
		public double VM_VerticalSpeed
		{
			get { return model.VerticalSpeed; }
			set { model.VerticalSpeed = value; }
		}
		public double VM_GroundSpeed
		{
			get { return model.GroundSpeed; }
			set { model.GroundSpeed = value; }
		}
		public double VM_Heading
		{
			get { return model.Heading; }
			set { model.Heading = value; }
		}

		public double VM_Throttle
        {
            get { return model.Throttle; }
            set { model.Throttle = value; }
        }
		public double VM_Rudder
		{
			get { return model.Rudder; }
			set { model.Rudder = value; }
		}
		public double VM_Aileron
		{
			get { return model.Aileron; }
			set { model.Aileron = value; }
		}
		public double VM_Elevator
		{
			get { return model.Elevator; }
			set { model.Elevator = value; }
		}
		public DataRow VM_Row
		{
			get { return model.Row; }
			set { model.Row = value; }
		}

		public int VM_LineCounter
		{
			get { return model.LineCounter; }
			set { model.LineCounter = value; }
		}

		public void Run()
		{
			model.Client.Connect(VM_IP, VM_Port);
			model.Run();
		}

		public MainControllerViewModel(MainControllerModel model)
		{
			this.model = model;
			model.PropertyChanged +=
			delegate (Object sender, PropertyChangedEventArgs e) {
				NotifyPropertyChanged("VM_" + e.PropertyName);
			};
		}

		public void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}