using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This APP Namespace
using DriverCommApp.Conf.DV;
using StatType = DriverCommApp.Stat.StatReport.StatT;

namespace DriverCommApp.DriverComm
{
    class DriverGeneric
    {
        /// <summary>
        /// Configuration for the data areas of this driver.</summary>
        public DAConfClass[] thisAreaConf;

        /// <summary>
        /// Configuration for the data areas of this driver.</summary>
        public DVConfClass thisDriverConf;

        /// <summary>
        /// Data container.</summary>
        public DataExtClass[] ExtData;

        /// <summary>
        /// XWave Driver Class.</summary>
        protected XWave.DriverXWave ObjDriverXWave;

        /// <summary>
        /// Siemens ISO-TCP Driver Class.</summary>
        protected Siemens7.DriverS7 ObjDriverS7;

        /// <summary>
        /// Modbus TCP Driver Class.</summary>
        protected ModbusTCP.DriverModbusTCP ObjDriverModTCP;

        /// <summary>
        /// Driver Status.</summary>
        public Stat.StatReport Status;

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
        /// <param name="DriverConfigObj">Object with the Driver configuration</param></summary>
        public DriverGeneric(int DNum, DriverConfig DriverConfigObj)
        {
            int DACount, i, DVindex;
            thisDriverConf = new DVConfClass();

            //Reading and Writing Flags Init
            iamReading = false;
            iamWriting = false;

            thisDriverConf.Enable = false;
            isInitialized = false;

            Status = new Stat.StatReport(DNum, FileLog: true);
            Status.ResetStat();

            //Check Driver Number is not out of bounds
            if ((DriverConfigObj.DriversConf.Length >= DNum) && (DNum > 0))
            {
                //Driver index start from 0, while ID start from 1. ID=0 is reserved to the System.
                DVindex = DNum - 1;

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
                            Status.NewStat(StatType.Warning, "Wrong Driver Config Params: Driver Type.");
                            break;
                    }

                    if (thisDriverConf.Type != DriverConfig.DriverType.XWave)
                    {
                        //Run thru all the DataAreas and copy the ones that has the link to the Driver ID.
                        DACount = 0;
                        foreach (DataAreaConf DataAreaElement in DriverConfigObj.DataAreasConf)
                        {
                            if (DataAreaElement.ID_Driver == thisDriverConf.ID)
                            {
                                if (DataAreaElement.Enable) DACount++;
                            }
                        }

                        if (DACount > 0)
                        {
                            thisAreaConf = new DAConfClass[DACount];
                            ExtData = new DataExtClass[DACount];
                            thisDriverConf.NDataAreas = DACount;
                            i = 0;
                            foreach (DataAreaConf DataAreaElement in DriverConfigObj.DataAreasConf)
                            {
                                if (DataAreaElement.ID_Driver == thisDriverConf.ID)
                                {
                                    if (DataAreaElement.Enable)
                                    {
                                        thisAreaConf[i] = new DAConfClass(DataAreaElement.ID, DataAreaElement.ID_Driver,
                                        DataAreaElement.Enable, DataAreaElement.Write, DataAreaElement.ToHist,
                                        DataAreaElement.DataType, DataAreaElement.DB_Number, DataAreaElement.StartAddr,
                                        DataAreaElement.AmountVar);

                                        ExtData[i] = new DataExtClass();


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
                                                    //Disable this Driver, as it has a configuration problem.
                                                    Status.NewStat(StatType.Warning, "Wrong Driver Config Params: Data Area Type.");
                                                    thisDriverConf.Enable = false;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            Status.NewStat(StatType.Warning, "Wrong Driver Config Params: Data Area Type.");
                                        }
                                        i++;
                                    }// DataArea is Enabled
                                }
                                else
                                {
                                    Status.NewStat(StatType.Warning, "Wrong Driver Config Params: Data Area ID!=Driver ID.");
                                } //IF DataArea ID == Driver ID   
                            } //For each DataArea
                        }
                        else
                        {
                            Status.NewStat(StatType.Warning, "Wrong Driver Config Params: No Data Areas Configured.");
                        }// Data Area Count >0
                    }
                    else if (thisDriverConf.Type == DriverConfig.DriverType.XWave)
                    {
                        //Special case for the XWave Driver.
                        DACount = 4;
                        thisDriverConf.NDataAreas = DACount;
                        thisAreaConf = new DAConfClass[DACount];
                        ExtData = new DataExtClass[DACount];

                        //Reading and Writing Flags (The Xwave only reads data in this edition)
                        iamReading = true;
                        iamWriting = false;


                        //Initialize the driver to get the amount of variables for each type.
                        ObjDriverXWave = new XWave.DriverXWave(thisDriverConf, Status);

                        //The XWave Driver requires initialization to know the amount of data to be addressed.
                        ObjDriverXWave.Initialize();

                        //Configure each area acordingly.
                        if (ObjDriverXWave.isInitialized)
                        {
                            //Bool Areas.
                            ExtData[0] = new DataExtClass();
                            if (ObjDriverXWave.NumVars.nBool > 0)
                            {
                                thisAreaConf[0] = new DAConfClass(1, thisDriverConf.ID, true, false, true,
                                    DriverConfig.DatType.Bool, 0, "0", ObjDriverXWave.NumVars.nBool);
                                ExtData[0].Data.dBoolean = new bool[ObjDriverXWave.NumVars.nBool];
                                ExtData[0].VarNames = new string[ObjDriverXWave.NumVars.nBool];
                                ExtData[0].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[0] = new DAConfClass(1, thisDriverConf.ID, false, false, false,
                                    DriverConfig.DatType.Bool, 0, "0", ObjDriverXWave.NumVars.nBool);
                            }
                            ExtData[0].AreaConf = thisAreaConf[0];

                            //Unsigned Double Word Areas.
                            ExtData[1] = new DataExtClass();
                            if (ObjDriverXWave.NumVars.nDWord > 0)
                            {
                                thisAreaConf[1] = new DAConfClass(2, thisDriverConf.ID, true, false, true,
                                    DriverConfig.DatType.DWord, 0, "0", ObjDriverXWave.NumVars.nDWord);
                                ExtData[1].Data.dDWord = new UInt32[ObjDriverXWave.NumVars.nDWord];
                                ExtData[1].VarNames = new string[ObjDriverXWave.NumVars.nDWord];
                                ExtData[1].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[1] = new DAConfClass(2, thisDriverConf.ID, false, false, false,
                                    DriverConfig.DatType.DWord, 0, "0", ObjDriverXWave.NumVars.nDWord);
                            }
                            ExtData[1].AreaConf = thisAreaConf[1];

                            //Signed Double Word Areas.
                            ExtData[2] = new DataExtClass();
                            if (ObjDriverXWave.NumVars.nsDWord > 0)
                            {
                                thisAreaConf[2] = new DAConfClass(3, thisDriverConf.ID, true, false, true,
                                    DriverConfig.DatType.sDWord, 0, "0", ObjDriverXWave.NumVars.nsDWord);
                                ExtData[2].Data.dsDWord = new Int32[ObjDriverXWave.NumVars.nsDWord];
                                ExtData[2].VarNames = new string[ObjDriverXWave.NumVars.nsDWord];
                                ExtData[2].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[2] = new DAConfClass(3, thisDriverConf.ID, false, false, false,
                                    DriverConfig.DatType.sDWord, 0, "0", ObjDriverXWave.NumVars.nsDWord);
                            }
                            ExtData[2].AreaConf = thisAreaConf[2];

                            //Float point Areas.
                            ExtData[3] = new DataExtClass();
                            if (ObjDriverXWave.NumVars.nReal > 0)
                            {
                                thisAreaConf[3] = new DAConfClass(4, thisDriverConf.ID, true, false, true,
                                    DriverConfig.DatType.Real, 0, "0", ObjDriverXWave.NumVars.nReal);
                                ExtData[3].Data.dReal = new float[ObjDriverXWave.NumVars.nReal];
                                ExtData[3].VarNames = new string[ObjDriverXWave.NumVars.nReal];
                                ExtData[3].FirstInit = false;
                            }
                            else
                            {
                                thisAreaConf[3] = new DAConfClass(4, thisDriverConf.ID, false, false, false,
                                    DriverConfig.DatType.Real, 0, "0", ObjDriverXWave.NumVars.nReal);
                            }
                            ExtData[3].AreaConf = thisAreaConf[3];

                        }
                        else
                        {
                            Status.NewStat(StatType.Warning, "Wrong Driver Config Params: Driver Init Failed.");
                        }// if XWave Driver isInitialized

                    } //IF Driver Type, XWave Driver has a different treatment.

