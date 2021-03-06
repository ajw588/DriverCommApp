﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;

//This App Namespace
using DriverCommApp.Conf.DB;
using static DriverCommApp.Historics.Hist_Functions;
using DatType = DriverCommApp.Conf.DV.DriverConfig.DatType;
using StatT = DriverCommApp.Stat.StatReport.StatT;

namespace DriverCommApp.Historics.HistMySQL
{
   class Hist_MySQL : IDisposable
   {

      #region Global Objects for this class
      /// <summary>
      /// Database Server Configuration.</summary>
      HistConfClass.ServerConf SrvConf;

      /// <summary>
      /// Drivers Configuration.</summary>
      List<DriverComm.DVConfDAConfClass> DrvConfig;

      /// <summary>
      /// Database MySQL Connection Object.</summary>
      MySqlConnection conn;

      /// <summary>
      /// Exception info from the MySQL library.</summary>
      public Stat.StatReport Status;

      /// <summary>
      /// Initialization flag.</summary>
      public bool isInitialized;

      #endregion
      #region Class Constructors and Initialization
      /// <summary>
      /// Class Constructor.
      /// <param name="ServerConf">Server Configuration Parameters Object.</param> 
      /// <param name="iamMaster">Flag to declare as master or backup Historics.</param></summary>
      public Hist_MySQL(HistConfClass.ServerConf ServerConf, bool iamMaster)
      {
         //Keep the Server Configuration
         SrvConf = ServerConf;
         isInitialized = false;

         if (iamMaster)
         {
            Status = new Stat.StatReport((int)Stat.StatReport.IDdef.Hist);
         }
         else
         {
            Status = new Stat.StatReport((int)Stat.StatReport.IDdef.Histbackup);
         }
      }

      public void Initialize(bool InitialSet, List<DriverComm.DVConfDAConfClass> DriversConf)
      {
         string myConnectionString;
         int retVal = 0;

         //Reset the flag
         isInitialized = false;

         //Reset the Status Buffer
         Status.ResetStat();

         if (DriversConf != null)
         {
            //Keep a copy of the Drivers Configuration.
            DrvConfig = DriversConf;

            //myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=12345;database=test;";
            myConnectionString = "SERVER=" + SrvConf.URL + ";" + "PORT=" + SrvConf.Port.ToString() + ";" +
                "PROTOCOL=" + SrvConf.Protocol.ToString().ToLower() + ";" + "DATABASE=" + SrvConf.DBname + ";" +
                "UID=" + SrvConf.Username + ";" + "PASSWORD=" + SrvConf.Passwd + ";" +
                "ConnectionTimeout=5; DefaultCommandTimeout=5;Keepalive=3" + ";";

            try
            {
               conn = new MySqlConnection(myConnectionString);
               if (Connect())
               {
                  Disconnect();

                  //Database tables initialization.
                  if (InitialSet) retVal = InitDB();

                  if (retVal == 0)
                  {
                     isInitialized = true;
                     Status.NewStat(StatT.Good);
                  }
                  else
                  {
                     Status.NewStat(StatT.Bad, "Initialization Failed...");
                  }
               }
               else
               {
                  Status.NewStat(StatT.Bad, "Database Connection Failed...");
               }
            }
            catch (Exception e)
            {
               Status.NewStat(StatT.Bad, e.Message);
            }

         }
         else { Status.NewStat(StatT.Bad, "Corrupted Conf Data."); }

      } //END Func Initialize

      /// <summary>
      /// Initialize the Database.
      /// </summary>
      private int InitDB()
      {
         int i, retVal = -10;
         string TBname, STRcmd, STRdrop, idNameSTR;

         //nTables = TBConf.Length;

         //Create tables (one for each driver)
         foreach (DriverComm.DVConfDAConfClass Driver in DrvConfig)
         {
            if (Driver.DAConf != null)
            {
               foreach (DriverComm.DAConfClass AreaC in Driver.DAConf)
               {
                  if (AreaC != null)
                  {
                     //String Table
                     TBname = GetHistTbName(Driver.DVConf.ID, AreaC.ID);
                     STRdrop = "DROP TABLES IF EXISTS " + TBname + ";";

                     STRcmd = "CREATE TABLE IF NOT EXISTS " + TBname + " ( IdAuto BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,";

                     switch (AreaC.dataType)
                     {
                        case DatType.Bool:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " ENUM('True', 'False'),";
                           }
                           break;
                        case DatType.Byte:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " TINYINT UNSIGNED,";
                           }
                           break;
                        case DatType.Word:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " SMALLINT UNSIGNED,";
                           }
                           break;
                        case DatType.DWord:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " INT UNSIGNED,";
                           }
                           break;
                        case DatType.sDWord:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " INT,";
                           }
                           break;
                        case DatType.Real:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " FLOAT,";
                           }
                           break;
                        case DatType.String:
                           for (i = 0; i < AreaC.Amount; i++)
                           {
                              idNameSTR = "v" + Driver.DVConf.ID.ToString("00") +
                                          AreaC.ID.ToString("00") + i.ToString("0000");
                              STRcmd = STRcmd + " " + idNameSTR + " VARCHAR(255),";
                           }
                           break;
                        default:
                           isInitialized = false;
                           return -2;
#pragma warning disable CS0162 // Unreachable code detected
                           break;
#pragma warning restore CS0162 // Unreachable code detected
                     }// END Switch Type of Data

