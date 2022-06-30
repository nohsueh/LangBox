using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class C_CPPManager
    {
        private static string localPath = "D:\\LangBox Files";  //防止localPath为空
        private static bool isChecked;
        private static string url = "https://osdn.net/projects/mingw/downloads/68260/mingw-get-setup.exe/";
        private const string fileName = "mingw-get-setup.exe";
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

            logger.Info("下载mingw");
            WebClient wc = new WebClient();
            wc.DownloadFile(url, filePath);
            logger.Info("下载mingw成功");
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
