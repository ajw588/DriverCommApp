using System;
using System.Collections;
using System.Linq;
using EasyModbus;

//This APP Namespace
using DriverCommApp.Conf.DV;
using static DriverCommApp.DriverComm.DriverFunctions;
using StatType = DriverCommApp.Stat.StatReport.StatT;


namespace DriverCommApp.DriverComm.ModbusTCP
{

   class DriverModbusTCP
   {
      /// <summary>
      /// Definition for the driver data types.</summary>
      public struct ModbusDataConta
      {
         public int nVals;
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
      DVConfClass MasterDriverConf;

      /// <summary>
      /// Master Data Area Conf.</summary>
      DAConfClass[] MasterDataAreaConf;

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
      public Stat.StatReport Status;

      /// <summary>
      /// Class contructor.
      /// <param name="DriverConf">Driver Configuration Object</param>
      /// <param name="DataAreaConf">Data Area Block Configuration Object Array</param>
      /// <param name="StatObject">Object for Status Reporting</param></summary>
      public DriverModbusTCP(DVConfClass DriverConf, DAConfClass[] DataAreaConf, Stat.StatReport StatObject)
      {
         MasterDriverConf = DriverConf;
         MasterDataAreaConf = DataAreaConf;

         //The Status Report Object
         Status = StatObject;

         isInitialized = false; isConnected = false;

         //Byte register order. HighLow=Big-endian.
         RegOrder = ModbusClient.RegisterOrder.HighLow;

      }

      /// <summary>
      /// Initialize the object class and prepares for the Server Device connection.</summary>
      public void Initialize()
      {
         bool retVal = true;
         int i, j, SAddress, boolReg;
         DAConfClass thisArea;

         if (!((MasterDriverConf != null) && (Status != null)))
         {
            retVal = false;
            Status.NewStat(StatType.Warning, "Master Objects are Invalid.");
         }

         if (retVal)
            if ((!isInitialized) && (MasterDriverConf.Enable))
            {

               try
               {
                  //Create the driver object
                  ModTCPObj = new ModbusClient(MasterDriverConf.Address, MasterDriverConf.portTCP);
                  ModTCPObj.ConnectionTimeout = MasterDriverConf.Timeout;

                  IntData = new ModbusDataConta[MasterDriverConf.NDataAreas];

                  j = 0;  //Count the enabled Data Areas

                  //Cicle and configure the data areas
                  for (i = 0; i < MasterDriverConf.NDataAreas; i++)
                  {
                     thisArea = MasterDataAreaConf[i];
                     SAddress = int.Parse(thisArea.StartAddress);

                     if (thisArea.Enable)
                     {
                        IntData[i].nVals = thisArea.Amount;

                        switch (thisArea.dataType)
                        {
                           case DriverConfig.DatType.Bool:
                              boolReg = (int)Math.Ceiling(thisArea.Amount / 16.0);
                              IntData[i].dInt = new int[boolReg];
                              IntData[i].nVals = boolReg;
                              break;
                           case DriverConfig.DatType.Byte:
                           case DriverConfig.DatType.Word:
                              IntData[i].dInt = new int[thisArea.Amount];
                              break;
                           case DriverConfig.DatType.DWord:
                           case DriverConfig.DatType.Real:
                              IntData[i].dInt = new int[(thisArea.Amount * 2)];
                              break;
                           default:
                              retVal = false;
                              Status.NewStat(StatType.Warning, "Wrong DataArea Type, Check Config.");
                              break;
                        }
                        j++;
                     }// Area Enable
                  } // For Data Areas

                  //check there is enabled data areas.
                  if (j == 0) retVal = false;
               }
               catch (Exception e)
               {
                  Status.NewStat(StatType.Bad, e.Message);
                  isInitialized = false;
               }
            } //IF Not Initialized and Driver is Enabled

         if (retVal)
            Status.NewStat(StatType.Good);
         else
            Status.NewStat(StatType.Bad, "Initialization Failed.");

         isInitialized = retVal;
      }

      /// <summary>
      /// Attemps to connect to the Server Device.</summary>
      public void Connect()
      {

         if ((!isConnected) && (isInitialized))
            try
            {
               ModTCPObj.Connect(MasterDriverConf.Address, MasterDriverConf.portTCP);
               isConnected = ModTCPObj.Connected;

               if (isConnected)
                  Status.NewStat(StatType.Good);
               else
                  Status.NewStat(StatType.Warning, "Connection Failed");
            }
            catch (Exception e)
            {
               Status.NewStat(StatType.Warning, e.Message);
            }

      } // END Function Connect

      /// <summary>
      /// Disconnect from the Server Device.</summary>
      public void Disconect()
      {
         try
         {
            if (ModTCPObj != null) ModTCPObj.Disconnect();
            isConnected = false;

            if (isInitialized)
               Status.NewStat(StatType.Good);
         }
         catch (Exception e)
         {
            if (isInitialized)
               Status.NewStat(StatType.Warning, e.Message);
         }

      } //End Function Disconnect

