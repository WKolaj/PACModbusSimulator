using ModbusSimulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PACModbusSimulator
{
    public abstract class ModbusMeterBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Base class of Meter
        /// </summary>
        /// <param name="simulator">Simulator associated with meter</param>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number of device</param>
        protected ModbusMeterBase(PACSimulator simulator, string name , Int32 portNumber , byte unitId )
        {
            this.Simulator = simulator;
            this.Sampler = new Sampler(this);
            this.Device = ModbusFactory.CreateNewDevice(portNumber, unitId);
            this.Name = name;
        }

        /// <summary>
        /// Base class of Meter
        /// </summary>
        /// <param name="xElement">
        /// xElement to create device
        /// </param>
        protected ModbusMeterBase(PACSimulator simulator, XElement xElement)
        {
            this.Simulator = simulator;
            this.Sampler = new Sampler(this);

            SetFromXML(xElement);
        }

        /// <summary>
        /// Method for getting type name of device
        /// </summary>
        /// <returns>
        /// String representing type of device
        /// </returns>
        public abstract string GetTypeName();

        /// <summary>
        /// Type representing type of device
        /// </summary>
        public string TypeName
        {
            get
            {
                return GetTypeName();
            }
        }

        private PACSimulator _simulator;
        /// <summary>
        /// Simulator associated with meter
        /// </summary>
        public PACSimulator Simulator
        {
            get
            {
                return _simulator;
            }

            private set
            {
                _simulator = value;
                OnPropertyChanged("Simulator");
            }
        }

        private String _name;
        /// <summary>
        /// Name of meter
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }

            private set
            {
                this._name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Port number of meter
        /// </summary>
        public Int32 PortNumber
        {
            get
            {
                return this.Device.Port;
            }
        }

        /// <summary>
        /// Port number of meter
        /// </summary>
        public byte UnitId
        {
            get
            {
                return this.Device.UnitId;
            }
        }

        private Dictionary<String, VariableBase> _variables = new Dictionary<String, VariableBase>();
        /// <summary>
        /// All variables associated with meter
        /// </summary>
        public Dictionary<String, VariableBase> Variables
        {
            get
            {
                return _variables;
            }

            private set
            {
                _variables = value;
                OnPropertyChanged("Variables");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private Boolean _isRunning = false;
        /// <summary>
        /// Is device running
        /// </summary>
        public Boolean IsRunning
        {
            get
            {
                return _isRunning;
            }

            set
            {
                this._isRunning = value;
                OnPropertyChanged("IsRunning");
                OnPropertyChanged("Editable");
            }
        }

        /// <summary>
        /// Can device be edited
        /// </summary>
        public Boolean Editable
        {
            get
            {
                return !IsRunning;
            }

        }

        private ModbusDevice _device = null;
        /// <summary>
        /// Modbus server representing device
        /// </summary>
        public ModbusDevice Device
        {
            get
            {
                return _device;
            }

            private set
            {
                this._device = value;
                OnPropertyChanged("ModbusDevice");
            }
        }

        private Sampler _sampler;
        /// <summary>
        /// Device sampler
        /// </summary>
        public Sampler Sampler
        {
            get
            {
                return _sampler;
            }

            protected set
            {
                _sampler = value;
                OnPropertyChanged("Sampler");
            }
        }

        private Int64 _lastTickNumber = -1;
        /// <summary>
        /// Tick id of last tick operation
        /// </summary>
        public Int64 LastTickNumber
        {
            get
            {
                return _lastTickNumber;
            }

            private set
            {
                _lastTickNumber = value;
                OnPropertyChanged("LastTickNumber");
            }
        }

        /// <summary>
        /// Modbus holding registers of device
        /// </summary>
        public HoldingRegisters HoldingRegisters
        {
            get
            {
                return Device.holdingRegisters;
            }
        }

        /// <summary>
        /// Modbus input registers representing device
        /// </summary>
        public InputRegisters InputRegisters
        {
            get
            {
                return Device.inputRegisters;
            }
        }

        /// <summary>
        /// Method for refreshing device
        /// </summary>
        /// <param name="tickNumber">
        /// Tick number of refreshing action
        /// </param>
        public void Refresh(Int64 tickNumber)
        {
            if (this.LastTickNumber == -1)
            {
                initCalculation(tickNumber);
            }
            else
            {
                doCalculation(tickNumber, LastTickNumber);
            }

            this.LastTickNumber = tickNumber;

            RefreshAllVariablesValue();
            RewriteValuesToRegisters();
        }

        /// <summary>
        /// Method for refreshing all variables values
        /// </summary>
        protected void RefreshAllVariablesValue()
        {
            foreach (var variable in Variables.Values)
            {
                variable.RefreshValue();
            }
        }

        /// <summary>
        /// Method for initializing auto calculation mechanism
        /// </summary>
        /// <param name="tickNumber">
        /// Tick number of operation
        /// </param>
        protected abstract void initCalculation(Int64 tickNumber);

        /// <summary>
        /// Method for preforming auto calulcation based on variable values
        /// </summary>
        /// <param name="tickNumber">tick id</param>
        /// <param name="lastTickNumber">last tick id</param>
        protected abstract void doCalculation(Int64 tickNumber, Int64 lastTickNumber);

        /// <summary>
        /// Method for rewriting values to modbus holding and input registers
        /// </summary>
        protected virtual void RewriteValuesToRegisters()
        {
            foreach (var variable in Variables.Values)
            {
                if (variable.ShouldInsertToHR)
                {
                    variable.AssignValueToRegisters(HoldingRegisters);
                }

                if (variable.ShouldInsertToIR)
                {
                    variable.AssignValueToRegisters(InputRegisters);
                }
            }
        }

        /// <summary>
        /// Method for adding variable to meter
        /// </summary>
        /// <param name="variable">
        /// Variable to be added
        /// </param>
        public void AddVariable(VariableBase variable)
        {
            Variables[variable.Name] = variable;
        }

        /// <summary>
        /// Method for removing variable from meter
        /// </summary>
        /// <param name="variable">
        /// Variable to be removed
        /// </param>
        public void RemoveVariable(VariableBase variable)
        {
            Variables.Remove(variable.Name);
        }

        /// <summary>
        /// Method for starting device and auto calculation
        /// </summary>
        public void Start()
        {
            this.Device.Enable();
            this.Sampler.Start();
            IsRunning = true;
        }

        /// <summary>
        /// Method for stopping device and auto calculation
        /// </summary>
        public void Stop()
        {
            this.Device.Disable();
            this.Sampler.Stop();
            IsRunning = false;
        }


        /// <summary>
        /// Setting device from XML content
        /// </summary>
        /// <param name="element">
        /// XML content based on which, meters parameters should be set
        /// </param>
        public virtual void SetFromXML(XElement element)
        {
            var name = element.Attribute("Name").Value;
            var portNumber = Convert.ToInt32(element.Attribute("PortNumber").Value);
            var unitId = Convert.ToByte(element.Attribute("UnitId").Value);

            this.Name = name;
            this.Device = ModbusFactory.CreateNewDevice(portNumber, unitId);
        }

        /// <summary>
        /// Method for getting XML content representing meters settings
        /// </summary>
        /// <returns></returns>
        public virtual XElement GetXMLElement()
        {
            XElement elementToReturn = new XElement(TypeName);
            elementToReturn.Add(new XAttribute("Name", this.Name));
            elementToReturn.Add(new XAttribute("PortNumber", this.PortNumber));
            elementToReturn.Add(new XAttribute("UnitId", this.UnitId));

            return elementToReturn;
        }

        /// <summary>
        /// Method for getting user control of meter
        /// </summary>
        /// <returns>
        /// User control for meter
        /// </returns>
        public abstract UserControl GetUserControl();

        /// <summary>
        /// Event invoked when one of settings of MBDevice has changed
        /// </summary>
        public event Action MBDeviceSettingsChanged;

        /// <summary>
        /// Method for invoking event of parameters change
        /// </summary>
        protected void InvokeSettingsChanged()
        {
            if (MBDeviceSettingsChanged != null)
            {
                MBDeviceSettingsChanged();
            }
        }
    }
}
