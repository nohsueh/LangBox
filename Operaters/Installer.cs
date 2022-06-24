using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters
{
    internal class Installer
    {
        private static string langPath = "lang";

        public static void Start(Dictionary<string, bool> langMap, string langboxPath)
        {
            if (!Directory.Exists(langboxPath))
            {
                Directory.CreateDirectory(langboxPath);
            }
            //langboxPath下无文件或文件夹
            string temp = Path.Combine(langboxPath, langPath);
            Directory.CreateDirectory(temp);
        }
    }
}
