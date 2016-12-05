using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UFN_Licensing_System___Key_generator
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
            Application.Run(new ufnlskg_main());
        }
    }
}
