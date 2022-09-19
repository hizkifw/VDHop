using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CodeAnalysis;
using NHotkey;
using NHotkey.WindowsForms;
using WindowsDesktop;

namespace VDHop
{
    public partial class Form1 : Form
    {
        private VirtualDesktop[] desktops;

        public Form1()
        {
            InitializeComponent();

            // Create 10 Virtual Desktops
            desktops = VirtualDesktop.GetDesktops();
            while (desktops.Length < 10)
            {
                VirtualDesktop.Create();
                desktops = VirtualDesktop.GetDesktops();
            }

            HotkeyManager.Current.AddOrReplace("0", Keys.Alt | Keys.D1, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("1", Keys.Alt | Keys.D2, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("2", Keys.Alt | Keys.D3, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("3", Keys.Alt | Keys.D4, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("4", Keys.Alt | Keys.D5, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("5", Keys.Alt | Keys.D6, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("6", Keys.Alt | Keys.D7, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("7", Keys.Alt | Keys.D8, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("8", Keys.Alt | Keys.D9, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("9", Keys.Alt | Keys.D0, HandleSwitch);
        }

        private void HandleSwitch(object sender, HotkeyEventArgs e)
        {
            var i = Int32.Parse(e.Name);
            desktops[i].Switch();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
               notifyIcon1.Visible = true;
               notifyIcon1.ShowBalloonTip(500);
               this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
               notifyIcon1.Visible = false;
            }
        }
    }
}
