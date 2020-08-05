using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PACModbusSimulator
{

    public class StandardPAC : TCPMeterBase
    {
        #region constants

        //constant names of standard PAC meter variable
        
        const string voltageL1NName = "Voltage L1-N";
        const string voltageL2NName = "Voltage L2-N";
        const string voltageL3NName = "Voltage L3-N";
        const string voltageL1L2Name = "Voltage L1-L2";
        const string voltageL2L3Name = "Voltage L2-L3";
        const string voltageL3L1Name = "Voltage L3-L1";
        const string currentL1Name = "Current L1";
        const string currentL2Name = "Current L2";
        const string currentL3Name = "Current L3";
        const string apparentPowerL1Name = "Apparent power L1";
        const string apparentPowerL2Name = "Apparent power L2";
        const string apparentPowerL3Name = "Apparent power L3";
        const string activePowerL1Name = "Active power L1";
        const string activePowerL2Name = "Active power L2";
        const string activePowerL3Name = "Active power L3";
        const string reactivePowerL1Name = "Reactive power L1";
        const string reactivePowerL2Name = "Reactive power L2";
        const string reactivePowerL3Name = "Reactive power L3";
        const string powerFactorL1Name = "Power factor L1";
        const string powerFactorL2Name = "Power factor L2";
        const string powerFactorL3Name = "Power factor L3";
        const string THDVoltageL1Name = "THD voltage L1";
        const string THDVoltageL2Name = "THD voltage L2";
        const string THDVoltageL3Name = "THD voltage L3";
        const string THDCurrentL1Name = "THD current L1";
        const string THDCurrentL2Name = "THD current L2";
        const string THDCurrentL3Name = "THD current L3";
        const string FrequencyName = "Frequency";
        const string AvertageVoltageLNName = "Average voltage L-N";
        const string AvertageVoltageLLName = "Average voltage L-L";
        const string AvertageCurrentName = "Average current";
        const string totalApparentPowerName = "Total apparent power";
        const string totalActivePowerName = "Total active power";
        const string totalReactivePowerName = "Total reactive power";
        const string totalPowerFactorName = "Total power factor";
        const string unbalanceVoltageName = "Unbalance voltage";
        const string unbalanceCurrentName = "Unbalance current";
        const string activeEnergyImportTarif1Name = "Active energy import T1";
        const string activeEnergyImportTarif2Name = "Active energy import T2";
        const string activeEnergyExportTarif1Name = "Active energy export T1";
        const string activeEnergyExportTarif2Name = "Active energy export T1";
        const string reactiveEnergyImportTarif1Name = "Reactive energy import T1";
        const string reactiveEnergyImportTarif2Name = "Reactive energy import T2";
        const string reactiveEnergyExportTarif1Name = "Reactive energy export T1";
        const string reactiveEnergyExportTarif2Name = "Reactive energy export T1";
        const string apparentEnergyTarif1Name = "Apparent energy T1";
        const string apparentEnergyTarif2Name = "Apparent energy T2";
        const string digitalInputStateName = "Digital Inputs";
        const string digitalOutputStateName = "Digital Outputs";

        #endregion constants

        #region variables

        //Variables of standard PAC meter

        FloatVariable VoltageL1N { get { return (FloatVariable)Variables[voltageL1NName]; } }
        FloatVariable VoltageL2N { get { return (FloatVariable)Variables[voltageL2NName]; } }
        FloatVariable VoltageL3N { get { return (FloatVariable)Variables[voltageL3NName]; } }
        FloatVariable VoltageL1L2 { get { return (FloatVariable)Variables[voltageL1L2Name]; } }
        FloatVariable VoltageL2L3 { get { return (FloatVariable)Variables[voltageL2L3Name]; } }
        FloatVariable VoltageL3L1 { get { return (FloatVariable)Variables[voltageL3L1Name]; } }
        FloatVariable CurrentL1 { get { return (FloatVariable)Variables[currentL1Name]; } }
        FloatVariable CurrentL2 { get { return (FloatVariable)Variables[currentL2Name]; } }
        FloatVariable CurrentL3 { get { return (FloatVariable)Variables[currentL3Name]; } }
        FloatVariable ApparentPowerL1 { get { return (FloatVariable)Variables[apparentPowerL1Name]; } }
        FloatVariable ApparentPowerL2 { get { return (FloatVariable)Variables[apparentPowerL2Name]; } }
        FloatVariable ApparentPowerL3 { get { return (FloatVariable)Variables[apparentPowerL3Name]; } }
        FloatVariable ActivePowerL1 { get { return (FloatVariable)Variables[activePowerL1Name]; } }
        FloatVariable ActivePowerL2 { get { return (FloatVariable)Variables[activePowerL2Name]; } }
        FloatVariable ActivePowerL3 { get { return (FloatVariable)Variables[activePowerL3Name]; } }
        FloatVariable ReactivePowerL1 { get { return (FloatVariable)Variables[reactivePowerL1Name]; } }
        FloatVariable ReactivePowerL2 { get { return (FloatVariable)Variables[reactivePowerL2Name]; } }
        FloatVariable ReactivePowerL3 { get { return (FloatVariable)Variables[reactivePowerL3Name]; } }
        FloatVariable PowerFactorL1 { get { return (FloatVariable)Variables[powerFactorL1Name]; } }
        FloatVariable PowerFactorL2 { get { return (FloatVariable)Variables[powerFactorL2Name]; } }
        FloatVariable PowerFactorL3 { get { return (FloatVariable)Variables[powerFactorL3Name]; } }
        FloatVariable THDVoltageL1 { get { return (FloatVariable)Variables[THDVoltageL1Name]; } }
        FloatVariable THDVoltageL2 { get { return (FloatVariable)Variables[THDVoltageL2Name]; } }
        FloatVariable THDVoltageL3 { get { return (FloatVariable)Variables[THDVoltageL3Name]; } }
        FloatVariable THDCurrentL1 { get { return (FloatVariable)Variables[THDCurrentL1Name]; } }
        FloatVariable THDCurrentL2 { get { return (FloatVariable)Variables[THDCurrentL2Name]; } }
        FloatVariable THDCurrentL3 { get { return (FloatVariable)Variables[THDCurrentL3Name]; } }
        FloatVariable Frequency { get { return (FloatVariable)Variables[FrequencyName]; } }
        FloatVariable AvertageVoltageLN { get { return (FloatVariable)Variables[AvertageVoltageLNName]; } }
        FloatVariable AvertageVoltageLL { get { return (FloatVariable)Variables[AvertageVoltageLLName]; } }
        FloatVariable AvertageCurrent { get { return (FloatVariable)Variables[AvertageCurrentName]; } }
        FloatVariable TotalApparentPower { get { return (FloatVariable)Variables[totalApparentPowerName]; } }
        FloatVariable TotalActivePower { get { return (FloatVariable)Variables[totalActivePowerName]; } }
        FloatVariable TotalReactivePower { get { return (FloatVariable)Variables[totalReactivePowerName]; } }
        FloatVariable TotalPowerFactor { get { return (FloatVariable)Variables[totalPowerFactorName]; } }
        FloatVariable UnbalanceVoltage { get { return (FloatVariable)Variables[unbalanceVoltageName]; } }
        FloatVariable UnbalanceCurrent { get { return (FloatVariable)Variables[unbalanceCurrentName]; } }
        FloatVariable ActiveEnergyImportTarif1 { get { return (FloatVariable)Variables[activeEnergyImportTarif1Name]; } }
        FloatVariable ActiveEnergyImportTarif2 { get { return (FloatVariable)Variables[activeEnergyImportTarif2Name]; } }
        FloatVariable ActiveEnergyExportTarif1 { get { return (FloatVariable)Variables[activeEnergyExportTarif1Name]; } }
        FloatVariable ActiveEnergyExportTarif2 { get { return (FloatVariable)Variables[activeEnergyExportTarif2Name]; } }
        FloatVariable ReactiveEnergyImportTarif1 { get { return (FloatVariable)Variables[reactiveEnergyImportTarif1Name]; } }
        FloatVariable ReactiveEnergyImportTarif2 { get { return (FloatVariable)Variables[reactiveEnergyImportTarif2Name]; } }
        FloatVariable ReactiveEnergyExportTarif1 { get { return (FloatVariable)Variables[reactiveEnergyExportTarif1Name]; } }
        FloatVariable ReactiveEnergyExportTarif2 { get { return (FloatVariable)Variables[reactiveEnergyExportTarif2Name]; } }
        FloatVariable ApparentEnergyTarif1 { get { return (FloatVariable)Variables[apparentEnergyTarif1Name]; } }
        FloatVariable ApparentEnergyTarif2 { get { return (FloatVariable)Variables[apparentEnergyTarif2Name]; } }
        BitCollectionVariable DigitalInputState { get { return (BitCollectionVariable)Variables[digitalInputStateName]; } }
        BitCollectionVariable DigitalOutputState { get { return (BitCollectionVariable)Variables[digitalOutputStateName]; } }

        #endregion variables

        private Single _nominalCurrent;
        /// <summary>
        /// Nominal current of PAC meter
        /// </summary>
        public Single NominalCurrent
        {
            get
            {
                return _nominalCurrent;
            }

            set
            {
                this._nominalCurrent = value;
                OnPropertyChanged("NominalCurrent");
                InvokeSettingsChanged();
            }
        }

        private Single _nominalPowerFactor;
        /// <summary>
        /// Nominal power factor measured by PAC meter
        /// </summary>
        public Single NominalPowerFactor
        {
            get
            {
                return _nominalPowerFactor;
            }

            set
            {
                this._nominalPowerFactor = value;
                OnPropertyChanged("NominalCurrent");
                InvokeSettingsChanged();
            }
        }

        /// <summary>
        /// Standard PAC meter
        /// </summary>
        /// <param name="simulator">Simulator object</param>
        /// <param name="name">Name of PAC meter</param>
        /// <param name="portNumber">Port number</param>
        /// <param name="nominalCurrent">Nominal current</param>
        /// <param name="nominalPowerFactor">Nominal power factor</param>
        public StandardPAC(PACSimulator simulator, string name, Int32 portNumber = 502, float nominalCurrent = 100, float nominalPowerFactor = 0.8f) : base(simulator, name, portNumber)
        {
            this.NominalCurrent = nominalCurrent;
            this.NominalPowerFactor = nominalPowerFactor;

            AssignVariables();
            InitVariableMechanism();
        }

        /// <summary>
        /// Method for initializing variable mechanism
        /// </summary>
        protected virtual void InitVariableMechanism()
        {
            VoltageL1N.RandomMax = 236;
            VoltageL1N.RandomMin = 235;
            VoltageL1N.RandomGeneration = true;

            VoltageL2N.RandomMax = 235;
            VoltageL2N.RandomMin = 234;
            VoltageL2N.RandomGeneration = true;

            VoltageL3N.RandomMax = 237;
            VoltageL3N.RandomMin = 236;
            VoltageL3N.RandomGeneration = true;

            VoltageL1L2.RandomMax = 406;
            VoltageL1L2.RandomMin = 405;
            VoltageL1L2.RandomGeneration = true;

            VoltageL2L3.RandomMax = 405;
            VoltageL2L3.RandomMin = 404;
            VoltageL2L3.RandomGeneration = true;

            VoltageL3L1.RandomMax = 407;
            VoltageL3L1.RandomMin = 406;
            VoltageL3L1.RandomGeneration = true;

            CurrentL1.RandomMax = 1.1f * NominalCurrent;
            CurrentL1.RandomMin = 1.0f * NominalCurrent;
            CurrentL1.RandomGeneration = true;

            CurrentL2.RandomMax = 1.05f * NominalCurrent;
            CurrentL2.RandomMin = 0.95f * NominalCurrent;
            CurrentL2.RandomGeneration = true;

            CurrentL3.RandomMax = 1.0f * NominalCurrent;
            CurrentL3.RandomMin = 0.9f * NominalCurrent;
            CurrentL3.RandomGeneration = true;

            PowerFactorL1.RandomMax = NominalPowerFactor;
            PowerFactorL1.RandomMin = NominalPowerFactor*0.9f;
            PowerFactorL1.RandomGeneration = true;

            PowerFactorL2.RandomMax = NominalPowerFactor;
            PowerFactorL2.RandomMin = NominalPowerFactor * 0.95f;
            PowerFactorL2.RandomGeneration = true;

            PowerFactorL3.RandomMax = NominalPowerFactor;
            PowerFactorL3.RandomMin = NominalPowerFactor * 0.85f;
            PowerFactorL3.RandomGeneration = true;

            THDCurrentL1.RandomMax = 12.0f;
            THDCurrentL1.RandomMin = 10.0f;
            THDCurrentL1.RandomGeneration = true;

            THDCurrentL2.RandomMax = 9.0f;
            THDCurrentL2.RandomMin = 8.0f;
            THDCurrentL2.RandomGeneration = true;

            THDCurrentL3.RandomMax = 7.0f;
            THDCurrentL3.RandomMin = 6.0f;
            THDCurrentL3.RandomGeneration = true;

            THDVoltageL1.RandomMax = 2.0f;
            THDVoltageL1.RandomMin = 1.0f;
            THDVoltageL1.RandomGeneration = true;

            THDVoltageL2.RandomMax = 3.0f;
            THDVoltageL2.RandomMin = 2.0f;
            THDVoltageL2.RandomGeneration = true;

            THDVoltageL3.RandomMax = 3.0f;
            THDVoltageL3.RandomMin = 2.5f;
            THDVoltageL3.RandomGeneration = true;

            Frequency.RandomMax = 50.5f;
            Frequency.RandomMin = 49.5f;
            Frequency.RandomGeneration = true;

            ApparentPowerL1.AutoCalculation = true;
            ApparentPowerL2.AutoCalculation = true;
            ApparentPowerL3.AutoCalculation = true;
            ActivePowerL1.AutoCalculation = true;
            ActivePowerL2.AutoCalculation = true;
            ActivePowerL3.AutoCalculation = true;
            ReactivePowerL1.AutoCalculation = true;
            ReactivePowerL2.AutoCalculation = true;
            ReactivePowerL3.AutoCalculation = true;
            AvertageVoltageLN.AutoCalculation = true;
            AvertageVoltageLL.AutoCalculation = true;
            AvertageCurrent.AutoCalculation = true;
            TotalApparentPower.AutoCalculation = true;
            TotalActivePower.AutoCalculation = true;
            TotalReactivePower.AutoCalculation = true;
            TotalPowerFactor.AutoCalculation = true;
            UnbalanceVoltage.AutoCalculation = true;
            UnbalanceCurrent.AutoCalculation = true;
            ActiveEnergyImportTarif1.AutoCalculation = true;
            ActiveEnergyImportTarif2.AutoCalculation = true;
            ActiveEnergyExportTarif1.AutoCalculation = true;
            ActiveEnergyExportTarif2.AutoCalculation = true;
            ReactiveEnergyImportTarif1.AutoCalculation = true;
            ReactiveEnergyImportTarif2.AutoCalculation = true;
            ReactiveEnergyExportTarif1.AutoCalculation = true;
            ReactiveEnergyExportTarif2.AutoCalculation = true;
            ApparentEnergyTarif1.AutoCalculation = true;
            ApparentEnergyTarif2.AutoCalculation = true;

        }

        /// <summary>
        /// Method for assigning variables to device's Variables
        /// </summary>
        protected virtual void AssignVariables()
        {
            AddVariable(new FloatVariable(voltageL1NName, 2));
            AddVariable(new FloatVariable(voltageL2NName, 4));
            AddVariable(new FloatVariable(voltageL3NName, 6));
            AddVariable(new FloatVariable(voltageL1L2Name, 8));
            AddVariable(new FloatVariable(voltageL2L3Name, 10));
            AddVariable(new FloatVariable(voltageL3L1Name, 12));
            AddVariable(new FloatVariable(currentL1Name, 14));
            AddVariable(new FloatVariable(currentL2Name, 16));
            AddVariable(new FloatVariable(currentL3Name, 18));
            AddVariable(new FloatVariable(apparentPowerL1Name, 20));
            AddVariable(new FloatVariable(apparentPowerL2Name, 22));
            AddVariable(new FloatVariable(apparentPowerL3Name, 24));
            AddVariable(new FloatVariable(activePowerL1Name, 26));
            AddVariable(new FloatVariable(activePowerL2Name, 28));
            AddVariable(new FloatVariable(activePowerL3Name, 30));
            AddVariable(new FloatVariable(reactivePowerL1Name, 32));
            AddVariable(new FloatVariable(reactivePowerL2Name, 34));
            AddVariable(new FloatVariable(reactivePowerL3Name, 36));
            AddVariable(new FloatVariable(powerFactorL1Name, 38));
            AddVariable(new FloatVariable(powerFactorL2Name, 40));
            AddVariable(new FloatVariable(powerFactorL3Name, 42));
            AddVariable(new FloatVariable(THDVoltageL1Name, 44));
            AddVariable(new FloatVariable(THDVoltageL2Name, 46));
            AddVariable(new FloatVariable(THDVoltageL3Name, 48));
            AddVariable(new FloatVariable(THDCurrentL1Name, 50));
            AddVariable(new FloatVariable(THDCurrentL2Name, 52));
            AddVariable(new FloatVariable(THDCurrentL3Name, 54));
            AddVariable(new FloatVariable(FrequencyName, 56));
            AddVariable(new FloatVariable(AvertageVoltageLNName, 58));
            AddVariable(new FloatVariable(AvertageVoltageLLName, 60));
            AddVariable(new FloatVariable(AvertageCurrentName, 62));
            AddVariable(new FloatVariable(totalApparentPowerName, 64));
            AddVariable(new FloatVariable(totalActivePowerName, 66));
            AddVariable(new FloatVariable(totalReactivePowerName, 68));
            AddVariable(new FloatVariable(totalPowerFactorName, 70));
            AddVariable(new FloatVariable(unbalanceVoltageName, 72));
            AddVariable(new FloatVariable(unbalanceCurrentName, 74));
            AddVariable(new BitCollectionVariable(digitalOutputStateName, 208));
            AddVariable(new BitCollectionVariable(digitalInputStateName, 210));
            AddVariable(new FloatVariable(activeEnergyImportTarif1Name, 2802));
            AddVariable(new FloatVariable(activeEnergyImportTarif2Name, 2804));
            AddVariable(new FloatVariable(activeEnergyExportTarif1Name, 2806));
            AddVariable(new FloatVariable(activeEnergyExportTarif2Name, 2808));
            AddVariable(new FloatVariable(reactiveEnergyImportTarif1Name, 2810));
            AddVariable(new FloatVariable(reactiveEnergyImportTarif2Name, 2812));
            AddVariable(new FloatVariable(reactiveEnergyExportTarif1Name, 2814));
            AddVariable(new FloatVariable(reactiveEnergyExportTarif2Name, 2816));
            AddVariable(new FloatVariable(apparentEnergyTarif1Name, 2818));
            AddVariable(new FloatVariable(apparentEnergyTarif2Name, 2820));
        }

        /// <summary>
        /// Method for calculating auto values of standard variables
        /// </summary>
        protected virtual void CaluculateStandardVariables()
        {

            VoltageL1N.AutoCalculatedValue = 230;
            VoltageL2N.AutoCalculatedValue = 230;
            VoltageL3N.AutoCalculatedValue = 230;

            VoltageL1L2.AutoCalculatedValue = 400;
            VoltageL2L3.AutoCalculatedValue = 400;
            VoltageL3L1.AutoCalculatedValue = 400;

            CurrentL1.AutoCalculatedValue = NominalCurrent;
            CurrentL2.AutoCalculatedValue = NominalCurrent;
            CurrentL3.AutoCalculatedValue = NominalCurrent;

            PowerFactorL1.AutoCalculatedValue = NominalPowerFactor;
            PowerFactorL2.AutoCalculatedValue = NominalPowerFactor;
            PowerFactorL3.AutoCalculatedValue = NominalPowerFactor;

            THDCurrentL1.AutoCalculatedValue = 10.0;
            THDCurrentL2.AutoCalculatedValue = 10.0;
            THDCurrentL3.AutoCalculatedValue = 10.0;

            THDVoltageL1.AutoCalculatedValue = 2.0;
            THDVoltageL2.AutoCalculatedValue = 2.0;
            THDVoltageL3.AutoCalculatedValue = 2.0;

            Frequency.AutoCalculatedValue = 50.0;

            ApparentPowerL1.AutoCalculatedValue = Convert.ToSingle(CurrentL1.Value) * Convert.ToSingle(VoltageL1N.Value);
            ApparentPowerL2.AutoCalculatedValue = Convert.ToSingle(CurrentL2.Value) * Convert.ToSingle(VoltageL2N.Value);
            ApparentPowerL3.AutoCalculatedValue = Convert.ToSingle(CurrentL3.Value) * Convert.ToSingle(VoltageL3N.Value);

            ActivePowerL1.AutoCalculatedValue = Convert.ToSingle(ApparentPowerL1.Value) * Convert.ToSingle(PowerFactorL1.Value);
            ActivePowerL2.AutoCalculatedValue = Convert.ToSingle(ApparentPowerL2.Value) * Convert.ToSingle(PowerFactorL2.Value);
            ActivePowerL3.AutoCalculatedValue = Convert.ToSingle(ApparentPowerL3.Value) * Convert.ToSingle(PowerFactorL3.Value);

            ReactivePowerL1.AutoCalculatedValue = Convert.ToSingle(Math.Sqrt(Math.Abs(Convert.ToSingle(ApparentPowerL1.Value) * Convert.ToSingle(ApparentPowerL1.Value) - Convert.ToSingle(ActivePowerL1.Value) * Convert.ToSingle(ActivePowerL1.Value))));
            ReactivePowerL2.AutoCalculatedValue = Convert.ToSingle(Math.Sqrt(Math.Abs(Convert.ToSingle(ApparentPowerL2.Value) * Convert.ToSingle(ApparentPowerL2.Value) - Convert.ToSingle(ActivePowerL2.Value) * Convert.ToSingle(ActivePowerL2.Value))));
            ReactivePowerL3.AutoCalculatedValue = Convert.ToSingle(Math.Sqrt(Math.Abs(Convert.ToSingle(ApparentPowerL3.Value) * Convert.ToSingle(ApparentPowerL3.Value) - Convert.ToSingle(ActivePowerL3.Value) * Convert.ToSingle(ActivePowerL3.Value))));

            AvertageVoltageLN.AutoCalculatedValue = (Convert.ToSingle(VoltageL1N.Value) + Convert.ToSingle(VoltageL2N.Value) + Convert.ToSingle(VoltageL3N.Value)) / 3;
            AvertageVoltageLL.AutoCalculatedValue = (Convert.ToSingle(VoltageL1L2.Value) + Convert.ToSingle(VoltageL2L3.Value) + Convert.ToSingle(VoltageL3L1.Value)) / 3;

            AvertageCurrent.AutoCalculatedValue = (Convert.ToSingle(CurrentL1.Value) + Convert.ToSingle(CurrentL2.Value) + Convert.ToSingle(CurrentL3.Value)) / 3;

            TotalApparentPower.AutoCalculatedValue = Convert.ToSingle(ApparentPowerL1.Value) + Convert.ToSingle(ApparentPowerL2.Value) + Convert.ToSingle(ApparentPowerL3.Value);
            TotalActivePower.AutoCalculatedValue = Convert.ToSingle(ActivePowerL1.Value) + Convert.ToSingle(ActivePowerL2.Value) + Convert.ToSingle(ActivePowerL3.Value);
            TotalReactivePower.AutoCalculatedValue = Convert.ToSingle(ReactivePowerL1.Value) + Convert.ToSingle(ReactivePowerL2.Value) + Convert.ToSingle(ReactivePowerL3.Value);
            TotalPowerFactor.AutoCalculatedValue = Convert.ToSingle(TotalApparentPower.Value) != 0 ? Convert.ToSingle(TotalActivePower.Value) / Convert.ToSingle(TotalApparentPower.Value) : 0.0f;
            UnbalanceVoltage.AutoCalculatedValue = Convert.ToSingle(AvertageVoltageLN.Value) != 0 ? Math.Max(Math.Max(Convert.ToSingle(VoltageL1N.Value), Convert.ToSingle(VoltageL2N.Value)), Convert.ToSingle(VoltageL3N.Value)) / Convert.ToSingle(AvertageVoltageLN.Value) : 0;

            UnbalanceCurrent.AutoCalculatedValue = Convert.ToSingle(AvertageCurrent.Value) != 0 ? Math.Max(Math.Max(Convert.ToSingle(CurrentL1.Value), Convert.ToSingle(CurrentL2.Value)), Convert.ToSingle(CurrentL3.Value)) / Convert.ToSingle(AvertageCurrent.Value) : 0;

        }

        /// <summary>
        /// Method for calculating auto values of energy variables
        /// </summary>
        protected virtual void CalculateEnergyVariables(long tickNumber, long lastTickNumber)
        {
            var timeDelta = Convert.ToSingle((tickNumber - lastTickNumber) / 1000);


            var activeEnergyToAdd = Convert.ToSingle(TotalActivePower.Value) * (timeDelta) / 3600;
            var reactiveEnergyToAdd = Convert.ToSingle(TotalReactivePower.Value) * (timeDelta) / 3600;
            var apparentEnergyToAdd = Convert.ToSingle(TotalApparentPower.Value) * (timeDelta) / 3600;

            if (activeEnergyToAdd > 0) ActiveEnergyImportTarif1.AutoCalculatedValue = Convert.ToSingle(ActiveEnergyImportTarif1.Value) + activeEnergyToAdd;
            if (activeEnergyToAdd < 0) ActiveEnergyExportTarif1.AutoCalculatedValue = Convert.ToSingle(ActiveEnergyExportTarif1.Value) - activeEnergyToAdd;
            if (reactiveEnergyToAdd > 0) ReactiveEnergyImportTarif1.AutoCalculatedValue = Convert.ToSingle(ReactiveEnergyImportTarif1.Value) + reactiveEnergyToAdd;
            if (reactiveEnergyToAdd < 0) ReactiveEnergyExportTarif1.AutoCalculatedValue = Convert.ToSingle(ReactiveEnergyExportTarif1.Value) - reactiveEnergyToAdd;
            ApparentEnergyTarif1.AutoCalculatedValue = Convert.ToSingle(ApparentEnergyTarif1.Value) + apparentEnergyToAdd;

        }

        /// <summary>
        /// Method for initializing energy counters values
        /// </summary>
        protected virtual void InitEnergyVariables()
        {
            ActiveEnergyImportTarif1.AutoCalculatedValue = 0;
            ActiveEnergyExportTarif1.AutoCalculatedValue = 0;
            ReactiveEnergyImportTarif1.AutoCalculatedValue = 0;
            ReactiveEnergyExportTarif1.AutoCalculatedValue = 0;
            ApparentEnergyTarif1.AutoCalculatedValue = 0;
        }

        /// <summary>
        /// Method for preforming auto calculation of variables
        /// </summary>
        /// <param name="tickNumber">tick number of actual operation</param>
        /// <param name="lastTickNumber">tick number of last operation</param>
        protected override void doCalculation(long tickNumber, long lastTickNumber)
        {
            CaluculateStandardVariables();
            CalculateEnergyVariables(tickNumber,lastTickNumber);
        }

        /// <summary>
        /// Method for initializing auto calculation
        /// </summary>
        /// <param name="tickNumber">
        /// Tick number of operation
        /// </param>
        protected override void initCalculation(long tickNumber)
        {
            InitVariableMechanism();
            CaluculateStandardVariables();
            InitEnergyVariables();
        }

        /// <summary>
        /// Method for getting type name representing device
        /// </summary>
        /// <returns></returns>
        public override string GetTypeName()
        {
            return "StandardPAC";
        }

        /// <summary>
        /// Method for getting user control of device
        /// </summary>
        /// <returns>
        /// User control of device
        /// </returns>
        public override UserControl GetUserControl()
        {
            var userControl = new MeterUserControl();
            userControl.Init(this);
            return userControl;
        }

        /// <summary>
        /// Setting device from XML content
        /// </summary>
        /// <param name="element">
        /// XML content based on which, meters parameters should be set
        /// </param>
        public override void SetFromXML(XElement element)
        {
            base.SetFromXML(element);

            if (element.Attribute("NominalCurrent") != null)
            {
                this.NominalCurrent = Convert.ToSingle(element.Attribute("NominalCurrent").Value, CultureInfo.InvariantCulture);
            }

            if (element.Attribute("NominalPowerFactor") != null)
            {
                this.NominalPowerFactor = Convert.ToSingle(element.Attribute("NominalPowerFactor").Value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Method for getting XML content representing meters settings
        /// </summary>
        /// <returns></returns>
        public override XElement GetXMLElement()
        {
            var xElement = base.GetXMLElement();

            xElement.Add(new XAttribute("NominalCurrent",this.NominalCurrent.ToString(CultureInfo.InvariantCulture)));
            xElement.Add(new XAttribute("NominalPowerFactor",this.NominalPowerFactor.ToString(CultureInfo.InvariantCulture)));

            return xElement;
        }
    }
}
