using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSimulator
{
    public class ModbusFactory
    {
        private static Dictionary<Int32, ModbusServer> _allServers = new Dictionary<int, ModbusServer>();
        
        /// <summary>
        /// Method for creating a new server and return it
        /// Returns existing server = if server of given port exists
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static ModbusServer CreateServer(Int32 port)
        {
            ModbusServer serverToReturn = null;

            if(ModbusFactory._allServers.ContainsKey(port))
            {
                serverToReturn = ModbusFactory._allServers[port];
            }
            else
            {
                serverToReturn = new ModbusServer();
                serverToReturn.Port = port;

                ModbusFactory._allServers[port] = serverToReturn;
            }

            return serverToReturn;
        }

        /// <summary>
        /// Method for returning server of given port
        /// Returns null if there is no server of given port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static ModbusServer GetServer(Int32 port)
        {
            if (ModbusFactory._allServers.ContainsKey(port)) return ModbusFactory._allServers[port];
            else return null;
        }

        /// <summary>
        /// Method for removing modbus server - returns null if server does not exist
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static ModbusServer RemoveServer(Int32 port)
        {
            ModbusServer serverToReturn = null;

            if (ModbusFactory._allServers.ContainsKey(port))
            {
                StopListening(port);
                serverToReturn = ModbusFactory._allServers[port];
                ModbusFactory._allServers.Remove(port);
            }

            return serverToReturn;
        }

        /// <summary>
        /// Method for getting number of servers 
        /// </summary>
        /// <returns></returns>
        public static Int32 GetNumberOfServers()
        {
            return ModbusFactory._allServers.Count;
        }

        /// <summary>
        /// Method for getting number of devices that are enabled for given server 
        /// </summary>
        /// <returns></returns>
        public static Int32 GetNumberOfEnabledDevices(Int32 port)
        {
            return ModbusFactory._allServers[port].GetNumberOfEnabledModbusDevices();
        }


        /// <summary>
        /// Method for starting listening of server
        /// </summary>
        /// <returns></returns>
        public static void StartListening(Int32 port)
        {
            if (!ModbusFactory._allServers[port].IsListening)
                ModbusFactory._allServers[port].Listen();
        }

        /// <summary>
        /// Method for stopping listening of server
        /// </summary>
        /// <returns></returns>
        public static void StopListening(Int32 port)
        {
            if (ModbusFactory._allServers[port].IsListening)
                ModbusFactory._allServers[port].StopListening();
        }

        /// <summary>
        /// Method for creating new device - returns existing one if device of given port and unitId exists
        /// </summary>
        /// <param name="port"></param>
        /// <param name="unitIdentifier"></param>
        /// <returns></returns>
        public static ModbusDevice CreateNewDevice(Int32 port, byte unitIdentifier = 1)
        {
            //Creating new modbus server - method for create returns exisitng one if server exists
            var server = ModbusFactory.CreateServer(port);

            //Creating new device - method for create returns exisitng one if device exists
            var device = server.CreateModbusDevice(unitIdentifier);

            return device;
        }

        /// <summary>
        /// Method for getting modbus device - returns null if there is no device of given port and unitId
        /// </summary>
        /// <param name="port"></param>
        /// <param name="unitIdentifier"></param>
        /// <returns></returns>
        public static ModbusDevice GetDevice(Int32 port, byte unitIdentifier = 1)
        {
            var server = ModbusFactory.GetServer(port);

            if (server == null) return null;

            var device = server.GetModbusDevice(unitIdentifier);

            return device;
        }

        /// <summary>
        /// Method for removing modbus device - returns null if there is no device of given port and unitId
        /// </summary>
        /// <param name="port"></param>
        /// <param name="unitIdentifier"></param>
        /// <returns></returns>
        public static ModbusDevice RemoveDevice(Int32 port, byte unitIdentifier = 1)
        {
            var server = ModbusFactory.GetServer(port);
            if (server == null) return null;

            var device = server.GetModbusDevice(unitIdentifier);
            if (device == null) return null;

            server.RemoveModbusDevice(unitIdentifier);

            if (server.GetNumberOfModbusDevices() <= 0)
                ModbusFactory.RemoveServer(port);

            return device;
        }

    }
}
