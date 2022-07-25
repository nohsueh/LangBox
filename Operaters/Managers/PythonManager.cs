using LangBox.Operators;
using System.IO;


namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private static string localPath = "D:\\LangBox Files\\python";  //防止localPath为空
        private static string url = "http://yhz.yhz2000.com/server/DownloadServlet?access_token=1ce26aae50874d9395398f38daec592f&fileId=750417";
        private const string fileName = "python-3.10.5-embed-amd64.zip";
        private const string directoryName = "python310";
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
            Logger.Info("调用安装python");
            if (!Directory.Exists(localPath))
            {
                Logger.Info("创建python文件夹");
                Directory.CreateDirectory(localPath);
            }

            string filePath = Path.Combine(localPath, fileName);
            if (!File.Exists(filePath))
            {
                Logger.Info("下载python-3.10.5-embed-amd64.zip");
                downloadHelper.Download(url, localPath);
                Logger.Info("成功下载python-3.10.5-embed-amd64.zip");
            }

            Logger.Info("解压python-3.10.5-embed-amd64.zip");
            extractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(Path.Combine("data", fileName), localPath);
            Logger.Info("成功解压python-3.10.5-embed-amd64.zip");

            string directoryPath = Path.Combine(localPath, directoryName);
            Logger.Info("配置pip");
            string command = "cd " + directoryPath + "\n" + "python get-pip.py";
            CmdResult cmdResult = CmdRunner.CmdRun(command);
            Logger.Info(cmdResult.result);
            Logger.Error(cmdResult.error);

            Logger.Info("添加用户Path路径: " + directoryPath);
            PathEditor.AddInUserPath("PATH", directoryPath);
            PathEditor.AddInUserPath("PATH", Path.Combine(directoryPath, "Scripts"));
            Logger.Info("成功添加用户Path路径: " + directoryPath);
        }

        private static void Uninstall()
        {
            string directoryPath = Path.Combine(localPath, directoryName);
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
