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

        private const int WM_INPUT = 0x00FF;
        private const int RID_INPUT = 0x10000003;
        private const uint RIDEV_INPUTSINK = 0x00000100;

        private const ushort RIM_TYPEKEYBOARD = 1;

        private const uint RIDI_PREPARSEDDATA = 0x20000005; // ???
        private const uint RIDI_DEVICEINFO = 0x2000000B;    // ???

        private static bool altDown;
        private static bool ctrlDown;
        private static bool shiftDown;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);
        [StructLayout(LayoutKind.Sequential)]
        private struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public uint dwFlags;
            public IntPtr hwndTarget;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct RAWINPUTHEADER
        {
            public uint dwType;
            public uint dwSize;
            public IntPtr hDevice;
            public IntPtr wParam;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct RAWINPUT
        {
            public RAWINPUTHEADER header;
            public RAWKEYBOARD keyboard;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct RAWKEYBOARD
        {
            public ushort MakeCode;
            public ushort Flags;
            public ushort Reserved;
            public ushort VKey;
            public uint Message;
            public uint ExtraInformation;
        }

        public static void Init()
        {
            RAWINPUTDEVICE[] devices =
            {
                new RAWINPUTDEVICE
                {
                    usUsagePage = 0x01, // Generic desktop controls
                    usUsage = 0x06,     // Keyboard
                    //dwFlags = 0,        // Do NOT suppress input
                    dwFlags = RIDEV_INPUTSINK,        // Do NOT suppress input
                    hwndTarget = Form1.form.Handle
                }
            };

            if (!RegisterRawInputDevices(devices, (uint)devices.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
            {
                MessageBox.Show("RegisterRawInputDevices failed", "ERROR", MessageBoxButtons.OK);
                throw new Exception("RegisterRawInputDevices failed");
            }

            HotkeysRegistered = true;
        }
        public static void WndProc(ref Message m)
        {
            // DO NOT OUTPUT ANYTHING HERE, THIS IS CONSTANTLY CALLED!!
            //Logger.log("HotkeyHandler.WndProc()");
            if (m.Msg == WM_INPUT)
            {
                //Logger.log("inside IF");
                if (HotkeysRegistered)
                {
                    ProcessRawInput(m.LParam);
                }
                else
                {
                    Logger.log("HotkeysRegistered == FALSE; --> IGNORING INPUT");
                }
            }
        }
        private static void ProcessRawInput(IntPtr hRawInput)
        {
            uint size = 0;
            GetRawInputData(hRawInput, RID_INPUT, IntPtr.Zero, ref size, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));
            IntPtr buffer = Marshal.AllocHGlobal((int)size);

            try
            {
                if (GetRawInputData(hRawInput, RID_INPUT, buffer, ref size, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) != size) { return; }
                RAWINPUT raw = Marshal.PtrToStructure<RAWINPUT>(buffer);

                if (raw.header.dwType == RIM_TYPEKEYBOARD)
                {
                    bool keyDown = raw.keyboard.Message == 0x0100 || raw.keyboard.Message == 0x0104;
                    bool keyUp = raw.keyboard.Message == 0x0101 || raw.keyboard.Message == 0x0105;
                    Keys key = (Keys)raw.keyboard.VKey;

                    if (keyDown) { OnKeyDown(key); }
                    if (keyUp) { OnKeyUp(key); }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        private static void OnKeyDown(Keys key)
        {
            if (key == Keys.Alt) { altDown = true; }
            if (key == Keys.ControlKey) { ctrlDown = true; }
            if (key == Keys.ShiftKey) { shiftDown = true; }

            int combinedModifiers = 0;
            if (altDown) { combinedModifiers += 1; }
            if (ctrlDown) { combinedModifiers += 2; }
            if (shiftDown) { combinedModifiers += 4; }

            if (combinedModifiers == ConfigHandler.config.hotkeys[0].CombinedModifiers && (uint)key == ConfigHandler.config.hotkeys[0].Key)
            {
                Logger.log("hotkey for SuspendGameProcesses() pressed");
                Task.Run(() => ProcessHandler.SuspendGameProcesses());
            }
            if (combinedModifiers == ConfigHandler.config.hotkeys[1].CombinedModifiers && (uint)key == ConfigHandler.config.hotkeys[1].Key)
            {
                Logger.log("hotkey for KillGameProcesses() pressed");
                Task.Run(() => ProcessHandler.KillGameProcesses());
            }
            if (combinedModifiers == ConfigHandler.config.hotkeys[2].CombinedModifiers && (uint)key == ConfigHandler.config.hotkeys[2].Key)
            {
                Logger.log("hotkey for DisableAdapter() pressed");
                string interfaceName = Form1.form.comboBox_Networks.Items[Form1.form.comboBox_Networks.SelectedIndex].ToString();
                Task.Run(() => NetworkHandler.DisableAdapter(interfaceName));

            }
        }
        private static void OnKeyUp(Keys key)
        {
            if (key == Keys.Alt) { altDown = false; }
            if (key == Keys.ControlKey) { ctrlDown = false; }
            if (key == Keys.ShiftKey) { shiftDown = false; }
        }
        /// <summary>
        /// Registers hotkeys
        /// </summary>
        public static void RegisterHotkeys()
        {
            Logger.logDEBUG($"RegisterHotkeys() called");
            // don't allow multi-registering
            if (!HotkeysRegistered)
            {
                HotkeysRegistered = true;
                Logger.log("Registered hotkeys");
            }
        }
        /// <summary>
        /// Unregisters hotkeys
        /// </summary>
        public static void UnregisterHotkeys()
        {
            Logger.logDEBUG($"UnregisterHotkeys() called");
            // don't allow multi-unregistering
            if (HotkeysRegistered)
            {
                Logger.log("Unregistered hotkeys");
                Logger.log("Automatically registering 3 seconds after your last activity");
                HotkeysRegistered = false;
            }
            Form1.form.StartTimer();
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
            UnregisterHotkeys();

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
