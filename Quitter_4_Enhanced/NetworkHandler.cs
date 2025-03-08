using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Quitter_4_Enhanced
{
    class NetworkHandler
    {
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
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
                Process p = new Process();
                p.StartInfo = psi;
                p.Start();
            }
            catch (Exception e)
            {
                if (e.GetType().IsAssignableFrom(typeof(System.IndexOutOfRangeException)))
                {
                    Console.WriteLine("No Network Interface Provided");
                }
                else
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static void DisableAdapter(string interfaceName)
        {
            Console.WriteLine("DisableAdapter()");
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
                Process p = new Process();
                p.StartInfo = psi;
                p.Start();
            }
            catch (Exception e)
            {
                if (e.GetType().IsAssignableFrom(typeof(System.IndexOutOfRangeException)))
                {
                    Console.WriteLine("No Network Interface Provided");
                }
                else
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
