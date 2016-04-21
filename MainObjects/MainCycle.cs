using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

//This APP Namespace
using DriverCommApp.Conf;
using DriverCommApp.DriverComm;
using DriverCommApp.Database;
using DriverCommApp.Historics;
using StatT = DriverCommApp.Stat.StatReport.StatT;

namespace DriverCommApp
{
   class MainCycle
   {
      /// <summary>
      /// Configuration Object.
      /// </summary>
      private ConfMain theConfig;

      /// <summary>
      /// Driver Collection Object.
      /// </summary>
      private DriverGeneric[] theDriver;

      /// <summary>
      /// Driver Collection Object.
      /// </summary>
      private BackgroundWorker[] WorkersCollection;

      /// <summary>
      /// Database Object.
      /// </summary>
      private DB_Main theDatabase;

      /// <summary>
      /// Historics Object.
      /// </summary>
      private HistoricsMain theHistorics;

      /// <summary>
      /// Historics Enabled Flag.
      /// </summary>
      private bool EnHistorics;

      /// <summary>
      /// Flag Initialized.
      /// </summary>
      public bool isInitialized;

      /// <summary>
      /// Workers Running Flag.
      /// </summary>
      public bool WorkersRuning;

      /// <summary>
      /// Main Cycle Status.
      /// </summary>
      public Stat.StatReport Status;

      /// <summary>
      /// Collection of Status.
      /// </summary>
      public Stat.StatReport StatusColl;

      /// <summary>
      /// Number of drives configurated.
      /// </summary>
      private int NumDrivers;

      /// <summary>
      /// Number of drives Running.
      /// </summary>
      private int NumDvRun;

      /// <summary>
      ///Parallel Block objects
      /// </summary>
      object LockDBRead = new object();
      object LockDBWrite = new object();

      /// <summary>
      /// Main Cycle constructor.
      /// </summary>
      public MainCycle()
      {
         //Initialize to some value.
         NumDrivers = 0;

         //Create Status Object and Reinitialize MSG for Status
         Status = new Stat.StatReport((int)Stat.StatReport.IDdef.MainTr, FileLog: true);
         Status.ResetStat();

         //Status Collection Object
         StatusColl = new Stat.StatReport((int)Stat.StatReport.IDdef.Collection, UniqueID: true);
         StatusColl.ResetStat();

         //Build the Config objects for Database and Historics
         theConfig = new ConfMain();

         if (theConfig.isInitialized)
         {
            Status.NewStat(StatT.Good, "Configuration is Loaded");
         }
         else
         {
            Status.NewStat(StatT.Bad, "Bad: Configuration NOT Loaded");
         }

         //Reset the flags
         isInitialized = false;
         WorkersRuning = false;
         EnHistorics = false;
      }

