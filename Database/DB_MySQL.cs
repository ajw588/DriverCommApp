using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql;
using MySql.Data.MySqlClient;

//This APP Namespace
using static DriverCommApp.Database.DB_Functions;
using DatType = DriverCommApp.Conf.DV.DriverConfig.DatType;
using StatT = DriverCommApp.Stat.StatReport.StatT;

namespace DriverCommApp.Database.DBMySQL
{
   class DB_MySQL : IDisposable
   {
      #region Global Objects for this class
      /// <summary>
      /// Database Server Configuration.</summary>
      DBConfClass.ServerConf DBConfig;

      /// <summary>
      /// Database MySQL Connection Object.</summary>
      MySqlConnection conn;

      /// <summary>
      /// Status Object.</summary>
      public Stat.StatReport Status;


      /// <summary>
      /// Initialization flag.</summary>
      public bool isInitialized;

      #endregion
      #region Class Constructor and Initialization
      /// <summary>
      /// Class Constructor.
      /// <param name="Server">Server configuration struct.</param> 
      /// <param name="iamMaster">Defines if its Master=true, for the reporting</param></summary>
      public DB_MySQL(DBConfClass.ServerConf Server, bool iamMaster)
      {
         DBConfig = Server;

         if (iamMaster)
         {
            Status = new Stat.StatReport((int)Stat.StatReport.IDdef.DB);
         }
         else
         {
            Status = new Stat.StatReport((int)Stat.StatReport.IDdef.DBbackup);
         }

         isInitialized = false;
      }

