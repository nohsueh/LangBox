using System.IO;
using System.Net;

namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private static string localPath = "D:\\LangBox Files";  //防止localPath为空
        private static bool isChecked;
        private static string url = "https://www.python.org/ftp/python/3.10.5/python-3.10.5-amd64.exe";
        private static string fileName = "python-3.10.5-amd64.exe";
        private static Logger logger = new Logger("debug.log");


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
                logger.Info("创建文件夹");
                Directory.CreateDirectory(localPath);
            }

            string filePath = Path.Combine(localPath, fileName);
            if (File.Exists(filePath))
            {
                logger.Info("删除文件");
                File.Delete(filePath);
            }

            logger.Info("下载python");
            WebClient wc = new WebClient();
            wc.DownloadFile(url, filePath);
            logger.Info("下载python成功");
        }

        private static void Uninstall()
        {
            if (Directory.Exists(localPath))
            {
                logger.Info("删除文件夹");
                Directory.Delete(localPath, true);
            }
        }
    }
}
