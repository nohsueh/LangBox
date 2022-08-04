using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace LangBox.Operators
{
    internal class UpdateChecker
    {
        private static ConfigHelper cfg = new ConfigHelper();
        private const string checkPage = "https://github.com/NOhsueh/LangBox/releases/latest";

        public static bool HasUpdate()
        {
            string nowVersion = GetVersion();
            string version = cfg.GetVersion();
            if (string.Compare(version, nowVersion) < 0)
                return true;
            else return false;
        }

        public static string GetVersion()
        {
            string content = ReadHttpSourceCode(checkPage);
            Regex regex = new Regex("href=\"/NOhsueh/LangBox/releases/tag/(.*)\" data-view-component=\"true\"");
            Match match = regex.Match(content);

            if (match.Success)
            {
                string nowVersion = match.Groups[1].Value;
                return nowVersion;
            }
            return cfg.GetVersion();
        }

        private static string ReadHttpSourceCode(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.Timeout = 10000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.63 Safari/537.36 Edg/102.0.1245.30";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(new BufferedStream(response.GetResponseStream()), Encoding.UTF8);
            string content = reader.ReadToEnd();
            response.Close();
            reader.Close();
            return content;
        }
    }
}
