﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Quitter_4_Enhanced
{
    class ConfigHandler
    {
        public static Config config = new Config();
        // Config's 3 hotkeys:
        // 0: solo
        // 1: kill
        // 2: net
        public class Config
        {
            public KeyCombo[] hotkeys;
            public int selectedAdapter;
            public int suspendInterval;
            public int dropDelay;
            public override string ToString()
            {
                string tmp = "";
                foreach (KeyCombo keys in hotkeys)
                {
                    tmp += "\nKEYS:" + keys.CombinedModifiers + ":" + keys.Key;
                }
                tmp += String.Format("\n selectedAdapter: {0}", selectedAdapter);
                tmp += String.Format("\n suspendInterval: {0}", suspendInterval);
                tmp += String.Format("\n dropDelay: {0}", dropDelay);

                return tmp;
            }
        }
        public class KeyCombo
        {
            public int Key;
            public int CombinedModifiers;
        }
        /// <summary>
        /// config's filename
        /// </summary>
        private const string configFilename = "Quitter4Enahnced.json";
        /// <summary>
        /// Loads config from file <br/>
        /// or from hardcoded defaults if file wasn't found.
        /// </summary>
        public static void TryLoadFromConfig()
        {
            // try to load from own file
            if (File.Exists(configFilename))
            {
                LoadConfig();
                Logger.log("Config loaded from own file");
                //Console.WriteLine("Config loaded from own file");
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
                //Console.WriteLine("Config files not found:\n - Defaults loaded");
                SaveConfig();
            }
            ApplyConfigValues();
        }
        /// <summary>
        /// Sets controls' values from config
        /// </summary>
        private static void ApplyConfigValues()
        {
            //Console.WriteLine("SetTextBoxValues()");

            Form1.form.textBox_SoloKey.Text = GetStringFromKeys(config.hotkeys[0]);
            Form1.form.textBox_KillKey.Text = GetStringFromKeys(config.hotkeys[1]);
            Form1.form.textBox_NetworkKey.Text = GetStringFromKeys(config.hotkeys[2]);
            Form1.form.textBox_SoloTime.Text = config.suspendInterval.ToString();
            Form1.form.textBox_NetworkTime.Text = config.dropDelay.ToString();

            Form1.form.timer_suspend.Interval = config.suspendInterval;
            Form1.form.timer_network.Interval = config.dropDelay * 1000;
        }
        /// <summary>
        /// turns the keys object into a string
        /// </summary>
        /// <param name="keys">MyKeys keys to check</param>
        /// <returns></returns>
        private static string GetStringFromKeys(KeyCombo keys)
        {
            StringBuilder keyCombo = new StringBuilder();
            if ((keys.CombinedModifiers & 1) == 1) { keyCombo.Append("Alt+"); }
            if ((keys.CombinedModifiers & 2) == 2) { keyCombo.Append("Ctrl+"); }
            if ((keys.CombinedModifiers & 4) == 4) { keyCombo.Append("Shift+"); }

            //Console.WriteLine(KeysToString(keys.Key));
            keyCombo.Append(KeysToString(keys.Key));

            return keyCombo.ToString();
        }
        /// <summary>
        /// System.Windows.Forms.Keys enum but in reverse<br/>
        /// as in: you provide an int and get a key's name
        /// </summary>
        static string KeysToString(int keyCode)
        {
            string name = Enum.GetName(typeof(Keys), keyCode) ?? "UnknownKey";
            if (name == "Prior") { name = "PageUp"; }
            if (name == "Next") { name = "PageDown"; }
            return name;
        }

        /// <summary>
        /// load default values into config
        /// </summary>
        private static void LoadDefaults()
        {
            config.selectedAdapter = 0;
            config.suspendInterval = 10000;
            config.dropDelay = 10;

            LoadDefaultHotkeys();
        }

        /// <summary>
        /// load default hotkey values into config
        /// </summary>
        private static void LoadDefaultHotkeys()
        {
            config.hotkeys = new KeyCombo[3];
            for (int i = 0; i < config.hotkeys.Length; i++)
            {
                config.hotkeys[i] = new KeyCombo();
            }

            config.hotkeys[0].Key = 33;
            config.hotkeys[0].CombinedModifiers = 6;
            config.hotkeys[1].Key = 46;
            config.hotkeys[1].CombinedModifiers = 6;
            config.hotkeys[2].Key = 35;
            config.hotkeys[2].CombinedModifiers = 6;
        }
        /// <summary>
        /// Saves config to file
        /// </summary>
        public static void SaveConfig()
        {
            //Console.WriteLine("SaveConfig()");
            // originally used JSON serialization.
            string JSONString = "{"
            + "\n  \"hotkeys\": ["
            + "\n  {"
            + $"\n    \"Key\": {config.hotkeys[0].Key},"
            + $"\n    \"CombinedModifiers\": {config.hotkeys[0].CombinedModifiers}"
            + "\n  },"
            + "\n  {"
            + $"\n    \"Key\": {config.hotkeys[1].Key},"
            + $"\n    \"CombinedModifiers\": {config.hotkeys[1].CombinedModifiers}"
            + "\n  },"
            + "\n  {"
            + $"\n    \"Key\": {config.hotkeys[2].Key},"
            + $"\n    \"CombinedModifiers\": {config.hotkeys[2].CombinedModifiers}"
            + "\n  }],"
            + $"\n  \"selectedAdapter\": {config.selectedAdapter},"
            + $"\n  \"suspendInterval\": {config.suspendInterval},"
            + $"\n  \"dropDelay\": {config.dropDelay}"
            + "\n}";

            File.WriteAllText(configFilename, JSONString);
            //Console.WriteLine(JSONString);
            Logger.log("Config autosaved");
        }
        /// <summary>
        /// Loads config From file
        /// </summary>
        private static void LoadConfig()
        {
            //Console.WriteLine("LoadConfig()");

            string[] lines = File.ReadAllLines(configFilename);
            // init config
            config = new Config();
            // "read" hotkeys
            config.hotkeys = new KeyCombo[3];
            for (int i = 0; i < config.hotkeys.Length; i++) { config.hotkeys[i] = new KeyCombo(); }
            config.hotkeys[0].Key = GetNumberFromLine(lines, 3);
            config.hotkeys[0].CombinedModifiers = GetNumberFromLine(lines, 4);
            config.hotkeys[1].Key = GetNumberFromLine(lines, 7);
            config.hotkeys[1].CombinedModifiers = GetNumberFromLine(lines, 8);
            config.hotkeys[2].Key = GetNumberFromLine(lines, 11);
            config.hotkeys[2].CombinedModifiers = GetNumberFromLine(lines, 12);
            // "read" other
            config.selectedAdapter = GetNumberFromLine(lines, 14);
            config.suspendInterval = GetNumberFromLine(lines, 15);
            config.dropDelay = GetNumberFromLine(lines, 16);

            if (config.hotkeys == null)
            {
                Logger.log("[Warn] Hotkeys were null after loading from file");
                LoadDefaultHotkeys();
            }
            //Console.WriteLine(config.ToString());
            return;
        }

        /// <summary>
        /// Gets last number from a line of text
        /// </summary>
        /// <param name="lines">array of lines</param>
        /// <param name="lineIdx">which line to get the number from</param>
        /// <returns></returns>
        private static int GetNumberFromLine(string[] lines, int lineIdx)
        {
            //Console.WriteLine("GetNumberFromLine()");
            string line = lines[lineIdx];
            // remove everything after the number
            while (!IsNumber(line.Substring(line.Length - 1, 1)))
            {
                line = line.Substring(0, line.Length - 1);
            }
            // get the number's "length"
            int numLength = 1;
            while (IsNumber(line.Substring(line.Length - numLength - 1, 1)))
            {
                numLength++;
            }
            // extract number
            int num = Convert.ToInt32(line.Substring(line.Length - numLength, numLength));
            //Console.WriteLine("number: " + num);
            return num;
        }
        /// <summary>
        /// is a piece of string a number or not<br/>
        /// 0-9 returns true, anything else returns false
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static bool IsNumber(string v)
        {
            return (v == "0" || v == "1" || v == "2" || v == "3" || v == "4" || v == "5" || v == "6" || v == "7" || v == "8" || v == "9");
        }
    }
}
