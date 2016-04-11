using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This APP Namespace
using DriverCommApp.Conf;
using static DriverCommApp.DriverComm.DriverFunctions;

namespace DriverCommApp.Database
{
    static class DB_Functions
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

        /// <summary>
        /// Database Configuration Type Def.</summary>
        public struct DBConf
        {
            public ServerConf MasterServer;
            public ServerConf BackupServer;
            public SrvSelection SrvEn;
        }

        /// <summary>
        /// Struct for multithread database write.</summary>
        public struct DBStructData
        {
            public DataExt[] DataDV;
            public int numDA;

            public void InitWrite(DataExt[] DataObj, int nDA)
            {
                numDA = nDA;
                DataDV = DataObj;
            }
        }

        /// <summary>
        /// Struct for multithread Database Read.</summary>
        public struct DBStatStruct
        {
            public bool StatAllOK;
            public string statusMSG;
        }

        /// <summary>
        /// Driver Complete Configuration Type Def.</summary>
        public struct DrvConf
        {
            public CConf DriverConf;
            public AreaData[] AreaConf;
        }
    }
}
