using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LangBox.Operaters
{
    internal class UpdateChecker
    {
        private const string version = "V0.0.0";
        private const string checkPage = "https://github.com/NOhsueh/LangBox/releases/latest";

        public static bool HasUpdate()
        {
            string content = ReadHttpSourceCode(checkPage);
            Trace.WriteLine(content);

            Regex regex = new Regex("/NOhsueh/LangBox/releases/tag/(.*)");
            Match match = regex.Match(content);

            Trace.WriteLine(version);

            if (match.Success)
            {

                Trace.WriteLine("success");

                string nowVersion = match.Groups[1].Value;
                if (string.Compare(version,nowVersion) < 0)
                    return true;
            }
            return false;
        }

        private static string ReadHttpSourceCode(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "GET";
            request.Timeout = 10000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.64 Safari/537.36 Edg/101.0.1210.53";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(new BufferedStream(response.GetResponseStream()), Encoding.UTF8);
            string content = reader.ReadToEnd();
            response.Close();
            reader.Close();
            return content;
        }
    }
}
