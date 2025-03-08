using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security.Principal;

namespace Quitter_4_Enhanced
{
    class NetworkHandler
    {
        private static bool IsAdmin;
        /// <summary>
        /// Checks if the program was started with Administrator rights
        /// </summary>
        /// <returns>true if program was 'Run as Admin'</returns>
        private static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// Sets IsAdmin and warns user if the program wasn't started with Administrator rights.
        /// </summary>
        private static void CheckAdminRights()
        {
            IsAdmin = IsAdministrator();
            // color net controls + warn user
            if (!IsAdmin)
            {
                Form1.form.label_DropConn.ForeColor = System.Drawing.Color.FromArgb(0, 255, 255);
                Form1.form.textBox_NetworkKey.ForeColor = System.Drawing.Color.FromArgb(170, 68, 68);
                Form1.form.comboBox_Networks.ForeColor = System.Drawing.Color.FromArgb(170, 68, 68);
                Logger.log("[WARN] Not launched as Administrator, can't manage network");
                Logger.log("Drop network connection is unavailable");
            }
        }
        /// <summary>
        /// Get network adapters' name
        /// </summary>
        public static void GetNetworks()
        {
            // clear items
            Form1.form.comboBox_Networks.Items.Clear();
            // add adapter names to comboBox/dropDown
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Adapter: \"{0}\"", nic.Name);
                Form1.form.comboBox_Networks.Items.Add(nic.Name);
            }
            // set selected
            Form1.form.comboBox_Networks.SelectedIndex = ConfigHandler.config.selectedAdapter;

            CheckAdminRights();
            // loading ends here
            Form1.IgnoreInputsBecauseLoading = false;
        }
        /// <summary>
        /// Tries to enable a network adapter
        /// </summary>
        /// <param name="interfaceName">name of the network adapter</param>
        public static void EnableAdapter(string interfaceName)
        {
            Console.WriteLine("EnableAdapter()");
            if (!IsAdmin)
            {
                Logger.log("Can't enable network adapter; Run as Administrator");
                return;
            }
            Logger.log($"Enabling adapter \"{interfaceName}\"");
            try
            {
                string query = $"SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID = '{interfaceName}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject adapter in searcher.Get())
                    {
                        adapter.InvokeMethod("Enable", null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Logger.log(e.Message);
            }
        }
        /// <summary>
        /// Tries to disable a network adapter
        /// </summary>
        /// <param name="interfaceName">name of the network adapter</param>
        public static void DisableAdapter(string interfaceName)
        {
            Console.WriteLine("DisableAdapter()");
            if (!IsAdmin)
            {
                Logger.log("Can't disable network adapter; Run as Administrator");
                return;
            }
            Logger.log($"Disabling adapter \"{interfaceName}\"");
            try
            {
                string query = $"SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID = '{interfaceName}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject adapter in searcher.Get())
                    {
                        adapter.InvokeMethod("Disable", null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Logger.log(e.Message);
            }
        }
    }
}
