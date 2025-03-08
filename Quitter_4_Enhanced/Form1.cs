﻿using System;
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
        public static int timerWaitsFor = 10;
        public static int comboSelectChanged = 0;
        public static bool IgnoreBecauseLoading = true;
        public Form1()
        {
            form = this;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IgnoreBecauseLoading = true;
            comboBox_Networks.SelectedIndex = 0;
            ConfigHandler.TryLoadFromConfig();
            NetworkHandler.GetNetworks();

            HotkeyHandler.RegisterAll();
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
            //RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)Keys.PageUp);

            //HotkeyHandler.RegisterHotkeys();

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
            HotkeyHandler.HandleHotkeyPress(ref m);
            //if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            //{
            //    a++;
            //    MessageBox.Show(a.ToString());
            //}
            base.WndProc(ref m);
        }

        #endregion


        private void textBox_SoloKey_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "SOLO"); }
        private void textBox_KillGame_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "KILL"); }
        private void textBox_DropNetwork_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "NET"); }

        // limit to numbers
        private void textBox_SoloTime_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); }
        // limit to numbers
        private void textBox_NetworkTime_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); }

        private void comboBox_Networks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IgnoreBecauseLoading)
            {
                HotkeyHandler.UnregisterAll();
                ConfigHandler.config.selectedAdapter = comboBox_Networks.SelectedIndex;
            }
        }

        internal void StartTimer()
        {
            timer_autisaver.Stop();
            timer_autisaver.Start();
            timerWaitsFor = 10;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerWaitsFor--;
            if (timerWaitsFor <= 0)
            {
                HotkeyHandler.RegisterAll();
                ConfigHandler.SaveConfig();

                timer_autisaver.Stop();
                timerWaitsFor = 10;
            }
        }

        private void timer_suspend_Tick(object sender, EventArgs e)
        {

            timer_suspend.Stop();
        }

        private void timer_network_Tick(object sender, EventArgs e)
        {
            NetworkHandler.EnableAdapter(Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString());
            timer_network.Stop();
        }

        private void textBox_SoloTime_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreBecauseLoading)
            {
                int suspend = Convert.ToInt32(textBox_SoloTime.Text);
                ConfigHandler.config.suspendInterval = suspend;
                timer_suspend.Interval = suspend;
                HotkeyHandler.UnregisterAll();
            }
        }

        private void textBox_NetworkTime_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreBecauseLoading)
            {
                int dropDelay = Convert.ToInt32(textBox_NetworkTime.Text);
                ConfigHandler.config.dropDelay = dropDelay;
                timer_network.Interval = dropDelay * 1000;
                HotkeyHandler.UnregisterAll();
            }
        }


        //private void textBox_SoloKey_MouseEnter(object sender, EventArgs e)
        //{
        //    textBox_SoloKey.BackColor = Color.FromArgb(0, 51, 51);
        //}
        //
        //private void textBox_SoloKey_MouseLeave(object sender, EventArgs e)
        //{
        //    textBox_SoloKey.BackColor = Color.FromArgb(0, 0, 0);
        //}

    }
}
