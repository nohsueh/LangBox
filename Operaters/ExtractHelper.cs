using System;
using System.Diagnostics;
using System.IO;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {
        public delegate void OnProgressChangedHandler(int percent, string message);

        public static event OnProgressChangedHandler OnProgressChanged;

        private const string szPath = @"libs\7z.exe";

        public static void Extract(string path, string outPutDirectory)
        {
            if (!File.Exists(szPath))
            {
                throw new FileNotFoundException("未能找到7z.exe\n尝试查找的目录为" + szPath);
            }

            Process p = new Process();
            p.StartInfo.FileName = szPath;
            p.StartInfo.Arguments = "x " + "\"" + path + "\"" + " -y -o\""  + outPutDirectory + "\"";
            p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.OutputDataReceived += ReceivedOutput;
            p.ErrorDataReceived += ReceivedOutput;

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.WaitForExit();
        }

        private static void ReceivedOutput(object sender, DataReceivedEventArgs e)
        {
            Logger.Info(e.Data);
            OnProgressChanged(50, "Extracting " + 50 + "%" + " - " + e.Data);
        }
    }
}