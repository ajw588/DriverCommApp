using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This APP Namespace
using DriverCommApp.Conf.DB;
using static DriverCommApp.DriverComm.DriverFunctions;


namespace DriverCommApp.Historics
{
    /// <summary>
    /// Database Configuration Type Def.</summary>
    public class HistConfClass
    {
        /// <summary>
        /// Database Server Selection.</summary>
        public enum SrvSelection
        {
            None = 0,
            MasterOnly,
            BackupOnly,
            BothSrv
        }

        /// <summary>
        /// Database Server Configuration Type Def.</summary>
        public struct ServerConf
        {
            public bool Enable;
            public float Rate;
            public string URL;
            public string Username;
            public string Passwd;
            public int Port;
            public DBConfig.DBServerProtocol Protocol;
            public DBConfig.DBServerType Type;
            public string DBname; //Database name
            public long HistLengh;
        }

        public readonly ServerConf MasterSrv;
        public readonly ServerConf BackupSrv;
        public readonly SrvSelection SrvEn;

        /// <summary>
        /// Main Cttor.</summary>
        public HistConfClass(ServerConf Master, ServerConf Backup)
        {
            MasterSrv = Master;
            BackupSrv = Backup;

            SrvEn = SrvSelection.None;

            if (Backup.Enable)
                SrvEn = SrvSelection.BackupOnly;

            if (Master.Enable)
                SrvEn = SrvSelection.MasterOnly;

            if (Master.Enable && Backup.Enable)
                SrvEn = SrvSelection.BothSrv;
        }
    }

    /// <summary>
    /// Static Functions and Definitions for the Historics.</summary>
    static class Hist_Functions
    {
        /// <summary>
        /// Number of elements in the queue, set from 20 to 200.</summary>
        public static readonly int MaxQueueElements = 100;

        /// <summary>
        /// Generate the name of the Table for the Actual Values
        /// </summary>
        /// <param name="IdDV">ID of the Driver</param>
        /// <param name="IdDA">ID of the Data Area Block</param>
        /// <returns>String with the name of the table</returns>
        static public string GetHistTbName(int IdDV, int IdDA)
        {
            if (IdDV < 0) IdDV = 0;
            if (IdDA < 0) IdDA = 0;

            return "HistDrv_" + IdDV.ToString("00") + IdDA.ToString("00");
        }

    }
}
