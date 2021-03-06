﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.LabVIEW.Interop;
using System.Net.Sockets;
using XWaveDv;
using System.Net;

//This APP Namespace
using DriverCommApp.Conf.DV;
using static DriverCommApp.DriverComm.DriverFunctions;
using StatType = DriverCommApp.Stat.StatReport.StatT;

namespace DriverCommApp.DriverComm.XWave
{

    class DriverXWave
    {
        /// <summary>
        /// Client for the TCP connection.</summary>
        private TcpClient TCPconn;

        /// <summary>
        /// Client for the UDP connection.</summary>
        private UdpClient UDPconn;

        /// <summary>
        /// Public var for the amount of variables.</summary>
        public DriverConfig.nVars NumVars;

        /// <summary>
        /// Copy of the Driver Configuration.</summary>
        private DVConfClass MasterDriverConf;

        /// <summary>
        ///Data Container for initialized VarsTree.</summary>
        private VarTree[,] DataInit;

        /// <summary>
        /// Driver Status.</summary>
        public Stat.StatReport Status;

        /// <summary>
        /// Flag for Driver Initialization.</summary>
        public bool isInitialized;

        /// <summary>
        /// Flag for Driver Connection.</summary>
        public bool isConnected;

        /// <summary>
        /// Class contructor, receives the Driver Configuration.
        /// <param name="DriverConf">Driver Configuration Object</param>
        /// <param name="StatObject">Object for Status Reporting</param></summary>
        public DriverXWave(DVConfClass DriverConf, Stat.StatReport StatObject)
        {
            //Copy the Driver Configuration.
            MasterDriverConf = DriverConf;

            NumVars = new DriverConfig.nVars();
            DataInit = new VarTree[1, 1];

            //The Status Report Object
            Status = StatObject;

            isInitialized = false; isConnected = false;
        }//END Constructor

        /// <summary>
        /// Initialize the object class and prepares for the Server Device connection.</summary>
        public void Initialize()
        {
            //Flag for the configuration validation.
            bool retVal = true;

            if (!((MasterDriverConf != null) && (Status != null)))
            {
                retVal = false;
                Status.NewStat(StatType.Warning, "Master Objects are Invalid.");
            }

            if (retVal)
                if ((!isInitialized) && (!isConnected))
                {
                    //Check the TCP port for the XWave device comms.
                    //Check the UDP port for the XWave device comms.
                    if (!((MasterDriverConf.portTCP > 100) && (MasterDriverConf.portUDP > 100)))
                    {
                        retVal = false;
                        Status.NewStat(StatType.Warning, "Wrong Config Params, Check Config.");
                    }

                    //Check the IP Address
                    if (MasterDriverConf.Address.Length < 6)
                    {
                        retVal = false;
                        Status.NewStat(StatType.Warning, "Wrong Config Params, Wrong IP.");
                    }

                    try
                    {
                        //Get the Vars Tree initialized from the XML configuration file.
                        if (retVal)
                            retVal = XWaveDriver.Init(MasterDriverConf.DefFile, out NumVars.nTVars, out DataInit);
                    }
                    catch (Exception e)
                    {
                        Status.NewStat(StatType.Bad, e.Message);
                        retVal = false;
                    }

                    //Count the vars of each type from the Var Tree.
                    if (retVal) retVal = CountVars();

                    if (retVal)
                        Status.NewStat(StatType.Good);
                    else
                        Status.NewStat(StatType.Bad, "Initialization Failed.");

                    isInitialized = retVal;
                }
        }//END Function Initialize

        /// <summary>
        /// Attemps to connect to the Server Device.</summary>
        public void Connect()
        {
            if ((isInitialized) && (!isConnected))
                try
                {
                    TCPconn = new TcpClient();

                    //Configure timeouts
                    TCPconn.ReceiveTimeout = MasterDriverConf.Timeout;
                    TCPconn.SendTimeout = MasterDriverConf.Timeout;

                    //Connect to the TCP
                    TCPconn.Connect(MasterDriverConf.Address, MasterDriverConf.portTCP);

                    if (TCPconn.Connected)
                    {
                        UDPconn = new UdpClient(MasterDriverConf.portUDP);
                        //UDPconn.Connect(MasterDriverConf.Address, MasterDriverConf.portUDP);
                    }

                    isConnected = TCPconn.Connected;

                    if (isConnected)
                        Status.NewStat(StatType.Good);
                    else
                        Status.NewStat(StatType.Warning, "Connection Failed");
                }
                catch (Exception e)
                {
                    Status.NewStat(StatType.Bad, e.Message);
                }
        } //END Connect Function

        /// <summary>
        /// Disconnect from the Server Device.</summary>
        public void Disconect()
        {

            try
            {
                if (TCPconn != null) TCPconn.Close();
                // XWaveDriver.CloseConn(ConnectionID);
                if (UDPconn != null) UDPconn.Close();
                isConnected = false;

                if (isInitialized)
                    Status.NewStat(StatType.Good);
            }
            catch (Exception e)
            {
                if (isInitialized)
                    Status.NewStat(StatType.Bad, e.Message);
            }
        } //END Disconnect Function

