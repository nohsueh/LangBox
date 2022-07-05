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
        private static string url = "https://udomain.dl.sourceforge.net/project/mingw-w64/Toolchains%20targetting%20Win64/Personal%20Builds/mingw-builds/8.1.0/threads-win32/seh/x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z";
        private const string downloadFileName = "x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z";
        private const string extractDirectoryName = "mingw81";
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
