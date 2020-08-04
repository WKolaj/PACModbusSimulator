using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PACModbusSimulator
{
    public class PAC4200 : StandardPAC
    {
        #region constants

        const string digitalInputStateModule1Name = "Digital Inputs Module 1";
        const string digitalOutputStateModule1Name = "Digital Outputs Module 1";

        const string digitalInputStateModule2Name = "Digital Inputs Module 2";
        const string digitalOutputStateModule2Name = "Digital Outputs Module 2";

        #endregion constants

        #region variables

        BitCollectionVariable DigitalInputStateModule1 { get { return (BitCollectionVariable)Variables[digitalInputStateModule1Name]; } }
        BitCollectionVariable DigitalOutputStateModule1 { get { return (BitCollectionVariable)Variables[digitalOutputStateModule1Name]; } }
        BitCollectionVariable DigitalInputStateModule2 { get { return (BitCollectionVariable)Variables[digitalInputStateModule2Name]; } }
        BitCollectionVariable DigitalOutputStateModule2 { get { return (BitCollectionVariable)Variables[digitalOutputStateModule2Name]; } }

        #endregion variables

        //String representing PAC4200 device type
        public const string TypeString = "PAC4200";

        /// <summary>
        /// PAC4200 meter
        /// </summary>
        /// <param name="simulator">Simulator object</param>
        /// <param name="name">Name of PAC meter</param>
        /// <param name="portNumber">Port number</param>
        /// <param name="nominalCurrent">Nominal current</param>
        /// <param name="nominalPowerFactor">Nominal power factor</param>
        public PAC4200(PACSimulator simulator, string name = "", Int32 portNumber = 502, float nominalCurrent = 100, float nominalPowerFactor = 0.8f) : base(simulator, name, portNumber, nominalCurrent, nominalPowerFactor)
        {
        }

        protected override void AssignVariables()
        {
            base.AssignVariables();

            AddVariable(new BitCollectionVariable(digitalOutputStateModule1Name, 232));
            AddVariable(new BitCollectionVariable(digitalInputStateModule1Name, 234));
            AddVariable(new BitCollectionVariable(digitalOutputStateModule2Name, 236));
            AddVariable(new BitCollectionVariable(digitalInputStateModule2Name, 238));
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
