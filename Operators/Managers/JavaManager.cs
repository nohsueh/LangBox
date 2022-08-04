using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operators.Managers
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

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
        }
        public void Download()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            DownloadHelper.Download(urlMap["Java"], localPath);
        }

        public void Extract()
        {
            ExtractHelper.Extract(filePath, localPath);
            File.Delete(filePath);
        }

        public void AddPath()
        {
            PathEditor.AddInUserPath("JAVA_HOME", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            PathEditor.AddInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\tools.jar"));
            PathEditor.AddInUserPath("CLASSPATH",Path.Combine(directoryPath, "lib\\dt.jar"));
        }

        public void Uninstall()
        {
            if (Directory.Exists(localPath))
            {
                Directory.Delete(localPath, true);
            }

            PathEditor.RemoveInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\tools.jar"));
            PathEditor.RemoveInUserPath("CLASSPATH", Path.Combine(directoryPath, "lib\\dt.jar"));
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "bin"));
            PathEditor.RemoveInUserPath("JAVA_HOME", directoryPath);
        }
    }
}
