using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static Quitter_4_Enhanced.ProcessExtension;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Quitter_4_Enhanced
{
    public partial class Form1 : Form
    {
        public static Form1 form;
        public Form1()
        {
            form = this;
            InitializeComponent();
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            // Remove focus from the current control
            this.ActiveControl = null;
        }

        #region process_stuff
        Process MyProcess;
        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();


            foreach (Process process in processlist)
            {
                Console.WriteLine("Process: \"{0}\" ID: {1}", process.ProcessName, process.Id);
                if (process.ProcessName == "notepad++")
                {
                    MyProcess = process;
                    Console.WriteLine("\"notepad++\" FOUND");
                }
            }
        }
        private void button_suspend_Click(object sender, EventArgs e)
        {
            MyProcess.Suspend();
        }

        private void button_continue_Click(object sender, EventArgs e)
        {
            MyProcess.Resume();
        }

        private void button_kill_Click(object sender, EventArgs e)
        {
            MyProcess.Kill();
        }
        #endregion

        #region networking_stuff
        string MyAdapter;
        private void button_get_networks_Click(object sender, EventArgs e)
        {
            List<string> adapters = net_adapters();
            foreach (string adapter in adapters)
            {
                Console.WriteLine("Adapter: \"{0}\"", adapter);
                if (adapter == "Ethernet 4")
                {
                    MyAdapter = adapter;
                }
            }
        }
        public System.Collections.Generic.List<String> net_adapters()
        {
            List<String> values = new List<String>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                values.Add(nic.Name);
            }
            return values;
        }
        private void button_OFF_Click(object sender, EventArgs e)
        {
            DisableAdapter(MyAdapter);
        }
        private void button_ON_Click(object sender, EventArgs e)
        {
            EnableAdapter(MyAdapter);
        }
        static void EnableAdapter(string interfaceName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
        }
        static void DisableAdapter(string interfaceName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            Process p = new Process();
            p.StartInfo = psi;
            p.Start();
        }
        #endregion

        #region hotkey_stuff
        public int a = 1;
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int MYACTION_HOTKEY_ID = 1;

        private void button_RegisterHotkey_Click(object sender, EventArgs e)
        {
            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            //RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 0, (int)Keys.F10);
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)Keys.PageUp);

            //Console.WriteLine(Keys.PageUp);
            //Console.WriteLine(Keys.Control);
            //Console.WriteLine(Keys.Shift);
            //Console.WriteLine(Keys.Alt);
        }

        private void button_UnegisterHotkey_Click(object sender, EventArgs e)
        {
            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_ID);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                a++;
                MessageBox.Show(a.ToString());
            }
            base.WndProc(ref m);
        }

        #endregion


        private void textBox_SoloKey_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "SOLO"); }
        private void textBox_KillGame_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "KILL"); }
        private void textBox_DropNetwork_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "NET"); }

        private void textBox_SoloTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // limit to numbers
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox_NetworkTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            // limit to numbers
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

    }
}
