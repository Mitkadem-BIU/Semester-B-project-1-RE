using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    /*
     * The purpose of this interface is to show what abilities and properties
     * the anomaly detection have to satisfy.
     */
    interface IAlgorithm
    {
        PlotModel GetGraphicalView();
        Dictionary<double, Anomaly> GetAnomalies();
    }

    class Anomaly // you can inherit this class and add more functionalities to it
    {
        string description;
        public string Description
        {
            get; set;
        }
    }
}
