using System;
using System.ComponentModel;

namespace AdvancedProgrammingProject1
{
    // Model View Class. Nothing special over here.
    public class MainControllerViewModel : INotifyPropertyChanged
    {
        private MainControllerModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public string VM_csv
        {
            get { return model.Csv; }
            set { model.Csv = value; }
        }

        public string VM_xml
        {
            get { return model.Xml; }
            set { model.Xml = value; }
        }

        public bool VM_stop
        {
            get { return model.Stop; }
            set { model.Stop = value; }
        }

        public float VM_altimeter
        {
            get { return model.Altimeter; }
            set { model.Altimeter = value; }
        }

        public void Start()
        {
            model.Start();
        }

        public MainControllerViewModel(MainControllerModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}