      /// <summary>
      /// Reads data from the Server Device.</summary>
      public bool Read(DataExtClass[] DataOut)
      {
         bool retVar = false;
         int i, j, jj, SAddress, boolReg, boolBit;
         uint highWord, lowWord;
         DAConfClass thisArea;

         //If is not initialized and not connected return  error.
         if (!(isInitialized && isConnected && (DataOut != null)))
         {
            Status.NewStat(StatType.Bad, "Not Ready for Reading");
            return false;
         }

         //If the DataOut and Internal data doesnt have the correct amount of data areas return error.
         if (!((DataOut.Length == MasterDriverConf.NDataAreas) && (IntData.Length == MasterDriverConf.NDataAreas)))
         {
            Status.NewStat(StatType.Bad, "Data Containers Mismatch");
            return false;
         }

         //Cicle thru Data Areas.
         for (i = 0; i < MasterDriverConf.NDataAreas; i++)
         {
            thisArea = MasterDataAreaConf[i];
            SAddress = int.Parse(thisArea.StartAddress);

            if (thisArea.Enable && (!thisArea.Write))
            {
               try
               {
                  //Read the data from the device
                  switch (thisArea.dataType)
                  {
                     case DriverConfig.DatType.Bool:
                        boolReg = (int)Math.Ceiling(thisArea.Amount / 16.0);
                        IntData[i].dInt = ModTCPObj.ReadHoldingRegisters(SAddress, boolReg);
                        //Check read complete set of data
                        if (IntData[i].dInt.Length == boolReg) retVar = true;
                        break;
                     case DriverConfig.DatType.Byte:
                     case DriverConfig.DatType.Word:
                        IntData[i].dInt = ModTCPObj.ReadHoldingRegisters(SAddress, thisArea.Amount);
                        //Check read complete set of data
                        if (IntData[i].dInt.Length == thisArea.Amount) retVar = true;
                        break;
                     case DriverConfig.DatType.DWord:
                     case DriverConfig.DatType.Real:
                        IntData[i].dInt = ModTCPObj.ReadHoldingRegisters(SAddress, (2 * thisArea.Amount));
                        //Check read complete set of data
                        if (IntData[i].dInt.Length == (2 * thisArea.Amount)) retVar = true;
                        break;
                     default:
                        Status.NewStat(StatType.Warning, "Wrong DataArea Type, Check Config.");
                        break;
                  }
               }
               catch (Exception e)
               {
                  Status.NewStat(StatType.Bad, e.Message);
                  return false;
               }

               //Copy data to the Data Out
               if (retVar)
               {
                  jj = 0; //Index reinitialize

                  for (j = 0; j < thisArea.Amount; j++)
                  {
                     switch (thisArea.dataType)
                     {
                        case DriverConfig.DatType.Bool:
                           boolReg = Math.DivRem(j, 16, out boolBit);
                           var b = new BitArray(new int[] { IntData[i].dInt[boolReg] }); ;
                           DataOut[i].Data.dBoolean[j] = b[boolBit];
                           break;
                        case DriverConfig.DatType.Byte:
                           DataOut[i].Data.dByte[j] = (byte)(IntData[i].dInt[j] & MaskByte);
                           break;
                        case DriverConfig.DatType.Word:
                           DataOut[i].Data.dWord[j] = (ushort)(IntData[i].dInt[j] & MaskWord);
                           break;
                        case DriverConfig.DatType.DWord:
                        case DriverConfig.DatType.Real:

                           //Endianess of the double word.
                           if (RegOrder == ModbusClient.RegisterOrder.HighLow)
                           {
                              highWord = ((uint)IntData[i].dInt[jj] & MaskWord) << 16;
                              lowWord = ((uint)IntData[i].dInt[(jj + 1)] & MaskWord);
                           }
                           else
                           {
                              highWord = ((uint)IntData[i].dInt[jj] & MaskWord);
                              lowWord = ((uint)IntData[i].dInt[(jj + 1)] & MaskWord) << 16;
                           }

                           //Store the value in the DataOut.
                           if (thisArea.dataType == DriverConfig.DatType.DWord)
                           {
                              DataOut[i].Data.dDWord[j] = (highWord | lowWord);
                           }
                           else
                           {
                              //Float point decimal.
                              DataOut[i].Data.dReal[j] = (highWord | lowWord) / ((float)1000.0);
                           }

                           jj = jj + 2;
                           break;
                        default:
                           Status.NewStat(StatType.Warning, "Wrong DataArea Type, Check Config.");
                           break;
                     }
                  } // For j

                  DataOut[i].NowTimeTicks = DateTime.UtcNow.Ticks;
               }
               else
               {
                  Status.NewStat(StatType.Warning, "ModBus Read error..");
                  DataOut[i].NowTimeTicks = 0;
               } //if retVar == 0. Was reading ok?

            }// Area Enable

         } //For cicle Data Areas.

         return retVar;
      } // End Read Function

