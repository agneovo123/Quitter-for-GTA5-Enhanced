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

        const int HOTKEY_ID_Solo = 1;
        const int HOTKEY_ID_Kill = 2;
        const int HOTKEY_ID_Net = 3;

        public static void HandleHotkeyPress(ref Message m)
        {
            if (/*m.Msg == 0x0312 && */m.WParam.ToInt32() == HOTKEY_ID_Solo)
            {
                Console.WriteLine("HOTKEY : SOLO PRESSED");
                ProcessHandler.SuspendGameProcess();
            }
            if (/*m.Msg == 0x0312 && */m.WParam.ToInt32() == HOTKEY_ID_Kill)
            {
                Console.WriteLine("HOTKEY : KILL PRESSED");
                ProcessHandler.KillGameProcess();
            }
            if (/*m.Msg == 0x0312 && */m.WParam.ToInt32() == HOTKEY_ID_Net)
            {
                Console.WriteLine("HOTKEY : NET PRESSED");
                NetworkHandler.DisableAdapter(Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString());
                Form1.form.timer_network.Start();
            }
        }

        public static void SetDefaults()
        {
            Form1.form.textBox_SoloKey.Text = "Ctrl+Shift+PageUp";
            RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Solo, 6, 33);

            Form1.form.textBox_KillKey.Text = "Ctrl+Shift+Delete";
            RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Kill, 6, 46);

            Form1.form.textBox_NetworkKey.Text = "Ctrl+Shift+End";
            RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Net, 6, 35);
        }

        public static void UnregisterAll()
        {
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
        public static void RegisterAll()
        {
            if (!HotkeysRegistered)
            {
                RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Solo, ConfigHandler.config.hotkeys[0].CombinedModifiers, ConfigHandler.config.hotkeys[0].Key);
                RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Kill, ConfigHandler.config.hotkeys[1].CombinedModifiers, ConfigHandler.config.hotkeys[1].Key);
                RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Net, ConfigHandler.config.hotkeys[2].CombinedModifiers, ConfigHandler.config.hotkeys[2].Key);
                HotkeysRegistered = true;
                Logger.log("Registered hotkeys");
            }
        }

        public static void HandleHotkeyTextBox(KeyEventArgs e, string name)
        {
            // Unregister previous hotkeys
            UnregisterAll();
            //if (name == "SOLO")
            //{
            //    UnregisterHotKey(Form1.form.Handle, HOTKEY_ID_Solo);
            //}
            //if (name == "KILL")
            //{
            //    UnregisterHotKey(Form1.form.Handle, HOTKEY_ID_Kill);
            //}
            //if (name == "NET")
            //{
            //    UnregisterHotKey(Form1.form.Handle, HOTKEY_ID_Net);
            //}


            // Build the key combination string
            StringBuilder keyCombo = new StringBuilder();
            int key = 0;
            int combinedModifiers = 0;

            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            // "CombinedModifiers"

            // Check for modifier keys
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

            // Avoid appending the modifier key itself if it is the only key pressed.
            if (e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.Menu && e.KeyCode != Keys.ShiftKey)
            {
                if (e.KeyCode.ToString() == "Next")
                {
                    keyCombo.Append("PageDown");
                }
                else
                {
                    keyCombo.Append(e.KeyCode.ToString());
                }
                key = (int)e.KeyCode;
            }

            //Logger.log(String.Format("{0}+{1}", combinedModifiers, key));

            // Display the key combination in the textbox
            if (name == "SOLO")
            {
                Form1.form.textBox_SoloKey.Text = keyCombo.ToString();
                ConfigHandler.config.hotkeys[0].Key = key;
                ConfigHandler.config.hotkeys[0].CombinedModifiers = combinedModifiers;
                //RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Solo, combinedModifiers, key);
            }
            if (name == "KILL")
            {
                Form1.form.textBox_KillKey.Text = keyCombo.ToString();
                ConfigHandler.config.hotkeys[1].Key = key;
                ConfigHandler.config.hotkeys[1].CombinedModifiers = combinedModifiers;
                //RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Kill, combinedModifiers, key);
            }
            if (name == "NET")
            {
                Form1.form.textBox_NetworkKey.Text = keyCombo.ToString();
                ConfigHandler.config.hotkeys[2].Key = key;
                ConfigHandler.config.hotkeys[2].CombinedModifiers = combinedModifiers;
                //RegisterHotKey(Form1.form.Handle, HOTKEY_ID_Net, combinedModifiers, key);
            }

            // Optionally, prevent the default behavior
            e.SuppressKeyPress = true;
            e.Handled = true;
        }
    }
}
