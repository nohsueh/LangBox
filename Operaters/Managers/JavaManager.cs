using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class JavaManager
    {
        private static string localPath = "D:\\LangBox Files\\java";  //防止localPath为空
        private static string url = "http://yhz.yhz2000.com/server/DownloadServlet?access_token=1ce26aae50874d9395398f38daec592f&fileId=750416";
        private const string fileName = "jdk-18_windows-x64_bin.zip";
        private const string directoryName = "jdk18";
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
            Logger.Info("调用安装java");
            if (!Directory.Exists(localPath))
            {
                Logger.Info("创建java文件夹");
                Directory.CreateDirectory(localPath);
            }

            string filePath = Path.Combine(localPath, fileName);
            if (!File.Exists(filePath))
            {
                Logger.Info("下载jdk-18_windows-x64_bin.zip");
                downloadHelper.Download(url, localPath);
                Logger.Info("下载jdk-18_windows-x64_bin.zip成功");
            }


            Logger.Info("解压jdk-18_windows-x64_bin.zip");
            extractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(Path.Combine("data",fileName), localPath);
            Logger.Info("成功解压jdk-18_windows-x64_bin.zip");

            string directoryPath = Path.Combine(localPath, directoryName);
            Logger.Info("添加用户Path路径："+ directoryPath);
            PathEditor.AddInUserPath("JAVA_HOME", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            PathEditor.AddInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\tools.jar"));
            PathEditor.AddInUserPath("CLASSPATH",Path.Combine(directoryPath, "lib\\dt.jar"));
            Logger.Info("成功添加用户Path路径："+ directoryPath);
        }

        private static void Uninstall()
        {
            string directoryPath = Path.Combine(localPath, directoryName);
            if (Directory.Exists(localPath))
            {
                Logger.Info("删除java文件夹");
                Directory.Delete(localPath, true);
                Logger.Info("成功删除java文件夹");
            }

            Logger.Info("删除用户Path路径："+ directoryPath);
            PathEditor.RemoveInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\tools.jar"));
            PathEditor.RemoveInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\dt.jar"));
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            PathEditor.RemoveInUserPath("JAVA_HOME", directoryPath);
            Logger.Info("成功删除用户Path路径："+ directoryPath);
        }
    }
}
