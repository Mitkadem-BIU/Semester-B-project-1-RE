using System;
using System.ComponentModel;

namespace AdvancedProgrammingProject1
{
    // Model View Class. Nothing special over here.
    class MainControllerViewModel : INotifyPropertyChanged
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