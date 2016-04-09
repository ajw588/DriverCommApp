using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DriverCommApp.Conf;
using DriverCommApp.CommDriver;
using DriverCommApp.DBMain;
using DriverCommApp.Historics;
using System.ComponentModel;

namespace DriverCommApp
{
    class MainCycle
    {
        /// <summary>
        /// Status Modes Definition.
        /// </summary>
        public enum StatT
        {
            Undefined,
            Good,
            Warning,
            Bad
        }

        /// <summary>
        /// Struct to report the Worker status to the GUI. </summary>
        public struct WorkerProgress
        {
            public long LoopTime;
            public int DriverID;
            public string StatMsg;
            public StatObj DVstat;
            public StatObj DBstat;
        }

        /// <summary>
        /// Status Struct.
        /// </summary>
        public struct StatObj
        {
            public StatT StatInt;
            public bool isOK;
            public string StatMSG;
            public long [] ID_Time;
            int cGood, cYellow;

            public void ResetStat()
            {
                StatMSG = "";
                isOK = false;
                StatInt = StatT.Undefined;
            }

            //Method to Update the Status
            public void NewStat(StatT IntStat, string MSGStat)
            {
                string time;
                DateTime NowTime;

                NowTime = DateTime.UtcNow;
                time = NowTime.ToShortDateString() + " " + NowTime.ToShortTimeString() + ": ";

                if (IntStat != StatT.Bad) { StatInt = IntStat; }

                if (MSGStat.Length > 3)
                    if (StatMSG.Length > 3)
                    { StatMSG = StatMSG + Environment.NewLine + time + MSGStat; }
                    else { StatMSG = time+MSGStat; }

                if (StatInt == StatT.Good)
                { isOK = true; }
                else { isOK = false; }
            }

            //Method to Update the Status, Including and ID
            public void NewStat(StatT IntStat, int ID, string MSGStat)
            {
                string time;
                DateTime NowTime;

                NowTime = DateTime.UtcNow;
                time = NowTime.ToShortDateString() + " " + NowTime.ToShortTimeString() + ": ";

                if (IntStat != StatT.Bad) { StatInt = IntStat; }

                if (MSGStat.Length > 3)
                    if (StatMSG.Length > 3)
                    { StatMSG = StatMSG + Environment.NewLine + time + "Dv" + ID.ToString("00") + ": " + MSGStat; }
                    else { StatMSG = time + "Dv" + ID.ToString("00") + ": " + MSGStat; }

                if (StatInt == StatT.Good)
                { isOK = true; }
                else { isOK = false; }
            }
        } // END StatObj Struct definition

        /// <summary>
        /// Configuration Object.
        /// </summary>
        internal ConfMain theConfig;

        /// <summary>
        /// Driver Collection Object.
        /// </summary>
        internal DriverGeneric[] theDriver;

        /// <summary>
        /// Driver Collection Object.
        /// </summary>
        internal BackgroundWorker[] WorkersCollection;

        /// <summary>
        /// Database Object.
        /// </summary>
        private DB_Main theDatabase;

        /// <summary>
        /// Historics Object.
        /// </summary>
        private HistoricsMain theHistorics;

        /// <summary>
        /// Historics Enabled Flag.
        /// </summary>
        private bool EnHistorics;

        /// <summary>
        /// Flag Initialized.
        /// </summary>
        public bool isInitialized;

        /// <summary>
        /// Workers Running Flag.
        /// </summary>
        public bool WorkersRuning;

        /// <summary>
        /// Main Drivers Status.
        /// </summary>
        public StatObj StatDVMain;

        /// <summary>
        /// Database Status.
        /// </summary>
        public StatObj StatDBMain;

        /// <summary>
        /// Number of drives configurated.
        /// </summary>
        private int NumDrivers;

        /// <summary>
        /// Number of drives Running.
        /// </summary>
        private int NumDvRun;

        /// <summary>
        ///Parallel Block objects
        /// </summary>
        object LockDBRead = new object();
        object LockDBWrite = new object();
        object LockHistorics = new object();

        /// <summary>
        /// Main Cycle constructor.
        /// </summary>
        public MainCycle()
        {
            string StatusMSG;

            //Initialize to some value.
            NumDrivers = 0;

            //Reinitialize MSG for Status
            StatDVMain.ResetStat(); StatDBMain.ResetStat();

            //Build the Config objects for Database and Historics
            theConfig = new ConfMain();

            if (theConfig.isInitialized)
            {
                StatusMSG = "Configuration is Loaded";
                StatDBMain.NewStat(StatT.Good, StatusMSG);
                StatDVMain.NewStat(StatT.Good, StatusMSG);
            }
            else
            {
                StatusMSG = "Bad: Configuration NOT Loaded";
                StatDBMain.NewStat(StatT.Bad, StatusMSG);
                StatDVMain.NewStat(StatT.Bad, StatusMSG);
            }

            //Reset the flags
            isInitialized = false;
            WorkersRuning = false;
            EnHistorics = false;
        }