      ///<summary>
      /// Initialize the Drivers and Database objects.
      ///</summary>
      public int Initialize()
      {
         int i, RTime;
         string StatusMSG;

         //Init Flags
         isInitialized = false; EnHistorics = false;

         if (theConfig.isInitialized && (!WorkersRuning))
         {
            NumDrivers = theConfig.ConfDriver.GeneralSett.cDrivers;
            if (NumDrivers > 0)
            {
               if (theConfig.MainConf.RTBaseLoop > 49) { RTime = theConfig.MainConf.RTBaseLoop; }
               else { RTime = 50; }

               //Create the Objects for the Drivers
               theDriver = new DriverGeneric[NumDrivers];

               Status.NewStat(StatT.Good, "Creating " + NumDrivers.ToString() + " Drivers.");

               for (i = 0; i < NumDrivers; i++)
               { theDriver[i] = new DriverGeneric((i + 1), theConfig.ConfDriver); }

               theDatabase = new DB_Main(theConfig.ConfDB);
               if (theConfig.MainConf.EnHistorics) theHistorics = new HistoricsMain(theConfig.ConfDB);

               //Initialize the Drivers, and add it to the Database manager
               for (i = 0; i < NumDrivers; i++)
               {
                  if (theDriver[i].thisDriverConf.Enable) theDriver[i].Initialize();
                  if (theDriver[i].isInitialized)
                  {
                     StatusMSG = "Driver " + (i + 1).ToString("00") + " is initialized";
                     Status.NewStat(StatT.Good, StatusMSG);

                     //Add the Driver to the database manager
                     if (theDatabase.addDriver(theDriver[i].thisDriverConf, theDriver[i].thisAreaConf) < 0)
                     {
                        StatusMSG = "Bad: Driver " + (i + 1).ToString("00") + " NOT added to Database manager";
                        Status.NewStat(StatT.Bad, StatusMSG);
                        theDriver[i].isInitialized = false;
                        return -4;
                     }
                     else
                     {
                        //If adding to Database succeeded, then add it to the historics.
                        if (theConfig.MainConf.EnHistorics)
                           theHistorics.addDriver(theDriver[i].thisDriverConf, theDriver[i].thisAreaConf);
                     }
                  }
                  else
                  {
                     StatusMSG = "Bad: Driver " + (i + 1).ToString("00") + " Failed to initialize";
                     Status.NewStat(StatT.Warning, StatusMSG);
                     if (theDriver[i].thisDriverConf.Enable) return -5;
                  }

                  //Update the Status Collection.
                  StatusColl.AddSummary(theDriver[i].Status.GetSummary());

               } // END FOR Drivers

               //Initialize the Database
               theDatabase.Initialize(theConfig.MainConf.InitialSet);
               if (theDatabase.isInitialized)
               {
                  StatusMSG = "Database is initialized";
                  Status.NewStat(StatT.Good, StatusMSG);
                  isInitialized = true;
               }
               else
               {
                  StatusMSG = "Bad: Database Failed to initialize";
                  Status.NewStat(StatT.Bad, StatusMSG);
                  isInitialized = false;
                  return -7;
               }

               //Initialize the Historics if enabled.
               if (theConfig.MainConf.EnHistorics)
               {
                  theHistorics.Initialize(theConfig.MainConf.InitialSet);

                  if (theHistorics.isInitialized)
                  {
                     StatusMSG = "Historics are initialized";
                     Status.NewStat(StatT.Good, StatusMSG);
                     EnHistorics = true;
                  }
                  else
                  {
                     StatusMSG = "Bad: Historics Failed to initialize";
                     Status.NewStat(StatT.Bad, StatusMSG);
                  }
               } //END Historics Initialization.

            } //Num Drivers >0

            //Reset the InitialSet in the Config if it was set.
            if (theConfig.MainConf.InitialSet) theConfig.WriteAppSetting("InitialSet", "False");

         }// If Config isInitialized and No WorkersRunning

         return 0;
      } // END Initialize Function.

      ///<summary>
      /// Start the cyclic work
      /// One worker for each driver
      /// </summary>
      public int StartWork()
      {
         int i;

         if (isInitialized && (!WorkersRuning))
         {

            if (EnHistorics)
            {
               theHistorics.CleanHist();
               theHistorics.StartWork();
            }

            //Create the Background workers.
            WorkersCollection = new BackgroundWorker[NumDrivers];

            for (i = 0; i < NumDrivers; i++)
            {
               WorkersCollection[i] = new BackgroundWorker();
               WorkersCollection[i].DoWork +=
                       new DoWorkEventHandler(Worker_DoWork);
               WorkersCollection[i].RunWorkerCompleted +=
                       new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
               WorkersCollection[i].ProgressChanged +=
                       new ProgressChangedEventHandler(Worker_ProgressChanged);

               //Properties
               WorkersCollection[i].WorkerReportsProgress = true;
               WorkersCollection[i].WorkerSupportsCancellation = true;

               WorkersCollection[i].RunWorkerAsync(theDriver[i]);
            }

            NumDvRun = NumDrivers;
            if (i > 0) WorkersRuning = true;

            return i;
         }

         return -1;

      }// END Function StartWork

      ///<summary>
      /// Stop the Workers.
      /// </summary>
      public int StopWorkers()
      {
         int i;
         if (WorkersRuning)
         {
            i = 0;
            foreach (BackgroundWorker Worker in WorkersCollection)
            {
               Worker.CancelAsync();
               Status.NewStat(StatT.Good, "Stopping worker for Driver " + (i + 1).ToString());
               i++;
            }

            if (EnHistorics) theHistorics.StopWork();

            Thread.Sleep(500); // Wait for the Threads Finishing

            if (NumDvRun <= 0) WorkersRuning = false;
            return i;
         }
         return -1;
      } //END Stop Workers Function.

