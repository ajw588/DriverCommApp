using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;

//This App Namespace
using DriverCommApp.Conf;
using static DriverCommApp.Historics.Hist_Functions;

namespace DriverCommApp.Historics
{
    class Hist_MySQL : IDisposable
    {
        /// <summary>
        /// Database Server Configuration.</summary>
        HServerConf SrvConf;

        /// <summary>
        /// Drivers Configuration.</summary>
        List<DrvHConf> DrvConfig;

        /// <summary>
        /// Database MySQL Connection Object.</summary>
        MySqlConnection conn;

        /// <summary>
        /// Exception info from the MySQL library.</summary>
        public string status;

        /// <summary>
        /// Initialization flag.</summary>
        public bool isInitialized;

        /// <summary>
        /// Class Constructor.
        /// <param name="ServerConf">Server Configuration Parameters Object.</param> </summary>
        public Hist_MySQL(HServerConf ServerConf)
        {
            //Keep the Server Configuration
            SrvConf = ServerConf;
            isInitialized = false;
        }

        public void Initialize(bool InitialSet, List<DrvHConf> DriversConf)
        {
            string myConnectionString;
            int retVal = 0;

            //Reset the flag
            isInitialized = false;

            //Keep a copy of the Drivers Configuration.
            DrvConfig = DriversConf;

            //myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=12345;database=test;";
            myConnectionString = "SERVER=" + SrvConf.URL + ";" + "PORT=" + SrvConf.Port.ToString() + ";" +
                "PROTOCOL=" + SrvConf.Protocol.ToString().ToLower() + ";" + "DATABASE=" + SrvConf.DBname + ";" +
                "UID=" + SrvConf.Username + ";" + "PASSWORD=" + SrvConf.Passwd + ";"+
                "ConnectionTimeout=5; DefaultCommandTimeout=5;Keepalive=3" + ";";

            conn = new MySqlConnection(myConnectionString);

            if (Connect())
            {
                Disconnect();

                //Database tables initialization.
                if (InitialSet) retVal = InitDB(DriversConf);

                if (retVal == 0) isInitialized = true;
            }

        } //END Func Initialize

