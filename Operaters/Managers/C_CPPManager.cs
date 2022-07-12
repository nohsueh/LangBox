using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class C_CPPManager
    {
        private static string localPath = "D:\\LangBox Files\\c_cpp";  //防止localPath为空
        private static bool isChecked;
        private static string url = "http://1.117.147.239/api/v3/file/download/5IcFFRTwvT9mLAKj?sign=4CL1JGU4Uv8XrXy0KVhxe6QAl9f18rQscYxpt44Srwk%3D%3A1657634329";
        private const string downloadFileName = "x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z";
        private const string extractDirectoryName = "MinGW";
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

            logger.Info("下载x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z");
            wc.DownloadFile(url, downloadFilePath);
            logger.Info("下载x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z成功");

            logger.Info("解压x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z到MinGW");
            ExtractHelper.Extract7ZIP(downloadFilePath, filePath);
            logger.Info("解压x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z到MinGW成功");

            logger.Info("添加用户Path路径");
            PathEditor.AddInUserPath("PATH",Path.Combine(filePath, "bin"));
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
            PathEditor.RemoveInUserPath("PATH", Path.Combine(filePath, "bin"));
            logger.Info("删除用户Path路径成功");
        }
    }
}
