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
        /// <summary>
        /// 10 second wait before registering hotkeys after an edit unregistered them
        /// </summary>
        public static int timerWaitsFor = 10;
        /// <summary>
        /// Some controls get their values changed during loading that causes some events to fire. <br/>
        /// This variable is used to prevents that.
        /// </summary>
        public static bool IgnoreInputsBecauseLoading = true;
        public Form1()
        {
            form = this;
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // set loading to true
            IgnoreInputsBecauseLoading = true;
            // select 1st item in dropdown (dummy text on startup)
            comboBox_Networks.SelectedIndex = 0;
            // load config
            ConfigHandler.TryLoadFromConfig();
            // get networks
            NetworkHandler.GetNetworks();
            // register hotkeys
            HotkeyHandler.RegisterAll();
        }
        // Remove focus from the current control when clicking off of it
        private void RemoveActiveControl(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        // hotkey handling start
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312) { HotkeyHandler.HandleHotkeyPress(ref m); }
            base.WndProc(ref m);
        }

        // keyDown events
        private void textBox_SoloKey_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "SOLO"); }
        private void textBox_KillGame_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "KILL"); }
        private void textBox_DropNetwork_KeyDown(object sender, KeyEventArgs e) { HotkeyHandler.HandleHotkeyTextBox(e, "NET"); }

        // limit to numbers
        private void textBox_SoloTime_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); }
        // limit to numbers
        private void textBox_NetworkTime_KeyPress(object sender, KeyPressEventArgs e) { e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); }

        private void comboBox_Networks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IgnoreInputsBecauseLoading)
            {
                // update config
                ConfigHandler.config.selectedAdapter = comboBox_Networks.SelectedIndex;
                // unregistering to autosave
                HotkeyHandler.UnregisterAll();
            }
        }
        // starts/resets the 10 second cooldown of the autosave
        internal void StartTimer()
        {
            timer_autosaver.Stop();
            timer_autosaver.Start();
            timerWaitsFor = 10;
        }

        // autosaver timer
        private void timer_autosaver_Tick(object sender, EventArgs e)
        {
            timerWaitsFor--;
            if (timerWaitsFor <= 0)
            {
                HotkeyHandler.RegisterAll();
                ConfigHandler.SaveConfig();

                timer_autosaver.Stop();
                timerWaitsFor = 10;
            }
        }
        // suspend timer
        private void timer_suspend_Tick(object sender, EventArgs e)
        {
            ProcessHandler.ResumeGameProcesses();
            timer_suspend.Stop();
        }
        // network timer
        private void timer_network_Tick(object sender, EventArgs e)
        {
            NetworkHandler.EnableAdapter(Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString());
            timer_network.Stop();
        }

        // time inputs' eventhandler
        private void textBox_SoloTime_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreInputsBecauseLoading)
            {
                int suspend = Convert.ToInt32(textBox_SoloTime.Text);
                ConfigHandler.config.suspendInterval = suspend;
                timer_suspend.Interval = suspend;
                HotkeyHandler.UnregisterAll();
            }
        }
        private void textBox_NetworkTime_TextChanged(object sender, EventArgs e)
        {
            if (!IgnoreInputsBecauseLoading)
            {
                int dropDelay = Convert.ToInt32(textBox_NetworkTime.Text);
                ConfigHandler.config.dropDelay = dropDelay;
                timer_network.Interval = dropDelay * 1000;
                HotkeyHandler.UnregisterAll();
            }
        }
        // Enable the adapter when closing to prevent some awkwardness
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Enable the adapter when closing to prevent some awkwardness
            NetworkHandler.EnableAdapter(Form1.form.comboBox_Networks.Items[comboBox_Networks.SelectedIndex].ToString());
        }
    }
}
