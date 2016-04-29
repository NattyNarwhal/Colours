﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            InitialAppState state = AppArgParser.ParseArgs(args,
                new HsvColor(Properties.Settings.Default.LastColor.ToRgbColor()),
                Properties.Settings.Default.SchemeType);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(state));
        }
    }
}
