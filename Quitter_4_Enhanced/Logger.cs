using System;
using System.Collections.Generic;
using System.Drawing;

namespace Quitter_4_Enhanced
{
    class Logger
    {
        private static List<string> logQueue = new List<string>();
        private static bool firstLine = true;
        /// <summary>
        /// Writes a message to the bottom of the program
        /// </summary>
        /// <param name="msg">message to write</param>
        public static void log(string msg)
        {
            //Form1.form.listBox_EventLog.Items.Insert(0, DateTime.Now + " > " + msg);
            //Form1.form.listBox_EventLog.Items.Add(DateTime.Now + " > " + msg);
            //Form1.form.richTextBox_EventLog.Text += ("\n" + DateTime.Now + " > " + msg);
            logQueue.Add(DateToStr(DateTime.Now) + " > " + msg);
        }

        private static string DateToStr(DateTime date)
        {
            return date + "." + date.Millisecond.ToString().PadLeft(3, '0');
        }

        public static void LogFromQueue()
        {
            int logLength = logQueue.Count;
            if (logLength > 0)
            {
                for (int i = 0; i < logLength; i++)
                {
                    if (firstLine) { Form1.form.richTextBox_EventLog.Text += logQueue[i]; firstLine = false; }
                    else { Form1.form.richTextBox_EventLog.Text += ("\n" + logQueue[i]); }
                }

                Form1.form.richTextBox_EventLog.SelectionStart = Form1.form.richTextBox_EventLog.TextLength;
                Form1.form.richTextBox_EventLog.SelectionLength = 0;
                Form1.form.richTextBox_EventLog.ScrollToCaret();
            }
            logQueue.RemoveRange(0, logLength);
        }

        public static void logDEBUG(string msg)
        {
            if (!Form1.DEBUGLOG) { return; }

            //Color org = Form1.form.richTextBox_EventLog.SelectionColor;
            //Form1.form.richTextBox_EventLog.SelectionColor = Color.Yellow;

            //string str = DateToStr(DateTime.Now) + " > [DEBUG] " + msg;
            //if (firstLine) { Form1.form.richTextBox_EventLog.Text += str; firstLine = false; }
            //else { Form1.form.richTextBox_EventLog.Text += ("\n" + str); }

            //Form1.form.richTextBox_EventLog.SelectionColor = org;



            logQueue.Add($"{DateToStr(DateTime.Now)} > [DEBUG] {msg}");
        }
        public static void logDEBUGConfig()
        {
            if (!Form1.DEBUGLOG) { return; }

            //Color org = Form1.form.richTextBox_EventLog.SelectionColor;
            //Form1.form.richTextBox_EventLog.SelectionColor = Color.Yellow;

            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG] CONFIG:";
            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG] Hotkeys:";

            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG]   solo: {ConfigHandler.config.hotkeys[0].CombinedModifiers}+{ConfigHandler.config.hotkeys[0].Key}";
            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG]   kill: {ConfigHandler.config.hotkeys[1].CombinedModifiers}+{ConfigHandler.config.hotkeys[1].Key}";
            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG]   net: {ConfigHandler.config.hotkeys[2].CombinedModifiers}+{ConfigHandler.config.hotkeys[2].Key}";

            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG] selectedAdapter: {ConfigHandler.config.selectedAdapter}";
            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG] suspendInterval: {ConfigHandler.config.suspendInterval}";
            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG] dropDelay: {ConfigHandler.config.dropDelay}";
            Form1.form.richTextBox_EventLog.Text += $"\n{DateToStr(DateTime.Now)} > [DEBUG] selfTerminate: {ConfigHandler.config.selfTerminate}";


            //Form1.form.richTextBox_EventLog.SelectionColor = org;
        }
    }
}
