using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class C_CPPManager
    {
        private static string localPath = "D:\\LangBox Files\\c_cpp";  //防止localPath为空
        private static readonly string url = "https://github.com/NOhsueh/LangBox/releases/download/V1.1.0/x86_64-8.1.0-release-win32-seh-rt_v6-rev0.zip";
        private const string fileName = "x86_64-8.1.0-release-win32-seh-rt_v6-rev0.zip";
        private const string directoryName = "mingw64";
        static Logger logger = new("debug.log");
        //private static DownloadHelper downloadHelper = new();
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
            logger.Info("调用安装c/cpp");
            if (!Directory.Exists(localPath))
            {
                logger.Info("创建文件夹");
                Directory.CreateDirectory(localPath);
            }

            //string filePath = Path.Combine(localPath, fileName);
            //if (!File.Exists(filePath))
            //{
            //    logger.Info("下载x86_64-8.1.0-release-win32-seh-rt_v6-rev0.zip");
            //    downloadHelper.Download(url, localPath);
            //    logger.Info("下载x86_64-8.1.0-release-win32-seh-rt_v6-rev0.zip成功");
            //} 

            logger.Info("解压x86_64-8.1.0-release-win32-seh-rt_v6-rev0.zip");
            //extractHelper.Extract(filePath, localPath);
            extractHelper.Extract(Path.Combine("data",fileName), localPath);
            logger.Info("解压x86_64-8.1.0-release-win32-seh-rt_v6-rev0.zip成功");

            logger.Info("添加用户Path路径");
            string directoryPath = Path.Combine(localPath, directoryName);
            PathEditor.AddInUserPath("PATH",Path.Combine(directoryPath, "bin"));
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
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            logger.Info("删除用户Path路径成功");
        }
    }
}
