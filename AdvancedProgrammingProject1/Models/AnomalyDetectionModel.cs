using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
            //this is the way to dynamically load a dll file and using its methods
            //string dllFile = @"C:\Users\User\Desktop\מסמכי אוניברסיטה\שנה ב- סמסטר ב\תכנות מתקדם 2\AdvancedProgrammingProject1\AdvancedProgrammingProject1\RegretionBasedDll\bin\Debug\netcoreapp3.1\RegretionBasedDll.dll";
            /*var assembly = Assembly.LoadFile(*//*dllFile*//*AlgoName);
            var type = assembly.GetType("RegretionBasedDll.RegretionAnomalyDetector");
            dynamic obj = Activator.CreateInstance(type);
            var method = type.GetMethod("LearnAndDetect");
            var result = method.Invoke(obj, new object[] { Model.CSVTable });*/
            
            var dllFile = new FileInfo(AlgoName);
            Assembly dllAssembly = Assembly.LoadFile(dllFile.FullName);
            Object dllInstance = (Object)dllAssembly.CreateInstance("AnomalyDetectionDll.RegretionAnomalyDetector");
           /* dll = dllInstance;
            object[] argslearn = new object[] { (object)trainLines };
            maxCorralatedFreatures = (List<Tuple<int, int>>)dll.GetType().GetMethod("learnNormal").Invoke(dll, argslearn);*/
        }
    }
}
