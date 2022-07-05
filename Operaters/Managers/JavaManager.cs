using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class JavaManager
    {
        private static string localPath = "D:\\LangBox Files\\java";  //防止localPath为空
        private static bool isChecked;
        private static string url = "https://download.oracle.com/java/18/latest/jdk-18_windows-x64_bin.zip";
        private const string downloadFileName = "jdk-18_windows-x64_bin.zip";
        private const string extractDirectoryName = "jdk18";
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

            //logger.Info("下载MinGW.7z");
            //wc.DownloadFile(url, downloadFilePath);
            //logger.Info("下载MinGW.7z成功");

            //logger.Info("解压MinGW.7z到MinGW");
            //ExtractHelper.Decompression(downloadFilePath, filePath);
            //logger.Info("解压MinGW.7z到MinGW成功");

            logger.Info("添加用户Path路径");
            PathEditor.AddInUserPath(Path.Combine(filePath, "bin"));
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
            PathEditor.RemoveInUserPath(Path.Combine(filePath, "bin"));
            logger.Info("删除用户Path路径成功");
        }
    }
}
