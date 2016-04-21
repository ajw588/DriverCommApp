using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverCommApp.Stat
{
   class StatReport
   {
      /// <summary>
      /// Status Modes Definition.
      /// </summary>
      public enum StatT
      {
         Undefined,
         Good,
         Warning,
         Bad
      }

      /// <summary>
      /// Status ID Definition.
      /// </summary>
      public enum IDdef
      {
         Reserved = 0,
         Drv1 = 1, Drv2, Drv3, Drv4, Drv5, Drv6, Drv7, Drv8, Drv9, Drv10, Drv11,
         Drv12, Drv13, Drv14, Drv15, Drv16, Drv17, Drv18, Drv19, Drv20, DrvAll = 100,
         MainTr = 140, MainGui, AuxTr, AuxGui,
         DBall = 160, DB, DBbackup,
         Histall = 180, Hist, Histbackup,
         Collection = 200
      }

      /// <summary>
      /// Struct to keep a collection of reports. </summary>
      public struct ReportProgress
      {
         public StatT Stat;
         public long TimeLoop;
         public long TimeTicks;
         public IDdef ReportID;
         public string StatMsg;
      }

      /// <summary>
      /// ID for log object, Follow the Status ID definition.
      /// </summary>
      public IDdef IDMain;

      //Object private vars
      List<ReportProgress> ReportCollection;
      bool LogFileEN, UniqueID, FirstWrite;
      int ListMaxSize = 100, ListToFile = 80, ListMinSize = 30, TimmingIndex = 0;
      long[] TimmingsArray;
      StreamWriter outputFile;

      /// <summary>
      ///Parallel Block objects
      /// </summary>
      object LockList = new object();

      /// <summary>
      /// File Log Constructor. </summary>
      public StatReport(int ID, bool FileLog = false, bool UniqueID = false)
      {
         //Call the initiator
         InitReport(ID, FileLog, UniqueID);
      }

      /// <summary>
      /// File Log Initializator Constructor. </summary>
      private void InitReport(int ID, bool FileLog, bool UniqueIDCollection)
      {
         string PathFileLog;
         DateTime timestamp = DateTime.Now;

         //Main ID of Report.
         IDMain = (IDdef)ID;

         //Flag for the FileLog
         //Incompatible with the unique ID option
         if (UniqueIDCollection) { LogFileEN = false; }
         else { LogFileEN = FileLog; }

         //Flag for the UniqueID
         UniqueID = UniqueIDCollection;

         //Flag for the first write of the log file
         if (LogFileEN) FirstWrite = true;

         //Init the list
         ReportCollection = new List<ReportProgress>(ListMaxSize);

         //Init the timming variable.
         TimmingsArray = new long[((int)IDdef.Collection + 1)];

         if (LogFileEN)
         {
            // Set a variable to the My Documents path.
            string myapppath = Environment.CurrentDirectory + @"\Logs\";

            if (!Directory.Exists(myapppath))
               Directory.CreateDirectory(myapppath);

            PathFileLog = myapppath + IDMain.ToString() + "_" +
                timestamp.Day.ToString("00") + timestamp.Month.ToString("00") + timestamp.Year.ToString("0000") +
                "_" + timestamp.Hour.ToString("00") + timestamp.Minute.ToString("00") + timestamp.Second.ToString("00") +
                ".log";

            outputFile = new StreamWriter(PathFileLog, false, Encoding.UTF8);
         }

      }//END Constructor

      /// <summary>
      /// Reset the Status List. </summary>
      public void ResetStat()
      {
         lock (LockList)
         {
            ReportCollection.Clear();
         }
      }

      /// <summary>
      /// Method to Add a new Object to the list. </summary>
      public void AddSummary(ReportProgress Summary)
      {
         int cReports = 0;

         if (CheckCompatible(IDMain, Summary.ReportID))
            lock (LockList)
            {

               if (UniqueID)
                  if ((IDMain == IDdef.Collection) || (IDMain == IDdef.DrvAll) ||
                  (IDMain == IDdef.DBall) || (IDMain == IDdef.Histall))
                  {
                     //Only one type per ID is allowed
                     //Erase any other first.
                     ReportCollection.RemoveAll(h => h.ReportID == Summary.ReportID);
                  }

               //Add the new element to the list.
               ReportCollection.Add(Summary);

               //Add info to the Timming Array
               if (Summary.TimeLoop != 0)
               {
                  if (UniqueID)
                  {
                     TimmingsArray[(int)Summary.ReportID] = Summary.TimeLoop;
                  }
                  else
                  {
                     TimmingsArray[TimmingIndex] = Summary.TimeLoop;
                     TimmingIndex++;
                     if (TimmingIndex > ((int)Summary.ReportID)) TimmingIndex = 0;
                  }
               }

               cReports = ReportCollection.Count;

               //Remove excess.
               if (cReports > ListMaxSize) ReportCollection.TrimExcess();

               //Save the log file
               if (LogFileEN)
               {
                  if (cReports > ListToFile) WriteLog();
               }

               ReportCollection.OrderBy(Var => Var.TimeTicks);
            }
      }

      /// <summary>
      /// Method to Get the Status with ID Main. </summary>
      public ReportProgress GetSummary()
      {
         return GetSummary((int)IDMain);
      }

      /// <summary>
      /// Method to Get the Status. </summary>
      public ReportProgress GetSummary(int ID)
      {
         long[] TimeArray;
         ReportProgress RepoOut = new ReportProgress();

         //Init the RepoOut
         RepoOut.ReportID = (IDdef)ID;
         RepoOut.Stat = StatT.Undefined;
         RepoOut.StatMsg = string.Empty;


         lock (LockList)
         {
            foreach (ReportProgress Repo in ReportCollection)
            {
               if (CheckCompatible((IDdef)ID, Repo.ReportID))
               {
                  // Merge the reports.
                  RepoOut.TimeTicks = Repo.TimeTicks;
                  RepoOut.Stat = MergeStatus(RepoOut.Stat, Repo.Stat);
                  RepoOut.StatMsg += RepoToString(Repo);
               }

            }

            //Report Average of timmings.
            TimeArray = GetTimeLoops();
            RepoOut.TimeLoop = TimeArray[0];

         }// END Lock List

         return RepoOut;
      }

      /// <summary>
      /// Method to Add a new Status (Only Stat). </summary>
      public void NewStat(StatT IntStat)
      {
         long TimeTicks = DateTime.UtcNow.Ticks;
         long TimeLoop = 0;
         string MSGStat = string.Empty;
         NewStat(IntStat, MSGStat, (int)IDMain, TimeLoop, TimeTicks);
      }

      /// <summary>
      /// Method to Add a new Status (Only Stat and MSG). </summary>
      public void NewStat(StatT IntStat, string MSGStat)
      {
         long TimeTicks = DateTime.UtcNow.Ticks;
         long TimeLoop = 0;
         NewStat(IntStat, MSGStat, (int)IDMain, TimeLoop, TimeTicks);
      }

      /// <summary>
      /// Method to Add a new Status (Only Stat and TimeLoop). </summary>
      public void NewStat(StatT IntStat, long TimeLoop)
      {
         long TimeTicks = DateTime.UtcNow.Ticks;
         string MSGStat = string.Empty;
         NewStat(IntStat, MSGStat, (int)IDMain, TimeLoop, TimeTicks);
      }

      /// <summary>
      /// Method to Add a new Status (Auto Timing, Auto Main ID). </summary>
      public void NewStat(StatT IntStat, string MSGStat, long TimeLoop)
      {
         long TimeTicks = DateTime.UtcNow.Ticks;
         NewStat(IntStat, MSGStat, (int)IDMain, TimeLoop, TimeTicks);
      }

      /// <summary>
      /// Method to Add a new Status (Auto Timing, Cero LoopTime). </summary>
      public void NewStat(StatT IntStat, string MSGStat, int ID)
      {
         long TimeLoop = 0;
         long TimeTicks = DateTime.UtcNow.Ticks;
         NewStat(IntStat, MSGStat, ID, TimeLoop, TimeTicks);
      }

      /// <summary>
      /// Method to Add a new Status (Auto Timing). </summary>
      public void NewStat(StatT IntStat, string MSGStat, int ID, long TimeLoop)
      {
         long TimeTicks = DateTime.UtcNow.Ticks;
         NewStat(IntStat, MSGStat, ID, TimeLoop, TimeTicks);
      }

      /// <summary>
      /// Full Method to Add a new Status. </summary>
      public void NewStat(StatT IntStat, string MSGStat, int ID, long TimeLoop, long TimeTicks)
      {
         ReportProgress NewReport = new ReportProgress();

         //Fill the Object.
         NewReport.Stat = IntStat;
         NewReport.ReportID = (IDdef)ID;
         NewReport.StatMsg = MSGStat;
         NewReport.TimeLoop = TimeLoop;
         NewReport.TimeTicks = TimeTicks;

         //Add the report to the list.
         AddSummary(NewReport);
      }

      /// <summary>
      /// Returns True if Status is perfect. </summary>
      public bool isStatOk()
      {
         bool retVal = true;

         lock (LockList)
         {
            foreach (ReportProgress Repo in ReportCollection)
            {
               if (Repo.Stat != StatT.Good) retVal = false;
            }
         }

         return retVal;
      }

      /// <summary>
      /// Returns an array with the loop timmings. </summary>
      public long[] GetTimeLoops()
      {
         int i, cAvg; float AverageSum;
         List<long> Timings = new List<long>((int)IDdef.Collection);
         cAvg = 0; AverageSum = 0;
         lock (LockList)
         {
            //First Element is the Array Average.
            for (i = 0; i < ((int)IDdef.Collection); i++)
            {
               if (TimmingsArray[i] > 0)
               {
                  AverageSum += TimmingsArray[i];
                  cAvg++;
               }
            }

            if (cAvg <= 0) cAvg = 1;//To avoid diving by 0
            Timings.Add((long)(AverageSum / cAvg));

            //Now add the rest of elements.
            for (i = 1; i < ((int)IDdef.Collection); i++)
            {
               if (TimmingsArray[i] != 0) Timings.Add(TimmingsArray[i]);
            }
         }
         return Timings.ToArray();
      }

      /// <summary>
      /// Write Log File on Exit. 
      /// Call this function on exit of the program/thread 
      /// to write the remaining log lines.</summary>
      public void FlushLog()
      {
         if (LogFileEN)
            lock (LockList)
            {
               WriteLog();
               outputFile.Flush();
               //outputFile.Close();
               //LogFileEN = false;
            }
      }


      /// <summary>
      /// Write Log File. </summary>
      private void WriteLog()
      {
         //Don't lock the List, lock on the caller.
         int i, cReports, Start, Delete;
         long[] TimeArray;
         string toWrite;
         DateTime timestamp;
         cReports = ReportCollection.Count;

         //Where it start, 
         //if its not first write avoid the first elements left.
         if (FirstWrite)
         {
            Start = 0;
            FirstWrite = false;
         }
         else
         { Start = ListMinSize; }

         //How many to delete.
         Delete = cReports - ListMinSize;

         for (i = Start; i < cReports; i++)
         {
            //Save this registers to logFile.
            if (ReportCollection[i].StatMsg.Length > 3)
            {
               toWrite = RepoToString(ReportCollection[i]);
               outputFile.WriteLine(toWrite);
            }

         } //For Registers

         //Time Loop Info
         timestamp = new DateTime(ReportCollection.LastOrDefault().TimeTicks);
         TimeArray = GetTimeLoops();
         toWrite = "Average TimeLoop=  " + TimeArray[0].ToString() + " ms; " +
                     timestamp.ToShortDateString() + "_" + timestamp.ToShortTimeString();

         outputFile.WriteLine(toWrite);

         //Remove some items.
         if (Delete > 0)
            ReportCollection.RemoveRange(0, Delete);
      }

      /// <summary>
      /// Produce a string with the report Info. </summary>
      private static string RepoToString(ReportProgress RepoIn)
      {
         string toWrite = string.Empty;
         DateTime timestamp;

         if (RepoIn.StatMsg.Length > 3)
         {
            //Asing the TimeStamp
            timestamp = new DateTime(RepoIn.TimeTicks);

            //Generate the String.
            toWrite = RepoIn.Stat.ToString() + "; " +
                        RepoIn.ReportID.ToString() + "; " +
                        RepoIn.StatMsg + "; " +
                        RepoIn.TimeLoop.ToString() + "; " +
                        timestamp.ToShortDateString() + "_" +
                        timestamp.ToShortTimeString() + ";" + Environment.NewLine;
         }

         return toWrite;
      }

      /// <summary>
      /// Rules to merge the status. </summary>
      public static StatT MergeStatus(StatT Stat1, StatT Stat2)
      {
         StatT OutStat;

         //Default is Undefined.
         OutStat = StatT.Undefined;

         //Lower priority Good
         if ((Stat1 == StatT.Good) || (Stat2 == StatT.Good))
            OutStat = StatT.Good;

         if ((Stat1 == StatT.Warning) || (Stat2 == StatT.Warning))
            OutStat = StatT.Warning;

         //Highest Priority is Bad.
         if ((Stat1 == StatT.Bad) || (Stat2 == StatT.Bad))
            OutStat = StatT.Bad;

         return OutStat;
      }

      /// <summary>
      /// Returns True if IDs are compatible. </summary>
      private static bool CheckCompatible(IDdef CheckID1, IDdef CheckID2)
      {
         bool retVal = false;

         //If ID is collection anything is compatible.
         if (CheckID1 == IDdef.Collection) retVal = true;

         //If ID is collection anything is compatible.
         if (CheckID2 == IDdef.Collection) retVal = true;

         //If both are same type
         if (CheckID1 == CheckID2) retVal = true;

         //If MainTr, and GUI, (including Aux types) are compatible.
         if ((CheckID1 >= IDdef.MainTr) && (CheckID1 <= IDdef.AuxGui))
            if ((CheckID2 >= IDdef.MainTr) && (CheckID2 <= IDdef.AuxGui)) retVal = true;

         //If type is Generic Driver
         if ((CheckID1 == IDdef.DrvAll) &&
             ((CheckID2 >= IDdef.Drv1) && (CheckID2 <= IDdef.Drv20))) retVal = true;

         //If type is Generic Driver
         if ((CheckID2 == IDdef.DrvAll) &&
             ((CheckID1 >= IDdef.Drv1) && (CheckID1 <= IDdef.Drv20))) retVal = true;

         //If type is Generic Database
         if ((CheckID1 == IDdef.DBall) &&
             ((CheckID2 >= IDdef.DB) && (CheckID2 <= IDdef.DBbackup))) retVal = true;

         //If type is Generic Database
         if ((CheckID2 == IDdef.DBall) &&
             ((CheckID1 >= IDdef.DB) && (CheckID1 <= IDdef.DBbackup))) retVal = true;

         //If type is Generic Historics
         if ((CheckID1 == IDdef.Histall) &&
             ((CheckID2 >= IDdef.Hist) && (CheckID2 <= IDdef.Histbackup))) retVal = true;

         //If type is Generic Historics
         if ((CheckID2 == IDdef.Histall) &&
             ((CheckID1 >= IDdef.Hist) && (CheckID1 <= IDdef.Histbackup))) retVal = true;

         return retVal;
      }
   }
}
