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
   /// <summary>
   /// This Class uses a pool of connections, 
   /// making it thread safe.</summary>
   class DB_MySQLPool : IDisposable
   {
      /// <summary>
      /// Database Server Configuration.</summary>
      DBConfClass.ServerConf DBConfig;

      /// <summary>
      /// Status Object.</summary>
      public Stat.StatReport Status;

      /// <summary>
      /// Connection String Object.</summary>
      string myConnectionString;

      /// <summary>
      /// Initialization flag.</summary>
      public bool isInitialized;

      /// <summary>
      /// Class Constructor.
      /// <param name="Server">Server configuration struct.</param> 
      /// <param name="iamMaster">Defines if its Master=true, for the reporting</param></summary>
      public DB_MySQLPool(DBConfClass.ServerConf Server, bool iamMaster)
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

         int retVal = 0;

         isInitialized = false;

         //Reset the Status Buffer
         Status.ResetStat();

         //myConnectionString = "server=127.0.0.1;uid=root;" + "pwd=12345;database=test;";
         myConnectionString = "SERVER=" + DBConfig.URL + ";" + "PORT=" + DBConfig.Port.ToString() + ";" +
             "PROTOCOL=" + DBConfig.Protocol.ToString().ToLower() + ";" + "DATABASE=" + DBConfig.DBname + ";" +
             "UID=" + DBConfig.Username + ";" + "PASSWORD=" + DBConfig.Passwd + ";" +
             "ConnectionTimeout=5; DefaultCommandTimeout=5; Keepalive=3;" +
             "Pooling=true; MinimumPoolSize=2; MaximumPoolsize=20; ConnectionReset=false;"+
             "ConnectionLifeTime=1800; CacheServerProperties=true;";

         try
         {
            MySqlConnection conn = new MySqlConnection(myConnectionString);

            conn.Open(); //Test the connection

            if (conn.State == System.Data.ConnectionState.Open)
            {
               conn.Close(); //Disconnect
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
            else
            {
               Status.NewStat(StatT.Bad, "Database Connection Failed...");
            }
         } catch(Exception e)
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
                "PRIMARY KEY  (IdAuto), UNIQUE KEY (IdName) ) ENGINE = INNODB;";
            if (!SQLcmdSingle(STRcmd)) return -1;

            //Create registers for each Data Area variables
            for (j = 0; j < (aDriverConfig.DVConf.NDataAreas); j++)
            {
               valStr = "";
               if (aDriverConfig.DAConf.Length > j)
               {
                  for (k = 0; k < (aDriverConfig.DAConf[j].Amount - 1); k++)
                  {
                     idNameSTR = aDriverConfig.DAConf[j].ID_Driver.ToString("00") +
                         aDriverConfig.DAConf[j].ID.ToString("00") + k.ToString("0000");

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

      /// <summary>
      /// Send a single command to the database.
      /// <param name="query">String with the SQL chain and data.</param> </summary>
      private bool SQLcmdSingle(string query)
      {
         //Default Param
         MySqlParameter[] Param = new MySqlParameter[1];
         Param[0] = new MySqlParameter();

         try
         {
            //Execute command
            if (MySqlHelper.ExecuteNonQuery(myConnectionString, query, Param) < 0)
               return false;
         }
         catch (Exception ex)
         {
            Status.NewStat(StatT.Bad, ex.Message);
            return false;
         }
         return true;
      } // End SQLcmdSingle

      /// <summary>
      /// Disconnect from the database. </summary>
      public bool Disconnect()
      {
         try
         {
            //TODO: Force close all the connections.
            return true;
         }
         catch (MySqlException ex)
         {
            Status.NewStat(StatT.Bad, ex.Message);
         }
         return false;
      } //END DisConnect function

      /// <summary>
      /// Read data from the database. 
      /// <param name="Data">Struct ref to object to save data.</param>
      /// </summary>
      public bool Read(DriverComm.DataExtClass[] Data)
      {
         int i, j, numDA, k;
         string STRcmd, TBname, ValSTR;
         MySqlDataReader dataReader;

         //Reset the Status Buffer
         Status.ResetStat();

         if (Data != null)
         {
            //Init
            numDA = Data.Length;
            k = 0;


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

                  using (dataReader = MySqlHelper.ExecuteReader(myConnectionString, STRcmd))
                  {
                     //Get the rows.
                     if (dataReader.HasRows)
                        while (dataReader.Read())
                        {
                           j = int.Parse(dataReader["idValNum"] + ""); k++;

                           if ((int.Parse(dataReader["idDA"] + "") == Data[i].AreaConf.ID) &&
                               (Data[i].AreaConf.Amount > j) && (j >= 0))

                              switch (Data[i].AreaConf.dataType)
                              {
                                 case DatType.Bool:
                                    if (Data[i].Data.dBoolean.Length > j)
                                       Data[i].Data.dBoolean[j] = bool.Parse(dataReader["dBool"] + "");
                                    break;
                                 case DatType.Byte:
                                    if (Data[i].Data.dByte.Length > j)
                                       Data[i].Data.dByte[j] = byte.Parse(dataReader["dByte"] + "");
                                    break;
                                 case DatType.Word:
                                    if (Data[i].Data.dWord.Length > j)
                                       Data[i].Data.dWord[j] = ushort.Parse(dataReader["dWord"] + "");
                                    break;
                                 case DatType.DWord:
                                    if (Data[i].Data.dWord.Length > j)
                                       Data[i].Data.dWord[j] = ushort.Parse(dataReader["dDWord"] + "");
                                    break;
                                 case DatType.sDWord:
                                    if (Data[i].Data.dDWord.Length > j)
                                       Data[i].Data.dDWord[j] = uint.Parse(dataReader["dsWord"] + "");
                                    break;
                                 case DatType.Real:
                                    if (Data[i].Data.dsDWord.Length > j)
                                       Data[i].Data.dsDWord[j] = int.Parse(dataReader["dReal"] + "");
                                    break;
                                 case DatType.String:
                                    if (Data[i].Data.dString.Length > j)
                                       Data[i].Data.dString[j] = dataReader["dString"] + "";
                                    break;
                                 default:
                                    Status.NewStat(StatT.Warning, "Wrong Data Type, Check Config DA.");
                                    break;
                              }
                        } // END While dataReader

                  }//END Using

               } //IF Data Area is to be Readed from DB and Write to device.

               if (k > 0) return true;
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
         bool isGood = false;
         int i, j, initialLenght, numDA;
         string STRcmd, CMDjoin, TBname, idNameSTR, caseSTR, whereSTR, timeSTR;
         DateTime DateObj;

         //Reset the Status Buffer
         Status.ResetStat();

         if (Data != null)
         {
            //Init
            numDA = Data.Length;
            CMDjoin = string.Empty;

            for (i = 0; i < numDA; i++)
            {
               if (Data[i] != null)
               {
                  TBname = GetDVTbName(Data[i].AreaConf.ID_Driver);

                  //Initialize the descriptions only once.
                  if ((!Data[i].FirstInit) && (Data[i].VarNames != null))
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
                           Data[i].FirstInit = true; //Reset the flag}
                        }
                     }
                  } //END If FirstWrite

                  // Write the data readed from the devices.
                  if (((Data.Length > i) && (Data[i].AreaConf.Enable)) && (!Data[i].AreaConf.Write))
                  {
                     caseSTR = ""; whereSTR = " WHERE IdName IN (";
                     STRcmd = "UPDATE " + TBname + " SET ";

                     switch (Data[i].AreaConf.dataType)
                     {
                        case DatType.Bool:
                           STRcmd += "dBool = CASE IdName ";
                           break;
                        case DatType.Byte:
                           STRcmd += "dByte = CASE IdName ";
                           break;
                        case DatType.Word:
                           STRcmd += "dWord = CASE IdName ";
                           break;
                        case DatType.DWord:
                           STRcmd += "dDWord = CASE IdName ";
                           break;
                        case DatType.sDWord:
                           STRcmd += "dsDWord = CASE IdName ";
                           break;
                        case DatType.Real:
                           STRcmd += "dReal = CASE IdName ";
                           break;
                        case DatType.String:
                           STRcmd += "dBool = CASE IdName ";
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

                     STRcmd += caseSTR + " END, TimeUpd='" + timeSTR + "'" + whereSTR + ");";

                     CMDjoin +=" " + STRcmd;

                     //In case is becoming too big
                     if (CMDjoin.Length > 200000)
                     {
                        isGood=SQLcmdSingle(CMDjoin);
                        CMDjoin = string.Empty;
                     }

                  } // END IF write data readed.
               } //IF Data[i]!=null
            }// For cicle thru Data Areas

            //Send the query
            if (CMDjoin.Length  > 3)
               isGood=SQLcmdSingle(CMDjoin);

            //Check how everything went.
            if (isGood)
            {
               Status.NewStat(StatT.Good);
               return true;
            }

         }//END If Data!=null

         Status.NewStat(StatT.Warning, "Writing Data Failed.");
         return false;
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
