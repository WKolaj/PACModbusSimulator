using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ModbusSimulator.ModbusServer;

namespace ModbusSimulator
{
    public class ModbusDevice
    {
        private byte _unitId;
        public byte UnitId
        {
            get
            {
                return _unitId;
            }

            private set
            {
                _unitId = value;
            }
        }

        private Int32 _port;
        public Int32 Port
        {
            get
            {
                return _port;
            }

            private set
            {
                _port = value;
            }
        }

        private Boolean _enabled = false;
        public Boolean Enabled
        {
            get
            {
                return _enabled;
            }
        }

        public HoldingRegisters holdingRegisters;
        public InputRegisters inputRegisters;
        public Coils coils;
        public DiscreteInputs discreteInputs;

        public ModbusDevice(Int32 port,byte unitId)
        {
            this.Port = port;
            this.UnitId = unitId;

            holdingRegisters = new HoldingRegisters();
            inputRegisters = new InputRegisters();
            coils = new Coils();
            discreteInputs = new DiscreteInputs();
        }

        /// <summary>
        /// Method for enabling device to communicate
        /// </summary>
        public void Enable()
        {
            if (!this.Enabled)
            {
                //Starting listening for server - method doesn't call listen if server is already listening
                ModbusFactory.StartListening(this.Port);

                this._enabled = true;
            }
        }

        /// <summary>
        /// Method for disabling device to communicate
        /// </summary>
        public void Disable()
        {
            if (this.Enabled)
            {
                //Stopping server communication if this the device is the only one that listens
                if(ModbusFactory.GetNumberOfEnabledDevices(this.Port) <= 1)
                    ModbusFactory.StopListening(this.Port);

                this._enabled = false;
            }
        }
    }


    public class HoldingRegisters
    {
        public Int16[] localArray = new Int16[65535];

        public HoldingRegisters()
        {
        }

        public Int16 this[int x]
        {
            get { return this.localArray[x]; }
            set
            {
                this.localArray[x] = value;

            }
        }
    }

    public class InputRegisters
    {
        public Int16[] localArray = new Int16[65535];

        public InputRegisters()
        {
        }

        public Int16 this[int x]
        {
            get { return this.localArray[x]; }
            set
            {
                this.localArray[x] = value;

            }
        }
    }

    public class Coils
    {
        public bool[] localArray = new bool[65535];
        public Coils()
        {
        }

        public bool this[int x]
        {
            get { return this.localArray[x]; }
            set
            {
                this.localArray[x] = value;

            }
        }
    }

    public class DiscreteInputs
    {
        public bool[] localArray = new bool[65535];

        public DiscreteInputs()
        {
        }

        public bool this[int x]
        {
            get { return this.localArray[x]; }
            set
            {
                this.localArray[x] = value;

            }
        }


    }
}

