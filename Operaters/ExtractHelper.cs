using System;
using System.IO;
using System.Reflection;
using SevenZip;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {

        public delegate void OnProgressChangedHandler(int percent, string message);

        public static event OnProgressChangedHandler OnProgressChanged;

        private static int MaxValue;
        private static int CurrentValue;

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="path">解压文件路径</param>
        public static void Extract(string filePath, string directoryPath)
        {
            // Toggle between the x86 and x64 bit dll
            var sevenZipPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            SevenZipBase.SetLibraryPath(sevenZipPath);

            if (File.Exists(filePath) && Directory.Exists(directoryPath))
            {

                var extractor = new SevenZipExtractor(filePath);
                MaxValue = extractor.ArchiveFileData.Count;
                CurrentValue = 0;

                extractor.FileExtractionStarted += new EventHandler<FileInfoEventArgs>(extr_FileExtractionStarted);
                extractor.ExtractArchive(directoryPath);
            }
        }

        private static void extr_FileExtractionStarted(object sender, FileInfoEventArgs e)
        {
            CurrentValue++;
            int percent = 100 * CurrentValue / MaxValue;
            OnProgressChanged(percent, string.Format("Extract: {0}%  {1}", percent, e.FileInfo.FileName));
        }
    }
}
