﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DriverCommApp.Conf;
using MySql;
using MySql.Data.MySqlClient;


namespace DriverCommApp.DBMain
{
    class DB_MySQL : IDisposable
    {
        /// <summary>
        /// Database Server Configuration.</summary>
        DB_Main.ServerConf DBConfig;

        /// <summary>
        /// Database MySQL Connection Object.</summary>
        MySqlConnection conn;

        /// <summary>
        /// Execption info from the MySQL library.</summary>
        public string status;

        /// <summary>
        /// Initialization flag.</summary>
        public bool isInitialized;

        /// <summary>
        /// Class Constructor.
        /// <param name="Server">Server configuration struct.</param> </summary>
        public DB_MySQL(DB_Main.ServerConf Server)
        {
            DBConfig = Server;
            isInitialized = false;
        }

        /// <summary>
        /// Initialize the Object.
        /// <param name="DriversConf">Driver Configuration and DataAreas (Needed for initial set).</param> 
        /// <param name="InitialSet">Initial set flag (will erase actual tables).</param> </summary>
        public void Initialize(List<DB_Main.DrvConf> DriversConf, bool InitialSet)
        {
            string myConnectionString;
            int retVal = 0;

            //myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=12345;database=test;";
            myConnectionString = "SERVER=" + DBConfig.URL + ";" + "PORT=" + DBConfig.Port.ToString() + ";" +
                "PROTOCOL=" + DBConfig.Protocol.ToString().ToLower() + ";" + "DATABASE=" + DBConfig.DBname + ";" +
                "UID=" + DBConfig.Username + ";" + "PASSWORD=" + DBConfig.Passwd + ";"+
                "ConnectionTimeout=5; DefaultCommandTimeout=5;Keepalive=3" +";";

            conn = new MySqlConnection(myConnectionString);

            if (Connect())
            {
                Disconnect();
                //Database tables initialization.
                if (InitialSet)
                    retVal = InitDB(DriversConf);

                if (retVal == 0) isInitialized = true;
            }

        }