        ///<summary>
        /// Initialize the Drivers and Database objects.
        /// </summary>
        public int Initialize()
        {
            int i, RTime;
            string StatusMSG;

            //Init Flags
            isInitialized = false; EnHistorics = false;

            if (theConfig.isInitialized && (!WorkersRuning))
            {
                NumDrivers = theConfig.ConfDriver.GeneralSett.cDrivers;
                if (NumDrivers > 0)
                {
                    if (theConfig.MainConf.RTBaseLoop > 49) { RTime = theConfig.MainConf.RTBaseLoop; }
                    else { RTime = 50; }

                    //Create the Objects for the Drivers
                    theDriver = new DriverGeneric[NumDrivers];

                    for (i = 0; i < NumDrivers; i++)
                    { theDriver[i] = new DriverGeneric((i + 1), theConfig.ConfDriver); }

                    theDatabase = new DB_Main(theConfig.ConfDB);
                    if (theConfig.MainConf.EnHistorics) theHistorics = new HistoricsMain(theConfig.ConfDB);
                    StatDVMain.ID_Time = new long [(NumDrivers+1)]; //To report the timeloop

                    //Initialize the Drivers, and add it to the Database manager
                    for (i = 0; i < NumDrivers; i++)
                    {
                        if (theDriver[i].thisDriverConf.Enable) theDriver[i].Initialize();
                        if (theDriver[i].isInitialized)
                        {
                            StatusMSG = "Driver " + (i+1).ToString("00") + " is initialized";
                            StatDVMain.NewStat(StatT.Good, StatusMSG);

                            //Add the Driver to the database manager
                            if (theDatabase.addDriver(theDriver[i].thisDriverConf, theDriver[i].thisAreaConf) < 0)
                            {
                                StatusMSG = "Bad: Driver " + (i + 1).ToString("00") + " NOT added to Database manager";
                                StatDVMain.NewStat(StatT.Bad, StatusMSG);
                                StatDVMain.ID_Time[(i + 1)] = -2; //Driver Not Initialized
                                isInitialized = false;
                                return -1;
                            } else
                            {
                                //If adding to Database succeeded, then add it to the historics.
                                if (theConfig.MainConf.EnHistorics)
                                    theHistorics.addDriver(theDriver[i].thisDriverConf, theDriver[i].thisAreaConf);
                            }
                        }
                        else
                        {
                            StatusMSG = "Bad: Driver " + i.ToString("00") + " Failed to initialize";
                            StatDVMain.NewStat(StatT.Bad, StatusMSG);
                            StatDVMain.ID_Time[(i + 1)] = -1; //Driver Not Initialized
                            if (theDriver[i].thisDriverConf.Enable) return -1;
                        }
                        StatDVMain.ID_Time[(i + 1)] = -100; //Driver Initialized
                    } // END FOR Drivers

                    //Initialize the Database
                    theDatabase.Initialize(theConfig.MainConf.InitialSet);
                    if (!theDatabase.isInitialized)
                    {
                        StatusMSG = "Bad: Database Failed to initialize";
                        StatDBMain.NewStat(StatT.Bad, StatusMSG);
                        isInitialized = false;
                        return -1;
                    }
                    else
                    {
                        StatusMSG = "Database is initialized";
                        StatDBMain.NewStat(StatT.Good, StatusMSG);
                        isInitialized = true;
                    }

                    //Initialize the Historics if enabled.
                    if (theConfig.MainConf.EnHistorics)
                    {
                        theHistorics.Initialize(theConfig.MainConf.InitialSet);

                        if (!theHistorics.isInitialized)
                        {
                            StatusMSG = "Bad: Historics Failed to initialize";
                            StatDBMain.NewStat(StatT.Bad, StatusMSG);
                        }
                        else
                        {
                            StatusMSG = "Historics are initialized";
                            StatDBMain.NewStat(StatT.Good, StatusMSG);
                            EnHistorics = true;
                        }
                    } //END Historics Initialization.

                } //Num Drivers >0

                //Reset the InitialSet in the Config if it was set.
                if (theConfig.MainConf.InitialSet) theConfig.WriteAppSetting("InitialSet", "False");

            }// If Config isInitialized and No WorkersRunning


            return 0;
        } // END Initialize Function.

