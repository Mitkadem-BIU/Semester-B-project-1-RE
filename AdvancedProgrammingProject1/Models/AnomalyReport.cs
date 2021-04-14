using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedProgrammingProject1
{
    public class AnomalyReport
    {
        string f1;
        string f2;
        double timestep;
        public AnomalyReport(string f1, string f2, double timestep)
        {
            this.f1 = f1;
            this.f2 = f2;
            this.timestep = timestep;
        }

        public string GetDescription() { return this.f1; }
        public double GetTimeStep() { return this.timestep; }
        public double TimeStep
        {
            get { return timestep; }
        }
        public string F1
        {
            get { return f1; }
        }

        public string F2
        {
            get { return f2; }
        }
    }
}