                     //End of the Command
                     STRcmd = STRcmd + " ValStat TINYINT,  TimeTicks BIGINT UNSIGNED NOT NULL, PRIMARY KEY (IdAuto) ) ENGINE = ARCHIVE;";

                     //Drop the Old tables
                     if (SQLcmdSingle(STRdrop))
                        //Create the new tables
                        if (SQLcmdSingle(STRcmd)) retVal = 0;

                  }//IF AreaC!=null
                  else { retVal = -10; }
               } //END Area Cicle

            } //IF DataArea Array!= null
            else { retVal = -10; }
         } //END Drivers Cicle

         return retVal; ;
      } // END Init DB function

      #endregion
      #region Public Methods
      /// <summary>
      /// Write data to the database. 
      /// <param name="DataExt">Array Struct with the data to be written in the DB. </param> </summary>
      public bool Write(DriverComm.DataExtClass[] DataExt)
      {
         int i, j, DriverID, numDA;
         byte ValStat = 0;
         string TBname, idNameSTR, valStr, colSTR;
         string[] STRcmd;
         long NowTicks = 0;

         //Reset the Status Buffer
         Status.ResetStat();

         //Process each Data Area
         if (DataExt != null)
         {
            numDA = DataExt.Length;
            if (numDA > 0)
            {
               STRcmd = new string[numDA];

               for (i = 0; i < numDA; i++)
               {
                  STRcmd[i] = null;

                  if ((DataExt[i] != null) && (DataExt[i].AreaConf != null))
                  {
                     DriverID = DataExt[i].AreaConf.ID_Driver;
                     TBname = GetHistTbName(DriverID, DataExt[i].AreaConf.ID);
                     colSTR = ""; valStr = "";

                     for (j = 0; j < DataExt[i].AreaConf.Amount; j++)
                     {
                        idNameSTR = "v" + Database.DB_Functions.GetStrIdName(DriverID, DataExt[i].AreaConf.ID, j);
                        colSTR = colSTR + idNameSTR + ", ";

                        switch (DataExt[i].AreaConf.dataType)
                        {
                           case DatType.Bool:
                              if ((DataExt[i].Data.dBoolean != null) && (DataExt[i].Data.dBoolean.Length > j))
                              { valStr = valStr + "'" + DataExt[i].Data.dBoolean[j].ToString() + "', "; }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           case DatType.Byte:
                              if ((DataExt[i].Data.dByte != null) && (DataExt[i].Data.dByte.Length > j))
                              { valStr = valStr + DataExt[i].Data.dByte[j].ToString() + ", "; }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           case DatType.Word:
                              if ((DataExt[i].Data.dWord != null) && (DataExt[i].Data.dWord.Length > j))
                              { valStr = valStr + DataExt[i].Data.dWord[j].ToString() + ", "; }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           case DatType.DWord:
                              if ((DataExt[i].Data.dDWord != null) && (DataExt[i].Data.dDWord.Length > j))
                              { valStr = valStr + DataExt[i].Data.dDWord[j].ToString() + ", "; }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           case DatType.sDWord:
                              if ((DataExt[i].Data.dsDWord != null) && (DataExt[i].Data.dsDWord.Length > j))
                              { valStr = valStr + DataExt[i].Data.dsDWord[j].ToString() + ", "; }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           case DatType.Real:
                              if ((DataExt[i].Data.dReal != null) && (DataExt[i].Data.dReal.Length > j))
                              {
                                 if ((float.IsNaN(DataExt[i].Data.dReal[j])) || (float.IsInfinity(DataExt[i].Data.dReal[j])))
                                    DataExt[i].Data.dReal[j] = 0;
                                 valStr = valStr + DataExt[i].Data.dReal[j].ToString() + ", ";
                              }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           case DatType.String:
                              if ((DataExt[i].Data.dString != null) && (DataExt[i].Data.dString.Length > j))
                              { valStr = valStr + "'" + DataExt[i].Data.dString[j] + "', "; }
                              else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }
                              break;
                           default:
                              Status.NewStat(StatT.Warning, "Wrong Data Type, Check Config DA.");
                              return false;
#pragma warning disable CS0162 // Unreachable code detected
                              break;
#pragma warning restore CS0162 // Unreachable code detected
                        }

                     } //END For Variable

                     //Timestamp in ticks
                     NowTicks = DataExt[i].NowTimeTicks;

                     STRcmd[i] = "INSERT LOW_PRIORITY INTO " + TBname + " (" + colSTR + " ValStat, TimeTicks) VALUES (" +
                         valStr + ValStat.ToString() + ", " + NowTicks.ToString() + ");";

                  }// If DataAreaW!=Null
                  else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }

               } //For Cicle Data Areas

               //Send the query
               if (!SQLcmdMult(STRcmd, numDA))
               {
                  Status.NewStat(StatT.Warning, "Writing Data Failed.");
                  return false;
               }
            }//IF numDA>0

         } //IF DataExt!=null
         else { Status.NewStat(StatT.Warning, "Writing Data Corrupted."); return false; }

         Status.NewStat(StatT.Good);
         return true;
      } //END Write function

      /// <summary>
      /// Clean the old registers from the Database.
      /// </summary>
      public bool CleanHist()
      {
         long numRegVar, nTableRows, toErase;
         string TBname, cmdSTR;

         //Reset the Status Buffer
         Status.ResetStat();

         if (isInitialized)
         {
            //Calculate the Max Allowed amount of rows in the register per driver.
            numRegVar = (SrvConf.HistLengh * (TimeSpan.TicksPerDay / TimeSpan.TicksPerMillisecond));

            foreach (DriverComm.DVConfDAConfClass Driver in DrvConfig)
               foreach (DriverComm.DAConfClass DataArea in Driver.DAConf)
               {
                  if ((DataArea.ToHistorics) && (DataArea.Enable))
                  {
                     TBname = GetHistTbName(Driver.DVConf.ID, DataArea.ID);
                     cmdSTR = "SELECT COUNT(*) FROM " + TBname + ";";
                     nTableRows = SQLreadLong(cmdSTR);


                     //Calculate the Max Allowed amount of rows in the register per driver.
                     numRegVar = (SrvConf.HistLengh * (TimeSpan.TicksPerDay / TimeSpan.TicksPerMillisecond) / Driver.DVConf.CycleTime);

                     if (nTableRows > (numRegVar + 1000))
                     {
                        //Erase some lines.
                        toErase = nTableRows - numRegVar;
                        cmdSTR = "DELETE FROM " + TBname +
                            " WHERE IdAuto IN (SELECT IdAuto FROM " + TBname + " ORDER BY id ASC LIMIT " + toErase.ToString() + ");";
                        Status.NewStat(StatT.Good, "Cleaning " + toErase.ToString() + " registers from Table: " + TBname + ".");
                        if (!SQLcmdSingle(cmdSTR))
                        {
                           Status.NewStat(StatT.Warning, "Cleaning Registers Failed.");
                           return false;
                        }
                     }
                  }
               } //FOREACH tables

            //SELECT COUNT(*) FROM fooTable;
            //DELETE FROM mytable WHERE id IN (SELECT id FROM mytable ORDER BY id ASC LIMIT 100)
            Status.NewStat(StatT.Good, "Cleaning Suceed.");
            return true;
         }
         else { Status.NewStat(StatT.Bad, "Not Initialized."); return false; }
      }
      #endregion
      #region Private Methods
      /// <summary>
      /// Send a single command to the database.
      /// <param name="query">String with the SQL chain and data.</param> </summary>
      private bool SQLcmdSingle(string query)
      {
         //http://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
         //open connection
         if (this.Connect() == true)
         {
            try
            {
               //create command and assign the query and connection from the constructor
               MySqlCommand cmd = new MySqlCommand(query, conn);

               //Execute command
               cmd.ExecuteNonQuery();

               cmd.Dispose();
            }
            catch (Exception e)
            {
               Status.NewStat(StatT.Bad, e.Message);
               return false;
            }
            
            return true;
         }
         else
         {
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
            try
            {
               //create command and assign the query and connection from the constructor
               MySqlCommand cmd = new MySqlCommand("", conn);

               if (querys.Length >= numQuerys)
                  for (i = 0; i < numQuerys; i++)
                  {
                     if (querys[i] != null)
                     {
                        //Add the Query
                        cmd.CommandText = querys[i];
                        //Execute command
                        cmd.ExecuteNonQuery();
                     }
                  }

               //Dispose the CMD
               cmd.Dispose();
            }
            catch (Exception e)
            {
               Status.NewStat(StatT.Bad, e.Message);
               return false;
            }
            return true;
         }
         else
         {
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
               Status.NewStat(StatT.Bad, e.Message);
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
            if (conn.State != System.Data.ConnectionState.Open)
            {
               this.Disconnect();
               System.Threading.Thread.Sleep(3000);
               conn.Open();

               //Check how well it went.
               if (conn.State != System.Data.ConnectionState.Open)
               {
                  Status.NewStat(StatT.Warning, "Connection Failed...");
                  this.Disconnect();
                  return false;
               }
               else
               {
                  return true;
               }
            }
            else
            {
               //It its Open
               return true;
            }
         }
         catch (MySqlException e)
         {
            Status.NewStat(StatT.Bad, e.Message);
            return false;
         }
      } //END Connect function

      /// <summary>
      /// Disconnect from the database. </summary>
      public bool Disconnect()
      {
         try
         {
            if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            return true;
         }
         catch (MySqlException e)
         {
            Status.NewStat(StatT.Bad, e.Message);
            return false;
         }
      } //END DisConnect function
      #endregion
      #region IDisposable/Desctructor Support
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
