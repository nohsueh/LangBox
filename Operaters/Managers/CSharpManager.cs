using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class CSharpManager
    {
        private static string localPath = "D:\\LangBox Files\\csharp";  //防止localPath为空
        private static bool isChecked;
        private static string url = "https://store2.lanzoug.com/070317bb/2019/11/23/0fd5474ca81b01ef604ddf2e0d019af2.7z?st=7Gg09AaqC2nA0NF_vB-N-g&e=1656844325&b=CBcLYlc5WBpWVAB4B2AOcA_c_c&fi=14367027&pid=117-136-30-6&up=2&mp=0&co=1";
        private const string downloadFileName = "CSharp.7z";
        private const string extractDirectoryName = "CSharp";
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
