using System.IO;
using System.Net;

namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private static string localPath = "D:\\LangBox Files";  //防止localPath为空
        private static bool isChecked;
        private const string fileName = "";

        public static void Start(string Path, bool Flag)
        {
            localPath = Path;
            isChecked = Flag;

            if (isChecked)
            {
                Install();
            }
            else
            {
                Uninstall();
            }
        }

        private static void Install()
        {
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
        }

        private static void Uninstall()
        {
            if (File.Exists(localPath))
            {
                Directory.Delete(localPath, true);
            }
        }
    }
}
