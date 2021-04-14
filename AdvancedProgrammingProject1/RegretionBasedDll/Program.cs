using System;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace AnomalyDetectionDll
{
    class Program
    {
        static void Main()
        {
            DataTable csvTable1 = new DataTable();
            DataTable csvTable2 = new DataTable();
            var csvReader1 = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\buein\OneDrive\שולחן העבודה\reg_flight.csv"), true));
            csvTable1.Load(csvReader1);
            var csvReader2 = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\buein\OneDrive\שולחן העבודה\anomaly_flight.csv"), true));
            csvTable2.Load(csvReader2);
            RegretionAnomalyDetector.Learn(csvTable1);
            var l = RegretionAnomalyDetector.Detect(csvTable2);
            var ll = RegretionAnomalyDetector.DetectTime(csvTable2);
            Console.WriteLine(ll.Count);
            foreach(AnomalyReport ar in l)
            {
                Console.WriteLine(ar.GetDescription());
                Console.WriteLine(ar.GetTimeStep());
            }

            Console.WriteLine(ll.Count);
        }
    }
}
