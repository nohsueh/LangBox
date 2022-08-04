using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class JavaManager
    {
        private readonly string localPath = "D:\\LangBox Files\\java";  //防止localPath为空
        private const string fileName = "jdk-18_windows-x64_bin.7z";
        private const string directoryName = "jdk-18.0.1.1";
        private readonly string filePath;
        private readonly string directoryPath;
        private static ConfigHelper cfg = new ConfigHelper();
        private readonly Dictionary<string, string> urlMap;

        public JavaManager(string localPath)
        {
            Logger.Info("初始化Java");
            this.localPath = localPath;
            filePath = Path.Combine(localPath, fileName);
            directoryPath = Path.Combine(localPath, directoryName);
            urlMap = cfg.GetUrlMap();

            Logger.Info("调用安装java");
            if (!Directory.Exists(localPath))
            {
                Logger.Info("创建java文件夹");
                Directory.CreateDirectory(localPath);
            }
        }
        public void Download()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            Logger.Info("下载" + fileName);
            DownloadHelper.Download(urlMap["Java"], localPath);
            Logger.Info("成功下载" + fileName);
        }

        public void Extract()
        {
            Logger.Info("解压" + fileName);
            ExtractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(Path.Combine("data",fileName), localPath);
            Logger.Info("成功解压" + fileName);
        }

        public void AddPath()
        {
            Logger.Info("添加用户Path路径："+ directoryPath);
            PathEditor.AddInUserPath("JAVA_HOME", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            PathEditor.AddInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\tools.jar"));
            PathEditor.AddInUserPath("CLASSPATH",Path.Combine(directoryPath, "lib\\dt.jar"));
            Logger.Info("成功添加用户Path路径："+ directoryPath);
        }

        public void Uninstall()
        {
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
