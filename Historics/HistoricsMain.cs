using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DriverCommApp.Conf;
using DriverCommApp.DBMain;
using System.Collections.Concurrent;

namespace DriverCommApp.Historics
{

    class HistoricsMain : IDisposable
    {
        /// <summary>
        /// Historics Database Server Configuration Type Def.</summary>
        public enum HSrvSelection
        {
            None = 0,
            MasterOnly,
            BackupOnly,
            BothSrv
        }

        /// <summary>
        /// Historics Database Server Configuration Type Def.</summary>
        public struct HServerConf
        {
            public bool Enable;
            public float Rate;
            public string URL;
            public string Username;
            public string Passwd;
            public int Port;
            public DBConfig.DBServerProtocol Protocol;
            public DBConfig.DBServerType Type;
            public string DBname; //Database name
            public long HistLengh;
        }

        /// <summary>
        /// Database Configuration Type Def.</summary>
        public struct HDBConf
        {
            public HServerConf MasterServer;
            public HServerConf BackupServer;
            public HSrvSelection SrvEn;
        }

        /// <summary>
        /// Database Configuration.</summary>
        HDBConf HistConf;

        /// <summary>
        /// Driver Complete Configuration Type Def.</summary>
        public struct DrvHConf
        {
            public CommDriver.DriverGeneric.CConf DriverConf;
            public CommDriver.DriverGeneric.AreaData[] AreaConf;
        }

        /// <summary>
        /// Drivers Complete Configuration.</summary>
        List<DrvHConf> DriversHistConf;

        ConcurrentQueue<CommDriver.DriverGeneric.DataExt []>[] FIFO_Hist;


        /// <summary>
        /// Struct for multithread database write.</summary>
        public struct DBWriteStruct
        {
            public CommDriver.DriverGeneric.DataExt[] DataWrite;
            public int numDA;
            public bool StatAllOK;
            public string statusMSG;

            public void InitWrite(CommDriver.DriverGeneric.DataExt[] DataObj, int nDA)
            {
                numDA = nDA;
                DataWrite = DataObj;
            }
        }

        /// <summary>
        /// Master Write Container.</summary>
        DBWriteStruct MasterWStat;

        /// <summary>
        /// Backup Write Container.</summary>
        DBWriteStruct BackupWStat;

        /// <summary>
        /// Struct for multithread Database Read.</summary>
        public struct StatStruct
        {
            public bool StatAllOK;
            public string statusMSG;
        }
        /// <summary>
        /// Master Status.</summary>
        public StatStruct GeneralStat;

        /// <summary>
        /// MySQL Database Master.</summary>
        Hist_MySQL MasterMySQL;

        /// <summary>
        /// MySQL Database Backup.</summary>
        Hist_MySQL BackupMySQL;

        /// <summary>
        /// Flag for initialization.</summary>
        public bool isInitialized;

