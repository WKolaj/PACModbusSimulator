using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PACModbusSimulator
{
    public abstract class MBDevice : INotifyPropertyChanged
    {
        /// <summary>
        /// Base class of Meter
        /// </summary>
        /// <param name="simulator">Simulator associated with meter</param>
        /// <param name="name">Name of device</param>
        /// <param name="portNumber">Port number of device</param>
        protected MBDevice(PACSimulator simulator, string name = "", Int32 portNumber = 502)
        {
            this.Simulator = simulator;
            this.Sampler = new Sampler(this);
            this.Server = new ModbusServer();
            this.PortNumber = portNumber;
            this.Name = name;
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

        /// <summary>
        /// Port number of meter
        /// </summary>
        public Int32 PortNumber
        {
            get
            {
                return this.Server.Port;
            }

            set
            {
                //Checking if such port already exists
                if (this.CheckPortNumber(value))
                {
                    this.Server.Port = value;
                    OnPropertyChanged("PortNumber");
                    InvokeSettingsChanged();
                }
                else
                {
                    //Recursive incrementing - as long as port number is not uniq
                    this.PortNumber = value + 1;
                }
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

        private ModbusServer _server = null;
        /// <summary>
        /// Modbus server representing device
        /// </summary>
        public ModbusServer Server
        {
            get
            {
                return _server;
            }

            private set
            {
                this._server = value;
                OnPropertyChanged("Server");
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
        public ModbusServer.HoldingRegisters HoldingRegisters
        {
            get
            {
                return Server.holdingRegisters;
            }
        }

        /// <summary>
        /// Modbus input registers representing device
        /// </summary>
        public ModbusServer.InputRegisters InputRegisters
        {
            get
            {
                return Server.inputRegisters;
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
            this.Server.Listen();
            this.Sampler.Start();
            IsRunning = true;
        }

        /// <summary>
        /// Method for stopping device and auto calculation
        /// </summary>
        public void Stop()
        {
            this.Server.StopListening();
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
            if (element.Attribute("Name") != null)
            {
                this.Name = element.Attribute("Name").Value;
            }

            if (element.Attribute("PortNumber") != null)
            {
                this.PortNumber = Convert.ToInt32(element.Attribute("PortNumber").Value);
            }
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

            return elementToReturn;
        }

        /// <summary>
        /// Method for checking if port number value can be added to meter
        /// </summary>
        /// <param name="portNumber">
        /// Port number of meter
        /// </param>
        /// <returns>
        /// Can port number be assigned to meter
        /// </returns>
        private Boolean CheckPortNumber(Int32 portNumber)
        {
            foreach (var meter in Simulator.AllMBDevices)
            {
                if (meter.PortNumber == portNumber && meter != this)
                {
                    return false;
                }
            }

            return true;
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
