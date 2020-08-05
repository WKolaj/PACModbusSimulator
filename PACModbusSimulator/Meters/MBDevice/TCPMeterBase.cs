using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PACModbusSimulator
{
    public abstract class TCPMeterBase: MBDevice, INotifyPropertyChanged
    {
        /// <summary>
        /// Base class of Meter
        /// </summary>
        /// <param name="simulator">Simulator associated with meter</param>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number of device</param>
        protected TCPMeterBase(PACSimulator simulator, string name = "", Int32 portNumber = 502) : base(simulator,name,502)
        {
        }

    }
}
