using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using LumenWorks.Framework.IO.Csv;
using System.Threading;
using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace AdvancedProgrammingProject1
{
    public class ControlBarModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainControllerModel Model { get; }

        public int SlideValue
        {

            get { return Model.SlideValue; }
            set
            {
                Model.SlideValue = value;
             
            }
        }
        public float Time
        {
            get { return Model.Time; }
            set { Model.Time = value; }
        }
        public int LineCounter
        {
            get { return Model.LineCounter; }
            set { Model.LineCounter = value;
                Model.SlideValue = value;
            }
        }

        public bool Stop
        {
            get { return Model.Stop; }
            set { Model.Stop = value; }
        }
        public bool Pause
        {
            get { return Model.Pause; }
            set { Model.Pause = value; }
        }
        public DataTable CSVTable
        {
            get { return Model.CSVTable; }
        }
        public double Speed
        {
            get { return Model.Speed; }
            set { Model.Speed = value; }
        }



        public ControlBarModel(MainControllerModel model)
        {
            Model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }


        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
