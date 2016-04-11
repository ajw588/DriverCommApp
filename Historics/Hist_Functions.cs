using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This APP Namespace
using DriverCommApp.Conf;
using static DriverCommApp.DriverComm.DriverFunctions;

namespace DriverCommApp.Historics
{
    static class Hist_Functions
    {

        /// <summary>
        /// Historics Database Server Configuration Type Def.</summary>
        public enum HSrvSelection
        {
            None = 0,
            MasterOnly,
            BackupOnly,
            BothSrv
        }

        /// <summary>
        /// Historics Database Server Configuration Type Def.</summary>
        public struct HServerConf
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

        /// <summary>
        /// Database Configuration Type Def.</summary>
        public struct HDBConf
        {
            public HServerConf MasterServer;
            public HServerConf BackupServer;
            public HSrvSelection SrvEn;
        }

        /// <summary>
        /// Struct for multithread database write.
        /// </summary>
        public struct DBWriteStruct
        {
            public DataExt[] DataWrite;
            public int numDA;
            public bool StatAllOK;
            public string statusMSG;

            public void InitWrite(DataExt[] DataObj, int nDA)
            {
                numDA = nDA;
                DataWrite = DataObj;
            }
        }

        /// <summary>
        /// Struct for multithread Database Read.</summary>
        public struct StatStruct
        {
            public bool StatAllOK;
            public string statusMSG;
        }

        /*
         * ########## Deprecated ##############
        /// <summary>
        /// TBName and Type Struct.</summary>
        private struct TableInit
        {
            public DB_Main.DrvConf DVConf;
            public string TBName;

            public void SetTBConfig(DB_Main.DrvConf DriverConfiguration, string Table_Name)
            {
                DVConf = DriverConfiguration;
                TBName = Table_Name;
            }
        }
        */

        /// <summary>
        /// Driver Complete Configuration Type Def.</summary>
        public struct DrvHConf
        {
            public CConf DriverConf;
            public AreaData[] AreaConf;
        }

        
    }
}
