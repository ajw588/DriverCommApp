﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriverCommApp.Conf;
using EasyModbus;

namespace DriverCommApp.CommDriver
{

    class DriverModbusTCP
    {
        /// <summary>
        /// Definition for the driver data types.</summary>
        public struct ModbusDataConta
        {
            public bool[] dBoolean;
            public int[] dInt;
        }

        /// <summary>
        /// Internal data container.</summary>
        ModbusDataConta[] IntData;

        /// <summary>
        /// Obj for the driver.</summary>
        ModbusClient ModTCPObj;

        /// <summary>
        /// Master Driver Conf.</summary>
        DriverGeneric.CConf MasterDriverConf;

        /// <summary>
        /// Master Data Area Conf.</summary>
        DriverGeneric.AreaData[] MasterDataAreaConf;

        /// <summary>
        /// Flag for Driver Initialization.</summary>
        public bool isInitialized;

        /// <summary>
        /// Flag for Driver Connection.</summary>
        public bool isConnected;

        /// <summary>
        /// Byte order.</summary>
        ModbusClient.RegisterOrder RegOrder;

        /// <summary>
        /// Driver Status.</summary>
        public MainCycle.StatObj Status;

        /// <summary>
        /// Class contructor.</summary>
        public DriverModbusTCP(DriverGeneric.CConf DriverConf, DriverGeneric.AreaData[] DataAreaConf)
        {
            MasterDriverConf = DriverConf;
            MasterDataAreaConf = DataAreaConf;

            isInitialized = false; isConnected = false;

            //Byte register order. HighLow=Big-endian.
            RegOrder = ModbusClient.RegisterOrder.HighLow;

            Status.ResetStat();
        }

        /// <summary>
        /// Initialize the object class and prepares for the Server Device connection.</summary>
        public void Initialize()
        {
            if ((!isInitialized) && (MasterDriverConf.Enable))
            {
                int i, SAddress;
                DriverGeneric.AreaData thisArea;

                //Create the driver object
                ModTCPObj = new ModbusClient(MasterDriverConf.Address, MasterDriverConf.portTCP);
                ModTCPObj.ConnectionTimeout = MasterDriverConf.Timeout;

                IntData = new ModbusDataConta[MasterDriverConf.NDataAreas];

                //Cicle and configure the data areas
                for (i = 0; i < MasterDriverConf.NDataAreas; i++)
                {
                    thisArea = MasterDataAreaConf[i];
                    SAddress = int.Parse(thisArea.StartAddress);

                    if (thisArea.Enable)
                    {
                        switch (thisArea.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                IntData[i].dBoolean = new bool[thisArea.Amount];
                                break;
                            case DriverConfig.DatType.Byte:
                            case DriverConfig.DatType.Word:
                                IntData[i].dInt = new int[thisArea.Amount];
                                break;
                            case DriverConfig.DatType.DWord:
                            case DriverConfig.DatType.Real:
                                IntData[i].dInt = new int[(thisArea.Amount * 2)];
                                break;
                        }
                    }// Area Enable
                } // For Data Areas


                isInitialized = true;
            }
        }

        /// <summary>
        /// Attemps to connect to the Server Device.</summary>
        public void Connect()
        {
            if (!isConnected)
                try
                {
                    ModTCPObj.Connect(MasterDriverConf.Address, MasterDriverConf.portTCP);
                    isConnected = ModTCPObj.Connected;
                }
                catch (Exception e)
                {
                    Status.NewStat(MainCycle.StatT.Warning, e.Message);
                }
                finally
                {
                    isConnected = ModTCPObj.Connected;
                }
        } // END Function Connect

        /// <summary>
        /// Disconnect from the Server Device.</summary>
        public void Disconect()
        {
            try
            {
                ModTCPObj.Disconnect();
                isConnected = false;
            }
            catch (Exception e)
            {
                Status.NewStat(MainCycle.StatT.Warning, e.Message);
            }
            finally
            {
                isConnected = false;
            }

        } //End Function Disconnect

