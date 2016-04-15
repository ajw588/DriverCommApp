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

        /// <summary>
        /// Generate the String for the ID Name of a Tag/Variable
        /// </summary>
        /// <param name="IdDV"> ID of the Driver this variable belows to</param>
        /// <param name="IdDA">ID of the Data Area Block this variable belows to</param>
        /// <param name="var">Variable number in the Data Block (Starts at cero-0)</param>
        /// <returns>String with the variable ID</returns>
        static public string GetStrIdName(int IdDV, int IdDA, int var)
        {
            if (IdDV < 0) IdDV = 0;
            if (IdDA < 0) IdDA = 0;
            if (var < 0) var = 0;

            return IdDV.ToString("00") +
                   IdDA.ToString("00") + var.ToString("0000");

        }

        /// <summary>
        /// Generate an Integer for the ID Name of a Tag/Variable
        /// </summary>
        /// <param name="IdDV"> ID of the Driver this variable belows to</param>
        /// <param name="IdDA">ID of the Data Area Block this variable belows to</param>
        /// <param name="var">Variable number in the Data Block (Starts at cero-0)</param>
        /// <returns>Integer with the variable ID</returns>
        static public int GetIntIdName(int IdDV, int IdDA, int var)
        {
            string IdName;

            if (IdDV < 0) IdDV = 0;
            if (IdDA < 0) IdDA = 0;
            if (var < 0) var = 0;

            IdName = IdDV.ToString("00") +
              IdDA.ToString("00") + var.ToString("0000");
            return int.Parse(IdName);
        }


        /// <summary>
        /// Generate the name of the Table for the Actual Values
        /// </summary>
        /// <param name="IdDV">ID of the Driver</param>
        /// <returns>String with the name of the table</returns>
        static public string GetDVTbName(int IdDV)
        {
            if (IdDV < 0) IdDV = 0;

            return "Drv" + IdDV.ToString("00");
        }
    }
}
