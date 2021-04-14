using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    public class AnomalyDetectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AnomalyDetectionModel Model { get; }

        public double VM_AP_Time
        {
            get { return Model.AP_Time; }
            set { Model.AP_Time = value; }
        }

        public string VM_AttrToPlot
        {
            get { return Model.AttrToPlot; }
        }

        public string VM_PearsonAttrToPlot
        {
            // set { Model.PearsonAttrToPlot = value; }
            get { return Model.PearsonAttrToPlot; }
        }

        public string VM_Csv
        {
            get { return Model.Csv; }
            set { Model.Csv = value; }
        }

        public string VM_AlgoName
        {
            get { return Model.AlgoName; }
            set { Model.AlgoName = value; }
        }

        public PlotModel VM_AnomalyPlotModel
        {
            get { return Model.AnomalyPlotModel; }
        }

        public AnomalyDetectionViewModel(AnomalyDetectionModel model)
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
