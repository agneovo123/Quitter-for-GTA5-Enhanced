using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quitter_4_Enhanced.ConfigHandler;

namespace Quitter_4_Enhanced
{
    public class HotkeyHandler : IDisposable
    {
        // delegate & imports
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private readonly LowLevelKeyboardProc _proc;
        private IntPtr _hookId = IntPtr.Zero;
        public HotkeyHandler()
        {
            _proc = HookCallback;
            _hookId = SetHook(_proc);
        }

        public static bool HotkeysRegistered = false;
        public static HotKey HOTKEY_Solo;
        public static HotKey HOTKEY_Kill;
        public static HotKey HOTKEY_Net;
        public struct HotKey
        {
            public int key;
            public bool Ctrl;
            public bool Alt;
            public bool Shift;
            public HotKey(int key, bool Ctrl, bool Alt, bool Shift)
            {
                this.key = key;
                this.Ctrl = Ctrl;
                this.Alt = Alt;
                this.Shift = Shift;
            }
        }
        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process cur = Process.GetCurrentProcess())
            using (ProcessModule mod = cur.MainModule)
            {
                return NativeMethods.SetWindowsHookEx(
                    NativeMethods.WH_KEYBOARD_LL,
                    proc,
                    NativeMethods.GetModuleHandle(mod.ModuleName),
                    0
                );
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)NativeMethods.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                //Console.WriteLine("HookCallback vkCode: " + vkCode);
                // check modifier state
                bool ctrl = (NativeMethods.GetAsyncKeyState(NativeMethods.VK_CONTROL) & 0x8000) != 0;
                bool shift = (NativeMethods.GetAsyncKeyState(NativeMethods.VK_SHIFT) & 0x8000) != 0;
                bool alt = (NativeMethods.GetAsyncKeyState(NativeMethods.VK_ALT) & 0x8000) != 0;
                if (vkCode == HOTKEY_Solo.key && HOTKEY_Solo.Ctrl == ctrl && HOTKEY_Solo.Shift == shift && HOTKEY_Solo.Alt == alt)
                {
                    Task.Run(() => ProcessHandler.SuspendGameProcesses());
                    //Console.WriteLine("HookCallback HOTKEY_Solo");
                }
                if (vkCode == HOTKEY_Kill.key && HOTKEY_Kill.Ctrl == ctrl && HOTKEY_Kill.Shift == shift && HOTKEY_Kill.Alt == alt)
                {
                    Task.Run(() => ProcessHandler.KillGameProcesses());
                    //Console.WriteLine("HookCallback HOTKEY_Kill");
                }
                if (vkCode == HOTKEY_Net.key && HOTKEY_Net.Ctrl == ctrl && HOTKEY_Net.Shift == shift && HOTKEY_Net.Alt == alt)
                {
                    string interfaceName = Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString();
                    Task.Run(() => NetworkHandler.DisableAdapter(interfaceName));
                    //Console.WriteLine("HookCallback HOTKEY_Net");
                }
            }
            // let the event pass through
            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
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
            //Console.WriteLine(String.Format("{0}+{1}", combinedModifiers, key));

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
        /// <summary>
        /// Registers all hotkeys
        /// </summary>
        public static void RegisterAll()
        {
            Console.WriteLine("RegisterAll()");
            // don't allow multi-registering
            if (!HotkeysRegistered)
            {
                SeparateModifiers(ConfigHandler.config.hotkeys[0].CombinedModifiers, out bool alt, out bool ctrl, out bool shift);
                HOTKEY_Solo = new HotKey(ConfigHandler.config.hotkeys[0].Key, ctrl, alt, shift);

                SeparateModifiers(ConfigHandler.config.hotkeys[1].CombinedModifiers, out alt, out ctrl, out shift);
                HOTKEY_Kill = new HotKey(ConfigHandler.config.hotkeys[1].Key, ctrl, alt, shift);

                SeparateModifiers(ConfigHandler.config.hotkeys[2].CombinedModifiers, out alt, out ctrl, out shift);
                HOTKEY_Net = new HotKey(ConfigHandler.config.hotkeys[2].Key, ctrl, alt, shift);

                Form1._hotkeyHandler = new HotkeyHandler();
                HotkeysRegistered = true;
                Logger.log("Registered hotkeys");
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
                // TODO: prevent [possible(?)] double Dispose();
                Form1._hotkeyHandler.Dispose();
                Logger.log("Unregistered hotkeys");
                Logger.log("Automatically registering 10 seconds after your last activity");
                HotkeysRegistered = false;
            }
            Form1.form.StartTimer();
        }

        private static void SeparateModifiers(int combinedModifiers, out bool alt, out bool ctrl, out bool shift)
        {
            alt = (combinedModifiers & 0b0001) != 0;
            ctrl = (combinedModifiers & 0b0010) != 0;
            shift = (combinedModifiers & 0b0100) != 0;
        }

        public void Dispose() { if (_hookId != IntPtr.Zero) { NativeMethods.UnhookWindowsHookEx(_hookId); } }

        private static class NativeMethods
        {
            public const int WH_KEYBOARD_LL = 13;
            public const int WM_KEYDOWN = 0x0100;

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);

            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);

            [DllImport("user32.dll")]
            public static extern short GetAsyncKeyState(int vKey);

            public const int VK_SHIFT = 0x10;
            public const int VK_CONTROL = 0x11;
            public const int VK_ALT = 0x12;
        }
    }
}
