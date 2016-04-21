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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.None'
            None = 0,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.None'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.MasterOnly'
            MasterOnly,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.MasterOnly'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.BackupOnly'
            BackupOnly,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.BackupOnly'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.BothSrv'
            BothSrv
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvSelection.BothSrv'
        }

        /// <summary>
        /// Database Server Configuration Type Def.</summary>
        public struct ServerConf
        {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Enable'
            public bool Enable;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Enable'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Rate'
            public float Rate;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Rate'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.URL'
            public string URL;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.URL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Username'
            public string Username;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Username'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Passwd'
            public string Passwd;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Passwd'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Port'
            public int Port;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Port'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Protocol'
            public DBConfig.DBServerProtocol Protocol;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Protocol'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Type'
            public DBConfig.DBServerType Type;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.Type'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.DBname'
            public string DBname; //Database name
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.DBname'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.HistLengh'
            public long HistLengh;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.ServerConf.HistLengh'
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.MasterSrv'
        public readonly ServerConf MasterSrv;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.MasterSrv'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.BackupSrv'
        public readonly ServerConf BackupSrv;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.BackupSrv'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvEn'
        public readonly SrvSelection SrvEn;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HistConfClass.SrvEn'

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
