using System;
using System.Collections.Generic;
using System.Text;

namespace AnomalyDetectionDll
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

        public string GetDescription() { return this.description; }
        public long GetTimeStep() { return this.timestep; }

        public long TimeStep
        {
            get { return timestep; }
        }
    }
}
