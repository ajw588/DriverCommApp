using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;



namespace DriverCommApp.Conf.DB
{
    /// <summary>
    /// This class loads the settings configured in the DB.config file.</summary>
    /// <remarks>
    /// This class contains the child classes: ServerMain, 
    /// ServerBackup and Database for the definition of the database config file sections.
    /// </remarks>
    public class DBConfig
    {

        /// <summary>
        /// Definition of the DB Server Type.</summary>
        public enum DBServerType
        {
            ///<summary> Undefined Server Type, or Wrong Config</summary>
            Undefined = 0,
            /// <summary> MySQL Type Database Server</summary>
            MySQL,
            /// <summary> Fully Complaint Generic SQL Type Database Server</summary>
            SQL,
            /// <summary> Maria DB Type Database Server</summary>
            Maria,
            /// <summary> Postgre SQL Type Database Server</summary>
            Postgre,
            /// <summary> Micrsoft SQL Type Database Server</summary>
            Microsoft
        }

        /// <summary>
        /// Definition of the DB Server Protocol.</summary>
        public enum DBServerProtocol
        {
            /// <summary> Undefined Protocol or Wrong Config</summary>
            Undefined = 0,
            /// <summary> TCP Communication Protocol</summary>
            TCP,
            /// <summary> Direct Socket Comm Protocol</summary>
            SOCKET,
            /// <summary> Pipe Type Protocol</summary>
            PIPE,
            /// <summary> Memory exchange area communication</summary>
            MEMORY
        }

        /// <summary>
        /// Mark for the Class Object Initialization.</summary>
        public bool isInitialized = false;

        /// <summary>
        /// Database Main Server Configuration.</summary>
        public ServerSect MainServer;

        /// <summary>
        /// Database Backup Server Configuration.</summary>
        public ServerSect BackupServer;

        /// <summary>
        /// Master Historics Settings and Configuration.</summary>
        public HistoricsSett HistMaster;

        /// <summary>
        /// Backup Historics Settings and Configuration.</summary>
        public HistoricsSett HistBackup;

        /// <summary>
        /// Internal variables to map the custom DB config file.</summary>
        private ExeConfigurationFileMap DBconfigMap;
        private Configuration DBconfigFile;

