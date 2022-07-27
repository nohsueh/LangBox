using LangBox.Operators;
using System.IO;


namespace LangBox.Operaters.Managers
{
    internal class PythonManager
    {
        private static string localPath = "D:\\LangBox Files\\python";  //防止localPath为空
        private static string url = "http://yhz.yhz2000.com/server/DownloadServlet?access_token=ed3c96cc1cfa4966a2585e118bd3a7a5&fileId=750499";
        private const string fileName = "python-3.10.5-embed-amd64.7z";
        private const string directoryName = "python310";


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
                Logger.Info("下载"+ fileName);
                DownloadHelper.Download(url, localPath);
                Logger.Info("成功下载"+ fileName);
            }

            Logger.Info("解压"+ fileName);
            ExtractHelper.Extract(filePath, localPath);
            //extractHelper.Extract(Path.Combine("data", fileName), localPath);
            Logger.Info("成功解压"+ fileName);

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
