using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colours
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            AssemblyName i = Assembly.GetExecutingAssembly().GetName();
            titleLabel.Text = String.Format("{0} {1}", i.Name, i.Version);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/NattyNarwhal/Colours");
        }
    }
}
