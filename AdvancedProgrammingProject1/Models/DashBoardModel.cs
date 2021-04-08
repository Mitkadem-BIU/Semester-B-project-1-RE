using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    public class DashBoardModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainControllerModel Model { get; }

        public double Altimeter
        {
            get { return Model.Altimeter; }
            set { Model.Altimeter = value; }
        }

        public double AirSpeed
        {
            get { return Model.AirSpeed; }
            set { Model.AirSpeed = value; }
        }

        public double Altitude
        {
            get { return Model.Altitude; }
            set { Model.Altitude = value; }
        }

        public double Heading
        {
            get { return Model.Heading; }
            set { Model.Heading = value; }
        }

        public double Roll
        {
            get { return Model.Roll; }
            set { Model.Roll = value; }
        }

        public double Pitch
        {
            get { return Model.Pitch; }
            set { Model.Pitch = value; }
        }

        public double GroundSpeed
        {
            get { return Model.GroundSpeed; }
            set { Model.GroundSpeed = value; }
        }

        public double VerticalSpeed
        {
            get { return Model.VerticalSpeed; }
            set { Model.VerticalSpeed = value; }
        }

        public DashBoardModel(MainControllerModel model)
        {
            Model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e){
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
