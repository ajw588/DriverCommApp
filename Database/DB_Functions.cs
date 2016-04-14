using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This APP Namespace
using DriverCommApp.Conf.DB;
using DriverCommApp.DriverComm;

namespace DriverCommApp.Database
{
    /// <summary>
    /// Database Configuration Type Def.</summary>
    public class DBConfClass
    {
        /// <summary>
        /// Database Server Active Selection.</summary>
        public enum SrvSelection
        {
            /// <summary> Active Selection of Servers: None Selected</summary>
            None = 0,
            /// <summary> Active Selection of Servers: Only Master is Active</summary>
            MasterOnly,
            /// <summary> Active Selection of Servers: Only Backup is Active</summary>
            BackupOnly,
            /// <summary> Active Selection of Servers: Both Servers Active</summary>
            BothSrv
        }

        /// <summary>
        /// Database Server Configuration Type Def.</summary>
        public struct ServerConf
        {
            /// <summary> IP Address or URL to locate the server</summary>
            public string URL;
            /// <summary> Database Username for login </summary>
            public string Username;
            /// <summary> Database Username Password</summary>
            public string Passwd;
            /// <summary> Database Communication Server Port</summary>
            public int Port;
            /// <summary> Database Server Protocol</summary>
            public DBConfig.DBServerProtocol Protocol;
            /// <summary> Database Server Type</summary>
            public DBConfig.DBServerType Type;
            /// <summary> Database Name</summary>
            public string DBname;
            /// <summary> Database Server Enable=True property</summary>
            public bool Enable;
        }

        /// <summary> Database Master Server Configuration</summary>
        public readonly ServerConf MasterSrv;
        /// <summary> Database Backup Server Configuration</summary>
        public readonly ServerConf BackupSrv;
        /// <summary> Active Selection of Servers</summary>
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