      ///<summary>
      /// Update the Status Collection.
      /// </summary>
      public void UpdStatus()
      {
         StatusColl.AddSummary(Status.GetSummary());

         if (isInitialized)
         {
            StatusColl.AddSummary(theDatabase.Status.GetSummary());

            if (EnHistorics)
               StatusColl.AddSummary(theHistorics.Status.GetSummary());
         }
      }

      ///<summary>
      /// Stop all the Workers, and
      /// Close all Drivers Comms.
      /// </summary>
      public int CloseAll()
      {

         try
         {
            if (isInitialized)
            {
               
            }
         }
         catch (Exception e)
         {
            Status.NewStat(StatT.Bad, e.Message);
         }

         return 0;
      }


      /// <summary>
      /// This event handler is where the actual,
      /// potentially time-consuming work is done.
      /// Database Read -> Drivers Read and Write -> Database Write.
      /// </summary>
      private void Worker_DoWork(object sender,
          DoWorkEventArgs e)
      {
         bool finishWork = false;
         int i, msLeft;
         DateTime initialTime, finalTime;
         long msCycle;
         Stat.StatReport.ReportProgress ToReport;

         //Get the Driver for this worker.
         DriverGeneric thisDriver = (DriverGeneric)e.Argument;

         // Get the BackgroundWorker that raised this event.
         BackgroundWorker worker = sender as BackgroundWorker;

         //Name the Worker
         if (Thread.CurrentThread.Name == null)
            Thread.CurrentThread.Name = "CycleDriver" + thisDriver.thisDriverConf.ID.ToString("00");

         //Initialize variables
         msCycle = 0; i = 0;

         while (!(e.Cancel || finishWork))
         {
            //Get time
            initialTime = DateTime.Now;

            if (i > 99) { i = 0; } else { i++; }
            if (worker.CancellationPending)
            {
               finishWork = true;
            }
            else
            {
               //Do Operations.
               if (!DoCycle(thisDriver))
               {
                  //Fatal Error, Cancel the Worker
                  e.Cancel = true;
               }

               //Sleep for the rest of the time cicle.
               finalTime = DateTime.Now;
               msCycle = ((finalTime.Ticks - initialTime.Ticks) / TimeSpan.TicksPerMillisecond);

               if (msCycle < thisDriver.thisDriverConf.CycleTime)
               {
                  msLeft = (int)(thisDriver.thisDriverConf.CycleTime - msCycle);
                  Thread.Sleep(msLeft);
                  ToReport.StatMsg = "";
               }
               else
               {
                  //Cicle is taking too much time!!!
                  //Only report if its 15% higger than limit.
                  if (msCycle > (thisDriver.thisDriverConf.CycleTime * 1.15))
                     thisDriver.Status.NewStat(StatT.Warning, "Main Cycle taking too long, " + msCycle.ToString() +
                             " ms, and it should be less than " + thisDriver.thisDriverConf.CycleTime.ToString() + " ms");
               }

               //Get the Driver Status Report.
               ToReport = thisDriver.Status.GetSummary();
               ToReport.TimeLoop = msCycle;

               worker.ReportProgress(i, ToReport);
            }
         } //While not Cancelation

         //Send the Driver as a result.
         e.Result = thisDriver;

      }//END Function DoWork

