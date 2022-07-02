using System.IO.Compression;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {
        public static void UnZipFile(string targetFile,string savePath)
        {
            ZipFile.ExtractToDirectory("D:\\a.zip", "D:\\");
        }
    }
}
