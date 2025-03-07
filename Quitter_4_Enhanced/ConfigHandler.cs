using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Text.Json;

namespace Quitter_4_Enhanced
{
    class ConfigHandler
    {

    }



    /*
    internal class BeatSaver
    {
        private class Map
        {
            public string id;
            public string name;
            public bool blRanked;
            public override string ToString()
            {
                return id + ":" + blRanked + ":" + name;
            }
        }
        private static List<Map> maps = new List<Map>();
        private static string rankedDBName = "_RankedMapsDataBase.txt";
        public static async void CheckRanked()
        {
            Console.WriteLine("Checking maps for ranked:");
            List<string> mapKeys = Optimizer.ReadMapKeys(2).ToList();
            Console.WriteLine("mapKeys: " + mapKeys.Count);
            // remove maps that are already known to be ranked from mapKeys list
            if (File.Exists(rankedDBName))
            {
                string[] lines = File.ReadAllLines(rankedDBName);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Split(':')[1] == "True")
                    {
                        // remove from mapKeys (a.k.a. don't get data; a.k.a. saving bandwidth)
                        mapKeys.Remove(lines[i].Split(':')[0]);
                        // put the map into maps List
                        Map tmp = new Map();
                        tmp.id = lines[i].Split(':')[0];
                        tmp.blRanked = true;
                        tmp.name = lines[i].Split(':')[2];
                        maps.Add(tmp);
                    }
                }
            }
            Console.WriteLine("mapKeys (after removes): " + mapKeys.Count);
            string[] fifties = new string[(int)Math.Ceiling((float)mapKeys.Count / 50)];
            string[] results = new string[fifties.Length];

            // divide mapkeys into groups of 50
            // (BeatSaver API has a limit of 50 maps at a time)
            int j;
            for (int i = 0; i < mapKeys.Count; i++)
            {
                j = i / 50;
                if (i + 1 == mapKeys.Count || j != (i + 1) / 50)
                { fifties[j] += mapKeys[i]; }
                else { fifties[j] += mapKeys[i] + "%2C"; }
            }

            // Get maps' data from BeatSaver API
            Console.WriteLine("Getting map info from BeatSaver");
            int loadingbarOffset = 5;
            if (Settings.EnableLoadingBar) { loadingbarOffset = 7; }
            Optimizer.ResetLoadingBar();
            for (int i = 0; i < fifties.Length; i++)
            {
                results[i] = await GetBunch(fifties[i]);
                Optimizer.LoadingBar((ulong)i, (ulong)fifties.Length, loadingbarOffset);
            }

            // read maps' data from JSON
            for (int i = 0; i < results.Length; i++)
            {
                //Console.WriteLine("Length: " + results[i].Length);
                GetMapsFromJSON(results[i]);
            }

            // save to file
            StreamWriter sw = new StreamWriter(rankedDBName);
            StreamWriter swrank = new StreamWriter("rankedPlaylist.bplist");
            string rankedList = "{"
                + "\n    \"playlistTitle\": \"Csaba\","
                + "\n    \"playlistAuthor\": \"KaeM\","
                + "\n    \"songs\": [";
            int rankedC = 0;
            for (int i = 0; i < maps.Count; i++)
            {
                //Console.WriteLine(maps[i]);
                if (maps[i].blRanked)
                {
                    rankedC++;
                    rankedList += "\n        {\"key\": \"" + maps[i].id + "\"},";
                }
                sw.WriteLine(maps[i]);
            }
            rankedList = rankedList.Substring(0, rankedList.Length - 1);
            rankedList += "\n    ]\n}";
            swrank.WriteLine(rankedList);
            swrank.Close();
            sw.Close();

            Console.WriteLine("maps: " + maps.Count);
            Console.WriteLine("ranked maps: " + rankedC);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[ DONE'D² ]");
        }
        private static void GetMapsFromJSON(string jsonString)
        {
            //Console.WriteLine("ASDF()");
            //Console.WriteLine(jsonString);

            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = doc.RootElement;
                //ParseElement(root);
                if (root.ValueKind == JsonValueKind.Object)
                {
                    foreach (JsonProperty property in root.EnumerateObject())
                    {
                        Map tmp = new Map();
                        tmp.id = property.Name;
                        //Console.WriteLine($"Property: {property.Name}");
                        foreach (JsonProperty property2 in property.Value.EnumerateObject())
                        {
                            //Console.WriteLine($"----Property: {property2.Name}");
                            //if (property2.Name == "id")
                            //{
                            //    //Console.WriteLine($"----val: {property2.Value}");
                            //    tmp.id = Convert.ToString(property2.Value);
                            //}
                            if (property2.Name == "name")
                            {
                                //Console.WriteLine($"----val: {property2.Value}");
                                tmp.name = Convert.ToString(property2.Value);
                            }
                            if (property2.Name == "blRanked")
                            {
                                //Console.WriteLine($"----val: {property2.Value}");
                                tmp.blRanked = Convert.ToString(property2.Value) == "True";
                            }
                        }
                        maps.Add(tmp);
                    }
                }
            }
        }

        static void ParseElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (JsonProperty property in element.EnumerateObject())
                    {
                        Console.WriteLine($"Property: {property.Name}");
                        ParseElement(property.Value);
                    }
                    break;

                case JsonValueKind.Array:
                    foreach (JsonElement arrayElement in element.EnumerateArray())
                    {
                        ParseElement(arrayElement);
                    }
                    break;

                case JsonValueKind.String:
                    Console.WriteLine($"String: {element.GetString()}");
                    break;

                case JsonValueKind.Number:
                    if (element.TryGetInt32(out int intValue))
                    {
                        Console.WriteLine($"Int: {intValue}");
                    }
                    else if (element.TryGetDouble(out double doubleValue))
                    {
                        Console.WriteLine($"Double: {doubleValue}");
                    }
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    Console.WriteLine($"Boolean: {element.GetBoolean()}");
                    break;

                case JsonValueKind.Null:
                    Console.WriteLine("Null");
                    break;

                default:
                    Console.WriteLine("Other");
                    break;
            }
        }
        private static async Task<string> GetBunch(string maps)
        {
            using (HttpClient client = new HttpClient())
            {
                // 1 map: 11b49
                //string url = "https://api.beatsaver.com/maps/id/11b49";
                // many maps (max 50 at a time): 11b49,12af0,13b17,19be1,35be7,37eaa,144bc,1689a
                // comma char becomes "%2C"
                //string url = "https://api.beatsaver.com/maps/ids/11b49%2C12af0%2C13b17%2C19be1%2C35be7%2C37eaa%2C144bc%2C1689a";
                string url = "https://api.beatsaver.com/maps/ids/" + maps;
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Throws an exception if the response status code is not successful

                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);
                    return responseBody;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n[ error ] <- in GetBunch() <- in CheckRanked()");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(e);
                    return "";
                }
            }
        }

        public static async void Synchronize()
        {
            Console.WriteLine("this doesn't work");
            //Console.WriteLine("BeatSaverAPIKey: " + Settings.BeatSaverAPIKey);
            //Console.WriteLine("BeatSaverPlaylistLink: " + Settings.BeatSaverPlaylistLink);
            /*
            using (HttpClient client = new HttpClient())
            {
                // Replace with your API endpoint
                //  https://beatsaver.com/playlists/565700
                string url = "https://api.beatsaver.com/maps/id/2f187";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Throws an exception if the response status code is not successful

                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            */
    //    }
    //}
}
