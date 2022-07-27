using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class C_CPPManager
    {
        private static string localPath = "D:\\LangBox Files\\c_cpp";  //防止localPath为空
        private static readonly string url = "http://yhz.yhz2000.com/server/DownloadServlet?access_token=ed3c96cc1cfa4966a2585e118bd3a7a5&fileId=750500";
        private const string fileName = "x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z";
        private const string directoryName = "mingw64";


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
            Logger.Info("调用安装c/cpp");
            if (!Directory.Exists(localPath))
            {
                Logger.Info("创建c/cpp文件夹");
                Directory.CreateDirectory(localPath);
            }

            string filePath = Path.Combine(localPath, fileName);
            if (!File.Exists(filePath))
            {
                Logger.Info("下载"+ fileName);
                DownloadHelper.Download(url, localPath);
                Logger.Info("成功下载"+ fileName);
            }

            Logger.Info("解压"+ fileName);
            ExtractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(Path.Combine("data",fileName), localPath);
            Logger.Info("成功解压"+ fileName);

            string directoryPath = Path.Combine(localPath, directoryName);
            Logger.Info("添加用户Path路径" + directoryPath);
            PathEditor.AddInUserPath("PATH",Path.Combine(directoryPath, "bin"));
            Logger.Info("成功添加用户Path路径" + directoryPath);
        }

        private static void Uninstall()
        {
            string directoryPath = Path.Combine(localPath, directoryName);
            if (Directory.Exists(localPath))
            {
                Logger.Info("删除c/cpp文件夹");
                Directory.Delete(localPath, true);
                Logger.Info("成功删除c/cpp文件夹");
            }

            Logger.Info("删除用户Path路径：" + directoryPath);
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            Logger.Info("成功删除用户Path路径："+ directoryPath);
        }
    }
}