      /// <summary>
      /// Write data to the Server Device.</summary>
      public bool Write(DataExtClass[] DataIn)
      {
         bool retVar = false;
         int i, j, jj, SAddress, boolReg, boolBit;
         uint intFloat;
         DAConfClass thisArea;
         var bitArray = new System.Collections.BitArray(16);
         var BitInt = new int[1];

         //If is not initialized and not connected return  error
         if (!(isInitialized && isConnected && (DataIn != null)))
         {
            Status.NewStat(StatType.Bad, "Not Ready for Writing");
            return false;
         }

         //If the DataIn and Internal data doesnt have the correct amount of data areas return error.
         if (!((DataIn.Length == MasterDriverConf.NDataAreas) && (IntData.Length == MasterDriverConf.NDataAreas)))
         {
            Status.NewStat(StatType.Bad, "Data Containers Mismatch");
            return false;
         }

         for (i = 0; i < MasterDriverConf.NDataAreas; i++)
         {
            thisArea = MasterDataAreaConf[i];
            SAddress = int.Parse(thisArea.StartAddress);

            if (thisArea.Enable && thisArea.Write)
            {
               jj = 0; //Index reinitialize

               for (j = 0; j < thisArea.Amount; j++)
               {
                  switch (thisArea.dataType)
                  {
                     case DriverConfig.DatType.Bool:
                        boolReg = Math.DivRem(j, 16, out boolBit);
                        if ((IntData[i].dInt.Length > boolReg) && (DataIn[i].Data.dBoolean.Length > j))
                        {
                           if (boolBit == 0) { bitArray.SetAll(false); BitInt[0] = 0; }
                           bitArray.Set(boolBit, DataIn[i].Data.dBoolean[j]);
                           bitArray.CopyTo(BitInt, 0);
                           IntData[i].dInt[boolReg] = BitInt[0];
                        }
                        break;
                     case DriverConfig.DatType.Byte:
                        IntData[i].dInt[j] = DataIn[i].Data.dByte[j];
                        break;
                     case DriverConfig.DatType.Word:
                        IntData[i].dInt[j] = DataIn[i].Data.dWord[j];
                        break;
                     case DriverConfig.DatType.DWord:
                        //Endianess of the double word.
                        if (RegOrder == ModbusClient.RegisterOrder.HighLow)
                        {
                           IntData[i].dInt[jj] = (int)((DataIn[i].Data.dDWord[j] & MaskHWord) >> 16);
                           IntData[i].dInt[(jj + 1)] = (int)(DataIn[i].Data.dDWord[j] & MaskWord);
                        }
                        else
                        {
                           IntData[i].dInt[jj] = (int)(DataIn[i].Data.dDWord[j] & MaskWord);
                           IntData[i].dInt[(jj + 1)] = (int)((DataIn[i].Data.dDWord[j] & MaskHWord) >> 16);
                        }

                        jj = jj + 2;

                        break;
                     case DriverConfig.DatType.Real:
                        //Float point decimal.

                        //Convert the 
                        intFloat = (uint)Math.Abs(Math.Round(DataIn[i].Data.dReal[j] * 1000.0));

                        //Turn ON/OFF the sign bit.
                        if (DataIn[i].Data.dReal[j] < 0)
                        {
                           intFloat = intFloat | MaskNeg;
                        }
                        else
                        {
                           intFloat = intFloat & MaskiNeg;
                        }

                        //Endianess of the double word.
                        if (RegOrder == ModbusClient.RegisterOrder.HighLow)
                        {
                           IntData[i].dInt[jj] = (int)((intFloat & MaskHWord) >> 16);
                           IntData[i].dInt[(jj + 1)] = (int)(intFloat & MaskWord);
                        }
                        else
                        {
                           IntData[i].dInt[jj] = (int)(intFloat & MaskWord);
                           IntData[i].dInt[(jj + 1)] = (int)((intFloat & MaskHWord) >> 16);
                        }

                        jj = jj + 2;

                        break;
                     default:
                        Status.NewStat(StatType.Warning, "Wrong DataArea Type, Check Config.");
                        break;
                  }
               } // For j


               try
               {
                  //Write the data to the device
                  ModTCPObj.WriteMultipleRegisters(SAddress, IntData[i].dInt);

                  //Report Good
                  Status.NewStat(StatType.Good);
                  retVar = true;
               }
               catch (Exception e)
               {
                  Status.NewStat(StatType.Bad, e.Message);
                  return false;
               }

            }// Area Enable

         } //For Data Areas.

         return retVar;
      }// END Write Function

   } // Class DriverModbusTCP

} //NameSpace