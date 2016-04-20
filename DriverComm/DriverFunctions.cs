using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//This APP Namespace
using DriverCommApp.Conf.DV;

namespace DriverCommApp.DriverComm
{
    /// <summary>
    /// Driver Client Configuration.</summary>
    public class DVConfClass
    {
        /// <summary>Driver Enabled=True </summary>
        public bool Enable;
        /// <summary>Driver Identification Number </summary>
        public int ID;
        /// <summary> Cycle for Driver Read/Write in ms</summary>
        public int CycleTime;
        /// <summary> Number of Data Areas configured for this driver</summary>
        public int NDataAreas;
        /// <summary> PLC Rack Number</summary>
        public int Rack;
        /// <summary> PLC Slot number</summary>
        public int Slot;
        /// <summary> Ethernet Communication Port TCP</summary>
        public int portTCP;
        /// <summary> Ethernet Communication Port UDP</summary>
        public int portUDP;
        /// <summary> Remote device ID for serial communication</summary>
        public int RTUid;
        /// <summary> Serial Communication Baud Rate in bpps</summary>
        public int RTUbaud;
        /// <summary> Serial Communication Parity</summary>
        public int RTUparity;
        /// <summary> Serial Communication Stop bits</summary>
        public int RTUstop;
        /// <summary> Communication Timeout waiting time in ms</summary>
        public int Timeout;
        /// <summary> Serial Communication Port</summary>
        public DriverConfig.RTUCommPort portRTU;
        /// <summary> Remote Device Type</summary>
        public DriverConfig.DriverType Type;
        /// <summary> Remote device address, URL or Ip address</summary>
        public string Address;
        /// <summary> Variables Symbols Definition (in XWave: VarTree Def) </summary>
        public string DefFile;

        /// <summary>
        /// Fast Method to fill configuration.</summary>
        public void ConnConfig(int id, bool DEnable, DriverConfig.DriverType Dtype, int Time, int TimeOutLimit)
        {
            ID = id;
            Enable = DEnable;
            Type = Dtype;
            CycleTime = Time;
            Timeout = TimeOutLimit;
        }

        /// <summary>
        /// Configure a Xwave Driver.</summary>
        public void ConfXwave(string IpAddress, int DPortTCP, int DPortUDP, string DDefFile)
        {
            Address = IpAddress;
            portTCP = DPortTCP;
            portUDP = DPortUDP;
            DefFile = DDefFile;
        }

        /// <summary>
        /// Configure a Siemens ISO TCP Driver.</summary>
        public void ConfS7(string IpAddress, int DRack, int DSlot)
        {
            Address = IpAddress;
            Rack = DRack;
            Slot = DSlot;
        }

        /// <summary>
        /// Configure a ModbusTCP Driver.</summary>
        public void ConfModbusTCP(string IpAddress, int DPortTCP)
        {
            Address = IpAddress;
            portTCP = DPortTCP;
        }

        /// <summary>
        /// Configure a ModbusRTU Driver.</summary>
        public void ConfModbusRTU(DriverConfig.RTUCommPort CommPort, int DevID, int Baud, int Parity, int StopBits)
        {
            portRTU = CommPort;
            RTUid = DevID;
            RTUbaud = Baud;
            RTUparity = Parity;
            RTUstop = StopBits;
        }

    }

    /// <summary>
    /// Definition for the data areas configuration.</summary>
    public class DAConfClass
    {
        /// <summary> Data Area Block ID</summary>
        public readonly int ID;
        /// <summary> Driver ID of the Driver this Data Area Block belows to</summary>
        public readonly int ID_Driver;
        /// <summary> Number of Variables in this Data Area Block </summary>
        public readonly int Amount;
        /// <summary> Remote Device DB number to Read/Write to</summary>
        public readonly int DBnumber;
        /// <summary> Remote Device Address to read the First Variable of this Data Area Block</summary>
        public readonly string StartAddress;
        /// <summary> Type of Data in the Data Area Block</summary>
        public readonly DriverConfig.DatType dataType;
        /// <summary> Flag to Enable=True this Data Area Block</summary>
        public readonly bool Enable;
        /// <summary> Flag to declare this Data Area Block as Read=False, or Write=True in the Remote Device.</summary>
        public readonly bool Write;
        /// <summary> Flag to include this Data Area in the Historics=True.</summary>
        public readonly bool ToHistorics;

        /// <summary>
        /// Fast config, and integer to TypeData conversion.</summary>
        public DAConfClass(int idDA, int idDv, bool En, bool isWrite, bool Tohist, int TypeData, int DBnum, string StarAdd, int NumOfVars)
        {
            ID = idDA;
            ID_Driver = idDv;
            DBnumber = DBnum;
            StartAddress = StarAdd;
            Amount = NumOfVars;
            Write = isWrite;
            Enable = En;
            ToHistorics = Tohist;

            switch (TypeData)
            {
                case 1:
                    dataType = DriverConfig.DatType.Bool;
                    break;
                case 2:
                    dataType = DriverConfig.DatType.Byte;
                    break;
                case 3:
                    dataType = DriverConfig.DatType.Word;
                    break;
                case 4:
                    dataType = DriverConfig.DatType.DWord;
                    break;
                case 5:
                    dataType = DriverConfig.DatType.sDWord;
                    break;
                case 6:
                    dataType = DriverConfig.DatType.Real;
                    break;
                case 7:
                    dataType = DriverConfig.DatType.String;
                    break;
                default:
                    dataType = DriverConfig.DatType.Undefined;
                    break;
            }

        }

