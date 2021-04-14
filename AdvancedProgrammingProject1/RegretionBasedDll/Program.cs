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
            /*DataTable csvTable1 = new DataTable();
            DataTable csvTable2 = new DataTable();
            var csvReader1 = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\User\Desktop\מסמכי אוניברסיטה\שנה ב- סמסטר ב\תכנות מתקדם 2\קבצים לפרויקט\reg_flight.csv"), true));
            csvTable1.Load(csvReader1);
            var csvReader2 = new CsvReader(new StreamReader(File.OpenRead(@"C:\Users\User\Desktop\מסמכי אוניברסיטה\שנה ב- סמסטר ב\תכנות מתקדם 2\קבצים לפרויקט\anomaly_flight.csv"), true));
            csvTable2.Load(csvReader2);
            RegretionAnomalyDetector.Learn(csvTable1);
            var l = RegretionAnomalyDetector.Detect(csvTable2);
            foreach(AnomalyReport ar in l)
            {
                Console.WriteLine(ar.GetDescription());
                Console.WriteLine(ar.GetTimeStep());
            }*/
        }
    }
}
