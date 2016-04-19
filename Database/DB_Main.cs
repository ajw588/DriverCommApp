using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

//This APP Namespace
using DriverCommApp.Conf.DB;
using static DriverCommApp.Database.DB_Functions;
using StatT = DriverCommApp.Stat.StatReport.StatT;
using DatType = DriverCommApp.Conf.DV.DriverConfig.DatType;


namespace DriverCommApp.Database
{
    /// <summary>
    /// Main Class to handle all the database iteractions.
    /// aChild classes will manage the database types, 
    /// and contains the special details for management.</summary>
    class DB_Main : IDisposable
    {
        /// <summary>
        /// Database Configuration.</summary>
        DBConfClass DatabaseConf;

        /// <summary>
        /// Drivers Complete Configuration.</summary>
        List<DriverComm.DVConfDAConfClass> DriversConf;

        /// <summary>
        /// Status Report for DB.</summary>
        public Stat.StatReport Status;

        /// <summary>
        /// Public var for the amount of variables.</summary>
        public Conf.DV.DriverConfig.nVars NumVars;

        /// <summary>
        /// MySQL Database Master.</summary>
        DBMySQL.DB_MySQL MasterMySQL;

        /// <summary>
        /// MySQL Database Backup.</summary>
        DBMySQL.DB_MySQL BackupMySQL;

        /// <summary>
        /// Flag for initialization.</summary>
        public bool isInitialized;

        /// <summary>
        /// Class Constructor.
        /// <param name="DBConfigFile">Configuration Object.</param> </summary>
        public DB_Main(DBConfig DBConfigFile)
        {
            DBConfClass.ServerConf Master, Backup;

            //Main Configuration
            isInitialized = false;

            //Create the list of drivers.
            DriversConf = new List<DriverComm.DVConfDAConfClass>(1);

            //Master Server Configuration
            Master.URL = DBConfigFile.MainServer.URL;
            Master.Type = DBConfigFile.MainServer.Type;
            Master.Protocol = DBConfigFile.MainServer.Protocol;
            Master.Port = DBConfigFile.MainServer.Port;
            Master.Username = DBConfigFile.MainServer.User1;
            Master.Passwd = DBConfigFile.MainServer.Passw1;
            Master.Enable = DBConfigFile.MainServer.Enable;
            Master.DBname = DBConfigFile.MainServer.DBname;

            //Backup Server Configuration
            Backup.URL = DBConfigFile.BackupServer.URL;
            Backup.Type = DBConfigFile.BackupServer.Type;
            Backup.Protocol = DBConfigFile.BackupServer.Protocol;
            Backup.Port = DBConfigFile.BackupServer.Port;
            Backup.Username = DBConfigFile.BackupServer.User1;
            Backup.Passwd = DBConfigFile.BackupServer.Passw1;
            Backup.Enable = DBConfigFile.BackupServer.Enable;
            Backup.DBname = DBConfigFile.BackupServer.DBname;

            //Create the Conf Object
            DatabaseConf = new DBConfClass(Master, Backup);

            //The Status Object
            Status = new Stat.StatReport((int)Stat.StatReport.IDdef.DBall, FileLog: true);

        } //END Class Constructor

        /// <summary>
        /// Add a driver to be managed by this Database.
        /// <param name="DriverConf">Configuration Object for driver config.</param> 
        /// <param name="DAreaConf">Configuration Object for Data Area config.</param> </summary>
        public int addDriver(DriverComm.DVConfClass DriverConf, DriverComm.DAConfClass[] DAreaConf)
        {
            int retVal;
            DriverComm.DVConfDAConfClass NewDriver;

            retVal = -1;

            if ((DriverConf != null) && (DAreaConf != null))
                if (DAreaConf.Length > 0)
                {
                    NewDriver = new DriverComm.DVConfDAConfClass(DriverConf, DAreaConf);

                    DriversConf.Add(NewDriver);

                    //Adding a new driver while initialized will require new initzialization.
                    if (isInitialized)
                    {
                        Status.NewStat(StatT.Warning, "DB Object Requires to Re-Initialize");
                        isInitialized = false;
                        retVal = 1;
                    }
                    else
                    {
                        Status.NewStat(StatT.Good, "DV Added: " + NewDriver.DVConf.ID.ToString("00"));
                        retVal = 0;
                    }

                }
                else
                {
                    retVal = -2;
                    Status.NewStat(StatT.Bad, "Internal Data Corruption.");
                }

            return retVal;
        } //END Add Driver function.

