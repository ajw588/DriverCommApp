﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DriverCommApp
{
    public partial class MainScreen : Form
    {
        /// <summary>
        /// Struct to report the Worker status to the GUI. </summary>
        public struct WorkerProgress
        {
            public string LoopTime;
            public int DVint, DBint;
            public string DBstat, DVstat;
        }

        //My Working Thread
        //https://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker(v=vs.110).aspx
        private BackgroundWorker bgnWorker1;

        /// <summary>
        /// Object for the MainLoop Work. </summary>
        MainCycle ObjMainWork;


        /// <summary>
        /// Flag of Runing Work Loop. </summary>
        public bool RuningWork;

        /// <summary>
        /// Command to Exit the App. </summary>
        public bool ExitCMD;

        /// <summary>
        /// Class Constructor. </summary>
        public MainScreen()
        {
            RuningWork = false;

            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "MainGUI";

            //GUI Components.
            InitializeComponent();

            //Start the Workerfor the first time.
            StartBackgroundWorker();

        }

        /// <summary>
        /// Button to Stop/Start the Main Loop. </summary>
        private void BTN_Stop_Click(object sender, EventArgs e)
        {
            //Flip text and Start/Stop the Worker.
            if (RuningWork)
            {
                bgnWorker1.CancelAsync();
                BTN_Stop.Text = "Start";

            }
            else
            {
                //Start the work
                StartBackgroundWorker();
                BTN_Stop.Text = "Stop";
            }

        }
        /// <summary>
        /// Button to Exit the App. </summary>
        private void BTN_Exit_Click(object sender, EventArgs e)
        {
            ExitCMD = false;
            if (MessageBox.Show("Exit application?", "", MessageBoxButtons.YesNo) ==
                    DialogResult.Yes) { ExitCMD = true; }
            else { ExitCMD = false; }

            if (ExitCMD)
            {
                if (RuningWork) { bgnWorker1.CancelAsync(); }
                else { ExitApp(); }
            }
        }

        /// <summary>
        /// Initialize the thread to run the work loop. </summary>
        private bool InitializeBackgroundWorker()
        {
            
            long msCycle;
            DateTime initialTime, finalTime;
            WorkerProgress ToReport;

            initialTime = DateTime.Now;

            //Initial Message
            ToReport = new WorkerProgress();
            ToReport.DVstat = "Initializing..."; ToReport.DBstat = "Initializing...";
            ToReport.DVint = 1; ToReport.DBint = 1; ToReport.LoopTime = "000";

            //Update the GUI
            UpdGUI(ToReport);

            //Create the Background worker.
            bgnWorker1 = new BackgroundWorker();

            bgnWorker1.DoWork +=
                new DoWorkEventHandler(bgnWorker1_DoWork);
            bgnWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            bgnWorker1_RunWorkerCompleted);
            bgnWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            bgnWorker1_ProgressChanged);

            //Properties
            bgnWorker1.WorkerReportsProgress = true;
            bgnWorker1.WorkerSupportsCancellation = true;

            

            finalTime = DateTime.Now;
            msCycle = ((finalTime.Ticks - initialTime.Ticks) / TimeSpan.TicksPerMillisecond);
            ToReport.LoopTime = msCycle.ToString();

            //Update the GUI
            UpdGUI(ToReport);

            //finalize
            return true;
        }

        /// <summary>
        /// Start the thread runing the work loop. </summary>
        private void StartBackgroundWorker()
        {
            //Check if a Background Worker has been launched already
            if (!RuningWork)
                //Initialize the Worker for the Work Loop.
                if (InitializeBackgroundWorker())
                {
                    // Start the asynchronous operation.               
                    bgnWorker1.RunWorkerAsync();

                    RuningWork = true;

                }
        }

        /// <summary>
        /// This event handler is where the actual,
        /// potentially time-consuming work is done.</summary>
        private void bgnWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
            int retVal;
            long msSpan;
            WorkerProgress ToReport;
            DateTime initialTime, finalTime;

            //Get time
            initialTime = DateTime.Now;

            //Initialize
            ToReport = new WorkerProgress();

            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            //Name the Worker
            if (Thread.CurrentThread.Name == null)
                Thread.CurrentThread.Name = "MainWorkerCicle";

            //Create Cicle Object
            ObjMainWork = new MainCycle();

            //Initialize the Drivers and Database.
            retVal = ObjMainWork.Initialize();

            //Collect Status Information
            if (retVal != 0)
            {
                ToReport.DVstat = ObjMainWork.StatDVMain.StatMSG;
                ToReport.DBstat = ObjMainWork.StatDBMain.StatMSG;
                ToReport.DVint = (int)ObjMainWork.StatDVMain.StatInt;
                ToReport.DBint = (int)ObjMainWork.StatDBMain.StatInt;
            }
            else
            {
                ToReport.DVstat = ObjMainWork.StatDVMain.StatMSG; ToReport.DVint = 1;
                ToReport.DBstat = ObjMainWork.StatDBMain.StatMSG; ToReport.DBint = 1;
            }

            //Get the time span
            finalTime = DateTime.Now;
            msSpan = ((finalTime.Ticks - initialTime.Ticks) / TimeSpan.TicksPerMillisecond);
            ToReport.LoopTime = msSpan.ToString();

            worker.ReportProgress(0, ToReport);

            //Start the work
            if (ObjMainWork.isInitialized)
            {
                // Do the Cicle
                DoTheCicle(worker, e);
            }
            else
            {
                ToReport.DVstat = "Failed: check for error messages."; ToReport.DVint = 3;
                ToReport.DBstat = "Application failed to initialize, and won't continue."; ToReport.DBint = 3;
                UpdGUI(ToReport);
            }

            
        }

        /// <summary>
        /// This event handler deals with finalizing the
        /// background operation. </summary>
        private void bgnWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            int WaitCount;

            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);

            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                if (!ExitCMD)
                    MessageBox.Show("Work loop cancelled by user request.");

            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                if (!ExitCMD)
                    MessageBox.Show("Work loop finalized.");
            }


            //If needed Call Stop All Workers Again and wait
            WaitCount = 0;
            if (ObjMainWork.WorkersRuning)
                while (WaitCount < 100)
                {
                    ObjMainWork.StopWorkers();
                    if (ObjMainWork.WorkersRuning) WaitCount = 1000;
                    WaitCount++;
                }

            //Close all comms
            ObjMainWork.CloseAll();

            //Worker is no longer runing.
            BTN_Stop.Text = "Start";
            RuningWork = false;

            if (ExitCMD) { ExitApp(); }
        }

        // This event handler updates.
        private void bgnWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            WorkerProgress StatReport;

            //Get the report data
            StatReport = (WorkerProgress)e.UserState;

            //Update the GUI
            UpdGUI(StatReport);

        }

        /// <summary>
        /// Does the Driver-Database communcation cicle. </summary>
        private void DoTheCicle(BackgroundWorker Worker, DoWorkEventArgs e)
        {
            int i, msCycle, ToSleep;
            WorkerProgress ToReport;
            DateTime initialTime, finalTime;
            long msSpan;


            //Initialize variables
            ToReport = new WorkerProgress();
            msSpan = 0;  msCycle = 500; i = 0;

            while (!e.Cancel)
            {
                //Get time
                initialTime = DateTime.Now;

                if (i > 99) { i = 0; } else { i++; }
                if (Worker.CancellationPending)
                {
                    //Stop All Workers
                    ObjMainWork.StopWorkers();
                    e.Cancel = true;
                }
                else
                {
                    //Start Workers if not done
                    if (!ObjMainWork.WorkersRuning) ObjMainWork.StartWork();

                    //Update the GUI
                    if (ObjMainWork.WorkersRuning)
                    {
                        ToReport.DVstat = ObjMainWork.StatDVMain.StatMSG;
                        ToReport.DBstat = ObjMainWork.StatDBMain.StatMSG;

                        ToReport.DVint = (int)ObjMainWork.StatDVMain.StatInt;
                        ToReport.DBint = (int)ObjMainWork.StatDBMain.StatInt;

                        ToReport.LoopTime = " ";
                        ObjMainWork.StatDVMain.ID_Time[0] = msSpan; //Main Cycle
                        foreach (int valms in ObjMainWork.StatDVMain.ID_Time)
                            ToReport.LoopTime = ToReport.LoopTime + " / " + valms.ToString();
                    }
                    else
                    {
                        ToReport.DBstat = "Workers Stopped"; ToReport.DVint = 1;
                        ToReport.DVstat = "Workers Stopped"; ToReport.DBint = 1;
                        ToReport.LoopTime = "000 000 000 000";
                    }

                    Worker.ReportProgress(i, ToReport);

                    //Sleep for the rest of the time cicle.
                    finalTime = DateTime.Now;
                    msSpan = ((finalTime.Ticks - initialTime.Ticks) / TimeSpan.TicksPerMillisecond);
                    ToSleep=(int) (msCycle- msSpan);

                    if (ToSleep>5) Thread.Sleep(ToSleep);
                }
            } //While not Cancelation

        } //END DoTheCicle Function

        /// <summary>
        /// Update the GUI. </summary>
        private void UpdGUI(WorkerProgress StatReport)
        {
            int strCopyLenght, txtMaxLenght;
            //Loop Times
            LBL_valueloop.Text = StatReport.LoopTime.ToString() + " ms";

            txtMaxLenght = 32766;
            //Driver Status Messages
            if (StatReport.DVstat.Length> txtMaxLenght)
            {
                strCopyLenght = StatReport.DVstat.Length - txtMaxLenght;
                LBL_COMMStat.Text = StatReport.DVstat.Substring(strCopyLenght);
            } else
            {
                LBL_COMMStat.Text = StatReport.DVstat;
            }

            //Database Status Messages
            if (StatReport.DBstat.Length> txtMaxLenght)
            {
                strCopyLenght = StatReport.DBstat.Length - txtMaxLenght;
                LBL_DBStat.Text = StatReport.DBstat.Substring(strCopyLenght);
            } else
            {
                LBL_DBStat.Text = StatReport.DBstat;
            }

            //Scroll to end of Text
            //LBL_DBStat.Focus();
            LBL_COMMStat.Select(LBL_COMMStat.Text.Length, 0);
            LBL_COMMStat.ScrollToCaret();

            //LBL_DBStat.Focus();
            LBL_DBStat.Select(LBL_DBStat.Text.Length, 0);
            LBL_DBStat.ScrollToCaret();

            switch (StatReport.DVint)
            {
                case 0:
                    //Undefined
                    panelDV.BackColor = Color.Black;
                    break;
                case 1:
                    //Good
                    panelDV.BackColor = Color.Green;
                    break;
                case 2:
                    //Warning
                    panelDV.BackColor = Color.Yellow;
                    break;
                case 3:
                    //Bad
                    panelDV.BackColor = Color.Red;
                    break;
            }

            switch (StatReport.DBint)
            {
                case 0:
                    //Undefined
                    panelDB.BackColor = Color.Black;
                    break;
                case 1:
                    //Good
                    panelDB.BackColor = Color.Green;
                    break;
                case 2:
                    //Warning
                    panelDB.BackColor = Color.Yellow;
                    break;
                case 3:
                    //Bad
                    panelDB.BackColor = Color.Red;
                    break;
            }
        }

        /// <summary>
        /// Close and Exit the App. </summary>
        private void ExitApp()
        {

            Application.Exit();
        }

    } // END Class
}