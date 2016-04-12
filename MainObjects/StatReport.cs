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
            DBall = 200, DB, DBbackup,
            Histall = 300, Hist, Histbackup,
            Collection = 1000
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
        bool LogFileEN, FirstWrite;
        StreamWriter outputFile;
        int ListMaxSize = 100, ListToFile = 60, ListMinSize = 10;


        /// <summary>
        ///Parallel Block objects
        /// </summary>
        object LockList = new object();

        /// <summary>
        /// File Log Constructor. </summary>
        public StatReport(int ID, bool FileLog)
        {
            string PathFileLog;
            DateTime timestamp = DateTime.Now;

            //Main ID of Report.
            IDMain = (IDdef)ID;

            //Flag for the FileLog
            LogFileEN = FileLog;
            FirstWrite = true;

            //Init the list
            ReportCollection = new List<ReportProgress>(ListMaxSize);

            // Set a variable to the My Documents path.
            string myapppath = Environment.CurrentDirectory + @"\Logs\";

            if (LogFileEN)
            {
                if (!Directory.Exists(myapppath))
                    Directory.CreateDirectory(myapppath);

                PathFileLog = myapppath + IDMain.ToString() + "_" +
                    timestamp.ToShortDateString() + "_" + timestamp.ToShortTimeString() + ".log";

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
                    if ( (IDMain==IDdef.Collection) || (IDMain == IDdef.DrvAll) ||
                        (IDMain == IDdef.DBall) || (IDMain == IDdef.Histall))
                    {
                        //Only one type per ID is allowed
                        //Erase any other first.
                        ReportCollection.RemoveAll(h => h.ReportID == Summary.ReportID);
                    }
                    ReportCollection.Add(Summary);
                    
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

        //Method to Get the Status with ID Main
        public ReportProgress GetSummary()
        {
            return GetSummary((int)IDMain);
        }

        //Method to Get the Status
        public ReportProgress GetSummary(int ID)
        {
            ReportProgress RepoOut = new ReportProgress();
            lock (LockList)
            {
                foreach (ReportProgress Repo in ReportCollection)
                {
                    if (CheckCompatible((IDdef)ID, Repo.ReportID))
                    {
                        // Merge the reports.

                    }

                }

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
        /// Returns True if Status is perfect. </summary>
        public string GetTimeLoops()
        {
            string Timings;

            lock (LockList)
            {

            }
            return Timings;
        }

        /// <summary>
        /// Write Log File. </summary>
        private void WriteLog()
        {
            //Don't lock the List, lock on the caller.
            int i, cReports, Start, Delete;
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
                    timestamp = new DateTime(ReportCollection[i].TimeTicks);

                    toWrite = ReportCollection[i].Stat.ToString() + "; " +
                        ReportCollection[i].ReportID.ToString() + "; " +
                        ReportCollection[i].StatMsg + "; " +
                        ReportCollection[i].TimeLoop.ToString() + "; " +
                        timestamp.ToShortDateString() + "_" + timestamp.ToShortTimeString();

                    outputFile.WriteLine(toWrite);
                }

            } //For Registers

            //Remove some items.
            ReportCollection.RemoveRange(0, Delete);
        }

        /// <summary>
        /// Returns True if Status is perfect. </summary>
        private bool CheckCompatible(IDdef CheckID1, IDdef CheckID2)
        {
            bool retVal = false;

            //If ID is collection anything is compatible.
            if (CheckID1 == IDdef.Collection) retVal = true;

            //If ID is collection anything is compatible.
            if (CheckID2 == IDdef.Collection) retVal = true;

            //If both are same type
            if (CheckID1 == CheckID2) retVal = true;

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
