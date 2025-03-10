using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitter_4_Enhanced
{
    class HotkeyHandler
    {
        public static bool HotkeysRegistered = false;
        // DLL libraries used to manage hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        // hotkey IDs
        const int HOTKEY_ID_Solo = 1;
        const int HOTKEY_ID_Kill = 2;
        const int HOTKEY_ID_Net = 3;
        /// <summary>
        /// Calls methods based on which hotkeys were pressed
        /// </summary>
        /// <param name="m">Windows message apparently (idk)</param>
        public static void HandleHotkeyPress(ref Message m)
        {
            if (/*m.Msg == 0x0312 && */m.WParam.ToInt32() == HOTKEY_ID_Solo)
            {
                //Console.WriteLine("HOTKEY : SOLO PRESSED");
                ProcessHandler.SuspendGameProcesses();
            }
            if (/*m.Msg == 0x0312 && */m.WParam.ToInt32() == HOTKEY_ID_Kill)
            {
                //Console.WriteLine("HOTKEY : KILL PRESSED");
                ProcessHandler.KillGameProcesses();
            }
            if (/*m.Msg == 0x0312 && */m.WParam.ToInt32() == HOTKEY_ID_Net)
            {
                //Console.WriteLine("HOTKEY : NET PRESSED");
                NetworkHandler.DisableAdapter(Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString());
            }
        }
        /// <summary>
        /// Unregisters all hotkeys
        /// </summary>
        public static void UnregisterAll()
        {
            // don't allow multi-unregistering
            if (HotkeysRegistered)
            {
                UnregisterHotKey(Form1.form.Handle, HOTKEY_ID_Solo);
                UnregisterHotKey(Form1.form.Handle, HOTKEY_ID_Kill);
                UnregisterHotKey(Form1.form.Handle, HOTKEY_ID_Net);
                Logger.log("Unregistered hotkeys");
                HotkeysRegistered = false;
            }
            Form1.form.StartTimer();
        }
        /// <summary>
        /// Registers all hotkeys
        /// </summary>
        public static void RegisterAll()
        {
            // don't allow multi-registering
            if (!HotkeysRegistered)
            {
                RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Solo, ConfigHandler.config.hotkeys[0].CombinedModifiers, ConfigHandler.config.hotkeys[0].Key);
                RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Kill, ConfigHandler.config.hotkeys[1].CombinedModifiers, ConfigHandler.config.hotkeys[1].Key);
                RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Net, ConfigHandler.config.hotkeys[2].CombinedModifiers, ConfigHandler.config.hotkeys[2].Key);
                HotkeysRegistered = true;
                Logger.log("Registered hotkeys");
            }
        }
        /// <summary>
        /// Handles the changing of hotkeys
        /// </summary>
        /// <param name="e">some forwarded KeyEventArgs</param>
        /// <param name="name">SOLO,KILL,NET</param>
        public static void HandleHotkeyTextBox(KeyEventArgs e, string name)
        {
            // unregister previous hotkeys
            UnregisterAll();

            // build the key combination string
            StringBuilder keyCombo = new StringBuilder();
            int key = 0;
            int combinedModifiers = 0;

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
                key = (int)e.KeyCode;
            }
            //Logger.log(String.Format("{0}+{1}", combinedModifiers, key));

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
