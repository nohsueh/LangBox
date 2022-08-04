using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace LangBox.Operaters
{
    internal class ConfigHelper
    {
        class Config
        {
            public string? Version { get; set; }
            public Dictionary<string, string>? UrlMap { get; set; }
            public string? FilesPath { get; set; }
            public Dictionary<string, bool>? LangMap { get; set; }
        }
        private static string configFilename = @"config.json";

        //初始化
        public ConfigHelper()
        {
            Config config = Json2Model();

            config.Version = @"V1.3.0";

            config.UrlMap = new Dictionary<string, string> {
                    { "C_CPP","https://github.com/NOhsueh/LangBox/releases/download/v1.3.0/x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z"},
                    { "Python","https://github.com/NOhsueh/LangBox/releases/download/v1.3.0/python-3.10.5-embed-amd64.7z"},
                    {"Java", "https://github.com/NOhsueh/LangBox/releases/download/v1.3.0/jdk-18_windows-x64_bin.7z"}
            };

            if (config.FilesPath == null)
            {
                config.FilesPath = @"D:\Program Files";
            }
            if (config.LangMap == null)
            {
                config.LangMap = new Dictionary<string, bool> {
                    { "C_CPP",false},
                    { "Python",false},
                    {"Java", false}
                    //{"CSharp",false}
                };
            }

            Model2Json(config);
        }

        private static Config Json2Model()
        {
            if (!File.Exists(configFilename))
            {
                return new Config();

            }
            string str = File.ReadAllText(configFilename);
            Config cfg = JsonConvert.DeserializeObject<Config>(str);

            return cfg;
        }

        private static void Model2Json(Config config)
        {
            string str = JsonConvert.SerializeObject(config); //转为字符串
            File.WriteAllText(configFilename, str);
        }

        public string GetVersion()
        {

            string version = Json2Model().Version;
            return version;
        }

        //public void SetVersion(string version)
        //{
        //    Config cfg = Json2Model();
        //    cfg.Version = version;
        //    Model2Json(cfg);
        //}

        public string GetFilesPath()
        {

            string filesPath = Json2Model().FilesPath;
            return filesPath;
        }

        public void SetFilesPath(string filesPath)
        {
            Config cfg = Json2Model();
            cfg.FilesPath = filesPath;
            Model2Json(cfg);
        }

        public Dictionary<string, bool> GetLangMap()
        {
            Dictionary<string, bool> langMap = Json2Model().LangMap;
            return langMap;
        }

        public void SetLangMap(string lang, bool isCheck)
        {
            Config cfg = Json2Model();

            if (cfg.LangMap.ContainsKey(lang))
            {
                cfg.LangMap[lang] = isCheck;
            }
            else
            {
                cfg.LangMap.Add(lang, isCheck);
            }
            Model2Json(cfg);
        }

        public Dictionary<string, string> GetUrlMap()
        {
            Dictionary<string, string> urlMap = Json2Model().UrlMap;
            return urlMap;
        }
    }
}
