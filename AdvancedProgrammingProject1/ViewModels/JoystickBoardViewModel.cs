using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using LumenWorks.Framework.IO.Csv;
using System.Threading;
using System;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace AdvancedProgrammingProject1.ViewModels
{
    class JoystickBoardViewModel : INotifyPropertyChanged
    {

        // data members

        double joystickElevatorY = 0;
        double joystickAileronX = 0;
        double rudderX = 0;
        double throttleY = 0;
        DataRow table;

        public event PropertyChangedEventHandler PropertyChanged;

        // properties

        public DataRow VM_Table
        {
            get
            {
                return table;
            }
            set
            {
                // gets update of a row of the data from the model
                table = value;
                // call a function that updates all the values (aileron, throttle, etc.)
                updateValues();
            }
        }
        /*
        public string ElevatorString
        {
            get
            {
                return VarStringFactoring(elevator);
            }
        }
        public string AileronString
        {
            get
            {
                return VarStringFactoring(aileron);
            }
        }
        public string RudderString
        {
            get
            {
                return VarStringFactoring(rudder);
            }
        }
        public string ThrottleString
        {
            get
            {
                return VarStringFactoring(throttle);
            }
        }

        */

        // methods
        public void updateValues()
        {

        }


        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