        ///<summary>
        /// Start the ciclic work
        /// One worker for each driver
        /// </summary>
        public int StartWork()
        {
            int i;

            if (isInitialized && (!WorkersRuning))
            {
                //Create the Background workers.
                WorkersCollection = new BackgroundWorker[NumDrivers];
               
                for(i=0;i<NumDrivers;i++)
                {
                    WorkersCollection[i] = new BackgroundWorker();
                    WorkersCollection[i].DoWork +=
                            new DoWorkEventHandler(Worker_DoWork);
                    WorkersCollection[i].RunWorkerCompleted +=
                            new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
                    WorkersCollection[i].ProgressChanged +=
                            new ProgressChangedEventHandler(Worker_ProgressChanged);

                    //Properties
                    WorkersCollection[i].WorkerReportsProgress = true;
                    WorkersCollection[i].WorkerSupportsCancellation = true;

                    WorkersCollection[i].RunWorkerAsync(theDriver[i]);
                }

                NumDvRun = NumDrivers;
                if (i > 0) WorkersRuning = true;

                return i;
            }

            return -1;
                        
        }// END Function StartWork

        ///<summary>
        /// Stop the Workers.
        /// </summary>
        public int StopWorkers()
        {
            int i;
            if (WorkersRuning)
            {
                i = 0;
                foreach (BackgroundWorker Worker in WorkersCollection)
                {
                    Worker.CancelAsync();
                    StatDVMain.NewStat(StatT.Good, (i+1), "Stopping worker for Driver ");
                    i++;
                }
                Thread.Sleep(500); // Wait for the Threads Finishing
                if (NumDvRun<=0) WorkersRuning = false;
                return i;
            }
            return -1;
        } //END Stop Workers Function.

        ///<summary>
        /// Stop all the Workers, and
        /// Close all Drivers Comms.
        /// </summary>
        public int CloseAll()
        {
            //Reinitialize MSG for Status
            StatDVMain.ResetStat();
            try
            {
                if (isInitialized && !WorkersRuning)
                    foreach (DriverGeneric thisDriver in theDriver)
                    {
                        thisDriver.Disconnect();
                        StatDVMain.NewStat(StatT.Good, thisDriver.thisDriverConf.ID, "Disconnected Driver ");
                    }
            }
            catch (Exception e)
            {
                StatDVMain.NewStat(StatT.Bad, e.Message);
            }

            return 0;
        }


        /// <summary>
        /// This event handler is where the actual,
        /// potentially time-consuming work is done.
        /// Database Read -> Drivers Read and Write -> Database Write.
        /// </summary>
        private void Worker_DoWork(object sender,
            DoWorkEventArgs e)
        {
            int i, msLeft;
            DateTime initialTime, finalTime;
            long msCycle;
            WorkerProgress ToReport;

            //Get the Driver for this worker.
            DriverGeneric thisDriver = (DriverGeneric) e.Argument;

            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            //Name the Worker
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "CycleDriver" + thisDriver.thisDriverConf.ID.ToString("00");

            //Initialize variables
            ToReport = new WorkerProgress();
            msCycle = 0; i = 0;

            while (!e.Cancel)
            {
                //Get time
                initialTime = DateTime.Now;

                if (i > 99) { i = 0; } else { i++; }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {
                    

                    //Do Operations.

                    if (!DoCycle(thisDriver, out ToReport.DVstat, out ToReport.DBstat))
                    {
                        //Fatal Error, Cancel the Worker
                        e.Cancel = true;
                    }

                    //Sleep for the rest of the time cicle.
                    finalTime = DateTime.Now;
                    msCycle = ((finalTime.Ticks - initialTime.Ticks) / TimeSpan.TicksPerMillisecond);
                    ToReport.LoopTime = msCycle;
                    ToReport.DriverID = thisDriver.thisDriverConf.ID;

                    if (msCycle < thisDriver.thisDriverConf.CycleTime)
                    {
                        msLeft = (int)(thisDriver.thisDriverConf.CycleTime - msCycle);
                        Thread.Sleep(msLeft);
                        ToReport.StatMsg = "";
                    }
                    else
                    {
                        //Cicle is taking too much time!!!
                        //Only report if its 15% higger than limit.
                        if (msCycle > (thisDriver.thisDriverConf.CycleTime*1.15))
                        ToReport.StatMsg = "Main Cycle taking too long, " + msCycle.ToString() +
                            " ms, and it should be less than " + thisDriver.thisDriverConf.CycleTime.ToString() + " ms";
                    }

                    worker.ReportProgress(i, ToReport);

                }
            } //While not Cancelation

        }//END Function DoWork

