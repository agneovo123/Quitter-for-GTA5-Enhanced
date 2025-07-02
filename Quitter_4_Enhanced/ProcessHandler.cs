using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Logger.logDEBUG($"GetGameProcesses() called");
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
                        Logger.logDEBUG($"process found: {process}");
                    }
                }
            }
        }
        /// <summary>
        /// Finds game processes, and returns how many it found
        /// </summary>
        //public static int GetGameProcessCount()
        //{
        //    // clear list
        //    MyProcesses.Clear();
        //    // get processes
        //    Process[] processlist = Process.GetProcesses();
        //    foreach (Process process in processlist)
        //    {
        //        //Console.WriteLine("Process: \"{0}\" ID: {1}", process.ProcessName, process.Id);
        //        // O(n*m) goes brrr
        //        for (int i = 0; i < ProcessNames.Length; i++)
        //        {
        //            if (process.ProcessName == ProcessNames[i])
        //            {
        //                MyProcesses.Add(process);
        //                //Console.WriteLine("\"process.ProcessName\" FOUND");
        //            }
        //        }
        //    }
        //    return MyProcesses.Count;
        //}
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
            Logger.logDEBUG($"SuspendGameProcesses() called");
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

            //Form1.form.timer_suspend.Start();
            Form1.form.Invoke((Action)(() => { Form1.form.timer_suspend.Start(); }));
            Logger.logDEBUG($"timer_suspend started");
            Logger.logDEBUG($"timer_suspend.Enabled: {Form1.form.timer_suspend.Enabled}");
            Logger.logDEBUG($"timer_suspend.Interval: {Form1.form.timer_suspend.Interval}(ms)");
        }
        /// <summary>
        /// Tries to resume the found processes
        /// </summary>
        public static void ResumeGameProcesses()
        {
            Logger.logDEBUG($"ResumeGameProcesses() called");
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
            Logger.logDEBUG($"KillGameProcesses() called");
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
            //Form1.lastTerminate = DateTime.Now;
            // report to user
            if (MyProcesses.Count > 1) { Logger.log($"Killed processes \"{names}\""); }
            else { Logger.log($"Killed process \"{names}\""); }

            if (ConfigHandler.config.selfTerminate) { Form1.selfTerminte = true; }
        }
    }
}