        /// <summary>
        /// Initialize the Database.
        /// <param name="DriversConf">Driver Configuration and DataAreas (Needed for initial set).</param> </summary>
        private int InitDB(List<DB_Main.DrvConf> DriversConf)
        {
            int j, k;
            string TBname, STRcmd, valStr, idNameSTR;

            //Create tables (one for each driver)
            foreach (DB_Main.DrvConf aDriverConfig in DriversConf)
            {
                TBname = "Drv" + aDriverConfig.DriverConf.ID.ToString("00");
                STRcmd = "DROP TABLES IF EXISTS " + TBname +";";
                if (!SQLcmdSingle(STRcmd)) return -1;

                STRcmd = "CREATE TABLE IF NOT EXISTS " + TBname + " ( IdAuto INT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                    "IdName INT(8) UNSIGNED NOT NULL, idDA TINYINT UNSIGNED, idValNum SMALLINT UNSIGNED, EngUnit VARCHAR(10), " +
                    "Writable ENUM('True', 'False'), TypeVal ENUM('Bool', 'Byte', 'Word', 'DWord', 'sDWord', 'Real', 'String'), " +
                    "Description VARCHAR(100), dBool ENUM('True', 'False'), dByte TINYINT UNSIGNED, dWord SMALLINT UNSIGNED, " +
                    "dDWord INT UNSIGNED, dsDWord INT, dReal FLOAT, dString VARCHAR(255), TimeUpd DATETIME, " +
                    "PRIMARY KEY  (IdAuto), UNIQUE KEY (IdName) ) ENGINE = INNODB;";
                if (!SQLcmdSingle(STRcmd)) return -1;

                //Create registers for each Data Area variables
                for (j = 0; j < (aDriverConfig.DriverConf.NDataAreas); j++)
                {
                    valStr = "";
                    if (aDriverConfig.AreaConf.Length > j)
                    {
                        for (k = 0; k < (aDriverConfig.AreaConf[j].Amount - 1); k++)
                        {
                            idNameSTR = aDriverConfig.AreaConf[j].ID_Driver.ToString("00") +
                                aDriverConfig.AreaConf[j].ID.ToString("00") + k.ToString("0000");

                            valStr = valStr + "(" + idNameSTR + "," + aDriverConfig.AreaConf[j].ID.ToString("00") + "," +
                                k.ToString("0000") + ",'" + aDriverConfig.AreaConf[j].Write.ToString() + "','" +
                                aDriverConfig.AreaConf[j].dataType.ToString() + "'),";
                        }
                        //Last val
                        idNameSTR = aDriverConfig.AreaConf[j].ID_Driver.ToString("00") +
                            aDriverConfig.AreaConf[j].ID.ToString("00") + k.ToString("0000");

                        valStr = valStr + "(" + idNameSTR + "," + aDriverConfig.AreaConf[j].ID.ToString("00") + "," +
                                    k.ToString("0000") + ",'" + aDriverConfig.AreaConf[j].Write.ToString() + "','" +
                                    aDriverConfig.AreaConf[j].dataType.ToString() + "');";

                        //Final CMD
                        STRcmd = "INSERT IGNORE " + TBname + " (IdName, idDA, idValNum, Writable, TypeVal) VALUES" + valStr;
                        if (!SQLcmdSingle(STRcmd)) return -1;
                    }
                } //END Data Areas cicle
            } //END Drivers Cicle.

            return 0;
        } // END Init DB function

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

        } // End SQLcmdSingle

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
        }

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

        /// <summary>
        /// Read data from the database. 
        /// <param name="DataExt">Array Struct reference to store the data readed from the DB.</param>
        /// <param name="NumDA">Number of Data Areas to be Read.</param></summary>
        public bool Read(ref CommDriver.DriverGeneric.DataExt[] DataExt, int NumDA)
        {
            int i, j;
            string STRcmd, TBname, ValSTR;
            MySqlDataReader dataReader;
            MySqlCommand cmd;

            //open connection
            if (this.Connect() == true)
            {
                for (i = 0; i < NumDA; i++)
                {
                    if (((DataExt.Length > i) && (DataExt[i].AreaConf.Enable)) && (DataExt[i].AreaConf.Write))
                    {
                        TBname = "Drv" + DataExt[i].AreaConf.ID_Driver.ToString("00");
                        ValSTR = "";
                        switch (DataExt[i].AreaConf.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                ValSTR = ", dBool";
                                break;
                            case DriverConfig.DatType.Byte:
                                ValSTR = ", dByte";
                                break;
                            case DriverConfig.DatType.Word:
                                ValSTR = ", dWord";
                                break;
                            case DriverConfig.DatType.DWord:
                                ValSTR = ", dDWord";
                                break;
                            case DriverConfig.DatType.sDWord:
                                ValSTR = ", dsDWord";
                                break;
                            case DriverConfig.DatType.Real:
                                ValSTR = ", dReal";
                                break;
                            case DriverConfig.DatType.String:
                                ValSTR = ", dString";
                                break;

                        }
                        //SQL Command String
                        STRcmd = "SELECT idDA, idValNum" + ValSTR + " FROM " + TBname + " WHERE idDA=" +
                            DataExt[i].AreaConf.ID.ToString("00") + " ORDER BY idValNum;";

                        //Create Command
                        cmd = new MySqlCommand(STRcmd, conn);
                        //Create a data reader and Execute the command
                        dataReader = cmd.ExecuteReader();

                        //Get the rows.
                        while (dataReader.Read())
                        {
                            j = int.Parse(dataReader["idValNum"] + "");
                            if ((int.Parse(dataReader["idDA"] + "") == DataExt[i].AreaConf.ID) &&
                                (DataExt[i].AreaConf.Amount > j) && (j >= 0))
                                switch (DataExt[i].AreaConf.dataType)
                                {
                                    case DriverConfig.DatType.Bool:
                                        if (DataExt[i].Data.dBoolean.Length > j)
                                            DataExt[i].Data.dBoolean[j] = bool.Parse(dataReader["dBool"] + "");
                                        break;
                                    case DriverConfig.DatType.Byte:
                                        if (DataExt[i].Data.dByte.Length > j)
                                            DataExt[i].Data.dByte[j] = byte.Parse(dataReader["dByte"] + "");
                                        break;
                                    case DriverConfig.DatType.Word:
                                        if (DataExt[i].Data.dWord.Length > j)
                                            DataExt[i].Data.dWord[j] = ushort.Parse(dataReader["dWord"] + "");
                                        break;
                                    case DriverConfig.DatType.DWord:
                                        if (DataExt[i].Data.dWord.Length > j)
                                            DataExt[i].Data.dWord[j] = ushort.Parse(dataReader["dDWord"] + "");
                                        break;
                                    case DriverConfig.DatType.sDWord:
                                        if (DataExt[i].Data.dDWord.Length > j)
                                            DataExt[i].Data.dDWord[j] = uint.Parse(dataReader["dsWord"] + "");
                                        break;
                                    case DriverConfig.DatType.Real:
                                        if (DataExt[i].Data.dsDWord.Length > j)
                                            DataExt[i].Data.dsDWord[j] = int.Parse(dataReader["dReal"] + "");
                                        break;
                                    case DriverConfig.DatType.String:
                                        if (DataExt[i].Data.dString.Length > j)
                                            DataExt[i].Data.dString[j] = dataReader["dString"] + "";
                                        break;
                                }

                        } // END While dataReader

                        //close Data Reader
                        dataReader.Close();
                        cmd.Dispose();
                        //dataReader.Dispose();

                    } //IF Data Area is to be Readed from DB and Write to device.

                }
                //close connection
                
                //this.Disconnect();
                return true;
            }
            else
            {
                return false;
            }

        } // END Read function

        /// <summary>
        /// Write data to the database. 
        /// <param name="DataExt">Array Struct with the data to be written in the DB.</param>
        /// <param name="NumDA">Number of Data Areas to be Written.</param></summary>
        public bool Write(CommDriver.DriverGeneric.DataExt[] DataExt, int NumDA)
        {
            int i, j, k, initialLenght;
            string STRcmd, TBname, idNameSTR, caseSTR, whereSTR, timeSTR;

            string[] STRcmdM = new string[NumDA]; //Var for multiple writes
            k = 0;
            for (i = 0; i < NumDA; i++)
            {
                TBname = "Drv" + DataExt[i].AreaConf.ID_Driver.ToString("00");

                //Initialize the descriptions only once.
                if ( (!DataExt[i].FirstInit) && (DataExt[i].VarNames != null) )
                {
                    caseSTR = ""; whereSTR = " WHERE IdName IN ( ";
                    initialLenght = whereSTR.Length + 1;
                    STRcmd = "UPDATE " + TBname + " SET Description = CASE IdName ";

                    for (j = 0; j < DataExt[i].AreaConf.Amount; j++)
                    {
                        if (DataExt[i].VarNames[j] != null)
                        {
                            idNameSTR = DataExt[i].AreaConf.ID_Driver.ToString("00") +
                               DataExt[i].AreaConf.ID.ToString("00") + j.ToString("0000");
                            caseSTR = caseSTR + " WHEN " + idNameSTR + " THEN " + "'" + DataExt[i].VarNames[j] + "' ";

                            if (whereSTR.Length < initialLenght)
                            {
                                whereSTR = whereSTR + idNameSTR;
                            }
                            else
                            {
                                whereSTR = whereSTR + "," + idNameSTR;
                            }
                        }
                    } //For area items

                    if (caseSTR.Length > 5)
                    {
                        STRcmd = STRcmd + caseSTR + " END " + whereSTR + ");";

                        //Send the query
                        if (!SQLcmdSingle(STRcmd)) return false;
                    }

                    DataExt[i].FirstInit = true; //Reset the flag
                } //END If FirstWrite

                // Write the data readed from the devices.
                if (((DataExt.Length > i) && (DataExt[i].AreaConf.Enable)) && (!DataExt[i].AreaConf.Write))
                {
                    caseSTR = ""; whereSTR = " WHERE IdName IN (";
                    STRcmdM[i] = "UPDATE " + TBname + " SET ";

                    switch (DataExt[i].AreaConf.dataType)
                    {
                        case DriverConfig.DatType.Bool:
                            STRcmdM[i] = STRcmdM[i] + "dBool = CASE IdName ";
                            break;
                        case DriverConfig.DatType.Byte:
                            STRcmdM[i] = STRcmdM[i] + "dByte = CASE IdName ";
                            break;
                        case DriverConfig.DatType.Word:
                            STRcmdM[i] = STRcmdM[i] + "dWord = CASE IdName ";
                            break;
                        case DriverConfig.DatType.DWord:
                            STRcmdM[i] = STRcmdM[i] + "dDWord = CASE IdName ";
                            break;
                        case DriverConfig.DatType.sDWord:
                            STRcmdM[i] = STRcmdM[i] + "dsDWord = CASE IdName ";
                            break;
                        case DriverConfig.DatType.Real:
                            STRcmdM[i] = STRcmdM[i] + "dReal = CASE IdName ";
                            break;
                        case DriverConfig.DatType.String:
                            STRcmdM[i] = STRcmdM[i] + "dBool = CASE IdName ";
                            break;
                        default:
                            return false;
                            break;
                    }

                    for (j = 0; j < DataExt[i].AreaConf.Amount; j++)
                    {
                        idNameSTR = DataExt[i].AreaConf.ID_Driver.ToString("00") +
                               DataExt[i].AreaConf.ID.ToString("00") + j.ToString("0000");

                        if (j == 0)
                        {
                            whereSTR = whereSTR + idNameSTR;
                        }
                        else
                        {
                            whereSTR = whereSTR + "," + idNameSTR;
                        }

                        caseSTR = caseSTR + " WHEN " + idNameSTR + " THEN ";

                        switch (DataExt[i].AreaConf.dataType)
                        {
                            case DriverConfig.DatType.Bool:
                                caseSTR = caseSTR + "'" + DataExt[i].Data.dBoolean[j].ToString() + "'";
                                break;
                            case DriverConfig.DatType.Byte:
                                caseSTR = caseSTR + DataExt[i].Data.dByte[j].ToString();
                                break;
                            case DriverConfig.DatType.Word:
                                caseSTR = caseSTR + DataExt[i].Data.dWord[j].ToString();
                                break;
                            case DriverConfig.DatType.DWord:
                                caseSTR = caseSTR + DataExt[i].Data.dDWord[j].ToString();
                                break;
                            case DriverConfig.DatType.sDWord:
                                caseSTR = caseSTR + DataExt[i].Data.dsDWord[j].ToString();
                                break;
                            case DriverConfig.DatType.Real:
                                if ( (float.IsNaN(DataExt[i].Data.dReal[j])) || (float.IsInfinity(DataExt[i].Data.dReal[j])) )
                                    DataExt[i].Data.dReal[j] = 0;
                                caseSTR = caseSTR + DataExt[i].Data.dReal[j].ToString();
                                break;
                            case DriverConfig.DatType.String:
                                caseSTR = caseSTR + "'" + DataExt[i].Data.dString[j] + "'";
                                break;
                            default:
                                return false;
                                break;
                        }

                        /*UPDATE categories
                             SET title = CASE id
                                 WHEN 1 THEN 'New Title 1'
                                 WHEN 2 THEN 'New Title 2'
                                 WHEN 3 THEN 'New Title 3'
                             END,
                             TimeUpd=''
                        WHERE id IN (1, 2, 3)*/

                    }

                    
                        timeSTR = DataExt[i].TimeStamp.Year.ToString("0000") + "-" +
                        DataExt[i].TimeStamp.Month.ToString("00") + "-" +
                        DataExt[i].TimeStamp.Day.ToString("00") + " " +
                        DataExt[i].TimeStamp.Hour.ToString("00") + ":" +
                        DataExt[i].TimeStamp.Minute.ToString("00") + ":" +
                        DataExt[i].TimeStamp.Second.ToString("00");



                    STRcmdM[i] = STRcmdM[i] + caseSTR + " END, TimeUpd='" + timeSTR + "'" + whereSTR + ");";
                    k++; //index counting the good Data-Areas
                    
                } // END IF write data readed.

            }// For cicle thru Data Areas
             //Send the query
            if (k > 0) { if (!SQLcmdMult(STRcmdM,NumDA)) return false; }
            return true;
        } //END Write function

        

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

    } //Close Class DB MySQL
} //Close Namespace