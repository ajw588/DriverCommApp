using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//This APP Namespace
using DriverCommApp.Conf;

namespace DriverCommApp.DriverComm
{
    /// <summary>
    /// Driver Client Configuration.</summary>
    public class DVConfClass
    {
        public bool Enable;
        public int ID, CycleTime, NDataAreas, Rack, Slot, portTCP, portUDP;
        public int RTUid, RTUbaud, RTUparity, RTUstop, Timeout;
        public DriverConfig.RTUCommPort portRTU;
        public DriverConfig.DriverType Type;
        public string Address, DefFile;

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
    public class AreaDataConfClass
    {
        public readonly int ID, ID_Driver, Amount, DBnumber;
        public readonly string StartAddress;
        public readonly DriverConfig.DatType dataType;
        public readonly bool Enable, Write, ToHistorics;

        /// <summary>
        /// Fast config, and integer to TypeData conversion.</summary>
        public AreaDataConfClass(int idDA, int idDv, bool En, bool isWrite, bool Tohist, int TypeData, int DBnum, string StarAdd, int NumOfVars)
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
        public AreaDataConfClass(int idDA, int idDv, bool En, bool isWrite, bool Tohist, DriverConfig.DatType TypeData, int DBnum, string StarAdd, int NumOfVars)
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
            public bool[] dBoolean;
            public byte[] dByte;
            public UInt16[] dWord;
            public UInt32[] dDWord;
            public Int32[] dsDWord;
            public float[] dReal;
            public string[] dString;
        }

        public DataContainer Data;
        public long NowTimeTicks;
        public string[] VarNames;
        public AreaDataConfClass AreaConf;
        public bool FirstInit;


        /// <summary>
        /// Cloning Method
        /// Returns a new copy of himself.</summary>
        public object clone()
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
    public class DVConfAreaConfClass
    {
        public DVConfClass DriverConf;
        public AreaDataConfClass[] AreaConf;
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
