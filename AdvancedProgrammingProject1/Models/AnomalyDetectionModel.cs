using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    public class AnomalyDetectionModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainControllerModel Model { get; }

        public string Csv
        {
            get { return Model.Csv; }
            set { Model.Csv = value; }
        }

        public string AlgoName
        {
            get { return Model.AlgoName; }
            set
            {
                Model.AlgoName = value;
                LoadDLL();
            }
        }

        public AnomalyDetectionModel(MainControllerModel model)
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

        public void LoadDLL()
        {
            Assembly asm = Assembly.LoadFrom(AlgoName);
            // Type t = asm.GetType("NAMESPACE.AnomalyDetector");
            // dynamic det = Activator.CreateInstance(t);
            
            /* 
            MethodInfo method = t.GetMethod("MethodName"); */
            // object result = method.Invoke(null, new object[] { param1, param2, ... });

            // DoSomeMagicUsingTheAssemblyToConvertObjectToCorrectType(asm);
            // ... myObject = DoSomeMagicUsingTheAssemblyToConvertObjectToCorrectType(asm);


            // Output the result using application:
            // MessageBox.Show(myObject.DoGreeting());
        }

    }
}
