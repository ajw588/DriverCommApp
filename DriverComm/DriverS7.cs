using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snap7;

//This APP namespace
using static DriverCommApp.DriverComm.DriverFunctions;
using DriverCommApp.Conf;

namespace DriverCommApp.DriverComm.Siemens7
{
    class DriverS7
    {
        /// <summary>
        /// Driver Client to PLC.</summary>
        static S7Client Client;
        private static S7Client.S7CliCompletion Completion; // <== Static var containig the callback
        static int AsyncResult;
        static bool AsyncDone;

        /// <summary>
        /// Multivar Object to Read Data.</summary>
        S7MultiVar Reader;

        /// <summary>
        /// Multivar Object to Write Data.</summary>
        S7MultiVar Writer;

        /// <summary>
        /// Internal data container.</summary>
        DataContainer[] IntData;

        /// <summary>
        /// Master Driver Conf.</summary>
        CConf MasterDriverConf;

        /// <summary>
        /// Master Data Area Conf.</summary>
        AreaData[] MasterDataAreaConf;

        /// <summary>
        /// Driver Status.</summary>
        public MainCycle.StatObj Status;

        /// <summary>
        /// Flag for Driver Initialization.</summary>
        public bool isInitialized;

        /// <summary>
        /// Flag for Driver Connection.</summary>
        public bool isConnected;

        /// <summary>
        /// Class Constructor.</summary>
        public DriverS7(CConf DriverConf, AreaData[] DataAreaConf)
        {
            Status.ResetStat();

            MasterDriverConf = DriverConf;
            MasterDataAreaConf = DataAreaConf;

            isInitialized = false; isConnected = false;
        }

        /// <summary>
        /// Initialize the Driver variables and prepare for connection.</summary>
        public void Initialize()
        {
            int i, datSize, SAddress, tAmount;
            AreaData thisArea;

            if ((!isInitialized) && (MasterDriverConf.Enable))
            {
                try
                {
                    
                    // Client creation
                    Client = new S7Client();

                    // Set the callbacks (using the static var to avoid the garbage collect)
                    Completion = new S7Client.S7CliCompletion(CompletionProc);
                    Client.SetAsCallBack(Completion, IntPtr.Zero);

                    //Configure the timeouts
                    //Client.SetParam(S7Consts.p_i32_PingTimeout, ref MasterDriverConf.Timeout);
                    Client.SetParam(S7Consts.p_i32_RecvTimeout, ref MasterDriverConf.Timeout);
                    Client.SetParam(S7Consts.p_i32_SendTimeout, ref MasterDriverConf.Timeout);

                    //Generate the MultiVar Objects
                    Reader = new S7MultiVar(Client);
                    Writer = new S7MultiVar(Client);

                    IntData = new DataContainer[MasterDriverConf.NDataAreas];

                    //Cicle and configure the data areas
                    for (i = 0; i < MasterDriverConf.NDataAreas; i++)
                    {
                        thisArea = MasterDataAreaConf[i];
                        SAddress = int.Parse(thisArea.StartAddress);
                        datSize = S7Client.S7WLByte; //Always read bytes

                        if (thisArea.Enable)
                        {

                            switch (thisArea.dataType)
                            {
                                case DriverConfig.DatType.Bool:
                                    tAmount = (int) Math.Ceiling((thisArea.Amount / 8.0));
                                    IntData[i].dByte = new byte[tAmount];

                                    if (!thisArea.Write)
                                    {
                                        //Add Reader variable areas.
                                        Reader.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                           SAddress, tAmount, ref IntData[i].dByte);
                                    }
                                    else
                                    {
                                        //Add Writer variable areas.
                                        Writer.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                            SAddress, tAmount, ref IntData[i].dByte);
                                    }

                                    break;
                                case DriverConfig.DatType.Byte:
                                    IntData[i].dByte = new byte[thisArea.Amount];

                                    if (!thisArea.Write)
                                    {
                                        //Add Reader variable areas.
                                        Reader.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                           SAddress, thisArea.Amount, ref IntData[i].dByte);
                                    }
                                    else
                                    {
                                        //Add Writer variable areas.
                                        Writer.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                            SAddress, thisArea.Amount, ref IntData[i].dByte);
                                    }

                                    break;
                                case DriverConfig.DatType.Word:
                                    tAmount = (int) (thisArea.Amount * 2.0);
                                    IntData[i].dByte = new byte[tAmount];
                                    IntData[i].dWord = new UInt16[thisArea.Amount];

                                    if (!thisArea.Write)
                                    {
                                        //Add Reader variable areas.
                                        Reader.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                           SAddress, tAmount, ref IntData[i].dByte);
                                    }
                                    else
                                    {
                                        //Add Writer variable areas.
                                        Writer.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                            SAddress, tAmount, ref IntData[i].dByte);
                                    }

