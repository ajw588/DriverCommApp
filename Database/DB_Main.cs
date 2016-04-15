using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//This APP Namespace
using DriverCommApp.Conf.DB;
using static DriverCommApp.Database.DB_Functions;
using StatT = DriverCommApp.Stat.StatReport.StatT;

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
        List<DriverComm.DVConfAreaConfClass> DriversConf;

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
            DriversConf = new List<DriverComm.DVConfAreaConfClass>(1);

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
            Status = new Stat.StatReport((int) Stat.StatReport.IDdef.DBall, FileLog: true);

        } //END Class Constructor

        /// <summary>
        /// Add a driver to be managed by this Database.
        /// <param name="DriverConf">Configuration Object for driver config.</param> 
        /// <param name="DAreaConf">Configuration Object for Data Area config.</param> </summary>
        public int addDriver(DriverComm.DVConfClass DriverConf, DriverComm.AreaDataConfClass[] DAreaConf)
        {
            int retVal;
            DriverComm.DVConfAreaConfClass NewDriver;

            retVal = -1;

            if ((DriverConf != null) && (DAreaConf != null))
                if (DAreaConf.Length > 0)
                {
                    NewDriver = new DriverComm.DVConfAreaConfClass(DriverConf, DAreaConf);

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
                        Status.NewStat(StatT.Good, "DV Added: "+ NewDriver.DVConf.ID.ToString("00"));
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
        public int WriteDB(DriverComm.DataExtClass[] DataExt)
        {
            int retVal;
            retVal = -1;

            if (!isInitialized) return -2;

            if (DatabaseConf.SrvEn == DBConfClass.SrvSelection.MasterOnly)
            {
                if (DatabaseConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                    if (WriteMySQL(MasterMySQL, DataExt)) retVal = 0;

                if (retVal!=0) Status.NewStat(StatT.Bad, "Master Srv Write Failed.");
            }
            else if (DatabaseConf.SrvEn == DBConfClass.SrvSelection.BackupOnly)
            {
                if (DatabaseConf.BackupSrv.Type == DBConfig.DBServerType.MySQL)
                    if (WriteMySQL(BackupMySQL, DataExt)) retVal = 0;

                if (retVal != 0) Status.NewStat(StatT.Bad, "Backup Srv Write Failed.");
            }
            else if (DatabaseConf.SrvEn == DBConfClass.SrvSelection.BothSrv)
            {

                //Create the Tasks
                //https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx



                //Start the Tasks

                //Now Wait for them


                //Only one ok return 1
                if ((MasterWStat.StatAllOK) || (BackupWStat.StatAllOK)) retVal = 1;

                //All ok return 0
                if ((MasterWStat.StatAllOK) && (BackupWStat.StatAllOK)) retVal = 0;
            }

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
                    retVal=DBObject.Write(DataWrite);
                    Status.AddSummary(DBObject.Status.GetSummary());
                }
            }

            return retVal;
        } //END WriteMySQL Function


        /// <summary>
        /// Read data from the database.
        /// <param name="DataExt">Data readed from the database to be writen into the devices.</param> 
        /// <param name="numDA">Number of Data areas in the object.</param> </summary>
        public int ReadDB(ref DriverComm.DataExtClass[] DataExt)
        {
            int retVal;
            retVal = -1;

            if (!isInitialized) return -2;

            if (DatabaseConf.MasterSrv.Enable)
            {
                if (DatabaseConf.MasterSrv.Type == DBConfig.DBServerType.MySQL)
                {
                    if (MasterMySQL.Read(DataExt))
                    {
                        retVal = 0;
                        MasterRStat.StatAllOK = true;
                    }
                    else
                    {
                        MasterRStat.StatAllOK = false;
                        MasterRStat.statusMSG = MasterMySQL.status;
                    }
                }
            }
            else if ((DatabaseConf.BackupSrv.Enable) && (retVal < 0))
            {
                //Read always reads from master. 
                //Only in case reading from master fails, then it reads from backup.
                if (BackupMySQL.Read(ref DatatoRead))
                {
                    retVal = 1;
                    BackupRStat.StatAllOK = true;
                }
                else
                {
                    BackupRStat.StatAllOK = false;
                    BackupRStat.statusMSG = MasterMySQL.status;
                }
            }

            return retVal;
        } //END Read Function

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

            foreach (DrvConf DvDevice in DriversConf)
                foreach (DriverComm.DriverFunctions.AreaData DeviceDA in DvDevice.AreaConf)
                {

                    NumVars.nDA++; //Count the Data Areas (total).

                    if (DeviceDA.Enable)
                    {
                        NumVars.nDAen++; //Count the enabled Data Areas.

                        switch (DeviceDA.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                NumVars.nBool += DeviceDA.Amount;
                                break;
                            case DriverConfig.DatType.Byte:
                                NumVars.nByte += DeviceDA.Amount;
                                break;
                            case DriverConfig.DatType.Word:
                                NumVars.nWord += DeviceDA.Amount;
                                break;
                            case DriverConfig.DatType.DWord:
                                NumVars.nDWord += DeviceDA.Amount;
                                break;
                            case DriverConfig.DatType.sDWord:
                                NumVars.nsDWord += DeviceDA.Amount;
                                break;
                            case DriverConfig.DatType.Real:
                                NumVars.nReal += DeviceDA.Amount;
                                break;
                            case DriverConfig.DatType.String:
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
