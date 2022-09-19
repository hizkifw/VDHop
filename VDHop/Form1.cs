using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

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

            // Virtual Desktops
            HotkeyManager.Current.AddOrReplace("s0", Keys.Alt | Keys.D1, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s1", Keys.Alt | Keys.D2, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s2", Keys.Alt | Keys.D3, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s3", Keys.Alt | Keys.D4, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s4", Keys.Alt | Keys.D5, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s5", Keys.Alt | Keys.D6, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s6", Keys.Alt | Keys.D7, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s7", Keys.Alt | Keys.D8, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s8", Keys.Alt | Keys.D9, HandleSwitch);
            HotkeyManager.Current.AddOrReplace("s9", Keys.Alt | Keys.D0, HandleSwitch);

            // Moving windows around virtual desktops
            HotkeyManager.Current.AddOrReplace("m0", Keys.Alt | Keys.Shift | Keys.D1, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m1", Keys.Alt | Keys.Shift | Keys.D2, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m2", Keys.Alt | Keys.Shift | Keys.D3, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m3", Keys.Alt | Keys.Shift | Keys.D4, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m4", Keys.Alt | Keys.Shift | Keys.D5, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m5", Keys.Alt | Keys.Shift | Keys.D6, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m6", Keys.Alt | Keys.Shift | Keys.D7, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m7", Keys.Alt | Keys.Shift | Keys.D8, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m8", Keys.Alt | Keys.Shift | Keys.D9, HandleMoveSwitch);
            HotkeyManager.Current.AddOrReplace("m9", Keys.Alt | Keys.Shift | Keys.D0, HandleMoveSwitch);
        }

        private void HandleSwitch(object sender, HotkeyEventArgs e)
        {
            var i = int.Parse(e.Name.Substring(1));
            desktops[i].Switch();
        }

        private void HandleMoveSwitch(object sender, HotkeyEventArgs e)
        {
            var i = int.Parse(e.Name.Substring(1));

            var hWnd = GetForegroundWindow();
            VirtualDesktop.MoveToDesktop(hWnd, desktops[i]);
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
