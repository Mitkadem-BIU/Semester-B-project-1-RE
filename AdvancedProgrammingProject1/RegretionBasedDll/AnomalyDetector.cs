using System;
using System.Collections.Generic;
using System.Text;

namespace RegretionBasedDll
{
    public class AnomalyReport
    {
        string description;
        long timestep;
        public AnomalyReport(string description, long timestep)
        {
            this.description = description;
            this.timestep = timestep;
        }

        public AnomalyReport()
        {
            this.description = "";
            this.timestep = 0;
        }
    }
}
