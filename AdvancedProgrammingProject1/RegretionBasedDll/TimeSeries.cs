using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RegretionBasedDll
{
    public class TimeSeries
    {
        List<List<double>> table;
        Dictionary<int, string> features;

        public TimeSeries(DataTable csvTable)
        {
            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                for (int j = 0; j < csvTable.Columns.Count; j++)
                {
                    if (i == 0)
                    {
                        features.Add(j, (string)csvTable.Rows[0][j]);
                    }
                    else
                    {
                        table[i][j] = Double.Parse((string)csvTable.Rows[i][j]);
                    }
                }
            }
		}

        public List<List<double>> GetTable() { return this.table; }
        public Dictionary<int, string> GetFeatures() { return this.features; }
        ~TimeSeries()
        {
            table.Clear();
            features.Clear();
        } 
    }
}
