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
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="archive">解压文件路径</param>
        public static void Extract(string archive, string directory)
        {
            string extractInfo = archive;
            var sevenZipPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            SevenZipBase.SetLibraryPath(sevenZipPath);

            var file = new SevenZipExtractor(archive);
            file.Extracting += (sender, args) =>
            {
                int percent = args.PercentDone;
                OnProgressChanged(percent, "Extracting: " + extractInfo);
            };
            file.ExtractionFinished += (sender, args) =>
            {
                // Do stuff when done
            };

            //Extract the stuff
            file.ExtractArchive(directory);
        }
    }
}
