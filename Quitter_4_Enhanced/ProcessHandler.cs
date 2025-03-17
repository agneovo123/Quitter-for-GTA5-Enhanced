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
        private static List<Process> MyProcesses = new List<Process>();
        // The names of the processes to be suspended/killed
        private static string[] ProcessNames = { "GTA5_Enhanced", "GTA5" };
        /// <summary>
        /// Finds and collects the processes whose name is in the ProcessNames array
        /// </summary>
        public static void GetGameProcesses()
        {
            // clear list
            MyProcesses.Clear();
            // get processes
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                //Console.WriteLine("Process: \"{0}\" ID: {1}", process.ProcessName, process.Id);
                // O(n*m) goes brrr
                for (int i = 0; i < ProcessNames.Length; i++)
                {
                    if (process.ProcessName == ProcessNames[i])
                    {
                        MyProcesses.Add(process);
                        //Console.WriteLine("\"process.ProcessName\" FOUND");
                    }
                }
            }
        }
        /// <summary>
        /// saves all currently running processes to processes.txt
        /// used for expanding/debug
        /// </summary>
        public static void SaveAllProcessNames()
        {
            StreamWriter sw = new StreamWriter("processes.txt");
            // get processes
            Process[] processlist = Process.GetProcesses();
            foreach (Process process in processlist)
            {
                sw.WriteLine("Process: \"{0}\" ID: {1}", process.ProcessName, process.Id);
            }
            sw.Close();
        }
        /// <summary>
        /// Tries to suspend the found processes
        /// </summary>
        public static void SuspendGameProcesses()
        {
            // get processes
            GetGameProcesses();
            // warn user if none found
            if (MyProcesses.Count <= 0)
            {
                Logger.log($"Can't suspend processes, NOT FOUND");
                return;
            }
            // names for reporting
            string names = "";
            // suspend all
            for (int i = 0; i < MyProcesses.Count; i++)
            {
                MyProcesses[i].Suspend();
                // add name to names
                names += MyProcesses[i].ProcessName;
                if (i + 1 < MyProcesses.Count) { names += ", "; }
            }
            // report to user
            if (MyProcesses.Count > 1) { Logger.log($"Suspended processes \"{names}\""); }
            else { Logger.log($"Suspended process \"{names}\""); }

            Form1.form.timer_suspend.Start();
        }
        /// <summary>
        /// Tries to resume the found processes
        /// </summary>
        public static void ResumeGameProcesses()
        {
            // get processes
            GetGameProcesses();
            // warn user if none found
            if (MyProcesses.Count <= 0)
            {
                Logger.log($"Can't resume processes, NOT FOUND");
                return;
            }
            // names for reporting
            string names = "";
            // resume all
            for (int i = 0; i < MyProcesses.Count; i++)
            {
                MyProcesses[i].Resume();
                // add name to names
                names += MyProcesses[i].ProcessName;
                if (i + 1 < MyProcesses.Count) { names += ", "; }
            }
            // report to user
            if (MyProcesses.Count > 1) { Logger.log($"Resumed processes \"{names}\""); }
            else { Logger.log($"Resumed process \"{names}\""); }
        }
        /// <summary>
        /// Tries to kill the found processes
        /// </summary>
        public static void KillGameProcesses()
        {
            // get processes
            GetGameProcesses();
            // warn user if none found
            if (MyProcesses.Count <= 0)
            {
                Logger.log($"Can't kill processes, NOT FOUND");
                return;
            }
            // names for reporting
            string names = "";
            // kill all
            for (int i = 0; i < MyProcesses.Count; i++)
            {
                MyProcesses[i].Kill();
                // add name to names
                names += MyProcesses[i].ProcessName;
                if (i + 1 < MyProcesses.Count) { names += ", "; }
            }
            // report to user
            if (MyProcesses.Count > 1) { Logger.log($"Killed processes \"{names}\""); }
            else { Logger.log($"Killed process \"{names}\""); }
        }
    }
}