        /// <summary>
        /// The class constructor. </summary>
        ///  <param name="fileconfig">Database config filepath.</param>
        public DBConfig(string fileconfig)
        {
            //reset the flag
            isInitialized = false;

            try
            {
                //Change ConfigurationManager file to the DBConfig Path.
                DBconfigMap = new ExeConfigurationFileMap();
                DBconfigMap.ExeConfigFilename = fileconfig;
                DBconfigFile = ConfigurationManager.OpenMappedExeConfiguration(DBconfigMap, ConfigurationUserLevel.None);

                //Open the server definitions in the DB config file.
                //MainServer = new ServerSect("ServerMain", DBconfigFile);
                MainServer = (ServerSect) DBconfigFile.GetSection("ServerMain");
                //BackupServer = new ServerSect("ServerBackup", DBconfigFile);
                BackupServer = (ServerSect) DBconfigFile.GetSection("ServerBackup");

                //Initialize the History Object Config.
                //HistMaster = new HistoricsSett("HistoricsMaster", DBconfigFile);
                HistMaster = (HistoricsSett) DBconfigFile.GetSection("HistoricsMaster");
                HistBackup = (HistoricsSett) DBconfigFile.GetSection("HistoricsBackup");

                if ( (MainServer!=null) && (HistMaster!=null) ) isInitialized = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// The class Destructor. </summary>
        ~DBConfig()
        {
            //Clear objects.
            MainServer = null;
            BackupServer = null;
            HistMaster = null;
            HistBackup = null;
            DBconfigMap = null;
            DBconfigFile = null;

            //Final
            isInitialized = false;
        }
    }

    /// <summary>
    /// This class defines the ServerMain section for the DB Configuration.</summary>
    public class ServerSect : ConfigurationSection
    {

        /// <summary>
        /// Variable to retrieve the section parameters. </summary>
        private static ServerSect settings;

        /// <summary>
        /// The class default constructor. </summary>
        public ServerSect()
        {
            settings = null;
        }

        /// <summary>
        /// The class constructor. 
        /// Requires to specify the section to be opened. </summary>
        /// <param name="sect"> Section to Open in the Config File.</param>
        /// <param name="DBconfigFile"> Configuration file mapped.</param>
        public ServerSect(string sect, Configuration DBconfigFile)
        {
            settings = DBconfigFile.GetSection(sect) as ServerSect;
        }

        /// <summary>
        /// The class destructor. </summary>
        ~ServerSect()
        {
            settings = null;
        }

        /// <summary>
        /// Property to open all the settings collection. </summary>
        public static ServerSect Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Property for the Server URL locator. </summary>
        [ConfigurationProperty("URL", DefaultValue = "123", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string URL
        {
            get { return (string)this["URL"]; }
            set { this["URL"] = value; }
        }

        /// <summary>
        /// Property for the Server port. </summary>
        [ConfigurationProperty("Port", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }

        /// <summary>
        /// Property for the Server port identifier. </summary>
        [ConfigurationProperty("Protocol", DefaultValue = DBConfig.DBServerProtocol.Undefined, IsKey = false, IsRequired = false)]
        public DBConfig.DBServerProtocol Protocol
        {
            get { return (DBConfig.DBServerProtocol)this["Protocol"]; }
            set { this["Protocol"] = value; }
        }

        /// <summary>
        /// Property for the Server type selector. </summary>
        [ConfigurationProperty("Type", DefaultValue = DBConfig.DBServerType.Undefined, IsKey = false, IsRequired = false)]
        public DBConfig.DBServerType Type
        {
            get { return (DBConfig.DBServerType)this["Type"]; }
            set { this["Type"] = value; }
        }

        /// <summary>
        /// Property for the Server Username. </summary>
        [ConfigurationProperty("User1", DefaultValue = "none", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string User1
        {
            get { return (string)this["User1"]; }
            set { this["User1"] = value; }
        }

        /// <summary>
        /// Property for the Server Password. </summary>
        [ConfigurationProperty("Passw1", DefaultValue = "none", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string Passw1
        {
            get { return (string)this["Passw1"]; }
            set { this["Passw1"] = value; }
        }

        /// <summary>
        /// Property for the Database name. </summary>
        [ConfigurationProperty("DBname", DefaultValue = "DatabaseName", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string DBname
        {
            get { return (string)this["DBname"]; }
            set { this["DBname"] = value; }
        }

        /// <summary>
        /// Property for the Enable of the Server beign configured. </summary>
        [ConfigurationProperty("Enable", DefaultValue = false, IsKey = false, IsRequired = false)]
        public bool Enable
        {
            get { return (bool)this["Enable"]; }
            set { this["Enable"] = value; }
        }

    } //END ServerSect Class

    /// <summary>
    /// This class defines the Database section for the DB Configuration.</summary>
    public class HistoricsSett : ConfigurationSection
    {
        /// <summary>
        /// Variable to retrieve the section parameters. </summary>
        private static HistoricsSett settings;

        /// <summary>
        /// The class default constructor. </summary>
        public HistoricsSett()
        {
            settings = null;
        }

        /// <summary>
        /// The class constructor. 
        /// Requires to specify the section to be opened. </summary>
        /// <param name="sect"> Section to Open in the Config File.</param>
        /// <param name="DBconfigFile"> Configuration file mapped.</param>
        public HistoricsSett(string sect, Configuration DBconfigFile)
        {
            settings = DBconfigFile.GetSection(sect) as HistoricsSett;
        }

        /// <summary>
        /// The class destructor. </summary>
        ~HistoricsSett()
        {
            settings = null;
        }

        /// <summary>
        /// Property to open all the settings collection. </summary>
        public static HistoricsSett Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Property for the Server URL locator. </summary>
        [ConfigurationProperty("URL", DefaultValue = "123", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string URL
        {
            get { return (string)this["URL"]; }
            set { this["URL"] = value; }
        }

        /// <summary>
        /// Property for the Server port. </summary>
        [ConfigurationProperty("Port", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }

        /// <summary>
        /// Property for the Server port identifier. </summary>
        [ConfigurationProperty("Protocol", DefaultValue = DBConfig.DBServerProtocol.Undefined, IsKey = false, IsRequired = false)]
        public DBConfig.DBServerProtocol Protocol
        {
            get { return (DBConfig.DBServerProtocol)this["Protocol"]; }
            set { this["Protocol"] = value; }
        }

        /// <summary>
        /// Property for the Server type selector. </summary>
        [ConfigurationProperty("Type", DefaultValue = DBConfig.DBServerType.Undefined, IsKey = false, IsRequired = false)]
        public DBConfig.DBServerType Type
        {
            get { return (DBConfig.DBServerType)this["Type"]; }
            set { this["Type"] = value; }
        }

        /// <summary>
        /// Property for the Server Username. </summary>
        [ConfigurationProperty("User1", DefaultValue = "none", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string User1
        {
            get { return (string)this["User1"]; }
            set { this["User1"] = value; }
        }

        /// <summary>
        /// Property for the Server Password. </summary>
        [ConfigurationProperty("Passw1", DefaultValue = "none", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string Passw1
        {
            get { return (string)this["Passw1"]; }
            set { this["Passw1"] = value; }
        }

        /// <summary>
        /// Property for the Database name. </summary>
        [ConfigurationProperty("DBname", DefaultValue = "DatabaseName", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string DBname
        {
            get { return (string)this["DBname"]; }
            set { this["DBname"] = value; }
        }

        /// <summary>
        /// Property for the Enable of the BackupServer beign configured. </summary>
        [ConfigurationProperty("Enable", DefaultValue = false, IsKey = false, IsRequired = false)]
        public bool Enable
        {
            get { return (bool)this["Enable"]; }
            set { this["Enable"] = value; }
        }

        /// <summary>
        /// Property for the Historics Write Rate.
        /// The Rate is a multiplier between 0 to 1 and its based in the app RealTime cycle.</summary>
        [ConfigurationProperty("Rate", DefaultValue = "1.0", IsKey = false, IsRequired = false)]
        public float Rate
        {
            get
            {
                float tempVal = (float) this["Rate"];
                // Check the boundary
                if ((tempVal > 0.0) && (tempVal <= 1.0))
                {
                    return tempVal;
                }
                else
                {
                    tempVal = (float) 1.0;
                    return tempVal;
                }
            } //End Get

            set { this["Rate"] = value; }
        }

        /// <summary>
        /// Property for the Historics Write Rate.
        /// The Rate is a multiplier between 0 to 1 and its based in the app RealTime cicle.
        /// Maximun = 10 years @100ms </summary>
        [ConfigurationProperty("HistLenght", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 3651)]
        public int HistLenght
        {
            get { return (int) this["HistLenght"]; }
            set { this["HistLenght"] = value; }
        }
    }

}
