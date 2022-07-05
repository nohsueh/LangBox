using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private static string localPath = "D:\\LangBox Files\\python";  //防止localPath为空
        private static bool isChecked;
        private static string url = "https://www.python.org/ftp/python/3.10.5/python-3.10.5-embed-amd64.zip";
        private const string downloadFileName = "python3.10.zip";
        private const string extractDirectoryName = "python3.10";
        static Logger logger = new Logger("debug.log");


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
            string downloadFilePath = Path.Combine(localPath, downloadFileName);
            string filePath = Path.Combine(localPath, extractDirectoryName);
            WebClient wc = new WebClient();

            if (!Directory.Exists(localPath))
            {
                logger.Info("创建文件夹");
                Directory.CreateDirectory(localPath);
            }

            if (File.Exists(downloadFilePath))
            {
                logger.Info("删除文件");
                File.Delete(downloadFilePath);
            }

            //logger.Info("下载python-3.10.5-embed-amd64.zip");
            //wc.DownloadFile(url, downloadFilePath);
            //logger.Info("下载python-3.10.5-embed-amd64.zip成功");

            //logger.Info("解压python-3.10.5-embed-amd64.zip到python310");
            //ExtractHelper.Decompression(downloadFilePath, filePath);
            //logger.Info("解压python-3.10.5-embed-amd64.zip到python310成功");

            logger.Info("添加用户Path路径");
            PathEditor.AddInUserPath("PATH", filePath);
            logger.Info("添加用户Path路径成功");
        }

        private static void Uninstall()
        {
            string filePath = Path.Combine(localPath, extractDirectoryName);
            if (Directory.Exists(localPath))
            {
                logger.Info("删除文件夹");
                Directory.Delete(localPath, true);
                logger.Info("删除文件夹成功");
            }

            logger.Info("删除用户Path路径");
            PathEditor.RemoveInUserPath("PATH",filePath);
            logger.Info("删除用户Path路径成功");
        }
    }
}
