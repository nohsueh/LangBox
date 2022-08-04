using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LangBox.Operaters.Managers
{
    internal class C_CPPManager
    {
        private readonly string localPath = "D:\\LangBox Files\\c_cpp";  //防止localPath为空
        private const string fileName = "x86_64-8.1.0-release-win32-seh-rt_v6-rev0.7z";
        private const string directoryName = "mingw64";
        private readonly string filePath;
        private readonly string directoryPath;
        private static ConfigHelper cfg = new ConfigHelper();
        private readonly Dictionary<string, string> urlMap;


        public C_CPPManager(string localPath)
        {
            Logger.Info("初始化C_CPP");
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
            DownloadHelper.Download(urlMap["C_CPP"], localPath);
        }

        public void Extract()
        {
            ExtractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(localPath.Combine("data",fileName), localPath);
            File.Delete(filePath);
        }

        public void AddPath()
        {
            PathEditor.AddInUserPath("PATH",Path.Combine(directoryPath, "bin"));
        }

        public void Uninstall()
        {
            if (Directory.Exists(localPath))
            {
                Directory.Delete(localPath, true);
            }

            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "bin"));
        }
    }
}