                    // Built the Drivers.
                    switch (thisDriverConf.Type)
                    {
                        case DriverConfig.DriverType.XWave:
                            //This driver is built and initialized above.
                            break;
                        case DriverConfig.DriverType.S7_TCP:
                            ObjDriverS7 = new Siemens7.DriverS7(thisDriverConf, thisAreaConf, Status);
                            break;
                        case DriverConfig.DriverType.ModbusTCP:
                            ObjDriverModTCP = new ModbusTCP.DriverModbusTCP(thisDriverConf, thisAreaConf, Status);
                            break;
                        default:
                            Status.NewStat(StatType.Warning, "Wrong Driver Type, Check Config.");
                            break;
                    }

                } //IF driver is enabled

            }
            else {
                Status.NewStat(StatType.Warning, "Wrong Driver Config Params: Wrong Driver ID.");
            }//IF Driver Number is out of bounds.

        } //DriverGeneric Cttor

        /// <summary>
        /// Initialize the drivers.</summary>
        public void Initialize()
        {
            if ((!isConnected) && (thisDriverConf.Enable))
            {
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        //This driver is initialized in the constructor
                        isInitialized = ObjDriverXWave.isInitialized;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7.Initialize();
                        isInitialized = ObjDriverS7.isInitialized;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP.Initialize();
                        isInitialized = ObjDriverModTCP.isInitialized;
                        break;
                    default:
                        Status.NewStat(StatType.Warning, "Wrong Driver Type, Check Config.");
                        break;
                }

                if (!isInitialized)
                    Status.NewStat(StatType.Warning, "Driver Initialization Failed.");
            }
        }

        /// <summary>
        /// Read data from the drivers.</summary>
        public bool Read()
        {
            bool retVal = false;

            if (isInitialized && isConnected)
                switch (thisDriverConf.Type)
                {
                    //Read the XWave driver.
                    case DriverConfig.DriverType.XWave:
                        retVal = ObjDriverXWave.Read(ExtData);
                        break;
                    //Read the Siemens driver.
                    case DriverConfig.DriverType.S7_TCP:
                        retVal = ObjDriverS7.Read(ExtData);
                        break;
                    //Read the Modbus Ethernet driver.
                    case DriverConfig.DriverType.ModbusTCP:
                        retVal = ObjDriverModTCP.Read(ExtData);
                        break;
                    //Read the Modbus RTU driver.
                    /*case DriverConfig.DriverType.ModbusRTU:
                        //Future
                        break;
                    //Read the AB Ethernet driver.
                    case DriverConfig.DriverType.AB_Eth:
                        //Future
                        break;*/
                    default:
                        Status.NewStat(StatType.Warning, "Wrong Driver Type, Check Config.");
                        break;
                }

            return retVal;
        }

        /// <summary>
        /// Write data to the drivers.</summary>
        public bool Write()
        {
            bool retVal = false;

            if (isInitialized && isConnected)
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        retVal = ObjDriverXWave.Write(ExtData);
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        retVal = ObjDriverS7.Write(ExtData);
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        retVal = ObjDriverModTCP.Write(ExtData);
                        break;
                    default:
                        Status.NewStat(StatType.Warning, "Wrong Driver Type, Check Config.");
                        break;
                }

            return retVal;
        }

        /// <summary>
        /// Connect the drivers.</summary>
        public void Connect()
        {
            if (isInitialized && (!isConnected))
            {
                switch (thisDriverConf.Type)
                {
                    case DriverConfig.DriverType.XWave:
                        ObjDriverXWave.Connect();
                        isConnected = ObjDriverXWave.isConnected;
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7.Connect();
                        isConnected = ObjDriverS7.isConnected;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP.Connect();
                        isConnected = ObjDriverModTCP.isConnected;
                        break;
                    default:
                        Status.NewStat(StatType.Warning, "Wrong Driver Type, Check Config.");
                        break;
                }
                if (!isConnected)
                    Status.NewStat(StatType.Warning, "Driver Connection Failed.");

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
                        break;
                    case DriverConfig.DriverType.S7_TCP:
                        ObjDriverS7.Disconect();
                        isConnected = ObjDriverS7.isConnected;
                        break;
                    case DriverConfig.DriverType.ModbusTCP:
                        ObjDriverModTCP.Disconect();
                        isConnected = ObjDriverModTCP.isConnected;
                        break;
                    default:
                        Status.NewStat(StatType.Warning, "Wrong Driver Type, Check Config.");
                        break;
                }
        }

        /// <summary>
        /// Close, and free the memory of the drivers.</summary>
        public void CloseAll()
        {
            this.Disconnect();

            if (!isConnected)
            {
                isInitialized = false;
            }

            //Flush the logs
            Status.FlushLog();
        }

        /// <summary>
        /// Class Destructor.</summary>
        ~DriverGeneric()
        {
            //Delete all the data pointers, and free the memory.
            thisAreaConf = null;
            thisDriverConf = null;
            ExtData = null;

            ObjDriverS7 = null;
            ObjDriverModTCP = null;
            ObjDriverXWave = null;

            Status = null;
        }

    } // Class DriverGeneric

} //NameSpace

