using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    class AttrPlotViewModel : INotifyPropertyChanged
    {
        private AttrPlotModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public AttrPlotModel Model
        {
            get { return model; }
        }

        public AttrPlotViewModel(AttrPlotModel model)
        {
            this.model = model;
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
