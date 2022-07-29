using LangBox.Operators;
using System.IO;


namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private readonly string localPath = "D:\\LangBox Files\\python";  //防止localPath为空
        private static readonly string url = "http://yhz.yhz2000.com/server/DownloadServlet?access_token=ba594059ea3a4eb2bb2c45a9afd597f0&fileId=750499";
        private const string fileName = "python-3.10.5-embed-amd64.7z";
        private const string directoryName = "python310";
        private readonly string filePath;
        private readonly string directoryPath;

        public PythonManager(string localPath)
        {
            this.localPath = localPath;
            filePath = Path.Combine(localPath, fileName);
            directoryPath = Path.Combine(localPath, directoryName);

            Logger.Info("调用安装python");
            if (!Directory.Exists(localPath))
            {
                Logger.Info("创建python文件夹");
                Directory.CreateDirectory(localPath);
            }
        }

        public void Download()
        {
            if (!File.Exists(filePath))
            {
                Logger.Info("下载" + fileName);
                DownloadHelper.Download(url, localPath);
                Logger.Info("成功下载" + fileName);
            }
        }

        public void Extract()
        {
            Logger.Info("解压" + fileName);
            ExtractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(Path.Combine("data", fileName), localPath);
            Logger.Info("成功解压" + fileName);
        }

        public void CmdRun()
        {
            Logger.Info("配置pip");
            string command = "cd " + directoryPath + "\n" + "python get-pip.py";
            CmdResult cmdResult = CmdRunner.CmdRun(command);
            Logger.Info(cmdResult.result);
            Logger.Error(cmdResult.error);
        }

        public void AddPath()
        {
            Logger.Info("添加用户Path路径: " + directoryPath);
            PathEditor.AddInUserPath("PATH", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "Scripts"));
            Logger.Info("成功添加用户Path路径: " + directoryPath);
        }

        public void Uninstall()
        {
            if (Directory.Exists(localPath))
            {
                Logger.Info("删除python文件夹");
                Directory.Delete(localPath, true);
                Logger.Info("删除python文件夹成功");
            }

            Logger.Info("删除用户Path路径" + directoryPath);
            PathEditor.RemoveInUserPath("PATH",directoryPath);
            PathEditor.RemoveInUserPath("PATH", Path.Combine(directoryPath, "Scripts"));
            Logger.Info("成功删除用户Path路径" + directoryPath);
        }
    }
}
