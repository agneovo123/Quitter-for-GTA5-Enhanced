using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Quitter_4_Enhanced
{
    [Flags]
    public enum ThreadAccess : int
    {
        SUSPEND_RESUME = (0x0002),
    }
    public static class ProcessExtension
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        /// <summary>
        /// Suspends a process
        /// </summary>
        /// <param name="process"></param>
        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                // break if null
                if (pOpenThread == IntPtr.Zero) { break; }
                // system call
                SuspendThread(pOpenThread);
            }
        }
        /// <summary>
        /// Resumes a process
        /// </summary>
        /// <param name="process"></param>
        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                // break if null
                if (pOpenThread == IntPtr.Zero) { break; }
                // system call
                ResumeThread(pOpenThread);
            }
        }
    }
}
