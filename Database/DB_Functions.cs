using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This APP Namespace
using DriverCommApp.Conf;
using DriverCommApp.DriverComm;

namespace DriverCommApp.Database
{
    /// <summary>
    /// Database Configuration Type Def.</summary>
    public class DBConfClass
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
            public string URL;
            public string Username;
            public string Passwd;
            public int Port;
            public DBConfig.DBServerProtocol Protocol;
            public DBConfig.DBServerType type;
            public string DBname; //Database name
            public bool Enable;
        }

        public readonly ServerConf MasterSrv;
        public readonly ServerConf BackupSrv;
        public readonly SrvSelection SrvEn;


        /// <summary>
        /// Main Cttor.</summary>
        public DBConfClass(ServerConf Master, ServerConf Backup)
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


    static class DB_Functions
    {
        

        
    }
}
