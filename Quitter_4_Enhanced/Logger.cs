using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitter_4_Enhanced
{
    class Logger
    {
        /// <summary>
        /// Writes a message to the bottom of the program
        /// </summary>
        /// <param name="msg">message to write</param>
        public static void log(string msg)
        {
            //Form1.form.listBox_EventLog.Items.Insert(0, DateTime.Now + " > " + msg);
            Form1.form.listBox_EventLog.Items.Add(DateTime.Now + " > " + msg);
        }
    }
}
