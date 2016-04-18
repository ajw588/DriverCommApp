using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Collections.Concurrent;

//This APP Namespaces
using DriverCommApp.Conf.DB;
using static DriverCommApp.Historics.Hist_Functions;
using StatT = DriverCommApp.Stat.StatReport.StatT;
using DatType = DriverCommApp.Conf.DV.DriverConfig.DatType;


namespace DriverCommApp.Historics
{

    class HistoricsMain : IDisposable
    {
        /// <summary>
        /// Database Configuration.</summary>
        HistConfClass HistConf;

        /// <summary>
        /// Drivers Complete Configuration.
        /// </summary>
        List<DriverComm.DVConfDAConfClass> DriversHistConf;

        ///<summary>
        /// Queue for Data to Write to the Historics Database
        ///</summary>
        private ConcurrentQueue<DriverComm.DataExtClass>[] FIFO_Hist;

        ///<summary>
        /// BackgroundWorker for Historics Queue Processing
        ///</summary>
        private BackgroundWorker Worker;

        /// <summary>
        /// Master Status.</summary>
        public Stat.StatReport Status;

        /// <summary>
        /// MySQL Database Master.</summary>
        HistMySQL.Hist_MySQL MasterMySQL;

        /// <summary>
        /// MySQL Database Backup.</summary>
        HistMySQL.Hist_MySQL BackupMySQL;

        /// <summary>
        /// Array with the ID number to indicate the position of the Queue array.</summary>
        private int[] IdtoPos;

        /// <summary>
        /// Workers Running Flag.
        /// </summary>
        public bool WorkersRuning;

        /// <summary>
        /// Flag for initialization.</summary>
        public bool isInitialized;

        /// <summary>
        /// Class Constructor.
        /// <param name="DBConfigFile">Configuration Object.</param> </summary>
        public HistoricsMain(DBConfig DBConfigFile)
        {
            HistConfClass.ServerConf Master, Backup;

            //Create the list of drivers.
            DriversHistConf = new List<DriverComm.DVConfDAConfClass>(1);

            //Master Server
            Master.Enable = DBConfigFile.HistMaster.Enable;
            Master.DBname = DBConfigFile.HistMaster.DBname;
            Master.URL = DBConfigFile.HistMaster.URL;
            Master.Username = DBConfigFile.HistMaster.User1;
            Master.Passwd = DBConfigFile.HistMaster.Passw1;
            Master.Port = DBConfigFile.HistMaster.Port;
            Master.Protocol = DBConfigFile.HistMaster.Protocol;
            Master.Rate = DBConfigFile.HistMaster.Rate;
            Master.Type = DBConfigFile.HistMaster.Type;
            Master.HistLengh = DBConfigFile.HistMaster.HistLenght;

            //Backup Server
            Backup.Enable = DBConfigFile.HistBackup.Enable;
            Backup.DBname = DBConfigFile.HistBackup.DBname;
            Backup.URL = DBConfigFile.HistBackup.URL;
            Backup.Username = DBConfigFile.HistBackup.User1;
            Backup.Passwd = DBConfigFile.HistBackup.Passw1;
            Backup.Port = DBConfigFile.HistBackup.Port;
            Backup.Protocol = DBConfigFile.HistBackup.Protocol;
            Backup.Rate = DBConfigFile.HistBackup.Rate;
            Backup.Type = DBConfigFile.HistBackup.Type;
            Backup.HistLengh = DBConfigFile.HistBackup.HistLenght;

            //Create the conf object.
            HistConf = new HistConfClass(Master, Backup);

            //The Status Object
            Status = new Stat.StatReport((int)Stat.StatReport.IDdef.Histall, FileLog: true);

            isInitialized = false;
        } //END Class Constructor

