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

            Logger.Info("调用安装c/cpp");
            if (!Directory.Exists(localPath))
            {
                Logger.Info("创建c/cpp文件夹");
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
            DownloadHelper.Download(urlMap["C_CPP"], localPath);
            Logger.Info("成功下载" + fileName);
        }

        public void Extract()
        {
            Logger.Info("解压" + fileName);
            ExtractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(localPath.Combine("data",fileName), localPath);
            Logger.Info("成功解压" + fileName);
        }

        public void AddPath()
        {
            Logger.Info("添加用户Path路径" + directoryPath);
            PathEditor.AddInUserPath("PATH",Path.Combine(directoryPath, "bin"));
            Logger.Info("成功添加用户Path路径" + directoryPath);
        }

        public void Uninstall()
        {
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
