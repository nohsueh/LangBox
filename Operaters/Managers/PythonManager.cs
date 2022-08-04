using LangBox.Operators;
using System.Collections.Generic;
using System.IO;


namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private readonly string localPath = "D:\\LangBox Files\\python";  //防止localPath为空
        private const string fileName = "python-3.10.5-embed-amd64.7z";
        private const string directoryName = "python310";
        private readonly string filePath;
        private readonly string directoryPath;
        private static ConfigHelper cfg = new ConfigHelper();
        private readonly Dictionary<string, string> urlMap;

        public PythonManager(string localPath)
        {
            Logger.Info("初始化Python");
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
            DownloadHelper.Download(urlMap["Python"], localPath);
        }

        public void Extract()
        {
            ExtractHelper.Extract(filePath, localPath);
            File.Delete(filePath);
        }

        public void CmdRun()
        {
            Logger.Info("配置pip");
            string command = "cd " + "\"" + directoryPath + "\"" + "\n" + "python get-pip.py";
            CmdResult cmdResult = CmdRunner.CmdRun(command);
            Logger.Info(cmdResult.result);
            Logger.Info(cmdResult.error);
        }

        public void AddPath()
        {
            PathEditor.AddInUserPath("PATH", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "Scripts"));
        }

        public void Uninstall()
        {
            if (Directory.Exists(localPath))
            {
                Directory.Delete(localPath, true);
            }

            PathEditor.RemoveInUserPath("PATH", directoryPath);
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "Scripts"));
        }
    }
}