        /// <summary>
        /// Add a driver to be managed by this Database.
        /// <param name="DriverConf">Configuration Object for driver config.</param> 
        /// <param name="DAreaConf">Configuration Object for Data Area config.</param> </summary>
        public int addDriver(DriverComm.DVConfClass DriverConf, DriverComm.DAConfClass[] DAreaConf)
        {
            int retVal=-1;
            DriverComm.DVConfDAConfClass NewDriver;

            if ((DriverConf != null) && (DAreaConf != null))
                if (DAreaConf.Length > 0)
            {
                    NewDriver = new DriverComm.DVConfDAConfClass(DriverConf, DAreaConf);

                    DriversHistConf.Add(NewDriver);

                    //Adding a new driver while initialized will require new initzialization.
                    if (isInitialized)
                    {
                        Status.NewStat(StatT.Warning, "Hist Object Requires to Re-Initialize");
                        isInitialized = false;
                        retVal = 1;
                    }
                    else
                    {
                        Status.NewStat(StatT.Good, "DV Added: " + NewDriver.DVConf.ID.ToString("00"));
                        retVal = 0;
                    }

                }

            return retVal;
        } //END Add Driver function.

        /// <summary>
        /// Object Initialization
        /// </summary>
        public int Initialize(bool InitialSet)
        {
            int k, retVal, numDrivers;
            
            retVal = -1;

            numDrivers = DriversHistConf.Count;

            IdtoPos = new int[100];

            //Init the array
            for (k = 0; k < 100; k++) IdtoPos[k] = -10;

            if (numDrivers > 0)
            {
                if (HistConf.MasterSrv.Enable)
                {
                    //Build the Driver and Initialize for MySQL
                    if (HistConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                    {
                        MasterMySQL = new HistMySQL.Hist_MySQL(HistConf.MasterSrv);
                        MasterMySQL.Initialize(InitialSet, DriversHistConf);

                        if (MasterMySQL.isInitialized)
                        {
                            Status.NewStat(StatT.Good, "Master Srv Initialized.");
                            isInitialized = true; retVal = 1;
                        }
                        else
                        {
                            Status.NewStat(StatT.Bad, "Master Srv Init Failed.");
                            isInitialized = false; retVal = -1;
                        }

                    } //END IF MySQL Initialization.
                } //Master Server Initialization.

                if (HistConf.BackupSrv.Enable)
                {
                    //Build the Driver and Initialize for MySQL.
                    if (HistConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                    {
                        BackupMySQL = new HistMySQL.Hist_MySQL(HistConf.BackupSrv);
                        BackupMySQL.Initialize(InitialSet, DriversHistConf);

                        if (BackupMySQL.isInitialized)
                        {
                            Status.NewStat(StatT.Good, "Backup Srv Initialized.");
                            isInitialized = true; retVal = 2;
                        }
                        else
                        {
                            Status.NewStat(StatT.Bad, "Backup Srv Init Failed.");
                            isInitialized = false; retVal = -2;
                        }
                    } //END IF MySQL Initialization.
                } //Backup Server Initialization.

                //Initialize the Worker
                if ((HistConf.MasterSrv.Enable) || (HistConf.BackupSrv.Enable))
                {
                    //A queue for each driver.
                    FIFO_Hist = new ConcurrentQueue<DriverComm.DataExtClass>[numDrivers];

                    k = 0; //Init the index.
                    foreach (DriverComm.DVConfDAConfClass DriverConfig in DriversHistConf)
                    {
                        if ((DriverConfig.DVConf.ID>0) && (DriverConfig.DVConf.ID < 100))
                        {
                            IdtoPos[DriverConfig.DVConf.ID] = k;
                            FIFO_Hist[k] = new ConcurrentQueue<DriverComm.DataExtClass>();
                            k++;
                        }
                        else
                        {
                            Status.NewStat(StatT.Bad, "Creating Queue Failed.");
                            isInitialized = false; retVal = -2;
                        }
                    }

                    if (k>0)
                    {
                        Status.NewStat(StatT.Good, "Created "+k.ToString()+" Queue for Hist." );
                    }

                    //Create the Background workers.
                    Worker = new BackgroundWorker();

                    //Attach the functions
                    Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
                    Worker.RunWorkerCompleted +=
                        new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
                    Worker.ProgressChanged +=
                        new ProgressChangedEventHandler(Worker_ProgressChanged);

                    //Properties
                    Worker.WorkerReportsProgress = true;
                    Worker.WorkerSupportsCancellation = true;
                }

            }

            return retVal;
        } //END Function Initialize

        /// <summary>
        /// Start Worker thread.
        /// </summary>
        public int StartWork()
        {
            if (!WorkersRuning)
            {
                Worker.RunWorkerAsync();
                WorkersRuning = true;
                return 0;
            }

            //Already something running, don't ask again.
            return -1;
               
        }

        /// <summary>
        /// Stop Worker thread, and close Opened Comms.
        /// </summary>
        public int StopWork()
        {
            if (WorkersRuning)
            {
                Worker.CancelAsync();
                return 0;
            }
            return -1;   
        }

        /// <summary>
        /// Add new Data Package to the FIFO.
        /// </summary>
        public int NewPackage(DriverComm.DataExtClass[] DataFromDv)
        {
            int retVal,k;

            retVal = -1000;

            if (isInitialized)
            {
                foreach (DriverComm.DataExtClass DataAreaDV in DataFromDv)
                {
                    if (DataAreaDV.AreaConf.ToHistorics)
                    {
                        if ((DataAreaDV.AreaConf.ID_Driver > 0) && (DataAreaDV.AreaConf.ID_Driver < 100))
                        {
                            k = IdtoPos[DataAreaDV.AreaConf.ID_Driver];
                            if (k < 0)
                            {
                                if (FIFO_Hist[k].Count > MaxQueueElements)
                                {
                                    Status.NewStat(StatT.Warning, "Queue is Full, a Package is dropped.");
                                }
                                else
                                {
                                    FIFO_Hist[k].Enqueue(DataAreaDV);
                                }
                            }
                            else
                            {
                                Status.NewStat(StatT.Warning, "This DV Id doesnt belows to this Historics.");
                                retVal = -5;
                            }

                        }
                        else
                        {
                            Status.NewStat(StatT.Warning, "New Package EnQueue Failed.");
                            retVal = -3;
                        }
                    } // END IF Historics is enabled for this DataArea.
                }
                if (retVal<-10) retVal = 0; //Everything is OK
            } else
            {
                //Not initialized
                retVal = -2;
            } //END IF isInitialized

            return retVal;
        }

        /// <summary>
        /// Close all connections after workers are stopped.
        /// </summary>
        public int CloseALL()
        {
            if (!WorkersRuning)
            {
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// This event handler is where the actual,
        /// potentially time-consuming work is done.
        /// Database Read -> Drivers Read and Write -> Database Write.
        /// </summary>
        private void Worker_DoWork(object sender,
            DoWorkEventArgs e)
        {
            int msLeft, cycleHist=500;
            DateTime initialTime, finalTime;
            long msCycle;

            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            //Name the Worker
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "CycleHist";

            //Initialize variables
            msCycle = 0;

            while (!e.Cancel)
            {
                //Get time
                initialTime = DateTime.Now;

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {

                    //Do Operations.
                    if (!DoCycle())
                    {
                        //Fatal Error, Cancel the Worker
                        e.Cancel = true;
                    }

                    //Sleep for the rest of the time cicle.
                    finalTime = DateTime.Now;
                    msCycle = ((finalTime.Ticks - initialTime.Ticks) / TimeSpan.TicksPerMillisecond);


                    if (msCycle < cycleHist)
                    {
                        msLeft = (int)(cycleHist - msCycle);
                        Thread.Sleep(msLeft);
                    }
                    else
                    {
                        //Cicle is taking too much time!!!
                        //Only report if its 15% higger than limit.
                        if (msCycle > (cycleHist * 1.15))

                    }

                }
            } //While not Cancelation

        }//END Function DoWork

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


        }

        ///<summary>
        /// This event handles the Worker updates.
        /// </summary>
        private void Worker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            WorkerProgress StatReport;

            //Get the report data
            StatReport = (WorkerProgress)e.UserState;

            if (StatReport.DVstat.StatInt != StatT.Undefined)
                StatDVMain.NewStat(StatReport.DVstat.StatInt, StatReport.DVstat.StatMSG);

            if (StatReport.DBstat.StatInt != StatT.Undefined)
                StatDBMain.NewStat(StatReport.DBstat.StatInt, StatReport.DBstat.StatMSG);

            if (StatReport.StatMsg.Length > 3)
                StatDBMain.NewStat(StatT.Good, StatReport.DriverID, StatReport.StatMsg);

            //Report the TimeLoop
            if (StatReport.DriverID < StatDVMain.ID_Time.Length)
                StatDVMain.ID_Time[StatReport.DriverID] = StatReport.LoopTime;
        }

        /// <summary>
        /// Check the Queue, and Sent data to the Database.
        ///  </summary>
        private int DoCycle()
        {
            int i, Qelements, retVal;
            DriverComm.DataExtClass QDataExt;
            DriverComm.DataExtClass[] ArrayDataExt;

            //Init
            retVal = 0;

            try { 
            if (isInitialized)
            foreach (ConcurrentQueue<DriverComm.DataExtClass> Queue in FIFO_Hist)
            {
                    if (!Queue.IsEmpty)
                    {
                        Qelements = Queue.Count;
                        

                        if (Qelements > ((int)(MaxQueueElements / 2)))
                        {
                            Qelements = (int)(MaxQueueElements / 2);
                            retVal = 1; //A Queue is getting full
                        }

                        ArrayDataExt = new DriverComm.DataExtClass[Qelements];

                        for (i = 0; i < Qelements; i++)
                        {
                            if(Queue.TryDequeue(out QDataExt))
                            {
                                ArrayDataExt[i] = QDataExt;
                            } else
                            {
                                ArrayDataExt[i] = null;
                            }
                        } //END For Qelements.

                        WriteDB(ArrayDataExt);

                    } //END If Queue is Empty.
                    
            } //END Foreach Queue FIFO

            } catch (Exception e)
            {
                Status.NewStat(StatT.Bad, e.Message);
                return -100;
            }

        }// END Fucntion DoCycle

        /// <summary>
        /// Write data into the Historics.
        /// <param name="DataExt">Data readed from devices to be writen into the database.</param> </summary>
        private int WriteDB(DriverComm.DataExtClass[] DataExt)
        {
            int retVal;
            retVal = -1;

            if (!isInitialized) return -2;

            if (HistConf.SrvEn == HistConfClass.SrvSelection.MasterOnly)
            {
                if (HistConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                    if (WriteMySQL(MasterMySQL, DataExt)) retVal = 0;

                if (retVal != 0) Status.NewStat(StatT.Warning, "Master Srv Write Failed.");
            }
            else if (HistConf.SrvEn == HistConfClass.SrvSelection.BackupOnly)
            {
                if (HistConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                    if (WriteMySQL(BackupMySQL, DataExt)) retVal = 0;

                if (retVal != 0) Status.NewStat(StatT.Warning, "Backup Srv Write Failed.");
            }
            else if (HistConf.SrvEn == HistConfClass.SrvSelection.BothSrv)
            {
                Task<bool> taskMaster = null, taskBackup = null;

                //Create the Tasks
                //https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx

                if (HistConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                    taskMaster = new Task<bool>(() => WriteMySQL(MasterMySQL, DataExt));

                if (HistConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                    taskBackup = new Task<bool>(() => WriteMySQL(BackupMySQL, DataExt));

                if ((taskMaster != null) && (taskBackup != null))
                {
                    //Start the Tasks
                    taskMaster.Start();
                    taskBackup.Start();

                    //Now Wait for them
                    taskMaster.Wait();
                    taskBackup.Wait();

                    //Check Result of Master
                    if (!taskMaster.Result)
                        Status.NewStat(StatT.Warning, "Master Srv Write Failed.");

                    //Check Result of Backup
                    if (!taskBackup.Result)
                        Status.NewStat(StatT.Warning, "Backup Srv Write Failed.");

                    //Only one ok return 1
                    if ((taskMaster.Result) || (taskBackup.Result)) retVal = 1;

                    //All ok return 0
                    if ((taskMaster.Result) && (taskBackup.Result)) retVal = 0;
                }
                else
                {
                    Status.NewStat(StatT.Bad, "Wrong Config Params.");
                    retVal = -10;
                }
            }//END Both Srv Selection

            return retVal;
        } //END Write Function

        /// <summary>
        /// Write data into the Historics (MySQL). </summary>
        private bool WriteMySQL(HistMySQL.Hist_MySQL HObject, DriverComm.DataExtClass[] DataWrite)
        {
            bool retVal = false;

            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "DBWrite";

            if (HObject.isInitialized)
            {
                if (DataWrite != null)
                {
                    retVal = HObject.Write(DataWrite);
                    Status.AddSummary(HObject.Status.GetSummary());
                }
            }

            return retVal;
        } //END WriteMySQL Function

        /// <summary>
        /// Clean database.
        /// </summary>
        public int CleanHist()
        {
            //Delete old DB registers
            int retVal;

            if (HistConf.SrvEn == HSrvSelection.MasterOnly)
            {
                //Clear OLD Master Server Registers.
                retVal = MasterMySQL.CleanHist();
                MasterWStat.statusMSG = MasterMySQL.status;

                if (retVal < 0)
                { GeneralStat.StatAllOK = true; }
                else
                { GeneralStat.statusMSG = "MasterHist: " + MasterWStat.statusMSG; }
            }
            else if (HistConf.SrvEn == HSrvSelection.BackupOnly)
            {
                //Clear OLD Master Server Registers.
                retVal = BackupMySQL.CleanHist();
                BackupWStat.statusMSG = BackupMySQL.status;

                if (retVal < 0)
                { GeneralStat.StatAllOK = true; }
                else
                { GeneralStat.statusMSG = "BackupHist: " + BackupWStat.statusMSG; }
            }
            else if (HistConf.SrvEn == HSrvSelection.BothSrv)
            {
                //Clear the old registers.
                retVal = MasterMySQL.CleanHist();
                if (retVal < 0) { MasterWStat.StatAllOK = true; }
                else { MasterWStat.StatAllOK = false; MasterWStat.statusMSG = MasterMySQL.status; }

                retVal = BackupMySQL.CleanHist();
                if (retVal < 0) { BackupWStat.StatAllOK = true; }
                else { BackupWStat.StatAllOK = false; BackupWStat.statusMSG = BackupMySQL.status; }

                //Only one ok return 1
                if ((MasterWStat.StatAllOK) || (BackupWStat.StatAllOK)) retVal = 1;

                //All ok return 0
                if ((MasterWStat.StatAllOK) && (BackupWStat.StatAllOK))
                { retVal = 0; GeneralStat.StatAllOK = true; }
                else
                {
                    GeneralStat.statusMSG = "MasterHist: " + MasterWStat.statusMSG +
                          Environment.NewLine + "BackupHist: " + BackupWStat.statusMSG;
                }
            }
            return 0;
        } //END Function CleanHist


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //dispose managed state (managed objects).
                    if (MasterMySQL != null) MasterMySQL.Dispose();
                    if (BackupMySQL != null) BackupMySQL.Dispose();
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.
                MasterWStat.DataWrite = null;
                BackupWStat.DataWrite = null;

                disposedValue = true;
            }
        }

        //  override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DB_Main() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            //uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