      private bool DoCycle(DriverGeneric thisDriver)
      {
         bool DVreadOK, DVwriteOK, DBreadOK, DBwriteOK;
         int cRecon;

         //Init Flags
         DVreadOK = false; DVwriteOK = false;
         DBreadOK = false; DBwriteOK = false;

         try
         {
            if (thisDriver.isInitialized)
            {
               //*********************************************************
               //Read the Database
               //*********************************************************
               if (thisDriver.iamWriting)
               {
                  lock (LockDBRead)
                  {
                     //Read the Database
                     DBreadOK = theDatabase.ReadDB(thisDriver.ExtData);
                  } //END LockDBRead
               }

               //*********************************************************
               //Connect to the driver
               //*********************************************************

               //Counter for reconnections
               cRecon = 0;

               Connect:
               if (!thisDriver.isConnected)
               {
                  thisDriver.Connect();
                  if (!thisDriver.isConnected)
                  {
                     //Driver Conn Error
                     if (cRecon >= 3)
                        thisDriver.Status.NewStat(StatT.Bad, "Error Conn to Driver, 3 retries");

                     //Try reconnect.
                     if (cRecon < 3)
                     {
                        cRecon++;

                        thisDriver.Disconnect();
                        thisDriver.Status.NewStat(StatT.Warning, "Reconnecting to Driver");

                        //Wait a bit to get the driver disconnected and re-try
                        Thread.Sleep(thisDriver.thisDriverConf.Timeout);

                        goto Connect;
                     }
                  }
               }

               //*********************************************************
               //Read and Write data to the Driver.
               //*********************************************************
               if (thisDriver.isConnected)
               {
                  //Write to the Driver
                  if (thisDriver.iamWriting && DBreadOK)
                  {
                     DVwriteOK = thisDriver.Write();
                     if ((!DVwriteOK) && (cRecon < 3))
                     {
                        cRecon++;
                        thisDriver.Disconnect();
                        goto Connect;
                     }
                  } //Write Driver

                  //Read from the Driver
                  if (thisDriver.iamReading)
                  {
                     DVreadOK = thisDriver.Read();
                     if ((!DVreadOK) && (cRecon < 3))
                     {
                        //Error Reading
                        cRecon++;
                        thisDriver.Disconnect();
                        goto Connect;
                     }
                  } //Read Driver

               } // If Driver is connected.

               //*********************************************************
               //Write to the Database
               //*********************************************************
               if (thisDriver.iamReading && DVreadOK)
               {
                  lock (LockDBWrite)
                  {
                     //Write the Database
                     DBwriteOK = theDatabase.WriteDB(thisDriver.ExtData);
                  }

                  //*********************************************************
                  //Write to the Historics
                  //*********************************************************
                  if (EnHistorics)
                  {
                     theHistorics.NewPackage(thisDriver.ExtData);
                  }
               } //END If Reading

            }// IF the Driver is Initialized
            else
            {
               //Driver is not initialized, end the worker to save resources.
               return false;
            }
         }
         catch (Exception e)
         {
            thisDriver.Status.NewStat(StatT.Bad, e.Message);
            return false;
         }

         return true;

      } // END Do Cicle Function

      /// <summary>
      /// This event handler deals with finalizing the
      /// background operation. </summary>
      private void Worker_RunWorkerCompleted(
          object sender, RunWorkerCompletedEventArgs e)
      {
         

         // First, handle the case where an exception was thrown.
         if (e.Error != null)
         {

         }
         else if (e.Cancelled)
         {
            // Next, handle the case where the user canceled 
            // the operation.
            // Note that due to a race condition in 
            // the DoWork event handler, the Cancelled
            // flag may not have been set, even though
            // CancelAsync was called.

         }
         else
         {
            // Finally, handle the case where the operation 
            // succeeded.
            DriverGeneric thisDriver = (DriverGeneric)e.Result;
            thisDriver.CloseAll();
            Status.NewStat(StatT.Good, "Disconnected Driver " + thisDriver.thisDriverConf.ID.ToString());
         }

         //Driver Worker Finished and removed from counter.
         NumDvRun--;

         //The last driver to finish close the Database.
         if (NumDvRun<=0) theDatabase.CloseALL();

      }

      ///<summary>
      /// This event handles the Worker updates.
      /// </summary>
      private void Worker_ProgressChanged(object sender,
          ProgressChangedEventArgs e)
      {
         Stat.StatReport.ReportProgress StatReport;

         //Get the report data
         StatReport = (Stat.StatReport.ReportProgress)e.UserState;

         StatusColl.AddSummary(StatReport);
      }

      ///<summary>
      /// --Destructor--
      /// Close all Drivers and Database Comms.
      /// </summary>
      ~MainCycle()
      {
         //Close All comms
         CloseAll();

         //Destroy the Child objects
         if (theDatabase != null) theDatabase.Dispose();
         if (theHistorics != null) theHistorics.Dispose();

         theDriver = null;
         WorkersCollection = null;
         theDatabase = null;
         theHistorics = null;

         //Status Objects
         Status = null;
         StatusColl = null;

      }

   }
}
