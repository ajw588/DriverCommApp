using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DriverCommApp.Conf;
using DriverCommApp.CommDriver;
using DriverCommApp.DBMain;

namespace DriverCommApp
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainScreen MyMainScreen = new MainScreen();

            //Start the GUI and the App
            Application.Run(MyMainScreen);

           

        }

    }
}
