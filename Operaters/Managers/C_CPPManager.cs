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
        private static string url = "https://store2.lanzoug.com/070220bb/2019/11/23/0fd5474ca81b01ef604ddf2e0d019af2.7z?st=zd2u7K3MrRgLTDPYJ7Bccw&e=1656767491&b=BxgJYAFvAEIAAgJ6AGdXKQ_c_c&fi=14367027&pid=218-70-255-160&up=2&mp=0&co=1";
        private const string fileName = "MinGW.7z";
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

            logger.Info("下载MinGW.7z");
            WebClient wc = new WebClient();
            wc.DownloadFile(url, filePath);
            logger.Info("下载MinGW.7z成功");
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