        private bool DoCycle(DriverGeneric thisDriver, out StatObj StatDV, out StatObj StatDB)
        {
            bool DVreadOK, DBreadOK;
            int valRet, cRecon;
            string DBmsg;

            //Create the Stat Obj
            StatDV = new StatObj();
            StatDV.ResetStat();

            StatDB = new StatObj();
            StatDB.ResetStat();

            //Counter for reconnections
            cRecon = 0;

            //Init Flags
            DVreadOK = false;
            DBreadOK = false;

            try
            {
                if (thisDriver.isInitialized)
                {
                    //*********************************************************
                    //Read the Database
                    //*********************************************************
                    if (thisDriver.iamWriting)
                    {
                        lock (LockDBRead)
                        {
                            //Read the Database
                            valRet = theDatabase.ReadDB(ref thisDriver.ExtData, thisDriver.thisDriverConf.NDataAreas);
                            theDatabase.GetStatus(out DBmsg);
                            if (valRet < 0)
                            {
                                //Bad Error
                                theDatabase.GetStatus(out DBmsg);
                                StatDB.NewStat(StatT.Bad, thisDriver.thisDriverConf.ID, "Error DB Read, DV: ");
                                DBreadOK = false;
                            }
                            else
                            {
                                DBreadOK = true;
                                //Warning Reading from Backup, Master is Down
                                if (valRet == 1)
                                {
                                    theDatabase.GetStatus(out DBmsg);
                                    StatDB.NewStat(StatT.Warning, thisDriver.thisDriverConf.ID, "Warn DB Read, DV: ");
                                }
                            }
                        } //END LockDBRead
                    }

                    //*********************************************************
                    //Connect to the driver
                    //*********************************************************
                    Connect:
                    if (!thisDriver.isConnected)
                    {
                        thisDriver.Connect();
                        if (thisDriver.isConnected)
                        {
                            //Driver Connected
                            StatDV.NewStat(StatT.Good, thisDriver.thisDriverConf.ID, "Connected to Driver ");
                            //cRecon = 0; //only reset at top
                        }
                        else
                        {
                            //Driver Conn Error
                            if (cRecon >= 3)
                                StatDV.NewStat(StatT.Bad, thisDriver.thisDriverConf.ID, "Error Conn to Driver ");

                            //Try reconnect.
                            if (cRecon < 3)
                            {
                                cRecon++;
                                thisDriver.Disconnect();
                                
                                StatDV.NewStat(StatT.Warning, thisDriver.thisDriverConf.ID, "Reconnecting to Driver ");
                                
                                //Wait a bit to get the driver disconnected and re-try
                                Thread.Sleep(thisDriver.thisDriverConf.Timeout);

                                goto Connect;
                            }
                        }
                    }

                    //*********************************************************
                    //Read and Write data to the Driver.
                    //*********************************************************
                    valRet = 0;
                    if (thisDriver.isConnected)
                    {
                        //Write to the Driver
                        if (thisDriver.iamWriting && DBreadOK)
                        {
                            valRet = thisDriver.Write();
                            if (valRet < 0)
                            {
                                //Error Writing
                                StatDV.NewStat(StatT.Bad, thisDriver.thisDriverConf.ID, "Error Write Driver ");
                                if (cRecon < 3)
                                {
                                    cRecon++;
                                    thisDriver.Disconnect();
                                    goto Connect;
                                }
                            }
                        } //Write Driver

                        //Read from the Driver
                        if (thisDriver.iamReading)
                        {
                            valRet = thisDriver.Read();
                            if (valRet < 0)
                            {
                                //Error Reading
                                DVreadOK = false;
                                StatDV.NewStat(StatT.Bad, thisDriver.thisDriverConf.ID, "Error Read Driver ");

                                if (cRecon < 3)
                                {
                                    cRecon++;
                                    thisDriver.Disconnect();
                                    goto Connect;
                                }
                            }
                            else
                            {
                                DVreadOK = true;
                            }
                        } //Read Driver

                    } // If Driver is connected.

                    //*********************************************************
                    //Write to the Database
                    //*********************************************************
                    if (thisDriver.iamReading && DVreadOK)
                    {
                        lock (LockDBWrite)
                        {
                            //Write the Database
                            valRet = theDatabase.WriteDB(thisDriver.ExtData, thisDriver.thisDriverConf.NDataAreas);
                            if (valRet < 0)
                            {
                                //Bad Error, no write to backup or master.
                                theDatabase.GetStatus(out DBmsg);
                                StatDB.NewStat(StatT.Bad, thisDriver.thisDriverConf.ID, "Error DB Write, DV: ");
                            }
                            else if (valRet == 1)
                            {
                                //Warning Writing DB, Backup or Master is Down.
                                theDatabase.GetStatus(out DBmsg);
                                StatDB.NewStat(StatT.Warning, thisDriver.thisDriverConf.ID, "Warn DB Write, DV: ");
                            }
                        }

                        //*********************************************************
                        //Write to the Historics
                        //*********************************************************
                        if (EnHistorics)
                        {
                            lock (LockHistorics)
                            {
                                theHistorics.Write(thisDriver.ExtData, thisDriver.thisDriverConf.NDataAreas);
                                if (valRet < 0)
                                {
                                    //Bad Error, no write to backup or master.
                                    StatDB.NewStat(StatT.Warning, thisDriver.thisDriverConf.ID, "Error Historics Write, DV: ");
                                    //theHistorics.GetStatus(out DBmsg);
                                }
                            }
                        }
                    } //END If Reading
                        

                }// IF the Driver is Initialized
                else
                {
                    //Driver is not initialized, end the worker to save resources.
                    return false;
                }
            }
            catch (Exception e)
            {
                StatDV.NewStat(StatT.Bad, thisDriver.thisDriverConf.ID, e.Message);
                return false;
            }

            return true;

        } // END Do Cicle Function

