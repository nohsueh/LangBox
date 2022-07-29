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
            var sevenZipPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            SevenZipBase.SetLibraryPath(sevenZipPath);

            var file = new SevenZipExtractor(archive);
            file.Extracting += (sender, args) =>
            {
                int percent = args.PercentDone;
                OnProgressChanged(percent, file.ArchiveFileData+"Extracting-- " + percent + " % ");
            };
            file.ExtractionFinished += (sender, args) =>
            {
                // Do stuff when done
            };

            //Extract the stuff
            file.ExtractArchive(directory);
        }

        public void Extract()
        {
            using (Stream extractFrom = File.OpenRead("test.7z"))
            {
                using (SevenZipExtractor extractor = new SevenZipExtractor(extractFrom))
                {
                    extractor.ExtractFile("readme.md", new MemoryStream());
                }
            }
        }

        private void Extractor_Extracting(object? sender, ProgressEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
