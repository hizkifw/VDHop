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

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);


        private VirtualDesktop[] desktops;
        private IntPtr[] foregroundWindows;

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
            foregroundWindows = new IntPtr[desktops.Length];

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
            HotkeyManager.Current.AddOrReplace("m0", Keys.Alt | Keys.Shift | Keys.D1, HandleMove);
            HotkeyManager.Current.AddOrReplace("m1", Keys.Alt | Keys.Shift | Keys.D2, HandleMove);
            HotkeyManager.Current.AddOrReplace("m2", Keys.Alt | Keys.Shift | Keys.D3, HandleMove);
            HotkeyManager.Current.AddOrReplace("m3", Keys.Alt | Keys.Shift | Keys.D4, HandleMove);
            HotkeyManager.Current.AddOrReplace("m4", Keys.Alt | Keys.Shift | Keys.D5, HandleMove);
            HotkeyManager.Current.AddOrReplace("m5", Keys.Alt | Keys.Shift | Keys.D6, HandleMove);
            HotkeyManager.Current.AddOrReplace("m6", Keys.Alt | Keys.Shift | Keys.D7, HandleMove);
            HotkeyManager.Current.AddOrReplace("m7", Keys.Alt | Keys.Shift | Keys.D8, HandleMove);
            HotkeyManager.Current.AddOrReplace("m8", Keys.Alt | Keys.Shift | Keys.D9, HandleMove);
            HotkeyManager.Current.AddOrReplace("m9", Keys.Alt | Keys.Shift | Keys.D0, HandleMove);
        }

        private int GetActiveDesktopNumber()
        {
            var currentDesktop = VirtualDesktop.Current;
            return desktops.TakeWhile(d => d.Id != currentDesktop.Id).Count();
        }

        private void HandleSwitch(object sender, HotkeyEventArgs e)
        {
            // Store active window in current desktop
            var currentIdx = GetActiveDesktopNumber();
            if (currentIdx >= 0 && currentIdx < foregroundWindows.Length)
                foregroundWindows[currentIdx] = GetForegroundWindow();

            // Switch to new desktop
            var i = int.Parse(e.Name.Substring(1));
            if (i < 0 || i >= foregroundWindows.Length)
                return;

            desktops[i].Switch();

            // Focus window in new desktop
            if (foregroundWindows[i] != IntPtr.Zero)
                SetForegroundWindow(foregroundWindows[i]);
        }

        private void HandleMove(object sender, HotkeyEventArgs e)
        {
            var i = int.Parse(e.Name.Substring(1));
            if (i < 0 || i >= foregroundWindows.Length)
                return;

            var hWnd = GetForegroundWindow();
            if (hWnd == IntPtr.Zero)
                return;

            VirtualDesktop.MoveToDesktop(hWnd, desktops[i]);

            // Set the target desktop's foreground window
            foregroundWindows[i] = hWnd;
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