      /// <summary>
      /// Initialize the Object.
      /// <param name="DriversConf">Driver Configuration and DataAreas (Needed for initial set).</param> 
      /// <param name="InitialSet">Initial set flag (will erase actual tables).</param> </summary>
      public void Initialize(List<DriverComm.DVConfDAConfClass> DriversConf, bool InitialSet)
      {
         string myConnectionString;
         int retVal = 0;

         //Reset the Status Buffer
         Status.ResetStat();

         //myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=12345;database=test;";
         myConnectionString = "SERVER=" + DBConfig.URL + ";" + "PORT=" + DBConfig.Port.ToString() + ";" +
             "PROTOCOL=" + DBConfig.Protocol.ToString().ToLower() + ";" + "DATABASE=" + DBConfig.DBname + ";" +
             "UID=" + DBConfig.Username + ";" + "PASSWORD=" + DBConfig.Passwd + ";" +
             "ConnectionTimeout=5; DefaultCommandTimeout=5;Keepalive=3" + ";";

         try
         {
            conn = new MySqlConnection(myConnectionString);

            if (Connect())
            {
               Disconnect();
               //Database tables initialization.
               if (InitialSet)
                  retVal = InitDB(DriversConf);

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
         }
         catch (Exception e)
         {
            Status.NewStat(StatT.Bad, e.Message);
         }

      } //END Function Initialize

      /// <summary>
      /// Initialize the Database.
      /// <param name="DriversConf">Driver Configuration and DataAreas (Needed for initial set).</param> </summary>
      private int InitDB(List<DriverComm.DVConfDAConfClass> DriversConf)
      {
         int j, k;
         string TBname, STRcmd, valStr, idNameSTR;

         //Create tables (one for each driver)
         foreach (DriverComm.DVConfDAConfClass aDriverConfig in DriversConf)
         {
            TBname = "Drv" + aDriverConfig.DVConf.ID.ToString("00");
            STRcmd = "DROP TABLES IF EXISTS " + TBname + ";";
            if (!SQLcmdSingle(STRcmd)) return -1;

            STRcmd = "CREATE TABLE IF NOT EXISTS " + TBname + " ( IdAuto INT UNSIGNED NOT NULL AUTO_INCREMENT, " +
                "IdName INT(8) UNSIGNED NOT NULL, idDA TINYINT UNSIGNED, idValNum SMALLINT UNSIGNED, EngUnit VARCHAR(10), " +
                "Writable ENUM('True', 'False'), TypeVal ENUM('Bool', 'Byte', 'Word', 'DWord', 'sDWord', 'Real', 'String'), " +
                "Description VARCHAR(100), dBool ENUM('True', 'False'), dByte TINYINT UNSIGNED, dWord SMALLINT UNSIGNED, " +
                "dDWord INT UNSIGNED, dsDWord INT, dReal FLOAT, dString VARCHAR(255), TimeUpd DATETIME, " +
                "PRIMARY KEY  (IdAuto), UNIQUE KEY (IdName) ) ROW_FORMAT=DYNAMIC ENGINE = INNODB;";
            if (!SQLcmdSingle(STRcmd)) return -1;

            //Create registers for each Data Area variables
            for (j = 0; j < (aDriverConfig.DVConf.NDataAreas); j++)
            {
               valStr = "";
               if (aDriverConfig.DAConf.Length > j)
               {
                  for (k = 0; k < (aDriverConfig.DAConf[j].Amount - 1); k++)
                  {
                     idNameSTR = GetStrIdName(aDriverConfig.DAConf[j].ID_Driver, aDriverConfig.DAConf[j].ID, k);

                     valStr = valStr + "(" + idNameSTR + "," + aDriverConfig.DAConf[j].ID.ToString("00") + "," +
                         k.ToString("0000") + ",'" + aDriverConfig.DAConf[j].Write.ToString() + "','" +
                         aDriverConfig.DAConf[j].dataType.ToString() + "'),";
                  }
                  //Last val
                  idNameSTR = aDriverConfig.DAConf[j].ID_Driver.ToString("00") +
                      aDriverConfig.DAConf[j].ID.ToString("00") + k.ToString("0000");

                  valStr = valStr + "(" + idNameSTR + "," + aDriverConfig.DAConf[j].ID.ToString("00") + "," +
                              k.ToString("0000") + ",'" + aDriverConfig.DAConf[j].Write.ToString() + "','" +
                              aDriverConfig.DAConf[j].dataType.ToString() + "');";

                  //Final CMD
                  STRcmd = "INSERT IGNORE " + TBname + " (IdName, idDA, idValNum, Writable, TypeVal) VALUES" + valStr;
                  if (!SQLcmdSingle(STRcmd)) return -1;
               }
            } //END Data Areas cicle
         } //END Drivers Cicle.

         return 0;
      } // END Init DB function
      #endregion
      #region Public Methods

      /// <summary>
      /// Read data from the database. 
      /// <param name="Data">Struct ref to object to save data.</param>
      /// </summary>
      public bool Read(DriverComm.DataExtClass[] Data)
      {
         int i, j, numDA, k;
         string STRcmd, TBname, ValSTR;
         MySqlDataReader dataReader;
         MySqlCommand cmd;

         //Reset the Status Buffer
         Status.ResetStat();

         if (Data != null)
         {
            //Init
            numDA = Data.Length;
            k = 0;

            //open connection
            if (this.Connect() == true)
            {
               for (i = 0; i < numDA; i++)
               {
                  if (((Data.Length > i) && (Data[i].AreaConf.Enable)) && (Data[i].AreaConf.Write))
                  {
                     TBname = GetDVTbName(Data[i].AreaConf.ID_Driver);
                     ValSTR = "";
                     switch (Data[i].AreaConf.dataType)
                     {
                        case DatType.Bool:
                           ValSTR = ", dBool";
                           break;
                        case DatType.Byte:
                           ValSTR = ", dByte";
                           break;
                        case DatType.Word:
                           ValSTR = ", dWord";
                           break;
                        case DatType.DWord:
                           ValSTR = ", dDWord";
                           break;
                        case DatType.sDWord:
                           ValSTR = ", dsDWord";
                           break;
                        case DatType.Real:
                           ValSTR = ", dReal";
                           break;
                        case DatType.String:
                           ValSTR = ", dString";
                           break;
                        default:
                           Status.NewStat(StatT.Warning, "Wrong Data Type, Check Config DA.");
                           break;
                     }
                     //SQL Command String
                     STRcmd = "SELECT idDA, idValNum" + ValSTR + " FROM " + TBname + " WHERE idDA=" +
                         Data[i].AreaConf.ID.ToString("00") + " ORDER BY idValNum;";

                     //Create Command
                     cmd = new MySqlCommand(STRcmd, conn);
                     //Create a data reader and Execute the command
                     dataReader = cmd.ExecuteReader();

                     //Get the rows.
                     while (dataReader.Read())
                     {
                        j = int.Parse(dataReader["idValNum"] + ""); k++;

                        if ((int.Parse(dataReader["idDA"] + "") == Data[i].AreaConf.ID) &&
                            (Data[i].AreaConf.Amount > j) && (j >= 0))

                           switch (Data[i].AreaConf.dataType)
                           {
                              case DatType.Bool:
                                 Data[i].Data.dBoolean[j] = bool.Parse(dataReader["dBool"] + "");
                                 break;
                              case DatType.Byte:
                                 Data[i].Data.dByte[j] = byte.Parse(dataReader["dByte"] + "");
                                 break;
                              case DatType.Word:
                                 Data[i].Data.dWord[j] = ushort.Parse(dataReader["dWord"] + "");
                                 break;
                              case DatType.DWord:
                                 Data[i].Data.dWord[j] = ushort.Parse(dataReader["dDWord"] + "");
                                 break;
                              case DatType.sDWord:
                                 Data[i].Data.dDWord[j] = uint.Parse(dataReader["dsWord"] + "");
                                 break;
                              case DatType.Real:
                                 Data[i].Data.dsDWord[j] = int.Parse(dataReader["dReal"] + "");
                                 break;
                              case DatType.String:
                                 Data[i].Data.dString[j] = dataReader["dString"] + "";
                                 break;
                              default:
                                 Status.NewStat(StatT.Warning, "Wrong Data Type, Check Config DA.");
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
               if (k > 0)
               {
                  Status.NewStat(StatT.Good);
                  return true;
               }
            }
         } //END IF Data!=null
         else { Status.NewStat(StatT.Bad, "DataIn Corrupted."); }

         Status.NewStat(StatT.Warning, "Reading Data Failed.");
         return false;

      } // END Read function

      /// <summary>
      /// Write data to the database. 
      /// <param name="Data">Struct with the data to be written in the DB.</param>
      /// </summary>
      public bool Write(DriverComm.DataExtClass[] Data)
      {
         int i, j, k, initialLenght, numDA;
         string STRcmd, TBname, idNameSTR, caseSTR, whereSTR, timeSTR;
         DateTime DateObj;
         string[] STRcmdM;

         //Reset the Status Buffer
         Status.ResetStat();

         if (Data != null)
         {
            //Init
            numDA = Data.Length; k = 0;

            //Var for multiple writes
            STRcmdM = new string[numDA];

            for (i = 0; i < numDA; i++)
            {
               if ((Data[i] != null) && (Data[i].AreaConf != null))
               {
                  TBname = GetDVTbName(Data[i].AreaConf.ID_Driver);

                  //Initialize the descriptions only once.
                  if ((Data[i].VarNames != null) && (!Data[i].FirstInit))
                  {
                     caseSTR = ""; whereSTR = " WHERE IdName IN ( ";
                     initialLenght = whereSTR.Length + 1;
                     STRcmd = "UPDATE " + TBname + " SET Description = CASE IdName ";

                     for (j = 0; j < Data[i].AreaConf.Amount; j++)
                     {
                        if (Data[i].VarNames[j] != null)
                        {
                           idNameSTR = GetStrIdName(Data[i].AreaConf.ID_Driver, Data[i].AreaConf.ID, j);

                           caseSTR = caseSTR + " WHEN " + idNameSTR + " THEN " + "'" + Data[i].VarNames[j] + "' ";

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
                        if (!SQLcmdSingle(STRcmd))
                        {
                           Status.NewStat(StatT.Warning, "Failed to Init Symbolics/Descriptions.");
                        }
                     } //CaseSTR has data

                     Data[i].FirstInit = true; //Reset the flag
                  } //END If FirstWrite

                  // Write the data readed from the devices.
                  if (((Data.Length > i) && (Data[i].AreaConf.Enable)) && (!Data[i].AreaConf.Write))
                  {
                     caseSTR = ""; whereSTR = " WHERE IdName IN (";
                     STRcmdM[i] = "UPDATE " + TBname + " SET ";

                     switch (Data[i].AreaConf.dataType)
                     {
                        case DatType.Bool:
                           STRcmdM[i] = STRcmdM[i] + "dBool = CASE IdName ";
                           break;
                        case DatType.Byte:
                           STRcmdM[i] = STRcmdM[i] + "dByte = CASE IdName ";
                           break;
                        case DatType.Word:
                           STRcmdM[i] = STRcmdM[i] + "dWord = CASE IdName ";
                           break;
                        case DatType.DWord:
                           STRcmdM[i] = STRcmdM[i] + "dDWord = CASE IdName ";
                           break;
                        case DatType.sDWord:
                           STRcmdM[i] = STRcmdM[i] + "dsDWord = CASE IdName ";
                           break;
                        case DatType.Real:
                           STRcmdM[i] = STRcmdM[i] + "dReal = CASE IdName ";
                           break;
                        case DatType.String:
                           STRcmdM[i] = STRcmdM[i] + "dBool = CASE IdName ";
                           break;
                        default:
                           Status.NewStat(StatT.Warning, "Wrong Data Type, Check Config DA.");
                           break;
                     }

                     for (j = 0; j < Data[i].AreaConf.Amount; j++)
                     {
                        idNameSTR = GetStrIdName(Data[i].AreaConf.ID_Driver, Data[i].AreaConf.ID, j);

                        if (j == 0)
                        {
                           whereSTR = whereSTR + idNameSTR;
                        }
                        else
                        {
                           whereSTR = whereSTR + "," + idNameSTR;
                        }

                        caseSTR = caseSTR + " WHEN " + idNameSTR + " THEN ";

                        switch (Data[i].AreaConf.dataType)
                        {
                           case DatType.Bool:
                              caseSTR = caseSTR + "'" + Data[i].Data.dBoolean[j].ToString() + "'";
                              break;
                           case DatType.Byte:
                              caseSTR = caseSTR + Data[i].Data.dByte[j].ToString();
                              break;
                           case DatType.Word:
                              caseSTR = caseSTR + Data[i].Data.dWord[j].ToString();
                              break;
                           case DatType.DWord:
                              caseSTR = caseSTR + Data[i].Data.dDWord[j].ToString();
                              break;
                           case DatType.sDWord:
                              caseSTR = caseSTR + Data[i].Data.dsDWord[j].ToString();
                              break;
                           case DatType.Real:
                              if ((float.IsNaN(Data[i].Data.dReal[j])) || (float.IsInfinity(Data[i].Data.dReal[j])))
                                 Data[i].Data.dReal[j] = 0;
                              caseSTR = caseSTR + Data[i].Data.dReal[j].ToString();
                              break;
                           case DatType.String:
                              caseSTR = caseSTR + "'" + Data[i].Data.dString[j] + "'";
                              break;
                           default:
                              Status.NewStat(StatT.Warning, "Wrong Data Type, Check Config DA.");
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
                     DateObj = new DateTime(Data[i].NowTimeTicks, DateTimeKind.Utc);

                     timeSTR = DateObj.Year.ToString("0000") + "-" +
                         DateObj.Month.ToString("00") + "-" +
                         DateObj.Day.ToString("00") + " " +
                         DateObj.Hour.ToString("00") + ":" +
                         DateObj.Minute.ToString("00") + ":" +
                         DateObj.Second.ToString("00");

                     STRcmdM[i] = STRcmdM[i] + caseSTR + " END, TimeUpd='" + timeSTR + "'" + whereSTR + ");";
                     k++; //index counting the good Data-Areas

                  } // END IF write data readed.
               } //IF Data[i]!=null
            }// For cicle thru Data Areas

            //Send the query
            if (k > 0)
            {
               if (SQLcmdMult(STRcmdM, numDA))
               {
                  Status.NewStat(StatT.Good);
                  return true;
               }
            }

         }//END If Data!=null

         Status.NewStat(StatT.Warning, "Writing Data Failed.");
         return false;
      } //END Write function

      /// <summary>
      /// Disconnect from the database. </summary>
      public bool Disconnect()
      {
         try
         {
            if (conn.State != System.Data.ConnectionState.Closed) conn.Close();
            return true;
         }
         catch (MySqlException ex)
         {
            Status.NewStat(StatT.Bad, ex.Message);
         }
         return false;
      } //END DisConnect function

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
            catch (Exception ex)
            {
               Status.NewStat(StatT.Bad, ex.Message);
               return false;
            }
            return true;
         }
         else
         {
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
            try
            {
               //create command and assign the query and connection from the constructor
               MySqlCommand cmd = new MySqlCommand("", conn);

               if (querys.Length >= numQuerys)
                  for (i = 0; i < numQuerys; i++)
                  {
                     if (querys[i] != null)
                     {
                        //Execute command
                        cmd.CommandText = querys[i];
                        cmd.ExecuteNonQuery();
                     }
                  }

               //Dispose the CMD
               cmd.Dispose();
            }
            catch (Exception ex)
            {
               Status.NewStat(StatT.Bad, ex.Message);
               return false;
            }
            return true;
         }
         else
         {
            return false;
         }
      }

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
         catch (MySqlException ex)
         {
            Status.NewStat(StatT.Bad, ex.Message);
            return false;
         }
      } //END Connect function

      
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

   } //Close Class DB MySQL
} //Close Namespace
