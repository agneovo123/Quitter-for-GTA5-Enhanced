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
        private static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
        private static void CheckAdminRights()
        {
            IsAdmin = IsAdministrator();
            if (!IsAdmin)
            {
                Form1.form.label_DropConn.ForeColor = System.Drawing.Color.FromArgb(0, 255, 255);
                Form1.form.textBox_NetworkKey.ForeColor = System.Drawing.Color.FromArgb(170, 68, 68);
                Form1.form.comboBox_Networks.ForeColor = System.Drawing.Color.FromArgb(170, 68, 68);
                Logger.log("[WARN] Not launched as Administrator, can't manage network");
                Logger.log("Drop network connection is unavailable");
            }
        }
        public static void GetNetworks()
        {
            List<string> adapters = net_adapters();
            // clear items
            Form1.form.comboBox_Networks.Items.Clear();
            // add adapter names
            foreach (string adapter in adapters)
            {
                Console.WriteLine("Adapter: \"{0}\"", adapter);
                Form1.form.comboBox_Networks.Items.Add(adapter);
            }
            Form1.form.comboBox_Networks.SelectedIndex = ConfigHandler.config.selectedAdapter;

            CheckAdminRights();
            //loading ends here.
            Form1.IgnoreBecauseLoading = false;
        }
        private static System.Collections.Generic.List<String> net_adapters()
        {
            List<String> values = new List<String>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                values.Add(nic.Name);
            }
            return values;
        }
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
