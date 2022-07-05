using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {
        private const string szPath = @"libs\7z.exe";
        private static Logger logger = new Logger("extract.log");
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="targetFile">解压文件路径</param>
        /// <param name="outPutDirectory">解压文件后路径</param>
        public static void Decompression(string targetFile, string outPutDirectory)
        {
            if (!Directory.Exists(outPutDirectory))
            {
                Directory.CreateDirectory(outPutDirectory);
            }
            var archive = ArchiveFactory.Open(targetFile);
            foreach (var entry in archive.Entries)
            {
                if (!entry.IsDirectory)
                {
                    entry.WriteToDirectory(outPutDirectory, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    logger.Info(entry.Key);
                }
            }
        }


        public static void Extract7ZIP(string targetFile, string outPutDirectory)
        {
            if (!File.Exists(szPath))
            {
                throw new FileNotFoundException("未能找到7z.exe\n尝试查找的目录为" + szPath);
            }
            if (!Directory.Exists(outPutDirectory))
            {
                Directory.CreateDirectory(outPutDirectory);
            }
            Process p = new Process();
            p.StartInfo.FileName = szPath;
            p.StartInfo.Arguments = "x " + targetFile + " -y -o\"" + outPutDirectory + "\"";
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
            logger.Info(e.Data);
        }
    }
}
