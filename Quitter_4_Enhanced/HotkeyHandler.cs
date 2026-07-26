using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitter_4_Enhanced
{
    public class HotkeyHandler
    {
        public static bool HotkeysRegistered = false;
        public static HotKey HOTKEY_Solo;
        public static HotKey HOTKEY_Kill;
        public static HotKey HOTKEY_Net;
        public struct HotKey
        {
            public uint key;
            public bool Ctrl;
            public bool Alt;
            public bool Shift;
            public HotKey(uint key, bool Ctrl, bool Alt, bool Shift)
            {
                this.key = key;
                this.Ctrl = Ctrl;
                this.Alt = Alt;
                this.Shift = Shift;
            }
        }

        [DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Registers all hotkeys
        /// </summary>
        public static void RegisterAll()
        {
            Logger.logDEBUG($"RegisterAll() called");
            Console.WriteLine("RegisterAll()");
            // don't allow multi-registering
            if (!HotkeysRegistered)
            {
                RegisterHotKey(Form1.form.Handle, 1, ConfigHandler.config.hotkeys[0].CombinedModifiers, ConfigHandler.config.hotkeys[0].Key);
                RegisterHotKey(Form1.form.Handle, 2, ConfigHandler.config.hotkeys[1].CombinedModifiers, ConfigHandler.config.hotkeys[1].Key);
                RegisterHotKey(Form1.form.Handle, 3, ConfigHandler.config.hotkeys[2].CombinedModifiers, ConfigHandler.config.hotkeys[2].Key);

                HotkeysRegistered = true;
                Logger.log("Registered hotkeys");
            }
        }
        /// <summary>
        /// Unregisters all hotkeys
        /// </summary>
        public static void UnregisterAll()
        {
            Logger.logDEBUG($"UnregisterAll() called");
            // don't allow multi-unregistering
            if (HotkeysRegistered)
            {
                UnregisterHotKey(Form1.form.Handle, 1);
                UnregisterHotKey(Form1.form.Handle, 2);
                UnregisterHotKey(Form1.form.Handle, 3);
                Logger.log("Unregistered hotkeys");
                Logger.log("Automatically registering 10 seconds after your last activity");
                HotkeysRegistered = false;
            }
            Form1.form.StartTimer();
        }


        public static void WndProc(ref Message m)
        {
            // DO NOT OUTPUT ANYTHING HERE, THIS IS CONSTANTLY CALLED!!
            //Logger.log("HotkeyHandler.WndProc()");

            // WM_HOTKEY magic number 0x0312
            if (m.Msg == 0x0312)
            {
                //Logger.log("inside IF");
                switch (m.WParam.ToInt32())
                {
                    case 1:
                        {
                            Logger.log("hotkey for SuspendGameProcesses() pressed");
                            Task.Run(() => ProcessHandler.SuspendGameProcesses());
                            break;
                        }
                    case 2:
                        {
                            Logger.log("hotkey for KillGameProcesses() pressed");
                            Task.Run(() => ProcessHandler.KillGameProcesses());
                            break;
                        }
                    case 3:
                        {
                            Logger.log("hotkey for DisableAdapter() pressed");
                            string interfaceName = Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString();
                            Task.Run(() => NetworkHandler.DisableAdapter(interfaceName));
                            break;
                        }
                }
            }
        }


        /// <summary>
        /// Handles the changing of hotkeys
        /// </summary>
        /// <param name="e">some forwarded KeyEventArgs</param>
        /// <param name="name">SOLO,KILL,NET</param>
        public static void HandleHotkeyTextBox(KeyEventArgs e, string name)
        {
            Logger.logDEBUG($"HandleHotkeyTextBox() called");
            // unregister previous hotkeys
            UnregisterAll();

            // build the key combination string
            StringBuilder keyCombo = new StringBuilder();
            uint key = 0;
            uint combinedModifiers = 0;

            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...

            // check for modifier keys
            if (e.Alt)
            {
                keyCombo.Append("Alt+");
                combinedModifiers += 1;
            }
            if (e.Control)
            {
                keyCombo.Append("Ctrl+");
                combinedModifiers += 2;
            }
            if (e.Shift)
            {
                keyCombo.Append("Shift+");
                combinedModifiers += 4;
            }

            // avoid appending the modifier key itself if it is the only key pressed.
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ShiftKey)
            {
                // translate "Next" to "PageDown" because windows is stupid
                if (e.KeyCode.ToString() == "Next") { keyCombo.Append("PageDown"); }
                // append key to string
                else { keyCombo.Append(e.KeyCode.ToString()); }
                key = (uint)e.KeyCode;
            }

            Logger.logDEBUG($"name: {name}");
            Logger.logDEBUG($"  keyCombo: {keyCombo.ToString()}");
            Logger.logDEBUG($"  key: {key}");
            Logger.logDEBUG($"  combinedModifiers: {combinedModifiers}");

            // display the key combination in the textbox(es)
            // and update config
            if (name == "SOLO")
            {
                Form1.form.textBox_SoloKey.Text = keyCombo.ToString();
                ConfigHandler.config.hotkeys[0].Key = key;
                ConfigHandler.config.hotkeys[0].CombinedModifiers = combinedModifiers;
            }
            if (name == "KILL")
            {
                Form1.form.textBox_KillKey.Text = keyCombo.ToString();
                ConfigHandler.config.hotkeys[1].Key = key;
                ConfigHandler.config.hotkeys[1].CombinedModifiers = combinedModifiers;
            }
            if (name == "NET")
            {
                Form1.form.textBox_NetworkKey.Text = keyCombo.ToString();
                ConfigHandler.config.hotkeys[2].Key = key;
                ConfigHandler.config.hotkeys[2].CombinedModifiers = combinedModifiers;
            }
            // prevent default behavior
            e.SuppressKeyPress = true;
            e.Handled = true;
        }
    }
}