        /// <summary>
        /// Initialize the Database conectivity. Requires that drives are added before Initializing.
        /// <param name="InitialSet">Do an initial set of the Database tables.</param>  </summary>
        public int Initialize(bool InitialSet)
        {
            int retVal;

            retVal = -1;

            if (DatabaseConf.MasterSrv.Enable)
            {
                //Build the Driver and Initialize for MySQL
                if (DatabaseConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                {
                    MasterMySQL = new DBMySQL.DB_MySQL(DatabaseConf.MasterSrv, true);
                    MasterMySQL.Initialize(DriversConf, InitialSet);

                    Status.AddSummary(MasterMySQL.Status.GetSummary());

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

            if (DatabaseConf.BackupSrv.Enable)
            {
                //Build the Driver and Initialize for MySQL
                if (DatabaseConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                {
                    BackupMySQL = new DBMySQL.DB_MySQL(DatabaseConf.BackupSrv, false);
                    BackupMySQL.Initialize(DriversConf, InitialSet);

                    Status.AddSummary(BackupMySQL.Status.GetSummary());

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

            //Count the enabled number of tags
            CountVars();

            return retVal;
        } // END Initialization Function

        /// <summary>
        /// Write data into the database.
        /// <param name="DataExt">Data readed from devices to be writen into the database.</param> </summary>
        public bool WriteDB(DriverComm.DataExtClass[] DataExt)
        {
            bool retVal = false;

            if (!isInitialized) return false;

            if (DatabaseConf.SrvEn == DBConfClass.SrvSelection.MasterOnly)
            {
                if (DatabaseConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                    retVal = WriteMySQL(MasterMySQL, DataExt);

                if (!retVal) Status.NewStat(StatT.Warning, "Master Srv Write Failed.");
            }
            else if (DatabaseConf.SrvEn == DBConfClass.SrvSelection.BackupOnly)
            {
                if (DatabaseConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                    retVal = WriteMySQL(BackupMySQL, DataExt);

                if (!retVal) Status.NewStat(StatT.Warning, "Backup Srv Write Failed.");
            }
            else if (DatabaseConf.SrvEn == DBConfClass.SrvSelection.BothSrv)
            {
                Task<bool> taskMaster = null, taskBackup = null;

                //Create the Tasks
                //https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx

                if (DatabaseConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                    taskMaster = new Task<bool>(() => WriteMySQL(MasterMySQL, DataExt));

                if (DatabaseConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
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

                    //At least one ok return true
                    if ((taskMaster.Result) || (taskBackup.Result)) retVal = true;

                }
                else
                {
                    Status.NewStat(StatT.Bad, "Wrong Config Params.");
                    retVal = false;
                }
            }//END Both Srv Selection

            return retVal;
        } //END Write Function

        /// <summary>
        /// Write data into the database (MySQL). </summary>
        private bool WriteMySQL(DBMySQL.DB_MySQL DBObject, DriverComm.DataExtClass[] DataWrite)
        {
            bool retVal = false;

            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "DBWrite";

            if (DBObject.isInitialized)
            {
                if (DataWrite != null)
                {
                    retVal = DBObject.Write(DataWrite);
                    Status.AddSummary(DBObject.Status.GetSummary());
                }
            }

            return retVal;
        } //END WriteMySQL Function


        /// <summary>
        /// Read data from the database.
        /// <param name="DataExt">Data readed from the database to be writen into the devices.</param>  </summary>
        public bool ReadDB(DriverComm.DataExtClass[] DataExt)
        {
            bool retVal = false;

            if (!isInitialized) return false;

            //Read first reads from master if Enabled. 
            if (DatabaseConf.MasterSrv.Enable)
            {
                if (DatabaseConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                {
                    retVal = MasterMySQL.Read(DataExt);

                    if (!retVal) Status.NewStat(StatT.Warning, "Master Srv Read Failed.");
                }
            } //END IF Master is Enabled

            //Only in case reading from Master fails, then it reads from backup.
            if ((DatabaseConf.BackupSrv.Enable) && (!retVal))
            {
                if (DatabaseConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                {
                    retVal = BackupMySQL.Read(DataExt);

                    if (!retVal) Status.NewStat(StatT.Warning, "Backup Srv Read Failed.");
                }
            } //IF needs to read from Backup

            return retVal;
        } //END Read Function

        /*
         //*****************************************************
         // Deprecather Method, Now just access the Status Object.
         //*******************************************************

        /// <summary>
        /// Get Status of the Database conectivity.
        /// <param name="messages">Data readed from the database to be writen into the devices.</param> </summary>
        public int GetStatus(out string messages)
        {
            int retVal;
            retVal = 0;
            messages = String.Empty;

            if (!MasterWStat.StatAllOK && DatabaseConf.MasterServer.Enable) retVal += -20;
            if (!BackupWStat.StatAllOK && DatabaseConf.BackupServer.Enable) retVal += -30;

            if (!MasterRStat.StatAllOK && DatabaseConf.BackupServer.Enable)
            {
                retVal += -200;
                if (!BackupRStat.StatAllOK && DatabaseConf.BackupServer.Enable) retVal += -300;
            }


            // Message ConCat
            if (DatabaseConf.MasterServer.Enable)
            {
                messages = messages + "MasterDBWrite= " + MasterWStat.statusMSG + "; MasterDBRead= " + MasterRStat.statusMSG + " ;";
            }
            else
            {
                messages = " No Master DB enabled; ";
            }
            if (DatabaseConf.BackupServer.Enable)
            {
                messages = "BackupDBWrite= " + BackupWStat.statusMSG + "; BackupDBRead= " + BackupRStat.statusMSG + " ;";
            }

            return retVal;
        } */


        /// <summary>
        /// Close all connections after workers are stopped.
        /// </summary>
        public int CloseALL()
        {

            if (MasterMySQL != null) MasterMySQL.Disconnect();
            if (BackupMySQL != null) BackupMySQL.Disconnect();

            Status.FlushLog();
            return -1;
        }

        /// <summary>
        /// Count the number and type of vars in the Var Tree.
        /// </summary>
        private bool CountVars()
        {

            //Initialize the counters
            NumVars.nBool = 0; NumVars.nByte = 0;
            NumVars.nWord = 0; NumVars.nDWord = 0;
            NumVars.nReal = 0; NumVars.nsDWord = 0; NumVars.nString = 0;
            NumVars.nDA = 0; NumVars.nDAen = 0;


            NumVars.nDrivers = DriversConf.Count;

            foreach (DriverComm.DVConfDAConfClass DvDevice in DriversConf)
                foreach (DriverComm.DAConfClass DeviceDA in DvDevice.DAConf)
                {

                    NumVars.nDA++; //Count the Data Areas (total).

                    if (DeviceDA.Enable)
                    {
                        NumVars.nDAen++; //Count the enabled Data Areas.

                        switch (DeviceDA.dataType)
                        {
                            case DatType.Bool:
                                NumVars.nBool += DeviceDA.Amount;
                                break;
                            case DatType.Byte:
                                NumVars.nByte += DeviceDA.Amount;
                                break;
                            case DatType.Word:
                                NumVars.nWord += DeviceDA.Amount;
                                break;
                            case DatType.DWord:
                                NumVars.nDWord += DeviceDA.Amount;
                                break;
                            case DatType.sDWord:
                                NumVars.nsDWord += DeviceDA.Amount;
                                break;
                            case DatType.Real:
                                NumVars.nReal += DeviceDA.Amount;
                                break;
                            case DatType.String:
                                NumVars.nString += DeviceDA.Amount;
                                break;
                        }
                    }// Enabled Data - Areas

                } //END Foreach

            //Total enabled variables.
            NumVars.nTVars = NumVars.nBool + NumVars.nByte + NumVars.nWord +
                NumVars.nDWord + NumVars.nsDWord + NumVars.nReal + NumVars.nString;

            return true;
        } //END Function to count the variables

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
                DriversConf = null;
                Status = null;

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
