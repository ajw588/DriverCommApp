using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace DriverCommApp.Conf
{

    /// <summary>
    /// This class loads the main config file, and call the child config class.</summary>
    /// <remarks>
    /// This class contains the child classes: DBConfig for the database 
    /// configuration, and DriverConfig for the driver configuration.
    /// </remarks>
    class ConfMain
    {
        /// <summary>
        /// Mark for the Class Object Initialization.</summary>
        public bool isInitialized = false;

        /// <summary>
        /// Definition for the Main Configuration container.</summary>
        public struct MainSettings
        {
            /// <summary>
            /// Application Status:
            /// 0->Disabled, 1->Main App, 2-> First Slave, 3-> Second Slave. </summary>
            public int AppDef;

            /// <summary>
            /// Timing for the base loop.</summary>
            public int RTBaseLoop;

            /// <summary>
            /// Conf files path.</summary>
            public string ConfFolder;
            public string DBConfPath;
            public string DriverConfPath;

            /// <summary>
            /// Path for the App location.</summary>
            public string appPath;

            /// <summary>
            /// InitialSet, mark for App and Database initialization.</summary>
            public bool InitialSet;

            /// <summary>
            /// Main Conf word to enable the Historics.</summary>
            public bool EnHistorics;
        }

        /// <summary>
        /// Object for the Main Configuration. </summary>
        public MainSettings MainConf;

        /// <summary>
        /// Object for the Driver Configuration. </summary>
        public DV.DriverConfig ConfDriver;

        /// <summary>
        /// Object for the Database Configuration. </summary>
        public DB.DBConfig ConfDB;

        /// <summary>
        /// The class constructor. </summary>
        public ConfMain()
        {
            int ret_var = 0;

            MainConf = new MainSettings();

            ret_var = LoadMainConfig();

            // Load the Driver Config File
            ConfDriver = new DV.DriverConfig(MainConf.DriverConfPath);

            // Load the Database Config File
            ConfDB = new DB.DBConfig(MainConf.DBConfPath);

            if ((ret_var > 0) && (ConfDriver.isInitialized) && (ConfDB.isInitialized))
            {
                isInitialized = true;
            }
            else
            {
                isInitialized = false;
            }

        } //END Class Constructor

        /// <summary>
        /// Loads the Main configuration into the Object. 
        /// Return: 1 if All ok, Less than 0 if Error </summary>
        private int LoadMainConfig()
        {
            var appSettings = ConfigurationManager.AppSettings;

            //Reset initial values
            MainConf.InitialSet = false;
            MainConf.EnHistorics = false;
            MainConf.AppDef = 0;
            MainConf.RTBaseLoop = 0;

            if (appSettings.Count < 2)
            {
                //ERRROR: File seems Empty.
                return -1;
            }
            else
            {
                MainConf.AppDef = int.Parse(appSettings["AppDef"]);
                MainConf.RTBaseLoop = int.Parse(appSettings["RTBaseLoop"]);
                MainConf.InitialSet = bool.Parse(appSettings["InitialSet"]);
                MainConf.EnHistorics = bool.Parse(appSettings["HistoricsEn"]);
            }

            //Get the installation directory
            MainConf.appPath = AppDomain.CurrentDomain.BaseDirectory;

            //Get the config files directory
            MainConf.ConfFolder = ReadConfFilePath(MainConf.appPath, "ConfFolder") + "\\";
            if (MainConf.ConfFolder.Length < 4) return -2;

            // Get the full path for the config files.
            MainConf.DBConfPath = ReadConfFilePath(MainConf.ConfFolder, "DBConf");
            MainConf.DriverConfPath = ReadConfFilePath(MainConf.ConfFolder, "DriverConf");
            if (MainConf.DBConfPath.Length < 4) return -2;
            if (MainConf.DriverConfPath.Length < 4) return -2;

            return 1;
        }

        /// <summary>
        /// This function reads the ConfFiles section in the
        ///  app.config file and return the path for the conf files. </summary>
        ///  <param name="appPath"> Application Initial Path, the configuration 
        ///  file folder should be in a lower level.</param>
        /// <param name="filekey"> An ID for the file requested.</param>
        /// <returns>
        /// Returns the full path on the file requested.</returns>
        string ReadConfFilePath(string appPath, string filekey)
        {
            try
            {
                string filepath;
                var ConfigFilesSection = ConfigurationManager.GetSection("ConfFiles") as
                    System.Collections.Specialized.NameValueCollection;

                if (ConfigFilesSection != null)
                {
                    filepath = appPath + ConfigFilesSection[filekey].ToString();
                    return filepath;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (ConfigurationErrorsException)
            {
                return "Err";
            }
        }

        /// <summary>
        /// This function reads the appSettings section in the
        ///  app.config file and return the key requested. </summary>
        /// <param name="key"> An ID for the key requested.</param>
        /// <returns>
        /// Returns the key requested as string.</returns>
        string ReadAppSetting(string key)
        {

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? string.Empty;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return "Err";
            }
        }


        /// <summary>
        /// This function writes to the appSettings 
        /// section in the app.config file. </summary>
        /// <param name="key"> An ID for the key requested.</param>
        /// <param name="value"> String char to update.</param>
        /// <returns>
        /// Returns the key requested as string.</returns>
        public bool WriteAppSetting(string key, string value)
        {
            try
            {
                ConfigurationManager.AppSettings.Set(key, value);

                Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                configuration.AppSettings.Settings[key].Value = value;
                configuration.Save();

                ConfigurationManager.RefreshSection("appSettings");


                return true;
            }
            catch (ConfigurationErrorsException)
            {
                return false;
            }
        }

        /// <summary>
        /// The class Destructor. </summary>
        ~ConfMain()
        {
            //MainConf = null;
            ConfDriver = null;
            ConfDB = null;
        }

    } //END Class ConfMain
} //END NameSpace.