        /// <summary>
        /// Fast config, and No TypeData conversion.</summary>
        public DAConfClass(int idDA, int idDv, bool En, bool isWrite, bool Tohist, DriverConfig.DatType TypeData, int DBnum, string StarAdd, int NumOfVars)
        {
            ID = idDA;
            ID_Driver = idDv;
            DBnumber = DBnum;
            StartAddress = StarAdd;
            Amount = NumOfVars;
            Write = isWrite;
            Enable = En;
            dataType = TypeData;
            ToHistorics = Tohist;
        }

    } // END AreaDataConfClass

    /// <summary>
    /// Definition for the data exchange areas configuration.</summary>
    public class DataExtClass
    {
        /// <summary>
        /// Definition for the data containers for the driver.</summary>
        public struct DataContainer
        {
            /// <summary> Boolean Data Array</summary>
            public bool[] dBoolean;
            /// <summary> Unsigned Byte (8bits) Data Array</summary>
            public byte[] dByte;
            /// <summary> Unsigned Word (16bits) Data Array</summary>
            public UInt16[] dWord;
            /// <summary> Unsigned Double Word (32bits) Data Array</summary>
            public UInt32[] dDWord;
            /// <summary> Signed Double Word (32bits) Data Array</summary>
            public Int32[] dsDWord;
            /// <summary> Single Precision Floating Point Data Array</summary>
            public float[] dReal;
            /// <summary> Acii Char Array (String) Data Array</summary>
            public string[] dString;
        }
        /// <summary> Struct for the Read/Write Data</summary>
        public DataContainer Data;
        /// <summary> Timestamp in Ticks</summary>
        public long NowTimeTicks;
        /// <summary> Variable Symbolic Names</summary>
        public string[] VarNames;
        /// <summary> Data Area Block Configuration</summary>
        public DAConfClass AreaConf;
        /// <summary> Flag for the Data Area Block First Initialization</summary>
        public bool FirstInit;


        /// <summary>
        /// Cloning Method
        /// Returns a new copy of himself.</summary>
        public DataExtClass clone()
        {
            DataExtClass NewMe = new DataExtClass();

            //Clone the Data Struct Arrays
            if (this.Data.dBoolean != null)
                NewMe.Data.dBoolean = (bool[])this.Data.dBoolean.Clone();

            if (this.Data.dByte != null)
                NewMe.Data.dByte = (byte[])this.Data.dByte.Clone();

            if (this.Data.dWord != null)
                NewMe.Data.dWord = (UInt16[])this.Data.dWord.Clone();

            if (this.Data.dDWord != null)
                NewMe.Data.dDWord = (UInt32[])this.Data.dDWord.Clone();

            if (this.Data.dsDWord != null)
                NewMe.Data.dsDWord = (Int32[])this.Data.dsDWord.Clone();

            if (this.Data.dReal != null)
                NewMe.Data.dReal = (float[])this.Data.dReal.Clone();

            if (this.Data.dString != null)
                NewMe.Data.dString = (string[])this.Data.dString.Clone();


            NewMe.NowTimeTicks = this.NowTimeTicks;
            NewMe.FirstInit = this.FirstInit;

            //Clone the VarNames just in case.
            if (this.VarNames != null)
                NewMe.VarNames = (string[])this.VarNames.Clone();

            //The configuration won't be clone,
            //As its intented to be Initialized at the beggining,
            //and only read in the rest of the program.
            NewMe.AreaConf = this.AreaConf;

            return NewMe;
        }
    }

    /// <summary>
    /// Driver Complete Configuration Type Def.</summary>
    public class DVConfDAConfClass
    {
        /// <summary> Driver Configuration</summary>
        public readonly DVConfClass DVConf;

        /// <summary> Data Area Blocks Configuration Array</summary>
        public readonly DAConfClass[] DAConf;

        /// <summary> Class Cttr.</summary>
        public DVConfDAConfClass(DVConfClass DriverConf, DAConfClass[] DataAreaConf)
        {
            DVConf= DriverConf;
            DAConf=DataAreaConf;
        }
    }

    static class DriverFunctions
    {

        /// <summary>
        /// Mask for a byte, from 32bits.</summary>
        public const uint MaskByte = 0x000000FF;

        /// <summary>
        /// Mask for a word, from 32bits.</summary>
        public const uint MaskWord = 0x0000FFFF;

        /// <summary>
        /// Mask for the high part word, from 32bits.</summary>
        public const uint MaskHWord = 0xFFFF0000;

        /// <summary>
        /// Mask for a OR/on negative bit, from 32bits.</summary>
        public const uint MaskNeg = 0x80000000;

        /// <summary>
        /// Mask for a AND/off negative bit, from 32bits.</summary>
        public const uint MaskiNeg = 0x7FFFFFFF;

    }
}
