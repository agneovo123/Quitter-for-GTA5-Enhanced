using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Quitter_4_Enhanced
{
    class ProcessHandler
    {
        private static Process MyProcess = null;
        // "GTA5_Enhanced_BE"  // OH DEAR LORD THAT'S THE BATTLEEYE LAUNCHER OH GOD OH FUCK
        private const string ProcessName = "GTA5_Enhanced";
        /// <summary>
        /// Finds the process whose name is ProcessName
        /// Sets the MyProcess global variable to the found process
        /// </summary>
        public static void GetGameProcess()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                //Console.WriteLine("Process: \"{0}\" ID: {1}", process.ProcessName, process.Id);
                if (process.ProcessName == ProcessName)
                {
                    MyProcess = process;
                    //Console.WriteLine("\"process.ProcessName\" FOUND");
                }
            }
        }
        public static void SuspendGameProcess()
        {
            GetGameProcess();
            if (MyProcess == null)
            {
                Logger.log($"Can't suspend process \"{ProcessName}\" - NOT FOUND");
                return;
            }
            MyProcess.Suspend();
            Logger.log($"Suspended process \"{ProcessName}\"");

            Form1.form.timer_suspend.Start();
        }

        public static void ResumeGameProcess()
        {
            GetGameProcess();
            if (MyProcess == null)
            {
                Logger.log($"Can't resume process \"{ProcessName}\" - NOT FOUND");
                return;
            }
            MyProcess.Resume();
            Logger.log($"Resumed process \"{ProcessName}\"");
        }

        public static void KillGameProcess()
        {
            GetGameProcess();
            if (MyProcess == null)
            {
                Logger.log($"Can't kill process \"{ProcessName}\" - NOT FOUND");
                return;
            }
            MyProcess.Kill();
            Logger.log($"Killed process \"{ProcessName}\"");
        }
    }
}
