using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//This APP Namespace
using DriverCommApp.Conf;
using static DriverCommApp.Database.DB_Functions;

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
        DBConf DatabaseConf;

        /// <summary>
        /// Drivers Complete Configuration.</summary>
        List<DrvConf> DriversConf;

        /// <summary>
        /// Master Write Status.</summary>
        DBStructData MasterWData;

        /// <summary>
        /// Backup Write Status.</summary>
        DBStructData BackupWData;

        /// <summary>
        /// Backup Write Status.</summary>
        DBStatStruct MasterWStat;

        /// <summary>
        /// Master Read Status.</summary>
        DBStatStruct BackupWStat;

        /// <summary>
        /// Master Read Status.</summary>
        DBStatStruct MasterRStat;

        /// <summary>
        /// Backup Read Status.</summary>
        DBStatStruct BackupRStat;

        /// <summary>
        /// Public var for the amount of variables.</summary>
        public DriverConfig.nVars NumVars;

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
            //Main Configuration
            isInitialized = false;

            //Create the list of drivers.
            DriversConf = new List<DrvConf>(1);

            //Master Server Configuration
            DatabaseConf.MasterServer.URL = DBConfigFile.MainServer.URL;
            DatabaseConf.MasterServer.type = DBConfigFile.MainServer.Type;
            DatabaseConf.MasterServer.Protocol = DBConfigFile.MainServer.Protocol;
            DatabaseConf.MasterServer.Port = DBConfigFile.MainServer.Port;
            DatabaseConf.MasterServer.Username = DBConfigFile.MainServer.User1;
            DatabaseConf.MasterServer.Passwd = DBConfigFile.MainServer.Passw1;
            DatabaseConf.MasterServer.Enable = DBConfigFile.MainServer.Enable;
            DatabaseConf.MasterServer.DBname = DBConfigFile.MainServer.DBname;

            //Backup Server Configuration
            DatabaseConf.BackupServer.URL = DBConfigFile.BackupServer.URL;
            DatabaseConf.BackupServer.type = DBConfigFile.BackupServer.Type;
            DatabaseConf.BackupServer.Protocol = DBConfigFile.BackupServer.Protocol;
            DatabaseConf.BackupServer.Port = DBConfigFile.BackupServer.Port;
            DatabaseConf.BackupServer.Username = DBConfigFile.BackupServer.User1;
            DatabaseConf.BackupServer.Passwd = DBConfigFile.BackupServer.Passw1;
            DatabaseConf.BackupServer.Enable = DBConfigFile.BackupServer.Enable;
            DatabaseConf.BackupServer.DBname = DBConfigFile.BackupServer.DBname;

            //Check the server selection
            DatabaseConf.SrvEn = SrvSelection.None;
            if (DatabaseConf.MasterServer.Enable && DatabaseConf.BackupServer.Enable)
                DatabaseConf.SrvEn = SrvSelection.BothSrv;
            if (DatabaseConf.MasterServer.Enable && !DatabaseConf.BackupServer.Enable)
                DatabaseConf.SrvEn = SrvSelection.MasterOnly;
            if (!DatabaseConf.MasterServer.Enable && DatabaseConf.BackupServer.Enable)
                DatabaseConf.SrvEn = SrvSelection.BackupOnly;

        } //END Class Constructor

        /// <summary>
        /// Add a driver to be managed by this Database.
        /// <param name="DriverConf">Configuration Object for driver config.</param> 
        /// <param name="DAreaConf">Configuration Object for Data Area config.</param> </summary>
        public int addDriver(DriverComm.DriverFunctions.CConf DriverConf, DriverComm.DriverFunctions.AreaData[] DAreaConf)
        {
            int retVal;
            DrvConf NewDriver;

            if (DAreaConf.Length > 0)
            {
                NewDriver.DriverConf = DriverConf;
                NewDriver.AreaConf = DAreaConf;
                DriversConf.Add(NewDriver);

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
        /// Initialize the Database conectivity. Requires that drives are added before Initializing.
        /// <param name="InitialSet">Do an initial set of the Database tables.</param>  </summary>
        public int Initialize(bool InitialSet)
        {
            int retVal;

            retVal = -1;

            if (DatabaseConf.MasterServer.Enable)
            {
                //Build the Driver and Initialize for MySQL
                if (DatabaseConf.MasterServer.type == DBConfig.DBServerType.MySQL)
                {
                    MasterMySQL = new DBMySQL.DB_MySQL(DatabaseConf.MasterServer);
                    MasterMySQL.Initialize(DriversConf, InitialSet);

                    if (MasterMySQL.isInitialized) { isInitialized = true; retVal = 1; }
                    else { isInitialized = false; retVal = -1; }

                } //END IF MySQL Initialization.
            } //Master Server Initialization.

            if (DatabaseConf.BackupServer.Enable)
            {
                //Build the Driver and Initialize for MySQL
                if (DatabaseConf.BackupServer.type == DBConfig.DBServerType.MySQL)
                {
                    BackupMySQL = new DBMySQL.DB_MySQL(DatabaseConf.BackupServer);
                    BackupMySQL.Initialize(DriversConf, InitialSet);

                    if (BackupMySQL.isInitialized) { isInitialized = true; retVal = 2; }
                    else { isInitialized = false; retVal = -2; }
                } //END IF MySQL Initialization.
            } //Backup Server Initialization.

            //Count the enabled number of tags
            CountVars();

            return retVal;
        } // END Initialization Function

        /// <summary>
        /// Write data into the database.
        /// <param name="DataExt">Data readed from devices to be writen into the database.</param> 
        /// <param name="numDA">Number of Data areas in the object.</param> </summary>
        public int WriteDB(DriverComm.DriverFunctions.DataExt[] DataExt, int numDA)
        {
            int retVal;
            retVal = -1;

            if (!isInitialized) return -2;

            if (DatabaseConf.SrvEn == SrvSelection.MasterOnly)
            {
                //Init the Master Obj
                MasterWData.InitWrite(DataExt, numDA);
                WriteMaster();
                if (MasterWStat.StatAllOK) retVal = 0;
            }
            else if (DatabaseConf.SrvEn == SrvSelection.BackupOnly)
            {
                //Init the Backup Obj
                BackupWData.InitWrite(DataExt, numDA);
                WriteBackup();
                if (BackupWStat.StatAllOK) retVal = 0;
            }
            else if (DatabaseConf.SrvEn == SrvSelection.BothSrv)
            {
                //Init the Backup Obj
                MasterWData.InitWrite(DataExt, numDA);
                BackupWData.InitWrite(DataExt, numDA);

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
                if ((MasterWStat.StatAllOK) || (BackupWStat.StatAllOK)) retVal = 1;

                //All ok return 0
                if ((MasterWStat.StatAllOK) && (BackupWStat.StatAllOK)) retVal = 0;
            }

            return retVal;
        } //END Write Function

        /// <summary>
        /// Write data into the database (MasterServer). </summary>
        private void WriteMaster()
        {
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "DBMasterWrite";

            if (DatabaseConf.MasterServer.type == DBConfig.DBServerType.MySQL)
            {
                MasterWStat.StatAllOK = MasterMySQL.Write(MasterWData);
                MasterWStat.statusMSG = MasterMySQL.status;
            }
        } //END WriteMaster Function

        /// <summary>
        /// Write data into the database (BackupServer). </summary>
        private void WriteBackup()
        {
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "DBBackupWrite";

            if (DatabaseConf.BackupServer.type == DBConfig.DBServerType.MySQL)
            {
                BackupWStat.StatAllOK = BackupMySQL.Write(BackupWData);
                BackupWStat.statusMSG = BackupMySQL.status;
            }
        } // END WriteBackup Function

        /// <summary>
        /// Read data from the database.
        /// <param name="DataExt">Data readed from the database to be writen into the devices.</param> 
        /// <param name="numDA">Number of Data areas in the object.</param> </summary>
        public int ReadDB(ref DriverComm.DriverFunctions.DataExt[] DataExt, int numDA)
        {
            int retVal;
            DBStructData DatatoRead;
            retVal = -1;

            DatatoRead.DataDV = DataExt;
            DatatoRead.numDA = numDA;

            if (!isInitialized) return -2;

            if (DatabaseConf.MasterServer.Enable)
            {
                if (DatabaseConf.MasterServer.type == DBConfig.DBServerType.MySQL)
                {
                    if (MasterMySQL.Read(ref DatatoRead))
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
            else if ((DatabaseConf.BackupServer.Enable) && (retVal < 0))
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
                foreach (DriverComm.DriverFunctions.AreaData DeviceDA in DvDevice.AreaConf) { 

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
