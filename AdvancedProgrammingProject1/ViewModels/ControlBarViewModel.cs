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
    public class ControlBarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ControlBarModel Model { get; }

        public int VM_LineCounter
        {
            get { return Model.LineCounter; }
            set { Model.LineCounter = value; }
        }
        public bool VM_Stop
        {
            get { return Model.Stop; }
            set { Model.Stop = value; }
        }
        public bool VM_Pause
        {
            get { return Model.Pause; }
            set { Model.Pause = value; }
        }
        public DataTable VM_CSVTable
        {
            get { return Model.CSVTable; }
        }
        public double VM_Speed
        {
            get { return Model.Speed; }
            set { Model.Speed = value; }
        }

        public float VM_Time
        {
            get { return Model.Time; }
            set { Model.Time = value; }
        }

        public int VM_SlideValue
        {
            get { return Model.SlideValue; }
            set { Model.SlideValue = value; }
        }
        public bool VM_ValueChanged
        {
            get { return Model.ValueChanged; }
            set { Model.ValueChanged = value; }
        }
        public int VM_Minutes
        {
            get { return Model.Minutes; }
            set { Model.Minutes = value; }
        }
        public int VM_Seconds
        {
            get { return Model.Seconds; }
            set { Model.Seconds = value; }
        }
        public ControlBarViewModel(ControlBarModel model)
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