        /// <summary>
        /// Reads data from the Server Device.</summary>
        public int Read(ref DriverGeneric.DataExt[] DataOut)
        {
            int i, j, jj, SAddress, retVar;
            uint highWord, lowWord;
            DriverGeneric.AreaData thisArea;
            retVar = -1;

            //If is not initialized and not connected return  error.
            if (!(isInitialized && isConnected)) return retVar;

            //If the DataOut and Internal data doesnt have the correct amount of data areas return error.
            if (!((DataOut.Length == MasterDriverConf.NDataAreas) && (IntData.Length == MasterDriverConf.NDataAreas)))
                return retVar;

            //Cicle thru Data Areas.
            for (i = 0; i < MasterDriverConf.NDataAreas; i++)
            {
                thisArea = MasterDataAreaConf[i];
                SAddress = int.Parse(thisArea.StartAddress);
                retVar = -1;

                if (thisArea.Enable && (!thisArea.Write))
                {
                    try
                    {
                        //Read the data from the device
                        switch (thisArea.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                IntData[i].dBoolean = ModTCPObj.ReadCoils(SAddress, thisArea.Amount);
                                //Check read complete set of data
                                if (IntData[i].dBoolean.Length == thisArea.Amount) retVar = 0;
                                break;
                            case DriverConfig.DatType.Byte:
                            case DriverConfig.DatType.Word:
                                IntData[i].dInt = ModTCPObj.ReadHoldingRegisters(SAddress, thisArea.Amount);
                                //Check read complete set of data
                                if (IntData[i].dInt.Length == thisArea.Amount) retVar = 0;
                                break;
                            case DriverConfig.DatType.DWord:
                            case DriverConfig.DatType.Real:
                                IntData[i].dInt = ModTCPObj.ReadHoldingRegisters(SAddress, (2 * thisArea.Amount));
                                //Check read complete set of data
                                if (IntData[i].dInt.Length == (2 * thisArea.Amount)) retVar = 0;
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Status.NewStat(MainCycle.StatT.Warning, e.Message);
                        retVar = -10;
                    }

                    //Copy data to the Data Out
                    if (retVar == 0)
                    {
                        jj = 0; //Index reinitialize

                        for (j = 0; j < thisArea.Amount; j++)
                        {
                            switch (thisArea.dataType)
                            {
                                case DriverConfig.DatType.Bool:
                                    if (DataOut[i].Data.dBoolean.Length > j)
                                        DataOut[i].Data.dBoolean[j] = IntData[j].dBoolean[j];
                                    break;
                                case DriverConfig.DatType.Byte:
                                    if (DataOut[i].Data.dByte.Length > j)
                                        DataOut[i].Data.dByte[j] = (byte)(IntData[i].dInt[j] & DriverGeneric.MaskByte);
                                    break;
                                case DriverConfig.DatType.Word:
                                    if (DataOut[i].Data.dWord.Length > j)
                                        DataOut[i].Data.dWord[j] = (ushort)(IntData[i].dInt[j] & DriverGeneric.MaskWord);
                                    break;
                                case DriverConfig.DatType.DWord:
                                case DriverConfig.DatType.Real:

                                    //Endianess of the double word.
                                    if (RegOrder == ModbusClient.RegisterOrder.HighLow)
                                    {
                                        highWord = ((uint)IntData[i].dInt[jj] & DriverGeneric.MaskWord) << 16;
                                        lowWord = ((uint)IntData[i].dInt[(jj + 1)] & DriverGeneric.MaskWord);
                                    }
                                    else
                                    {
                                        highWord = ((uint)IntData[i].dInt[jj] & DriverGeneric.MaskWord);
                                        lowWord = ((uint)IntData[i].dInt[(jj + 1)] & DriverGeneric.MaskWord) << 16;
                                    }

                                    //Store the value in the DataOut.
                                    if (thisArea.dataType == DriverConfig.DatType.DWord)
                                    {
                                        if (DataOut[i].Data.dDWord.Length > j)
                                            DataOut[i].Data.dDWord[j] = (highWord | lowWord);
                                    }
                                    else
                                    {
                                        //Float point decimal.
                                        if (DataOut[i].Data.dReal.Length > j)
                                            DataOut[i].Data.dReal[j] = (highWord | lowWord) / ((float)1000.0);
                                    }

                                    jj = jj + 2;
                                    break;
                            }
                        } // For j

                        DataOut[i].TimeStamp = DateTime.UtcNow;
                    }
                    else {  //if retVar == 0. Was reading ok?

                        DataOut[i].TimeStamp = DateTime.MinValue;

                    } //if retVar == 0. Was reading ok?

                }// Area Enable

            } //For cicle Data Areas.

            return retVar;
        } // End Read Function

        /// <summary>
        /// Write data to the Server Device.</summary>
        public int Write(DriverGeneric.DataExt[] DataIn)
        {


            int i, j, jj, SAddress, retVar;
            uint intFloat;
            DriverGeneric.AreaData thisArea;

            retVar = -1;

            //If is not initialized and not connected return  error
            if (!(isInitialized && isConnected)) return retVar;

            //If the DataIn and Internal data doesnt have the correct amount of data areas return error.
            if (!((DataIn.Length == MasterDriverConf.NDataAreas) && (IntData.Length == MasterDriverConf.NDataAreas)))
                return retVar;

            for (i = 0; i < MasterDriverConf.NDataAreas; i++)
            {
                thisArea = MasterDataAreaConf[i];
                SAddress = int.Parse(thisArea.StartAddress);
                retVar = -1;

                if (thisArea.Enable && (thisArea.Write))
                {
                    jj = 0; //Index reinitialize

                    for (j = 0; j < thisArea.Amount; j++)
                    {
                        switch (thisArea.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                if ((IntData[i].dBoolean.Length > j) && (DataIn[i].Data.dBoolean.Length > j))
                                    IntData[i].dBoolean[j] = DataIn[i].Data.dBoolean[j];
                                break;
                            case DriverConfig.DatType.Byte:
                                if ((IntData[i].dInt.Length > j) && (DataIn[i].Data.dByte.Length > j))
                                    IntData[i].dInt[j] = DataIn[i].Data.dByte[j];
                                break;
                            case DriverConfig.DatType.Word:
                                if ((IntData[i].dInt.Length > j) && (DataIn[i].Data.dWord.Length > j))
                                    IntData[i].dInt[j] = DataIn[i].Data.dWord[j];
                                break;
                            case DriverConfig.DatType.DWord:
                                if ((IntData[i].dInt.Length > (jj + 1)) && (DataIn[i].Data.dWord.Length > j))
                                {
                                    //Endianess of the double word.
                                    if (RegOrder == ModbusClient.RegisterOrder.HighLow)
                                    {
                                        IntData[i].dInt[jj] = (int)((DataIn[i].Data.dDWord[j] & DriverGeneric.MaskHWord) >> 16);
                                        IntData[i].dInt[(jj + 1)] = (int)(DataIn[i].Data.dDWord[j] & DriverGeneric.MaskWord);
                                    }
                                    else
                                    {
                                        IntData[i].dInt[jj] = (int)(DataIn[i].Data.dDWord[j] & DriverGeneric.MaskWord);
                                        IntData[i].dInt[(jj + 1)] = (int)((DataIn[i].Data.dDWord[j] & DriverGeneric.MaskHWord) >> 16);
                                    }

                                    jj = jj + 2;
                                }
                                break;
                            case DriverConfig.DatType.Real:
                                //Float point decimal.

                                if ((IntData[i].dInt.Length > (jj + 1)) && (DataIn[i].Data.dReal.Length > j))
                                {
                                    //Convert the 
                                    intFloat = (uint)Math.Abs(Math.Round(DataIn[i].Data.dReal[j] * 1000.0));

                                    //Turn ON/OFF the sign bit.
                                    if (DataIn[i].Data.dReal[j] < 0)
                                    {
                                        intFloat = intFloat | DriverGeneric.MaskNeg;
                                    }
                                    else {
                                        intFloat = intFloat & DriverGeneric.MaskiNeg;
                                    }

                                    //Endianess of the double word.
                                    if (RegOrder == ModbusClient.RegisterOrder.HighLow)
                                    {
                                        IntData[i].dInt[jj] = (int)((intFloat & DriverGeneric.MaskHWord) >> 16);
                                        IntData[i].dInt[(jj + 1)] = (int)(intFloat & DriverGeneric.MaskWord);
                                    }
                                    else
                                    {
                                        IntData[i].dInt[jj] = (int)(intFloat & DriverGeneric.MaskWord);
                                        IntData[i].dInt[(jj + 1)] = (int)((intFloat & DriverGeneric.MaskHWord) >> 16);
                                    }

                                    jj = jj + 2;
                                }
                                break;
                        }
                    } // For j


                    try
                    {
                        //Write the data to the device
                        if (thisArea.dataType == DriverConfig.DatType.Bool)
                        {
                            ModTCPObj.WriteMultipleCoils(SAddress, IntData[i].dBoolean);
                        }
                        else {
                            ModTCPObj.WriteMultipleRegisters(SAddress, IntData[i].dInt);
                        }
                    }
                    catch (Exception e)
                    {
                        Status.NewStat(MainCycle.StatT.Warning, e.Message);
                        retVar = -10;
                    }

                }// Area Enable

            } //For Data Areas.

            return retVar;
        }// END Write Function

    } // Class DriverModbusTCP

} //NameSpace