        /// <summary>
        /// Initialize the Database.
        /// </summary>
        private int InitDB(List<DrvHConf> DriversConf)
        {
            int i;
            string TBname, STRcmd, STRdrop, idNameSTR;

            //nTables = TBConf.Length;

            //Create tables (one for each driver)
            foreach (DrvHConf Driver in DriversConf)
            {
                foreach (DriverComm.DriverFunctions.AreaData AreaC in Driver.AreaConf)
                {
                    //String Table
                    TBname = "HistDrv_" + Driver.DriverConf.ID.ToString("00") + AreaC.ID.ToString("00");
                    STRdrop = "DROP TABLES IF EXISTS " + TBname + ";";

                    STRcmd = "CREATE TABLE IF NOT EXISTS " + TBname + " ( IdAuto BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,";

                    switch (AreaC.dataType)
                    {
                        case DriverConfig.DatType.Bool:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " ENUM('True', 'False'),";
                            }
                            break;
                        case DriverConfig.DatType.Byte:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " TINYINT UNSIGNED,";
                            }
                            break;
                        case DriverConfig.DatType.Word:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " SMALLINT UNSIGNED,";
                            }
                            break;
                        case DriverConfig.DatType.DWord:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " INT UNSIGNED,";
                            }
                            break;
                        case DriverConfig.DatType.sDWord:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " INT,";
                            }
                            break;
                        case DriverConfig.DatType.Real:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " FLOAT,";
                            }
                            break;
                        case DriverConfig.DatType.String:
                            for (i = 0; i < AreaC.Amount; i++)
                            {
                                idNameSTR = "v" + Driver.DriverConf.ID.ToString("00") +
                                            AreaC.ID.ToString("00") + i.ToString("0000");
                                STRcmd = STRcmd + " " + idNameSTR + " VARCHAR(255),";
                            }
                            break;
                        default:
                            isInitialized = false;
                            return -2;
                            break;
                    }// END Switch Type of Data

                    //End of the Command
                    STRcmd = STRcmd + " ValStat TINYINT,  TimeTicks BIGINT UNSIGNED NOT NULL, PRIMARY KEY (IdAuto) ) ENGINE = ARCHIVE;";

                    //Drop the Old tables
                    if (!SQLcmdSingle(STRdrop)) return -1;

                    //Create the new tables
                    if (!SQLcmdSingle(STRcmd)) return -1;

                } //END Area Cicle

            } //END Drivers Cicle

            return 0;
        } // END Init DB function

        /// <summary>
        /// Write data to the database. 
        /// <param name="DataExt">Array Struct with the data to be written in the DB. </param> </summary>
        public bool Write(DriverComm.DriverFunctions.DataExt[] DataExt)
        {
            int j, DriverID;
            byte ValStat = 0;
            string STRcmd, TBname, idNameSTR, valStr, colSTR;
            long NowTicks = DateTime.UtcNow.Ticks;

            //Process each Data Area
            foreach (DriverComm.DriverFunctions.DataExt DataAreaW in DataExt)
            {
                //Check that the Data Area is enabled
                if (DataAreaW.AreaConf.Enable)
                {
                    DriverID = DataAreaW.AreaConf.ID_Driver;
                    TBname = "HistDrv_" + DriverID.ToString("00") + DataAreaW.AreaConf.ID.ToString("00");
                    colSTR = ""; valStr = "";

                    if (valStr.Length > 3) valStr = valStr + ", ";

                    for (j = 0; j < DataAreaW.AreaConf.Amount; j++)
                    {
                        idNameSTR = "v" + DriverID.ToString("00") +
                            DataAreaW.AreaConf.ID.ToString("00") + j.ToString("0000");
                        colSTR = colSTR + idNameSTR;

                        switch (DataAreaW.AreaConf.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                valStr = valStr + "'" + DataAreaW.Data.dBoolean[j].ToString() + "'";
                                break;
                            case DriverConfig.DatType.Byte:
                                valStr = valStr + DataAreaW.Data.dByte[j].ToString();
                                break;
                            case DriverConfig.DatType.Word:
                                valStr = valStr + DataAreaW.Data.dWord[j].ToString();
                                break;
                            case DriverConfig.DatType.DWord:
                                valStr = valStr + DataAreaW.Data.dDWord[j].ToString();
                                break;
                            case DriverConfig.DatType.sDWord:
                                valStr = valStr + DataAreaW.Data.dsDWord[j].ToString();
                                break;
                            case DriverConfig.DatType.Real:
                                if ((float.IsNaN(DataAreaW.Data.dReal[j])) || (float.IsInfinity(DataAreaW.Data.dReal[j])))
                                    DataAreaW.Data.dReal[j] = 0;
                                valStr = valStr + DataAreaW.Data.dReal[j].ToString();
                                break;
                            case DriverConfig.DatType.String:
                                valStr = valStr + "'" + DataAreaW.Data.dString[j] + "'";
                                break;
                            default:
                                return false;
                                break;
                        }

                        //add "," to separate next field
                        valStr = valStr + ", ";
                        colSTR = colSTR + ", ";

                    } //END For Variable

                    STRcmd = "INSERT LOW_PRIORITY INTO " + TBname + " (" + colSTR + " ValStat, TimeTicks) VALUES (" + 
                        valStr + ValStat.ToString() + ", " + NowTicks.ToString() + ");";

                    //Send the query
                    if (!SQLcmdSingle(STRcmd)) return false;
                } //IF DataArea is Enabled
            } //For Cicle Data Areas

            return true;
        } //END Write function

        /// <summary>
        /// Clean the old registers from the Database.
        /// </summary>
        public int CleanHist()
        {
            long numRegVar, nTableRows, toErase;
            string TBname, cmdSTR;

            //Calculate the Max Allowed amount of rows in the register per driver.
            numRegVar = (SrvConf.HistLengh * (TimeSpan.TicksPerDay / TimeSpan.TicksPerMillisecond));

            foreach (DrvHConf Driver in DrvConfig)
            {
                TBname = "HistDrv_" + Driver.DriverConf.ID.ToString("00");
                cmdSTR = "SELECT COUNT(*) FROM " + TBname + ";";
                nTableRows = SQLreadLong(cmdSTR);


                //Calculate the Max Allowed amount of rows in the register per driver.
                numRegVar = (SrvConf.HistLengh * (TimeSpan.TicksPerDay / TimeSpan.TicksPerMillisecond) / Driver.DriverConf.CycleTime);

                if (nTableRows > (numRegVar + 1000))
                {
                    //Erase some lines.
                    toErase = nTableRows - numRegVar;
                    cmdSTR = "DELETE FROM " + TBname +
                        " WHERE IdAuto IN (SELECT IdAuto FROM " + TBname + " ORDER BY id ASC LIMIT " + toErase.ToString() + ");";
                    if (!SQLcmdSingle(cmdSTR)) return -1;
                }

            } //FOREACH tables

            //SELECT COUNT(*) FROM fooTable;
            //DELETE FROM mytable WHERE id IN (SELECT id FROM mytable ORDER BY id ASC LIMIT 100)
            return 0;
        }

        /// <summary>
        /// Send a single command to the database.
        /// <param name="query">String with the SQL chain and data.</param> </summary>
        private bool SQLcmdSingle(string query)
        {
            //http://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
            //open connection
            if (this.Connect() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, conn);

                try
                {
                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    status = e.Message;
                    return false;
                }
                //close connection
                //this.Disconnect();

                cmd.Dispose();
                return true;
            }
            else {
                return false;
            }

        } // End SQLcmdSingle Function

        /// <summary>
        /// Send multiple command to the database.
        /// <param name="querys">Array with the strings containing the SQL chain and data.</param>
        /// <param name="numQuerys">Number of elements in the strings array.</param></summary>
        private bool SQLcmdMult(string[] querys, int numQuerys)
        {
            int i;
            //http://www.karlrixon.co.uk/writing/update-multiple-rows-with-different-values-and-a-single-sql-query/
            //open connection
            if (this.Connect() == true)
            {
                if (querys.Length >= numQuerys)
                    for (i = 0; i < numQuerys; i++)
                    {
                        if (querys[i] != null)
                        {
                            //create command and assign the query and connection from the constructor
                            MySqlCommand cmd = new MySqlCommand(querys[i], conn);
                            try
                            { //Execute command
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception e)
                            {
                                status = e.Message;
                                return false;
                            }
                            cmd.Dispose();
                        }
                    }
                //close connection
                //this.Disconnect();

                return true;
            }
            else {
                return false;
            }
        } //END SQLcmdMult Function

        /// <summary>
        /// Read a long type value from the database.
        /// <param name="query">String with the SQL chain and data.</param> </summary>
        private long SQLreadLong(string query)
        {
            long retVal = 0;

            //open connection
            if (this.Connect() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, conn);

                try
                {
                    //Execute command
                    retVal = (long)cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    status = e.Message;
                }
                cmd.Dispose();

                //close connection
                //this.Disconnect();

            }

            return retVal;

        } // End SQLcmdSingle Function

        /// <summary>
        /// Connect to the database. </summary>
        private bool Connect()
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                status = ex.Message;
                return false;
            }

        } //END Connect function

        /// <summary>
        /// Disconnect from the database. </summary>
        private bool Disconnect()
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                status = ex.Message;
                return false;
            }
        } //END DisConnect function

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    this.Disconnect();
                    conn.Dispose();
                }

                //  free unmanaged resources (unmanaged objects) and override a finalizer below.
                //  set large fields to null.

                //DBConfig = null;

                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DB_MySQL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
