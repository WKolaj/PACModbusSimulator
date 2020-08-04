using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;
using EasyModbus;

namespace PACModbusSimulator
{
    public abstract class MeterBase: MBDevice, INotifyPropertyChanged
    {
        /// <summary>
        /// Base class of Meter
        /// </summary>
        /// <param name="simulator">Simulator associated with meter</param>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number of device</param>
        protected MeterBase(PACSimulator simulator, string name = "", Int32 portNumber = 502) : base(simulator,name,502)
        {
        }

    }
}
