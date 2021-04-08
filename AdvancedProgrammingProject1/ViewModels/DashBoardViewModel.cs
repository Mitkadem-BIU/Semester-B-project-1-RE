using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdvancedProgrammingProject1
{
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DashBoardModel Model { get; }

        public double VM_Altimeter
        {
            get { return Model.Altimeter; }
            set { Model.Altimeter = value; }
        }

        public double VM_AirSpeed
        {
            get { return Model.AirSpeed; }
            set { Model.AirSpeed = value; }
        }

        public double VM_Altitude
        {
            get { return Model.Altitude; }
            set { Model.Altitude = value; }
        }

        public double VM_Heading
        {
            get { return Model.Heading; }
            set { Model.Heading = value; }
        }

        public double VM_Roll
        {
            get { return Model.Roll; }
            set { Model.Roll = value; }
        }

        public double VM_Pitch
        {
            get { return Model.Pitch; }
            set { Model.Pitch = value; }
        }

        public double VM_GroundSpeed
        {
            get { return Model.GroundSpeed; }
            set { Model.GroundSpeed = value; }
        }

        public double VM_VerticalSpeed
        {
            get { return Model.VerticalSpeed; }
            set { Model.VerticalSpeed = value; }
        }

        public DashBoardViewModel(DashBoardModel model)
        {
            this.Model = model;
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
