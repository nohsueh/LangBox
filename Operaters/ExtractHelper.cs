using SharpCompress.Archives;
using SharpCompress.Common;
using System.Text;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="targetFile">解压文件路径</param>
        /// <param name="zipFile">解压文件后路径(必须已经存在)</param>
        public static void Decompression(string targetFile, string zipFile)
        {
            var archive = ArchiveFactory.Open(targetFile);
            foreach (var entry in archive.Entries)
            {
                if (!entry.IsDirectory)
                {
                    entry.WriteToDirectory(zipFile, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
            }
        }

    }
}
