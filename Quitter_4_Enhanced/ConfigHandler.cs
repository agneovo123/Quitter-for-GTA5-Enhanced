using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Quitter_4_Enhanced
{
    class ConfigHandler
    {
        public static Config config = new Config();
        public class Config
        {
            public MyKeys[] hotkeys { get; set; }
            public int selectedAdapter { get; set; }
            public int suspendInterval { get; set; }
            public int dropDelay { get; set; }
            public override string ToString()
            {
                string tmp = "";
                foreach (MyKeys keys in hotkeys)
                {
                    tmp += "\nKEYS:" + keys.CombinedModifiers + ":" + keys.Key;
                }
                tmp += String.Format("\n selectedAdapter: {0}", selectedAdapter);
                tmp += String.Format("\n suspendInterval: {0}", suspendInterval);
                tmp += String.Format("\n dropDelay: {0}", dropDelay);

                return tmp;
            }
        }
        public class MyKeys
        {
            public int Key { get; set; }
            public int CombinedModifiers { get; set; }
        }
        private static string myFileName = "Quitter4Enahnced.json";
        public static void TryLoadFromConfig()
        {
            // loading starts here.


            // try to load from own file
            if (File.Exists(myFileName))
            {
                string JSONstr = File.ReadAllText(myFileName);
                LoadFromJSON(JSONstr);
                Logger.log("Config loaded from own file");
                Console.WriteLine("Config loaded from own file");
            }
            // no load from old quitter because it uses a different encoding of the keys
            //else if (File.Exists("Quitter.json"))
            //{
            //    string JSONstr = File.ReadAllText("Quitter.json");
            //    LoadFromJSON(JSONstr);
            //    Logger.log("Config loaded from the old quitter's file");
            //    SaveConfig();
            //}
            // if no file found, load defaults
            else
            {
                LoadDefaults();
                Logger.log("Config files not found:");
                Logger.log(" - Defaults loaded");
                Console.WriteLine("Config files not found:\n - Defaults loaded");
                SaveConfig();
            }
            SetTextBoxValues();
        }

        private static void SetTextBoxValues()
        {
            Console.WriteLine("SetTextBoxValues()");

            Form1.form.textBox_SoloKey.Text = GetStringFromKeys(config.hotkeys[0]);
            Form1.form.textBox_KillKey.Text = GetStringFromKeys(config.hotkeys[1]);
            Form1.form.textBox_NetworkKey.Text = GetStringFromKeys(config.hotkeys[2]);
            Form1.form.textBox_SoloTime.Text = config.suspendInterval.ToString();
            Form1.form.textBox_NetworkTime.Text = config.dropDelay.ToString();

            Form1.form.timer_suspend.Interval = config.suspendInterval;
            Form1.form.timer_network.Interval = config.dropDelay * 1000;
        }

        private static string GetStringFromKeys(MyKeys keys)
        {
            StringBuilder keyCombo = new StringBuilder();
            if ((keys.CombinedModifiers & 1) == 1)
            {
                keyCombo.Append("Alt+");
            }
            if ((keys.CombinedModifiers & 2) == 2)
            {
                keyCombo.Append("Ctrl+");
            }
            if ((keys.CombinedModifiers & 4) == 4)
            {
                keyCombo.Append("Shift+");
            }

            //Console.WriteLine(KeysToString(keys.Key));
            keyCombo.Append(KeysToString(keys.Key));

            return keyCombo.ToString();
        }

        static string KeysToString(int keyCode)
        {
            string name = Enum.GetName(typeof(Keys), keyCode) ?? "UnknownKey";
            if (name == "Prior") { name = "PageUp"; }
            if (name == "Next") { name = "PageDown"; }
            return name;
        }

        private static void LoadDefaults()
        {
            config.selectedAdapter = 0;
            config.suspendInterval = 10000;
            config.dropDelay = 10;

            LoadDefaultHotkeys();
        }

        private static void LoadDefaultHotkeys()
        {
            config.hotkeys = new MyKeys[3];
            for (int i = 0; i < config.hotkeys.Length; i++)
            {
                config.hotkeys[i] = new MyKeys();
            }

            config.hotkeys[0].Key = 33;
            config.hotkeys[0].CombinedModifiers = 6;
            config.hotkeys[1].Key = 46;
            config.hotkeys[1].CombinedModifiers = 6;
            config.hotkeys[2].Key = 35;
            config.hotkeys[2].CombinedModifiers = 6;
        }

        public static void SaveConfig()
        {
            Console.WriteLine("SaveConfig()");

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string JSONString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(myFileName, JSONString);

            //Console.WriteLine(JSONString);

            Logger.log("Config autosaved");
        }

        private static void LoadFromJSON(string jsonString)
        {
            Console.WriteLine(jsonString);


            // Enable case-insensitive property matching
            //JsonSerializerOptions options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //};
            //Config result = JsonSerializer.Deserialize<Config>(jsonString, options);

            config = JsonSerializer.Deserialize<Config>(jsonString);

            //Console.WriteLine(config.selectedAdapter);
            //Console.WriteLine(config.suspendInterval);
            //Console.WriteLine(config.dropDelay);

            if (config.hotkeys == null)
            {
                Logger.log("[Warn] Hotkeys were null after loading from file");
                LoadDefaultHotkeys();
            }

            Console.WriteLine(config.ToString());

            return;
        }
    }
}
