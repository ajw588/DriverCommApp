using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace DriverCommApp.Conf
{
    /// <summary>
    /// Driver Configuration Class.
    /// This Class reads and holds all the drivers configuration,
    /// Its based on the file DriverComm.config.</summary>
    public class DriverConfig
    {
        /// <summary>
        /// Definition of the driver type.</summary>
        public enum DriverType
        {
            Undefined=0,
            XWave,
            S7_TCP,
            ModbusTCP,
            ModbusRTU,
            AB_Eth,
            Verga
        }

        /// <summary>
        /// Definition of the data areas type.</summary>
        public enum DatType
        {
            Undefined = 0,
            Bool,
            Byte,
            Word,
            DWord,
            sDWord,
            Real,
            String
        }

        /// <summary>
        /// Definition of the RTU Serial port.</summary>
        public enum RTUCommPort
        {
            Undefined = 0,
            COM1, COM2, COM3, COM4, COM5,
            COM6, COM7, COM8, COM9
        }

        /// <summary>
        /// Struct for the amount of vars by type.</summary>
        public struct nVars
        {
            /// <summary>
            /// Total amount of vars, data-areas (total), data-areas enabled, and drivers.</summary>
            public int nTVars, nDA, nDAen, nDrivers;
            /// <summary>
            /// Amount of vars for designed type.</summary>
            public int nBool, nByte, nWord, nDWord, nsDWord, nReal, nString;
        }

        /// <summary>
        /// Mark for the Class Object Initialization.</summary>
        public bool isInitialized = false;

        /// <summary>
        /// Driver General Configuration.
        /// Use the variable to access the parameters values.</summary>
        private GeneralDriver GenDriverConf;

        /// <summary>
        /// Drivers Configuration.</summary>
        public DriverConf[] DriversConf;

        /// <summary>
        /// Data Areas Configuration.</summary>
        public DataAreaConf[] DataAreasConf;

        /// <summary>
        /// Internal variables to map the custom Driver config file.</summary>
        private ExeConfigurationFileMap DriverConfigMap;
        private Configuration DriverConfigFile;

        /// <summary>
        /// Type definition for the general settings.</summary>
        public struct GenSettings
        {
            public int cDrivers, cDataAreas;
        }

        /// <summary>
        /// Object variable to store the general settings.</summary>
        public GenSettings GeneralSett;

        /// <summary>
        /// The class constructor. </summary>
        ///  <param name="fileconfig">Database config filepath.</param>
        public DriverConfig(string fileconfig)
        {
            //Index for loops.
            int i;
            string sectName;

            //Reset the flag
            isInitialized = false;

            try
            {
                DriverConfigMap = new ExeConfigurationFileMap();
                DriverConfigMap.ExeConfigFilename = fileconfig;
                DriverConfigFile = ConfigurationManager.OpenMappedExeConfiguration(DriverConfigMap, ConfigurationUserLevel.None);

                //Open the General definitions in the config file.
                //GenDriverConf = new GeneralDriver("GeneralDriver", DriverConfigFile);
                GenDriverConf = (GeneralDriver) DriverConfigFile.GetSection("GeneralDriver");

                //Variable to keep the General data.
                GeneralSett = new GenSettings();

                GeneralSett.cDrivers = GenDriverConf.CountDrivers;
                GeneralSett.cDataAreas = GenDriverConf.CountDataAreas;

                DriversConf = new DriverConf[GeneralSett.cDrivers];
                DataAreasConf = new DataAreaConf[GeneralSett.cDataAreas];

                //Get Drivers Info
                for (i = 1; i <= GeneralSett.cDrivers; i++)
                {
                    sectName = "Driver_" + i.ToString("00");
                    //DriversConf[i] = new DriverConf(sectName, DriverConfigFile);
                    DriversConf[(i-1)] = (DriverConf) DriverConfigFile.GetSection(sectName);
                }

                //Get Data Areas Info
                for (i = 1; i <= GeneralSett.cDataAreas; i++)
                {
                    sectName = "DataArea_" + i.ToString("00");
                    //DataAreasConf[i] = new DataAreaConf(sectName, DriverConfigFile);
                    DataAreasConf[(i-1)] = (DataAreaConf) DriverConfigFile.GetSection(sectName);
                }

                if ( (GeneralSett.cDrivers>0) ) isInitialized = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }//END DriverConfig Class Constructor

        /// <summary>
        /// The class Destructor. </summary>
        ~DriverConfig()
        {
            DriversConf = null;
            DataAreasConf = null;
            GenDriverConf = null;

            DriverConfigMap = null;
            DriverConfigFile = null;

            isInitialized = false;
        }

    }

    /// <summary>
    /// This class defines the GeneralDriver section Configuration.</summary>
    public class GeneralDriver : ConfigurationSection
    {
        /// <summary>
        /// Variable to retrieve the section parameters. </summary>
        private static GeneralDriver settings;

        /// <summary>
        /// The class default constructor. </summary>
        public GeneralDriver()
        {
        }

        /// <summary>
        /// The class constructor. 
        /// Requires to specify the section to be opened. </summary>
        /// <param name="sect"> Section to Open in the Config File.</param>
        /// <param name="DriverConfigFile"> Configuration file mapped.</param>
        public GeneralDriver(string sect, Configuration DriverConfigFile)
        {
            settings = DriverConfigFile.GetSection(sect) as GeneralDriver;
        }

        /// <summary>
        /// The class destructor. </summary>
        ~GeneralDriver()
        {
            settings = null;
        }

        /// <summary>
        /// Property to open all the settings collection. </summary>
        public static GeneralDriver Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Property for the numbers of drivers in the conf file. </summary>
        [ConfigurationProperty("CountDrivers", DefaultValue = "1", IsKey = false, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int CountDrivers
        {
            get { return (int)this["CountDrivers"]; }
            set { this["CountDrivers"] = value; }
        }

        /// <summary>
        /// Property for the numbers of data areas configured in the conf file. </summary>
        [ConfigurationProperty("CountDataAreas", DefaultValue = "1", IsKey = false, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int CountDataAreas
        {
            get { return (int)this["CountDataAreas"]; }
            set { this["CountDataAreas"] = value; }
        }

    } // General Driver Class

    /// <summary>
    /// This class defines the DriverConf section Configuration.</summary>
    public class DriverConf : ConfigurationSection
    {
        /// <summary>
        /// Variable to retrieve the section parameters. </summary>
        private static DriverConf settings;

        /// <summary>
        /// The class default constructor. </summary>
        public DriverConf()
        {
        }

        /// <summary>
        /// The class constructor. 
        /// Requires to specify the section to be opened. </summary>
        /// <param name="sect"> Section to Open in the Config File.</param>
        /// <param name="DriverConfigFile"> Configuration file mapped.</param>
        public DriverConf(string sect, Configuration DriverConfigFile)
        {
            settings = DriverConfigFile.GetSection(sect) as DriverConf;
        }

        /// <summary>
        /// The class destructor. </summary>
        ~DriverConf()
        {
            settings = null;
        }

        /// <summary>
        /// Property to open all the settings collection. </summary>
        public static DriverConf Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Property for the ID of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("ID", DefaultValue = "1", IsKey = true, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int ID
        {
            get { return (int)this["ID"]; }
            set { this["ID"] = value; }
        }

        /// <summary>
        /// Property for the Enable of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("Enable", DefaultValue = false, IsKey = false, IsRequired = false)]
        public bool Enable
        {
            get { return (bool)this["Enable"]; }
            set { this["Enable"] = value; }
        }


        /// <summary>
        /// Property for the Real Time Cycle multiplier.
        /// A value of 1 will engage comms every Realtime cycle, a value of 2 will engage every 2 cycles.
        /// Maximun configurable value of 100, to engage comms every 100 master cycles.</summary>
        [ConfigurationProperty("CycleTime", DefaultValue = "100", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 100, MaxValue = 1000000)]
        public int CycleTime
        {
            get { return (int)this["CycleTime"]; }
            set { this["CycleTime"] = value; }
        }

        /// <summary>
        /// Property for the Driver Type of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("Type", DefaultValue = DriverConfig.DriverType.Undefined, IsKey = false, IsRequired = false)]
        public DriverConfig.DriverType Type
        {
            get { return (DriverConfig.DriverType)this["Type"]; }
            set { this["Type"] = value; }
        }

        /// <summary>
        /// Property for the Device Adress. </summary>
        [ConfigurationProperty("Address", DefaultValue = "123", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|\\", MinLength = 3, MaxLength = 256)]
        public string Address
        {
            get { return (string)this["Address"]; }
            set { this["Address"] = value; }
        }

        /// <summary>
        /// Property for the PortTCP of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("PortTCP", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int PortTCP
        {
            get { return (int)this["PortTCP"]; }
            set { this["PortTCP"] = value; }
        }

        /// <summary>
        /// Property for the PortUDP of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("PortUDP", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int PortUDP
        {
            get { return (int)this["PortUDP"]; }
            set { this["PortUDP"] = value; }
        }

        /// <summary>
        /// Property for the Rack of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("Rack", DefaultValue = "0", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 999)]
        public int Rack
        {
            get { return (int)this["Rack"]; }
            set { this["Rack"] = value; }
        }

        /// <summary>
        /// Property for the Slot of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("Slot", DefaultValue = "0", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 0, MaxValue = 999)]
        public int Slot
        {
            get { return (int)this["Slot"]; }
            set { this["Slot"] = value; }
        }

        /// <summary>
        /// Property for the XWave Def File. </summary>
        [ConfigurationProperty("DefFilePath", DefaultValue = "123", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|", MinLength = 3, MaxLength = 256)]
        public string DefFilePath
        {
            get { return (string)this["DefFilePath"]; }
            set { this["DefFilePath"] = value; }
        }

        /// <summary>
        /// Property for the Serial Port for the Driver/Device beign configured. </summary>
        [ConfigurationProperty("PortRTU", DefaultValue = DriverConfig.RTUCommPort.Undefined, IsKey = false, IsRequired = false)]
        public DriverConfig.RTUCommPort PortRTU
        {
            get { return (DriverConfig.RTUCommPort)this["PortRTU"]; }
            set { this["PortRTU"] = value; }
        }

        /// <summary>
        /// Property for the RTUid of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("RTUid", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int RTUid
        {
            get { return (int)this["RTUid"]; }
            set { this["RTUid"] = value; }
        }

        /// <summary>
        /// Property for the RTUBaud of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("RTUBaud", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 999999)]
        public int RTUBaud
        {
            get { return (int)this["RTUBaud"]; }
            set { this["RTUBaud"] = value; }
        }

        /// <summary>
        /// Property for the RTUParity of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("RTUParity", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int RTUParity
        {
            get { return (int)this["RTUParity"]; }
            set { this["RTUParity"] = value; }
        }

        /// <summary>
        /// Property for the RTUStop of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("RTUStop", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int RTUStop
        {
            get { return (int)this["RTUStop"]; }
            set { this["RTUStop"] = value; }
        }

        /// <summary>
        /// Property for the Timeout Connection of the Driver/Device beign configured. </summary>
        [ConfigurationProperty("Timeout", DefaultValue = "100", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 100, MaxValue = 120000)]
        public int Timeout
        {
            get { return (int)this["Timeout"]; }
            set { this["Timeout"] = value; }
        }

    } // DriverConf Class

    /// <summary>
    /// This class defines the DataAreaConf section Configuration.</summary>
    public class DataAreaConf : ConfigurationSection
    {
        /// <summary>
        /// Variable to retrieve the section parameters. </summary>
        private static DataAreaConf settings;

        /// <summary>
        /// The class default constructor. </summary>
        public DataAreaConf()
        {
        }

        /// <summary>
        /// The class constructor. 
        /// Requires to specify the section to be opened. </summary>
        /// <param name="sect"> Section to Open in the Config File.</param>
        /// <param name="DriverConfigFile"> Configuration file mapped.</param>
        public DataAreaConf(string sect, Configuration DriverConfigFile)
        {
            settings = DriverConfigFile.GetSection(sect) as DataAreaConf;
        }

        /// <summary>
        /// The class destructor. </summary>
        ~DataAreaConf()
        {
            settings = null;
        }

        /// <summary>
        /// Property to open all the settings collection. </summary>
        public static DataAreaConf Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// Property for the ID of the Data Area beign configured. </summary>
        [ConfigurationProperty("ID", DefaultValue = "1", IsKey = true, IsRequired = true)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int ID
        {
            get { return (int)this["ID"]; }
            set { this["ID"] = value; }
        }

        /// <summary>
        /// Property for the ID_Driver of the Data Area beign configured. </summary>
        [ConfigurationProperty("ID_Driver", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 99)]
        public int ID_Driver
        {
            get { return (int)this["ID_Driver"]; }
            set { this["ID_Driver"] = value; }
        }

        /// <summary>
        /// Property for the Enable of the Data Area beign configured. </summary>
        [ConfigurationProperty("Enable", DefaultValue = false, IsKey = false, IsRequired = false)]
        public bool Enable
        {
            get { return (bool)this["Enable"]; }
            set { this["Enable"] = value; }
        }

        /// <summary>
        /// Property for the Write of the Data Area beign configured. </summary>
        [ConfigurationProperty("Write", DefaultValue = false, IsKey = false, IsRequired = false)]
        public bool Write
        {
            get { return (bool)this["Write"]; }
            set { this["Write"] = value; }
        }

        /// <summary>
        /// Property for the Historics of the Data Area beign configured. </summary>
        [ConfigurationProperty("ToHist", DefaultValue = false, IsKey = false, IsRequired = false)]
        public bool ToHist
        {
            get { return (bool)this["ToHist"]; }
            set { this["ToHist"] = value; }
        }

        /// <summary>
        /// Property for the DataType of the Data Area beign configured. </summary>
        [ConfigurationProperty("DataType", DefaultValue = DriverConfig.DatType.Undefined, IsKey = false, IsRequired = false)]
        public DriverConfig.DatType DataType
        {
            get { return (DriverConfig.DatType)this["DataType"]; }
            set { this["DataType"] = value; }
        }

        /// <summary>
        /// Property for the DB_Number of the Data Area beign configured. </summary>
        [ConfigurationProperty("DB_Number", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int DB_Number
        {
            get { return (int)this["DB_Number"]; }
            set { this["DB_Number"] = value; }
        }

        /// <summary>
        /// Property for the StartAddr the Data Area beign configured. </summary>
        [ConfigurationProperty("StartAddr", DefaultValue = "123", IsKey = false, IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ,~!@#$%^&*()[]{};’\"|", MinLength = 3, MaxLength = 256)]
        public string StartAddr
        {
            get { return (string)this["StartAddr"]; }
            set { this["StartAddr"] = value; }
        }

        /// <summary>
        /// Property for the AmountVar of the Data Area beign configured. </summary>
        [ConfigurationProperty("AmountVar", DefaultValue = "1", IsKey = false, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int AmountVar
        {
            get { return (int)this["AmountVar"]; }
            set { this["AmountVar"] = value; }
        }

    } // DataAreaConf Class

}