        /// <summary>
        /// Class Constructor.
        /// <param name="DBConfigFile">Configuration Object.</param> </summary>
        public HistoricsMain(DBConfig DBConfigFile)
        {
            //Create the list of drivers.
            DriversHistConf = new List<DrvHConf>(1);

            //Master Server
            HistConf.MasterServer.Enable = DBConfigFile.HistMaster.Enable;
            HistConf.MasterServer.DBname = DBConfigFile.HistMaster.DBname;
            HistConf.MasterServer.URL = DBConfigFile.HistMaster.URL;
            HistConf.MasterServer.Username = DBConfigFile.HistMaster.User1;
            HistConf.MasterServer.Passwd = DBConfigFile.HistMaster.Passw1;
            HistConf.MasterServer.Port = DBConfigFile.HistMaster.Port;
            HistConf.MasterServer.Protocol = DBConfigFile.HistMaster.Protocol;
            HistConf.MasterServer.Rate = DBConfigFile.HistMaster.Rate;
            HistConf.MasterServer.Type = DBConfigFile.HistMaster.Type;
            HistConf.MasterServer.HistLengh = DBConfigFile.HistMaster.HistLenght;

            //Backup Server
            HistConf.BackupServer.Enable = DBConfigFile.HistBackup.Enable;
            HistConf.BackupServer.DBname = DBConfigFile.HistBackup.DBname;
            HistConf.BackupServer.URL = DBConfigFile.HistBackup.URL;
            HistConf.BackupServer.Username = DBConfigFile.HistBackup.User1;
            HistConf.BackupServer.Passwd = DBConfigFile.HistBackup.Passw1;
            HistConf.BackupServer.Port = DBConfigFile.HistBackup.Port;
            HistConf.BackupServer.Protocol = DBConfigFile.HistBackup.Protocol;
            HistConf.BackupServer.Rate = DBConfigFile.HistBackup.Rate;
            HistConf.BackupServer.Type = DBConfigFile.HistBackup.Type;
            HistConf.BackupServer.HistLengh = DBConfigFile.HistBackup.HistLenght;

            //Check the server selection
            HistConf.SrvEn = HSrvSelection.None;
            if (HistConf.MasterServer.Enable && HistConf.BackupServer.Enable)
                HistConf.SrvEn = HSrvSelection.BothSrv;
            if (HistConf.MasterServer.Enable && !HistConf.BackupServer.Enable)
                HistConf.SrvEn = HSrvSelection.MasterOnly;
            if (!HistConf.MasterServer.Enable && HistConf.BackupServer.Enable)
                HistConf.SrvEn = HSrvSelection.BackupOnly;

            isInitialized = false;
        } //END Class Constructor

        /// <summary>
        /// Add a driver to be managed by this Database.
        /// <param name="DriverConf">Configuration Object for driver config.</param> 
        /// <param name="DAreaConf">Configuration Object for Data Area config.</param> </summary>
        public int addDriver(CommDriver.DriverGeneric.CConf DriverConf, CommDriver.DriverGeneric.AreaData[] DAreaConf)
        {
            int retVal;
            DrvHConf NewDriver;

            if (DAreaConf.Length > 0)
            {
                NewDriver.DriverConf = DriverConf;
                NewDriver.AreaConf = DAreaConf;
                DriversHistConf.Add(NewDriver);

                //Adding a new driver while initialized will require new initzialization.
                if (isInitialized)
                {
                    isInitialized = false;
                    retVal = 1;
                }
                else
                {
                    retVal = 0;
                }

            }
            else
            {
                retVal = -1;
            }

            return retVal;
        } //END Add Driver function.

        /// <summary>
        /// Object Initialization
        /// </summary>
        public int Initialize(bool InitialSet)
        {
            int retVal, numDrivers;

            retVal = -1;

            numDrivers = DriversHistConf.Count;

            if (HistConf.MasterServer.Enable && (numDrivers > 0))
            {
                //A queue for each driver.
                FIFO_Hist = new ConcurrentQueue<CommDriver.DriverGeneric.DataExt[]>[numDrivers];

                //Build the Driver and Initialize for MySQL
                if (HistConf.MasterServer.Type == DBConfig.DBServerType.MySQL)
                {
                    MasterMySQL = new Hist_MySQL(HistConf.MasterServer);
                    MasterMySQL.Initialize(InitialSet, DriversHistConf);

                    if (MasterMySQL.isInitialized) { isInitialized = true; retVal = 1; }
                    else { isInitialized = false; retVal = -1; }

                } //END IF MySQL Initialization.
            } //Master Server Initialization.

            if (HistConf.BackupServer.Enable)
            {
                //Build the Driver and Initialize for MySQL
                if (HistConf.BackupServer.Type == DBConfig.DBServerType.MySQL)
                {
                    BackupMySQL = new Hist_MySQL(HistConf.BackupServer);
                    BackupMySQL.Initialize(InitialSet, DriversHistConf);

                    if (BackupMySQL.isInitialized) { isInitialized = true; retVal = 2; }
                    else { isInitialized = false; retVal = -2; }
                } //END IF MySQL Initialization.
            } //Backup Server Initialization.

            return retVal;
        } //END Function Initialize

