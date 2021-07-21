using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace TMInterfaceQuickScript
{
    public class Config
    {
        private const string _PATH = "config.json";

        public Dictionary<string, string> CommandTranslations { get; set; }
        public string ScriptFolderPath { get; set; }

        public static Config ReadConfig(string path = _PATH)
            => JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
    }
}
