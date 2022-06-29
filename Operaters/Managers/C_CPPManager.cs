using System.IO;
using System.Net;

namespace LangBox.Operaters.Managers
{
    internal class C_CPPManager
    {
        private static string localPath = "D:\\LangBox Files";  //防止localPath为空
        private static bool isChecked;
        private const string fileName = "mingw.exe";


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

            string fileDownload = Path.Combine(localPath, fileName);
            if (File.Exists(fileDownload))
            {
                File.Delete(fileDownload);
            }

            WebClient wc = new WebClient();
            wc.DownloadFile("https://osdn.net/projects/mingw/downloads/68260/mingw-get-setup.exe/", fileDownload);
        }

        private static void Uninstall()
        {
            if (Directory.Exists(localPath))
            {
                Directory.Delete(localPath, true);
            }
        }
    }
}
