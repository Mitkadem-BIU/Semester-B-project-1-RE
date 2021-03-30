using System;
using System.ComponentModel;
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

        public void Run()
        {
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