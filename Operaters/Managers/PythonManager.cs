using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;


namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private static string localPath = "D:\\LangBox Files\\python";  //防止localPath为空
        private static string url = "https://github.com/NOhsueh/LangBox/releases/download/V1.1.0/python-3.10.5-embed-amd64.zip";
        private const string fileName = "python-3.10.5-embed-amd64.zip";
        private const string directoryName = "python-3.10.5";
        private static Logger logger = new Logger("debug.log");
        private static DownloadHelper downloadHelper = new();
        private static ExtractHelper extractHelper = new();


        public static void Start(string Path, bool isChecked)
        {
            localPath = Path;

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
            logger.Info("调用安装python");
            if (!Directory.Exists(localPath))
            {
                logger.Info("创建文件夹");
                Directory.CreateDirectory(localPath);
            }

            string filePath = Path.Combine(localPath, fileName);
            if (!File.Exists(filePath))
            {
                logger.Info("下载python-3.10.5-embed-amd64.zip");
                downloadHelper.Download(url, localPath);
                logger.Info("下载python-3.10.5-embed-amd64.zip成功");
            }

            logger.Info("解压python-3.10.5-embed-amd64.zip");
            extractHelper.Extract(filePath, localPath);
            logger.Info("解压python-3.10.5-embed-amd64.zip成功");

            logger.Info("添加用户Path路径");
            string directoryPath = Path.Combine(localPath, directoryName);
            PathEditor.AddInUserPath("PATH", directoryPath);
            logger.Info("添加用户Path路径成功");
        }

        private static void Uninstall()
        {
            string directoryPath = Path.Combine(localPath, directoryName);
            if (Directory.Exists(localPath))
            {
                logger.Info("删除文件夹");
                Directory.Delete(localPath, true);
                logger.Info("删除文件夹成功");
            }

            logger.Info("删除用户Path路径");
            PathEditor.RemoveInUserPath("PATH",directoryPath);
            logger.Info("删除用户Path路径成功");
        }
    }
}
