using System.Diagnostics;

namespace LangBox.Operators
{
    public class CmdResult
    {
        public string result;
        public string error;

        public CmdResult(string r, string e)
        {
            result = r;
            error = e;
        }

        public void Set(string r, string e)
        {
            result = r;
            error = e;
        }
    }

    internal class CmdRunner
    {
        public static CmdResult CmdRun(string command)
        {
            command += "&exit";
            Process p = new();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            string[] commands = command.Split('\n');
            foreach (string line in commands)
                p.StandardInput.WriteLine(line);

            //向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;

            string result = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();

            p.WaitForExit();
            p.Close();

            CmdResult cmdResult = new(result, error);
            return cmdResult;
        }
    }
}