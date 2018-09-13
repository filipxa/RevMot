using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotoRev
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static LogIn loginDialog;
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DataManager.init();            
            login();

        }
        private static void login()
        {
            loginDialog = new LogIn();
            loginDialog.ShowDialog();
           
            if (loginDialog.result)
            {
                Application.Run(new MainForm());
            }
               

        }

    }
}
