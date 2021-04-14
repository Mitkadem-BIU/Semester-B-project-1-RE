using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnomalyDetectionDll
{
    public class TimeSeries
    {
        List<List<double>> table = new List<List<double>>();
        Dictionary<int, string> features = new Dictionary<int, string>();

        public TimeSeries(DataTable csvTable)
        {
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                List<double> line=new List<double>();
                for (int j = 0; j < csvTable.Columns.Count; j++)
                {
                    if (i == 0)
                    {
                        features.Add(j, (string)csvTable.Rows[0][j]);
                    }
                    else
                    {
                        line.Add(Double.Parse((string)csvTable.Rows[i][j]));
                    }
                }
                table.Add(line);
            }
		}

        public List<List<double>> GetTable() { return this.table; }
        public Dictionary<int, string> GetFeatures() { return this.features; }
    }
}
