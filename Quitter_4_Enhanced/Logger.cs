using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void LogFronmQueue()
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



    }
}