        /// <summary>
        /// Start Worker thread.
        /// </summary>
        public int StartWork()
        {

        }

        /// <summary>
        /// Add new Data Package to the FIFO.
        /// </summary>
        public int NewPackage()
        {

        }

        /// <summary>
        /// Stop Worker thread, and close Opened Comms.
        /// </summary>
        public int CloseALL()
        {

        }

        /// <summary>
        /// Write to database.
        /// </summary>
        private int Write(CommDriver.DriverGeneric.DataExt[] DataExt, int numDA)
        {
            int retVal;
            retVal = -1;

            GeneralStat.StatAllOK = false; GeneralStat.statusMSG = "";

            if (!isInitialized) return -2;

            if (HistConf.SrvEn == HSrvSelection.MasterOnly)
            {
                //Init the Master Obj
                MasterWStat.InitWrite(DataExt, numDA);
                WriteMaster();

                if (MasterWStat.StatAllOK)
                { retVal = 0; GeneralStat.StatAllOK = true; }
                else
                { GeneralStat.statusMSG = "MasterHist: " + MasterWStat.statusMSG; }
            }
            else if (HistConf.SrvEn == HSrvSelection.BackupOnly)
            {
                //Init the Backup Obj
                BackupWStat.InitWrite(DataExt, numDA);
                WriteBackup();

                if (BackupWStat.StatAllOK)
                    { retVal = 0; GeneralStat.StatAllOK = true; }
                else
                { GeneralStat.statusMSG = "BackupHist: " + BackupWStat.statusMSG; }
            }
            else if (HistConf.SrvEn == HSrvSelection.BothSrv)
            {
                //Init the Backup Obj
                BackupWStat.InitWrite(DataExt, numDA);

                //Create the threads
                Thread MasterServer = new Thread(new ThreadStart(WriteMaster));
                Thread BackupServer = new Thread(new ThreadStart(WriteBackup));

                //Start the Threads
                MasterServer.Start();
                BackupServer.Start();

                //Now Wait for them
                MasterServer.Join();
                BackupServer.Join();

                //Only one ok return 1
                if ( (MasterWStat.StatAllOK) || (BackupWStat.StatAllOK) ) retVal = 1;

                //All ok return 0
                if ((MasterWStat.StatAllOK) && (BackupWStat.StatAllOK))
                    { retVal = 0; GeneralStat.StatAllOK = true; }
                else
                { GeneralStat.statusMSG = "MasterHist: " + MasterWStat.statusMSG + 
                        Environment.NewLine + "BackupHist: " + BackupWStat.statusMSG; }
            }

            return retVal;

        } //END Function Write

        /// <summary>
        /// Write data into the database (MasterServer). </summary>
        private void WriteMaster()
        {
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "HistMaster";

            if (HistConf.MasterServer.Type == DBConfig.DBServerType.MySQL)
            {
                MasterWStat.StatAllOK = MasterMySQL.Write(MasterWStat.DataWrite);
                MasterWStat.statusMSG = MasterMySQL.status;
            }
        } //END WriteMaster Function

        /// <summary>
        /// Write data into the database (BackupServer). </summary>
        private void WriteBackup()
        {
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "HistBackup";

            if (HistConf.BackupServer.Type == DBConfig.DBServerType.MySQL)
            {
                BackupWStat.StatAllOK = BackupMySQL.Write(BackupWStat.DataWrite);
                BackupWStat.statusMSG = BackupMySQL.status;
            }
        } // END WriteBackup Function


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

                if (retVal<0)
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
                {  GeneralStat.StatAllOK = true; }
                else
                { GeneralStat.statusMSG = "BackupHist: " + BackupWStat.statusMSG; }
            }
            else if (HistConf.SrvEn == HSrvSelection.BothSrv)
            {
                //Clear the old registers.
                retVal = MasterMySQL.CleanHist();
                if (retVal<0) { MasterWStat.StatAllOK = true; }
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
