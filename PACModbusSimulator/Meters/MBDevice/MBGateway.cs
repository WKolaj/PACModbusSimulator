using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PACModbusSimulator
{
    public class MBGateway : MBDevice
    {
        /// <summary>
        /// Base class of Meter
        /// </summary>
        /// <param name="simulator">Simulator associated with meter</param>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number of device</param>
        protected MBGateway(PACSimulator simulator, string name = "", Int32 portNumber = 502) : base(simulator, name, 502)
        {
        }

        public override string GetTypeName()
        {
            return "MBGateway";
        }

        public override UserControl GetUserControl()
        {
            return null;
        }

        protected override void doCalculation(long tickNumber, long lastTickNumber)
        {
            throw new NotImplementedException();
        }

        protected override void initCalculation(long tickNumber)
        {
            throw new NotImplementedException();
        }
    }
}
