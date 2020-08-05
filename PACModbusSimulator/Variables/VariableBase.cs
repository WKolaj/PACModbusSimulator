using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EasyModbus;
using System.Windows.Controls;

namespace PACModbusSimulator
{
    public abstract class VariableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Base class of meters variable
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="offset">Offset of variable</param>
        /// <param name="shouldInsertToHR">Should variable be insterted to Holding registers</param>
        /// <param name="shouldInsertToIR">Should variable be insterted to Input registers</param>
        /// <param name="randomGeneration">Should varaible be calculated as random value - from start</param>
        /// <param name="autoCalculation">Should variable be calculated automatically from start</param>
        protected VariableBase(String name, Int32 offset, Boolean shouldInsertToHR = true, Boolean shouldInsertToIR = true, Boolean randomGeneration = false, Boolean autoCalculation = false)
        {
            this.Name = name;
            this.RandomGeneration = randomGeneration;
            this.AutoCalculation = autoCalculation;
            this.RND = new Random();
            this.Offset = offset;
            this.ShouldInsertToHR = shouldInsertToHR;
            this.ShouldInsertToIR = shouldInsertToIR;
        }

        private Boolean _shouldInsertToHR;
        /// <summary>
        /// Should value of variable be inserted to Modbus holding registers
        /// </summary>
        public Boolean ShouldInsertToHR
        {
            get
            {
                return _shouldInsertToHR;
            }

            private set
            {
                _shouldInsertToHR = value;
                OnPropertyChanged("ShouldInsertToHR");
            }
        }

        private Boolean _shouldInsertToIR;
        /// <summary>
        /// Should value of variable be inserted to Modbus input registers
        /// </summary>
        public Boolean ShouldInsertToIR
        {
            get
            {
                return _shouldInsertToIR;
            }

            set
            {
                _shouldInsertToIR = value;
                OnPropertyChanged("ShouldInsertToIR");
            }
        }

        private Int32 _offset;
        /// <summary>
        /// Modbus offset of variable
        /// </summary>
        public Int32 Offset
        {
            get
            {
                return _offset;
            }

            private set
            {
                this._offset = value;
                OnPropertyChanged("Offset");
            }
        }

        private Random _rnd;
        /// <summary>
        /// Random number generator
        /// </summary>
        public Random RND
        {
            get
            {
                return _rnd;
            }

            private set
            {
                this._rnd = value;
            }
        }

        private String _name;
        /// <summary>
        /// Name of variable
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private Int16[] _data;
        /// <summary>
        /// Modbus variable data - as collection of registers
        /// </summary>
        public Int16[] Data
        {
            get
            {
                return _data;
            }

            protected set
            {
                _data = value;
            }
        }

        /// <summary>
        /// Method for converting data to value of variable
        /// </summary>
        /// <param name="Data">Data to convert</param>
        /// <returns>Variable value</returns>
        abstract protected Object ConvertDataToValue(Int16[] Data);

        /// <summary>
        /// Method for converting value to modbus registers
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Modbus registers data</returns>
        abstract protected Int16[] ConvertValueToData(Object value);

        /// <summary>
        /// Method for generating random value
        /// </summary>
        /// <returns></returns>
        abstract public Object GenerateRandom();

        /// <summary>
        /// Value of variable
        /// </summary>
        public Object Value
        {
            get
            {
                return ConvertDataToValue(Data);
            }

            set
            {
                this.Data = ConvertValueToData(value);
                OnPropertyChanged("Value");
            }
        }

        private Object _autoCalculatedValue;
        /// <summary>
        /// Value of variable calculated automatically
        /// </summary>
        public Object AutoCalculatedValue
        {
            get
            {
                return _autoCalculatedValue;
            }

            set
            {
                this._autoCalculatedValue = value;
                OnPropertyChanged("AutoCalculatedValue");
            }
        }

        private Boolean _autoCalculation;
        /// <summary>
        /// Should value of variable be calucated automatically
        /// </summary>
        public Boolean AutoCalculation
        {
            get
            {
                return _autoCalculation;
            }

            set
            {
                _autoCalculation = value;

                if (value)
                {
                    startAutoCalculation();
                }

                OnPropertyChanged("AutoCalculation");
                OnPropertyChanged("ManualInserting");
            }
        }

        private Boolean _randomGeneration;
        /// <summary>
        /// Should value of variable be generated randomly
        /// </summary>
        public Boolean RandomGeneration
        {
            get
            {
                return _randomGeneration;
            }

            set
            {
                _randomGeneration = value;

                if (value)
                {
                    startRandomGeneration();
                }

                OnPropertyChanged("RandomGeneration");
                OnPropertyChanged("ManualInserting");
            }
        }

        /// <summary>
        /// Should value of variable be inserted manually
        /// </summary>
        public Boolean ManualInserting
        {
            get
            {
                return !(AutoCalculation || RandomGeneration);
            }
        }

        /// <summary>
        /// Method for starting random generation of value
        /// </summary>
        void startRandomGeneration()
        {
            if (AutoCalculation)
            {
                AutoCalculation = false;
            }
        }

        /// <summary>
        /// Method for starting auto generation of value
        /// </summary>
        void startAutoCalculation()
        {
            if (RandomGeneration)
            {
                RandomGeneration = false;
            }
        }

        /// <summary>
        /// Method for assigning value to modbus holding registers
        /// </summary>
        /// <param name="registers">
        /// Modbus holding registers to which variable should be added
        /// </param>
        public void AssignValueToRegisters(ModbusServer.HoldingRegisters registers)
        {
            for(int i=Offset; i< Offset + Data.Length; i++)
            {
                registers[i] = Data[i-Offset];
            }
        }

        /// <summary>
        /// Method for assigning value to modbus input registers
        /// </summary>
        /// <param name="registers">
        /// Modbus input registers to which variable should be added
        /// </param>
        public void AssignValueToRegisters(ModbusServer.InputRegisters registers)
        {
            for (int i = Offset; i < Offset + Data.Length; i++)
            {
                registers[i] = Data[i - Offset];
            }
        }

        /// <summary>
        /// Method for refreshing value of variable based on manual / random / auto mechanism
        /// </summary>
        public void RefreshValue()
        {
            if(RandomGeneration)
            {
                this.Value = GenerateRandom();
            }
            else if(AutoCalculation)
            {
                this.Value = AutoCalculatedValue;
            }
            else if(ManualInserting)
            {
                //Inserting value to autocalculatedValues in order to maintain autocalcualtedMechanism of rest variables based on manual value
                this.AutoCalculatedValue = this.Value;
            }
        }

        /// <summary>
        /// Method for getting user control associated with variable
        /// </summary>
        /// <returns>
        /// User control associated with variable
        /// </returns>
        public abstract UserControl GetUserControl();
    }

}