        /// <summary>
        /// This event handler deals with finalizing the
        /// background operation. </summary>
        private void Worker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {

            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.

            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
            }

            //Driver Worker Finished and removed from counter.
            NumDvRun--;
        }

        ///<summary>
        /// This event handles the Worker updates.
        /// </summary>
        private void Worker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            WorkerProgress StatReport;

            //Get the report data
            StatReport = (WorkerProgress) e.UserState;

            if (StatReport.DVstat.StatInt != StatT.Undefined)
                StatDVMain.NewStat(StatReport.DVstat.StatInt, StatReport.DVstat.StatMSG);

            if (StatReport.DBstat.StatInt != StatT.Undefined)
                StatDBMain.NewStat(StatReport.DBstat.StatInt, StatReport.DBstat.StatMSG);

            if (StatReport.StatMsg.Length>3)
                StatDBMain.NewStat(StatT.Good, StatReport.DriverID, StatReport.StatMsg);

            //Report the TimeLoop
            if (StatReport.DriverID<StatDVMain.ID_Time.Length)
                StatDVMain.ID_Time[StatReport.DriverID] = StatReport.LoopTime;
        }


        ///<summary>
        ///############################## Deprecated #################################
        ///############## Do not use, use the worker aproach instead #################
        ///######################## 22/03/2016 Jose Morales ##########################
        /// Do the ciclic work
        /// Database Read -> Drivers Read and Write -> Database Write.
        /// </summary>
        public int Deprecated_DoSomeWork(int DriverID)
        {
            
            int retVal = 0;
            string StatusMSG;
            /*
            //Parallel Block objects
            object LockDBRead = new object();
            object LockDBWrite = new object();
            object LockHistorics = new object();
            object LockDVStat = new object();

            //Reinitialize MSG for Status
            StatDV.ResetStat(); StatDB.ResetStat();

            if (isInitialized)
            {


                Parallel.ForEach(theDriver, new ParallelOptions { MaxDegreeOfParallelism = 2 }, thisDriver =>
                {
                    //Loop parallel duplicated variables
                    int cRecon = 0, valRet = 0;
                    ulong StatVar = 0;
                    string DBmsg = "";

                    if (Thread.CurrentThread.Name == null)
                        Thread.CurrentThread.Name = "CicleDriver" + thisDriver.thisDriverConf.ID.ToString("00");


                }); // Parallel FOR

            }
            else
            {

            }

            if ((StatDV.StatInt == StatT.Bad) || (StatDV.StatInt == StatT.Bad))
            {
                retVal = -10;
            }
            else if ((StatDV.StatInt == StatT.Warning) || (StatDV.StatInt == StatT.Warning))
            {
                retVal = -1;
            }

            StatusMSG = "";
            if (StatDV.StatInt == StatT.Undefined)
            {
                StatDV.NewStat(StatT.Good, StatusMSG);
            }

            if (StatDB.StatInt == StatT.Undefined)
            {
                StatDB.NewStat(StatT.Good, StatusMSG);
            }
            */
                        return retVal;

        } //END Function DoSomeWork


        ///<summary>
        /// --Destructor--
        /// Close all Drivers and Database Comms.
        /// </summary>
        ~MainCycle()
        {
            //Close All comms
            CloseAll();

            //Destroy the Child objects
            if (theDatabase != null) theDatabase.Dispose();
            if (theHistorics != null) theHistorics.Dispose();
        }

    }
}
