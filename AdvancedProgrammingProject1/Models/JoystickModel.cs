using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    public class JoystickModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainControllerModel Model { get; }

        public double Aileron
        {
            get { return Model.Aileron*125 -90; }
            set { Model.Aileron = value; }
        }

        public double Elevator
        {
            get { return -(Model.Elevator * 125) - 90; }
            set { Model.Elevator = value; }
        }

        public JoystickModel(MainControllerModel model)
        {
            Model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged( e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
