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
        private static string langPath = @"\lang";

        public static void Start(Dictionary<string, bool> langMap, string langboxPath)
        {
            string temp = langboxPath+langPath;
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
        }
    }
}
