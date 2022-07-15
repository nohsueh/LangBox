using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class JavaManager
    {
        private static string localPath = "D:\\LangBox Files\\java";  //防止localPath为空
        private static string url = "http://1.117.147.239/api/v3/file/download/Oo4X0Qh5IpUyv2Us?sign=_okGoeRctE-kkh0215rDANW0qY2xwXu-3IqggU_i6Q8%3D%3A1657634338";
        private const string fileName = "jdk-18_windows-x64_bin.zip";
        private const string directoryName = "jdk-18.0.1.1";
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
            if (!Directory.Exists(localPath))
            {
                logger.Info("创建文件夹");
                Directory.CreateDirectory(localPath);
            }

            string filePath = Path.Combine(localPath, fileName);
            if (!File.Exists(filePath))
            {
                logger.Info("下载jdk-18_windows-x64_bin.zip");
                downloadHelper.Download(url, localPath);
                logger.Info("下载jdk-18_windows-x64_bin.zip成功");
            }


            logger.Info("解压jdk-18_windows-x64_bin.zip");
            extractHelper.Extract(filePath, localPath);
            logger.Info("解压jdk-18_windows-x64_bin.zip成功");

            logger.Info("添加用户Path路径");
            string directoryPath = Path.Combine(localPath, directoryName);
            PathEditor.AddInUserPath("JAVA_HOME", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "bin"));
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
            PathEditor.RemoveInUserPath("JAVA_HOME", directoryPath);
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            logger.Info("删除用户Path路径成功");
        }
    }
}
