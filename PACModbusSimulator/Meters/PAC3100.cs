using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PACModbusSimulator
{
    public class PAC3100 : StandardPAC
    {
        //String representing PAC3200 device type
        public const string TypeString = "PAC3100";

        /// <summary>
        /// PAC3100 meter
        /// </summary>
        /// <param name="simulator">Simulator object</param>
        /// <param name="name">Name of PAC meter</param>
        /// <param name="portNumber">Port number</param>
        /// <param name="nominalCurrent">Nominal current</param>
        /// <param name="nominalPowerFactor">Nominal power factor</param>
        public PAC3100(PACSimulator simulator, string name = "", Int32 portNumber = 502, Byte unitId = 1, float nominalCurrent = 100, float nominalPowerFactor = 0.8f) : base(simulator, name, portNumber, unitId, nominalCurrent, nominalPowerFactor)
        {
        }

        public PAC3100(PACSimulator simulator, XElement xElement) : base(simulator, xElement)
        {
        }

        /// <summary>
        /// Method for getting string representing device type
        /// </summary>
        /// <returns></returns>
        public override string GetTypeName()
        {
            return TypeString;
        }
    }
}
