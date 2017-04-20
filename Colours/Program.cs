using Colours.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    static class Program
    {
        [DllImport("shcore.dll")]
        static extern int SetProcessDpiAwareness(int value);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var state = AppArgParser.ParseArgs(args,
                Properties.Settings.Default.LastColor.ToRgbColor().ToHsv(),
                Properties.Settings.Default.SchemeType);
            if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                Environment.OSVersion.Version == new Version(6,2))
                SetProcessDpiAwareness(1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(state));
        }
    }
}
