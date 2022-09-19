using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VDHop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Ensure single-instance
            bool result;
            var mutex = new System.Threading.Mutex(true, "b3ea32f8-fa5f-40c9-8675-c07bebe83b8d", out result);
            if (!result)
            {
                MessageBox.Show("Another instance is already running.");
                return;
            }

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            // mutex shouldn't be released
            GC.KeepAlive(mutex);
        }
    }
}