                                    break;
                                case DriverConfig.DatType.DWord:
                                    tAmount = (int)(thisArea.Amount * 4.0);
                                    IntData[i].dByte = new byte[tAmount];
                                    IntData[i].dDWord = new UInt32[thisArea.Amount];

                                    if (!thisArea.Write)
                                    {
                                        //Add Reader variable areas.
                                        Reader.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                           SAddress, tAmount, ref IntData[i].dByte);
                                    }
                                    else
                                    {
                                        //Add Writer variable areas.
                                        Writer.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                            SAddress, tAmount, ref IntData[i].dByte);
                                    }

                                    break;
                                case DriverConfig.DatType.Real:
                                    tAmount = (int)(thisArea.Amount * 4.0);
                                    IntData[i].dByte = new byte[tAmount];
                                    IntData[i].dReal = new float[thisArea.Amount];

                                    if (!thisArea.Write)
                                    {
                                        //Add Reader variable areas.
                                        Reader.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                           SAddress, tAmount, ref IntData[i].dByte);
                                    }
                                    else
                                    {
                                        //Add Writer variable areas.
                                        Writer.Add(S7Client.S7AreaDB, datSize, thisArea.DBnumber,
                                            SAddress, tAmount, ref IntData[i].dByte);
                                    }

                                    break;
                            }
                        }// Area Enable
                    } // For Data Areas

                    isInitialized = true;
                    Status.NewStat(MainCycle.StatT.Good, "");
                }
                catch (Exception e)
                {
                    Status.NewStat(MainCycle.StatT.Warning, e.Message);
                }

            } // IF not isInitialized

        } // END Function Initialized

        /// <summary>
        /// Attemps to connect to the Server Device.</summary>
        public void Connect()
        {
            try
            {
                if (!isConnected)
                    Client.ConnectTo(MasterDriverConf.Address, MasterDriverConf.Rack, MasterDriverConf.Slot);
                isConnected = Client.Connected();
                Status.NewStat(MainCycle.StatT.Good, "");
            }
            catch (Exception e)
            {
                Status.NewStat(MainCycle.StatT.Warning, e.Message);
            }
        } //END Connect Function

        /// <summary>
        /// Disconect from the Server Device.</summary>
        public void Disconect()
        {
            try
            {
                Client.Disconnect();
                isConnected = false;
                Status.NewStat(MainCycle.StatT.Good, "");
            }
            catch (Exception e)
            {
                Status.NewStat(MainCycle.StatT.Warning, e.Message);
            }
        } //END Disconnect Function

        /// <summary>
        /// Read the variables from the Server Device.</summary>
        public int Read(ref DataExt[] DataOut)
        {
            int i, j, retVar, Pos, Bit;
            retVar = -1;

            //If is not initialized and not connected return  error
            if (!(isInitialized && isConnected)) return retVar;

            //If the DataOut and Internal data doesnt have the correct amount of data areas return error.
            if (!((DataOut.Length == MasterDriverConf.NDataAreas) && (IntData.Length == MasterDriverConf.NDataAreas)))
                return retVar;

            try
            {
                retVar = Reader.Read();

                // Update the DataOut with the readed values.

                for (i = 0; i < MasterDriverConf.NDataAreas; i++)
                {
                    if (retVar == 0)
                    {
                        if (MasterDataAreaConf[i].Enable && (!MasterDataAreaConf[i].Write))
                        {
                            Bit = 0;
                            for (j = 0; j < MasterDataAreaConf[i].Amount; j++)
                            {
                                switch (MasterDataAreaConf[i].dataType)
                                {
                                    case DriverConfig.DatType.Bool:
                                        Pos = (int) Math.Floor(j / 8.0); if (Bit > 7) Bit = 0;
                                        if ((DataOut[i].Data.dBoolean.Length > j) && (IntData[i].dByte.Length > Pos))
                                            DataOut[i].Data.dBoolean[j] = S7.GetBitAt(IntData[i].dByte, Pos, Bit);
                                        Bit++;
                                        break;
                                    case DriverConfig.DatType.Byte:
                                        if ((DataOut[i].Data.dByte.Length > j) && (IntData[i].dByte.Length > j))
                                            DataOut[i].Data.dByte[j] = IntData[i].dByte[j];
                                        break;
                                    case DriverConfig.DatType.Word:
                                        Pos = (int) 2*j;
                                        if ((DataOut[i].Data.dWord.Length > j) && (IntData[i].dByte.Length > Pos))
                                            DataOut[i].Data.dWord[j] = S7.GetWordAt(IntData[i].dByte, Pos);
                                        break;
                                    case DriverConfig.DatType.DWord:
                                        Pos = (int) 4*j;
                                        if ((DataOut[i].Data.dDWord.Length > j) && (IntData[i].dByte.Length > Pos))
                                            DataOut[i].Data.dDWord[j] = S7.GetUDIntAt(IntData[i].dByte, Pos);
                                        break;
                                    case DriverConfig.DatType.Real:
                                        Pos = (int) 4*j;
                                        if ((DataOut[i].Data.dReal.Length > j) && (IntData[i].dByte.Length > Pos))
                                            DataOut[i].Data.dReal[j] = S7.GetRealAt(IntData[i].dByte, Pos);
                                        break;
                                }
                            } // For Data Element
                        }
                        DataOut[i].NowTimeTicks = DateTime.UtcNow.Ticks;
                    }
                    else { //IF Read is OK. 
                        DataOut[i].NowTimeTicks = 0;
                    }

                } // For DataAreas
                if (retVar == 0) { Status.NewStat(MainCycle.StatT.Good, ""); }
                    else { Status.NewStat(MainCycle.StatT.Warning, "S7 Read error.."); }
            }
            catch (Exception e)
            {
                Status.NewStat(MainCycle.StatT.Bad, e.Message);
            }

            return retVar;
        }

        /// <summary>
        /// Write data to the Server Device.</summary>
        public int Write(DataExt[] DataIn)
        {
            int i, j, retVar, Pos, Bit;
            retVar = -1;

            //If is not initialized and not connected return  error
            if (!(isInitialized && isConnected)) return retVar;

            //If the DataIn and Internal data doesnt have the correct amount of data areas return error.
            if (!((DataIn.Length == MasterDriverConf.NDataAreas) && (IntData.Length == MasterDriverConf.NDataAreas)))
                return retVar;

            // Update the DataOut with the readed values.
            for (i = 0; i < MasterDriverConf.NDataAreas; i++)
            {
                if (MasterDataAreaConf[i].Enable && MasterDataAreaConf[i].Write)
                {
                    for (j = 0; j < MasterDataAreaConf[i].Amount; j++)
                    {
                        Bit = 0;
                        switch (MasterDataAreaConf[i].dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                Pos = (int)Math.Floor(j / 8.0); if (Bit > 7) Bit = 0;
                                if ((DataIn[i].Data.dBoolean.Length > j) && (IntData[i].dByte.Length > Pos))
                                     S7.SetBitAt(ref IntData[i].dByte,Pos,Bit,DataIn[i].Data.dBoolean[j]);
                                Bit++;
                                break;
                            case DriverConfig.DatType.Byte:
                                if ((DataIn[i].Data.dByte.Length > j) && (IntData[i].dByte.Length > j))
                                    IntData[i].dByte[j] = DataIn[i].Data.dByte[j];
                                break;
                            case DriverConfig.DatType.Word:
                                Pos = (int) 2*j;
                                if ((DataIn[i].Data.dWord.Length > j) && (IntData[i].dWord.Length > j))
                                    S7.SetWordAt(IntData[i].dByte, Pos, DataIn[i].Data.dWord[j]);
                                break;
                            case DriverConfig.DatType.DWord:
                                Pos = (int) 4*j;
                                if ((DataIn[i].Data.dDWord.Length > j) && (IntData[i].dDWord.Length > j))
                                    S7.SetDWordAt(IntData[i].dByte, Pos, DataIn[i].Data.dDWord[j]);
                                break;
                            case DriverConfig.DatType.Real:
                                Pos = (int) 4*j;
                                if ((DataIn[i].Data.dReal.Length > j) && (IntData[i].dReal.Length > j))
                                    S7.SetLRealAt(IntData[i].dByte, Pos, DataIn[i].Data.dReal[j]);
                                break;
                        }
                    } // For Data Element
                }
            }// For DataAreas

            try
            {
                //Write the data and return.
                retVar = Writer.Write();
                Status.NewStat(MainCycle.StatT.Good, "");
            }
            catch (Exception e)
            {
                Status.NewStat(MainCycle.StatT.Warning, e.Message);
            }
            return retVar;
        }

        /// <summary>
        ///  Async completion is called when an async operation was complete
        /// For this simply text demo we only set a flag. 
        /// Use this Function for the Read Client.</summary>
        static void CompletionProc(IntPtr usrPtr, int opCode, int opResult)
        {
            AsyncResult = opResult;
            AsyncDone = true;
        }

        ~DriverS7()
        {

        }
    }
}