        /// <summary>
        /// Reads data from the Server Device.
        /// /// <param name="DataOut">Object with the data beign readed</param> </summary>
        public bool Read(DataExtClass[] DataOut)
        {
            VarTree[,] DataRead;
            IPAddress addressIP;
            int i, iBool, idWord, isWord, iFloat;
            string returnData = string.Empty; byte[] receiveBytes; bool goodRead;

            //Creates an IPEndPoint to record the IP Address and port number of the sender. 
            // The IPEndPoint will allow you to read datagrams sent from any source.
            addressIP = IPAddress.Parse(MasterDriverConf.Address);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(addressIP, MasterDriverConf.portUDP);

            //If is not initialized and not connected return  error
            if ( !(isInitialized && isConnected && (DataOut!=null)) )
            {
                Status.NewStat(StatType.Bad, "Not Ready for Reading");
                return false;
            }

            //If the DataOut and Internal data doesnt have the correct amount of data areas return error.
            if (!(DataOut.Length == 4))
            {
                Status.NewStat(StatType.Bad, "Data Containers Mismatch");
                return false;
            }

            //Initialize the read.
            goodRead = false;

            try
            {
                if (UDPconn.Available >= (NumVars.nTVars * 4))
                {
                    receiveBytes = UDPconn.Receive(ref RemoteIpEndPoint);
                    returnData = Encoding.ASCII.GetString(receiveBytes);
                    goodRead = XWaveDriver.Unflatten(returnData, DataInit, out DataRead);
                }
                else
                {
                    DataRead = DataInit;
                }
            }
            catch (Exception e)
            {
                Status.NewStat(StatType.Bad, e.Message);
                return false;
            }

            //If Getting data was sucessfull, then convert to local datatypes.
            if (goodRead)
            {
                //Initialize the index counters.
                iBool = 0; idWord = 0; isWord = 0; iFloat = 0;

                //Update TimeStamp on DataOut.
                DataOut[0].NowTimeTicks = DateTime.UtcNow.Ticks;
                DataOut[1].NowTimeTicks = DataOut[0].NowTimeTicks;
                DataOut[2].NowTimeTicks = DataOut[0].NowTimeTicks;
                DataOut[3].NowTimeTicks = DataOut[0].NowTimeTicks;

                for (i = 0; i < NumVars.nTVars; i++)
                {
                    switch (DataRead[0, i].val_type)
                    {
                        case val_type.bool_t:
                            if (iBool < NumVars.nBool)
                            {
                                DataOut[0].Data.dBoolean[iBool] = DataRead[0, i].@bool;

                                if (DataOut[0].VarNames[(NumVars.nBool - 1)] == null)
                                    DataOut[0].VarNames[iBool] = DataRead[0, i].name;

                                iBool++;
                            }
                            break;
                        case val_type.u32_t:
                            if (idWord < NumVars.nDWord)
                            {
                                DataOut[1].Data.dDWord[idWord] = DataRead[0, i].u32;

                                if (DataOut[1].VarNames[(NumVars.nDWord - 1)] == null)
                                    DataOut[1].VarNames[idWord] = DataRead[0, i].name;

                                idWord++;
                            }
                            break;
                        case val_type.i32_t:
                            if (isWord < NumVars.nsDWord)
                            {
                                DataOut[2].Data.dsDWord[isWord] = DataRead[0, i].i32;

                                if (DataOut[2].VarNames[(NumVars.nsDWord - 1)] == null)
                                    DataOut[2].VarNames[isWord] = DataRead[0, i].name;

                                isWord++;
                            }
                            break;
                        case val_type.f32_t:
                            if (iFloat < NumVars.nReal)
                            {
                                DataOut[3].Data.dReal[iFloat] = DataRead[0, i].f32;

                                if (DataOut[3].VarNames[(NumVars.nReal - 1)] == null)
                                    DataOut[3].VarNames[iFloat] = DataRead[0, i].name;

                                iFloat++;
                            }
                            break;
                        default:
                            Status.NewStat(StatType.Warning, "Wrong DataArea Type, Check Config.");
                            break;
                    }
                } //For GetData

                Status.NewStat(StatType.Good);
                return true;
            }
            else
            {
                DataOut[0].NowTimeTicks = 0; DataOut[1].NowTimeTicks = 0;
                DataOut[2].NowTimeTicks = 0; DataOut[3].NowTimeTicks = 0;

                Status.NewStat(StatType.Warning, "XWave Read error..");
                return false;
            }// if goodRead

        } //END read function.

        /// <summary>
        /// Write data to the Server Device.
        /// <param name="DataIn">Object with the data to write</param></summary>
        public bool Write(DataExtClass[] DataIn)
        {
            Status.NewStat(StatType.Warning, "Write is not implemented in XWave Driver.");
            return false;
        }

        /// <summary>
        /// Count the number and type of vars in the Var Tree.
        /// </summary>
        private bool CountVars()
        {
            int i;

            //Initialize the counters
            NumVars.nBool = 0; NumVars.nDWord = 0;
            NumVars.nReal = 0; NumVars.nsDWord = 0;

            for (i = 0; i < NumVars.nTVars; i++)
            {
                switch (DataInit[0, i].val_type)
                {
                    case val_type.bool_t:
                        NumVars.nBool++;
                        break;
                    case val_type.u32_t:
                        NumVars.nDWord++;
                        break;
                    case val_type.i32_t:
                        NumVars.nsDWord++;
                        break;
                    case val_type.f32_t:
                        NumVars.nReal++;
                        break;
                }

            }

            //Data validation, every var should have a type, and counted towards.
            if ((NumVars.nBool + NumVars.nDWord + NumVars.nsDWord + NumVars.nReal) == NumVars.nTVars)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//Function CountVars

    }// Class Ends




}