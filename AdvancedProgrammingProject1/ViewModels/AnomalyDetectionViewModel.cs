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
