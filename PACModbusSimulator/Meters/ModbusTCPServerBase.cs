using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PACModbusSimulator
{
    public abstract class ModbusTCPServerBase
    {
        private ModbusServer _server;
        public ModbusServer Server
        {
            get
            {
                return _server;
            }

            private set
            {
                this._server = value;
            }
        }

        public ModbusTCPServerBase()
        {
            this.Server = new ModbusServer();
        }
    }
}
