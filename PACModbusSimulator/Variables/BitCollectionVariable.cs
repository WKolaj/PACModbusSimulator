using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PACModbusSimulator
{
    public class BitCollectionVariable : VariableBase
    {

        /// <summary>
        /// Method for setting bit value in collection
        /// </summary>
        /// <param name="bitIndex">
        /// index of bit
        /// </param>
        /// <param name="bitValue">
        /// value of bit
        /// </param>
        public void SetBitValue(int bitIndex, Boolean bitValue)
        {
            //Creating a copy of bit array
            BitArray value = (BitArray)Value;
            BitArray copyOfValue = (BitArray) value.Clone();

            //Changing the copy of bit array
            copyOfValue[bitIndex] = bitValue;

            //Setting new, modified copy
            Value = copyOfValue;
        }

        /// <summary>
        /// UInt32 variable
        /// </summary>
        /// <param name="name">Name of variable</param>
        /// <param name="offset">Offset of variable</param>
        /// <param name="shouldInsertToHR">Should variable be insterted to Holding registers</param>
        /// <param name="shouldInsertToIR">Should variable be insterted to Input registers</param>
        /// <param name="randomGeneration">Should varaible be calculated as random value - from start</param>
        /// <param name="autoCalculation">Should variable be calculated automatically from start</param>
        public BitCollectionVariable(String name, Int32 offset, Boolean shouldInsertToHR = true, Boolean shouldInsertToIR = true, Boolean randomGeneration = false, Boolean autoCalculation = false) :
            base(name, offset, shouldInsertToHR, shouldInsertToIR, randomGeneration, autoCalculation)
        {
            this.Data = new UInt16[] { 0, 0 };
        }

        /// <summary>
        /// Method for converting bit array to byte array
        /// </summary>
        /// <param name="bits">
        /// bit array
        /// </param>
        /// <returns>
        /// byte array
        /// </returns>
        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        /// <summary>
        /// Method for converting data to value of variable
        /// </summary>
        /// <param name="Data">Data to convert</param>
        /// <returns>Variable value</returns>
        protected override object ConvertDataToValue(UInt16[] Data)
        {
            var firstRegisterBytes = BitConverter.GetBytes(Data[0]);
            var secondRegisterBytes = BitConverter.GetBytes(Data[1]);

            var allBytes = new Byte[] { secondRegisterBytes[0], secondRegisterBytes[1], firstRegisterBytes[0], firstRegisterBytes[1] };

            var allBits = new BitArray(allBytes);

            return allBits;
        }

        /// <summary>
        /// Method for converting value to modbus registers
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Modbus registers data</returns>
        protected override UInt16[] ConvertValueToData(object value)
        {
            var allBytes = BitArrayToByteArray((BitArray)value);

            return new UInt16[] { BitConverter.ToUInt16(allBytes, 2), BitConverter.ToUInt16(allBytes, 0) };
        }

        /// <summary>
        /// Method for getting user control associated with variable
        /// </summary>
        /// <returns>
        /// User control associated with variable
        /// </returns>
        public override UserControl GetUserControl()
        {
            var userControlToReturn = new BitCollectionUserControl();
            userControlToReturn.Init(this);
            return userControlToReturn;
        }

        private bool _generateRandomBool()
        {
            return RND.Next(0, 1)  == 1;
        }

        public override object GenerateRandom()
        {
            return new BitArray(32, _generateRandomBool());
        }
    }
}
