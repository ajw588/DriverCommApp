using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//This APP Namespace
using DriverCommApp.Conf;

namespace DriverCommApp.DriverComm
{
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

        /// <summary>
        /// Driver Client Configuration.</summary>
        public struct CConf
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
        public struct AreaData
        {
            public int ID, ID_Driver, Amount, DBnumber;
            public string StartAddress;
            public DriverConfig.DatType dataType;
            public bool Enable, Write, ToHistorics;

            /// <summary>
            /// Fast config, and integer to TypeData conversion.</summary>
            public void AreaConfig(int id, int idDriver, bool En, bool write, bool Tohist, int TypeData, int DBnum, string StarAdd, int NumOfVars)
            {
                ID = id;
                ID_Driver = idDriver;
                DBnumber = DBnum;
                StartAddress = StarAdd;
                Amount = NumOfVars;
                Write = write;
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
            public void AreaConfig(int id, int idDriver, bool En, bool write, bool Tohist, DriverConfig.DatType TypeData, int DBnum, string StarAdd, int NumOfVars)
            {
                ID = id;
                ID_Driver = idDriver;
                DBnumber = DBnum;
                StartAddress = StarAdd;
                Amount = NumOfVars;
                Write = write;
                Enable = En;
                dataType = TypeData;
                ToHistorics = Tohist;
            }

        }

        /// <summary>
        /// Definition for the data exchange areas configuration.</summary>
        public struct DataExt
        {
            public DataContainer Data;
            public long NowTimeTicks;
            public string[] VarNames;
            public AreaData AreaConf;
            public bool FirstInit;
        }
    }
}
