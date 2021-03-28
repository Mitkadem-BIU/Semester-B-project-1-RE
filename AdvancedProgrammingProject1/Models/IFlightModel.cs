using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Globalization;
using LumenWorks.Framework.IO.Csv;
using System.Threading;
using System.ComponentModel;

namespace AdvancedProgrammingProject1
{
    interface IFlightModel : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
    }
}
