using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OxyPlot;
using OxyPlot.Series;

namespace AdvancedProgrammingProject1
{
    public class JoystickViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public JoystickModel Model { get; }

        public double VM_J_Aileron
        {
            get { return Model.J_Aileron; }
            set 
            { Model.J_Aileron = value; }
        }

        public double VM_J_Eleveator
        {
            get { return Model.J_Elevator; }
            set 
            { Model.J_Elevator = value; }
        }


        public JoystickViewModel(JoystickModel model)
        {
            this.Model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_J_" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
