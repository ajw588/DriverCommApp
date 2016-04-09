using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverCommApp.Conf;

namespace DriverCommApp.CommDriver
{
    class DriverGeneric
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

        /*
        //Deprecated, Single objects are used instead.
        /// <summary>
        /// Definition for the Driver types containers for the driver.</summary>
        public struct DriverContainer
        {
            DriverConfig.DriverType thisType;
            DriverXWave XWaveObj;
            DriverS7 S7Obj;
            DriverModbusTCP ModTCPObj;
        }
        */

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
            public DateTime TimeStamp;
            public string[] VarNames;
            public AreaData AreaConf;
            public bool FirstInit;
        }

        /// <summary>
        /// Configuration for the data areas of this driver.</summary>
        public AreaData[] thisAreaConf;

        /// <summary>
        /// Configuration for the data areas of this driver.</summary>
        public CConf thisDriverConf;

        /// <summary>
        /// Data container.</summary>
        public DataExt[] ExtData;

        /// <summary>
        /// XWave Driver Class.</summary>
        protected DriverXWave ObjDriverXWave;

        /// <summary>
        /// Siemens ISO-TCP Driver Class.</summary>
        protected DriverS7 ObjDriverS7;

        /// <summary>
        /// Modbus TCP Driver Class.</summary>
        protected DriverModbusTCP ObjDriverModTCP;

        /// <summary>
        /// Driver Status.</summary>
        public MainCycle.StatObj Status;

        /// <summary>
        /// Driver Initialization Flag.</summary>
        public bool isInitialized;

        /// <summary>
        /// Driver Connected Flag.</summary>
        public bool isConnected;

        /// <summary>
        /// Driver Reading data Flag.</summary>
        public bool iamReading;

        /// <summary>
        /// Driver Writing data Flag.</summary>
        public bool iamWriting;

        /// <summary>
        /// Class Constructor.
        /// <param name="DNum">Driver number, ID of the driver in the configuration.</param> 
        /// <param name="Realtime">Cycle time in ms</param>
        /// <param name="DriverConfigObj">Object with the Driver configuration</param></summary>
        public DriverGeneric(int DNum, DriverConfig DriverConfigObj)
        {
            int DACount, i, DVindex;
            thisDriverConf = new CConf();

            //Reading and Writing Flags Init
            iamReading = false;
            iamWriting = false;

            thisDriverConf.Enable = false;
            isInitialized = false;

            //Init Status Obj
            Status.ResetStat();

            //Check Driver Number is not out of bounds
            if (DriverConfigObj.DriversConf.Length >= DNum)
            {
                //Driver index start from 0, while ID start from 1. ID=0 is reserved to the System.
                if (DNum > 0) { DVindex = DNum - 1; } else { DVindex = 0; }

                //General Driver Configuration parameters
                thisDriverConf.ConnConfig(DriverConfigObj.DriversConf[DVindex].ID, DriverConfigObj.DriversConf[DVindex].Enable,
                    DriverConfigObj.DriversConf[DVindex].Type, DriverConfigObj.DriversConf[DVindex].CycleTime, 
                    DriverConfigObj.DriversConf[DVindex].Timeout);

                if (thisDriverConf.Enable)
                {
                    //Driver Specific configuration parameters
                    switch (thisDriverConf.Type)
                    {
                        case DriverConfig.DriverType.XWave:
                            thisDriverConf.ConfXwave(DriverConfigObj.DriversConf[DVindex].Address,
                                DriverConfigObj.DriversConf[DVindex].PortTCP, DriverConfigObj.DriversConf[DVindex].PortUDP,
                                DriverConfigObj.DriversConf[DVindex].DefFilePath);
                            break;
                        case DriverConfig.DriverType.S7_TCP:
                            thisDriverConf.ConfS7(DriverConfigObj.DriversConf[DVindex].Address,
                                DriverConfigObj.DriversConf[DVindex].Rack, DriverConfigObj.DriversConf[DVindex].Slot);

                            break;
                        case DriverConfig.DriverType.ModbusTCP:
                            thisDriverConf.ConfModbusTCP(DriverConfigObj.DriversConf[DVindex].Address,
                                DriverConfigObj.DriversConf[DVindex].PortTCP);
                            break;
                        case DriverConfig.DriverType.ModbusRTU:
                            thisDriverConf.ConfModbusRTU(DriverConfigObj.DriversConf[DVindex].PortRTU,
                                DriverConfigObj.DriversConf[DVindex].RTUid, DriverConfigObj.DriversConf[DVindex].RTUBaud,
                                DriverConfigObj.DriversConf[DVindex].RTUParity, DriverConfigObj.DriversConf[DVindex].RTUStop);
                            break;
                        default:
                            // Disable the driver as it was not configured properly.
                            thisDriverConf.Enable = false;
                            break;
                    }

                    if (thisDriverConf.Type != DriverConfig.DriverType.XWave)
                    {
                        //Run thru all the DataAreas and copy the ones that has the link to the Driver ID.
                        DACount = 0;
                        foreach (Conf.DataAreaConf DataAreaElement in DriverConfigObj.DataAreasConf)
                        {
                            if (DataAreaElement.ID_Driver == thisDriverConf.ID)
                            {
                                if (DataAreaElement.Enable) DACount++;
                            }
                        }

                        if (DACount > 0)
                        {
                            thisAreaConf = new AreaData[DACount];
                            ExtData = new DataExt[DACount];
                            thisDriverConf.NDataAreas = DACount;
                            i = 0;
                            foreach (Conf.DataAreaConf DataAreaElement in DriverConfigObj.DataAreasConf)
                            {
                                if ((DataAreaElement.ID_Driver == thisDriverConf.ID) && (DataAreaElement.Enable))
                                {
                                    thisAreaConf[i].AreaConfig(DataAreaElement.ID, DataAreaElement.ID_Driver,
                                        DataAreaElement.Enable, DataAreaElement.Write, DataAreaElement.ToHist, DataAreaElement.DataType,
                                        DataAreaElement.DB_Number, DataAreaElement.StartAddr, DataAreaElement.AmountVar);

                                    //Asign the configuration section to the data area.
                                    ExtData[i].AreaConf = thisAreaConf[i];

                                    //VarNames
                                    ExtData[i].VarNames = new string[DataAreaElement.AmountVar];
                                    ExtData[i].FirstInit = false;

                                    //Create the Data container.
                                    if (DataAreaElement.DataType != DriverConfig.DatType.Undefined)
                                    {
                                        //Reading and Writing Flags Setup
                                        if (!thisAreaConf[i].Write) iamReading = true;
                                        if (thisAreaConf[i].Write) iamWriting = true;

                                        switch (DataAreaElement.DataType)
                                        {
                                            case DriverConfig.DatType.Bool:
                                                ExtData[i].Data.dBoolean = new bool[DataAreaElement.AmountVar];
                                                break;
                                            case DriverConfig.DatType.Byte:
                                                ExtData[i].Data.dByte = new byte[DataAreaElement.AmountVar];
                                                break;
                                            case DriverConfig.DatType.Word:
                                                ExtData[i].Data.dWord = new UInt16[DataAreaElement.AmountVar];
                                                break;
                                            case DriverConfig.DatType.DWord:
                                                ExtData[i].Data.dDWord = new UInt32[DataAreaElement.AmountVar];
                                                break;
                                            case DriverConfig.DatType.sDWord:
                                                ExtData[i].Data.dsDWord = new Int32[DataAreaElement.AmountVar];
                                                break;
                                            case DriverConfig.DatType.Real:
                                                ExtData[i].Data.dReal = new float[DataAreaElement.AmountVar];
                                                break;
                                            default:
                                                //Disable this Data Area, as it was not configured properly.
                                                ExtData[i].AreaConf.Enable = false;
                                                thisAreaConf[i].Enable = false;
                                                //DataAreaElement.Enable = false;
                                                break;
                                        }
                                    }
                                    i++;
                                } //IF DataArea ID == Driver ID   
                            } //For each DataArea
                        }// Data Area Count >0
                    }
                    else if (thisDriverConf.Type == DriverConfig.DriverType.XWave)
                    {
                        //Special case for the XWave Driver.
                        DACount = 4;
                        thisDriverConf.NDataAreas = DACount;
                        thisAreaConf = new AreaData[DACount];
                        ExtData = new DataExt[DACount];

                        //Reading and Writing Flags (The Xwave only reads data in this edition)
                        iamReading = true;
                        iamWriting = false;


                        //Initialize the driver to get the amount of variables for each type.
                        ObjDriverXWave = new DriverXWave(thisDriverConf);

                        //The XWave Driver requires initialization to know the amount of data to be addressed.
                        ObjDriverXWave.Initialize();

                        //Configure each area acordingly.
                        if (ObjDriverXWave.isInitialized)
                        {
                            //Bool Areas.
                            if (ObjDriverXWave.NumVars.nBool > 0)
                            {
                                thisAreaConf[0].AreaConfig(1, thisDriverConf.ID, true, false, true, DriverConfig.DatType.Bool,
                                                0, "0", ObjDriverXWave.NumVars.nBool);
                                ExtData[0].AreaConf = thisAreaConf[0];
                                ExtData[0].Data.dBoolean = new bool[ObjDriverXWave.NumVars.nBool];
                                ExtData[0].VarNames = new string[ObjDriverXWave.NumVars.nBool];
                                ExtData[0].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[0].AreaConfig(1, thisDriverConf.ID, false, false, false, DriverConfig.DatType.Bool,
                                                0, "0", ObjDriverXWave.NumVars.nBool);
                            }

                            //Unsigned Double Word Areas.
                            if (ObjDriverXWave.NumVars.nDWord > 0)
                            {
                                thisAreaConf[1].AreaConfig(2, thisDriverConf.ID, true, false, true, DriverConfig.DatType.DWord,
                                            0, "0", ObjDriverXWave.NumVars.nDWord);
                                ExtData[1].AreaConf = thisAreaConf[1];
                                ExtData[1].Data.dDWord = new UInt32[ObjDriverXWave.NumVars.nDWord];
                                ExtData[1].VarNames = new string[ObjDriverXWave.NumVars.nDWord];
                                ExtData[1].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[1].AreaConfig(2, thisDriverConf.ID, false, false, false, DriverConfig.DatType.DWord,
                                            0, "0", ObjDriverXWave.NumVars.nDWord);
                            }

                            //Signed Double Word Areas.
                            if (ObjDriverXWave.NumVars.nsDWord > 0)
                            {
                                thisAreaConf[2].AreaConfig(3, thisDriverConf.ID, true, false, true, DriverConfig.DatType.sDWord,
                                            0, "0", ObjDriverXWave.NumVars.nsDWord);
                                ExtData[2].AreaConf = thisAreaConf[2];
                                ExtData[2].Data.dsDWord = new Int32[ObjDriverXWave.NumVars.nsDWord];
                                ExtData[2].VarNames = new string[ObjDriverXWave.NumVars.nsDWord];
                                ExtData[2].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[2].AreaConfig(3, thisDriverConf.ID, false, false, false, DriverConfig.DatType.sDWord,
                                            0, "0", ObjDriverXWave.NumVars.nsDWord);
                            }

                            //Float point Areas.
                            if (ObjDriverXWave.NumVars.nReal > 0)
                            {
                                thisAreaConf[3].AreaConfig(4, thisDriverConf.ID, true, false, true, DriverConfig.DatType.Real,
                                            0, "0", ObjDriverXWave.NumVars.nReal);
                                ExtData[3].AreaConf = thisAreaConf[3];
                                ExtData[3].Data.dReal = new float[ObjDriverXWave.NumVars.nReal];
                                ExtData[3].VarNames = new string[ObjDriverXWave.NumVars.nReal];
                                ExtData[3].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[3].AreaConfig(4, thisDriverConf.ID, false, false, false, DriverConfig.DatType.Real,
                                            0, "0", ObjDriverXWave.NumVars.nReal);
                            }

                        }// if XWave Driver isInitialized

                    } //IF Driver Type, XWave Driver has a different treatment.

                    // Built the Drivers.
                    switch (thisDriverConf.Type)
                    {
                        case DriverConfig.DriverType.XWave:
                            //This driver is built and initialized above.
                            break;
                        case DriverConfig.DriverType.S7_TCP:
                            ObjDriverS7 = new DriverS7(thisDriverConf, thisAreaConf);
                            break;
                        case DriverConfig.DriverType.ModbusTCP:
                            ObjDriverModTCP = new DriverModbusTCP(thisDriverConf, thisAreaConf);
                            break;
                    }

                } //IF driver is enabled

            }//IF Driver Number is not out of bounds.

        } //DriverGeneric Cttor

        /// <summary>
        /// Initialize the drivers.</summary>
        public void Initialize()
        {
            if ((!isConnected) && (thisDriverConf.Enable))
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        //This driver is initialized in the constructor
                        isInitialized = ObjDriverXWave.isInitialized;
                        Status = ObjDriverXWave.Status;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7.Initialize();
                        isInitialized = ObjDriverS7.isInitialized;
                        Status = ObjDriverS7.Status;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP.Initialize();
                        isInitialized = ObjDriverModTCP.isInitialized;
                        Status = ObjDriverModTCP.Status;
                        break;
                }
        }

        /// <summary>
        /// Read data from the drivers.</summary>
        public int Read()
        {
            int retVal = -10;

            if (isInitialized && isConnected)
                switch (thisDriverConf.Type)
                {
                    //Read the XWave driver.
                    case DriverConfig.DriverType.XWave:
                        retVal = ObjDriverXWave.Read(ref ExtData);
                        Status = ObjDriverXWave.Status;
                        break;
                    //Read the Siemens driver.
                    case DriverConfig.DriverType.S7_TCP:
                        retVal = ObjDriverS7.Read(ref ExtData);
                        Status = ObjDriverS7.Status;
                        break;
                    //Read the Modbus Ethernet driver.
                    case DriverConfig.DriverType.ModbusTCP:
                        retVal = ObjDriverModTCP.Read(ref ExtData);
                        Status = ObjDriverModTCP.Status;
                        break;
                    //Read the Modbus RTU driver.
                    case DriverConfig.DriverType.ModbusRTU:
                        //Future
                        break;
                    //Read the AB Ethernet driver.
                    case DriverConfig.DriverType.AB_Eth:
                        //Future
                        break;
                }

            return retVal;
        }

        /// <summary>
        /// Write data to the drivers.</summary>
        public int Write()
        {
            int retVal = -10;

            if (isInitialized && isConnected)
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        retVal = ObjDriverXWave.Write(ExtData);
                        Status = ObjDriverXWave.Status;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        retVal = ObjDriverS7.Write(ExtData);
                        Status = ObjDriverS7.Status;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        retVal = ObjDriverModTCP.Write(ExtData);
                        Status = ObjDriverModTCP.Status;
                        break;
                }

            return retVal;
        }

        /// <summary>
        /// Connect the drivers.</summary>
        public void Connect()
        {
            if (isInitialized && (!isConnected))
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        ObjDriverXWave.Connect();
                        isConnected = ObjDriverXWave.isConnected;
                        Status = ObjDriverXWave.Status;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7.Connect();
                        isConnected = ObjDriverS7.isConnected;
                        Status = ObjDriverS7.Status;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP.Connect();
                        isConnected = ObjDriverModTCP.isConnected;
                        Status = ObjDriverModTCP.Status;
                        break;
                }
        }

        /// <summary>
        /// Disconnect the drivers.</summary>
        public void Disconnect()
        {
            if (isInitialized)
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        ObjDriverXWave.Disconect();
                        isConnected = ObjDriverXWave.isConnected;
                        Status = ObjDriverXWave.Status;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7.Disconect();
                        isConnected = ObjDriverS7.isConnected;
                        Status = ObjDriverS7.Status;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP.Disconect();
                        isConnected = ObjDriverModTCP.isConnected;
                        Status = ObjDriverModTCP.Status;
                        break;
                }
        }

        /// <summary>
        /// Kill, and free the memory of the drivers.</summary>
        public void Kill()
        {
            if (!isConnected)
            {
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        //ObjDriverXWave=null;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7 = null;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP = null;
                        break;
                }
                isInitialized = false;
            }
        }
        /// <summary>
        /// Class Destructor.</summary>
        ~DriverGeneric()
        {
            //Delete all the data pointers, and free the memory.
            thisAreaConf = null;
            //thisDriverConf = null;
            ExtData = null;
            //ObjDriverXWave = null;
            ObjDriverS7 = null;
            ObjDriverModTCP = null;
        }

    } // Class DriverGeneric

} //NameSpace

