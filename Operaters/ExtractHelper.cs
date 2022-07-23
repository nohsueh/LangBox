using System;
using System.IO.Compression;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {
        private Logger logger = new Logger("extract.log");
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="filePath">解压文件路径</param>
        /// <param name="directoryPath">解压文件后路径</param>
        public void Extract(string filePath, string directoryPath)
        {
            try
            {
                logger.Info("Extracted " + filePath);
                ZipFile.ExtractToDirectory(filePath, directoryPath, true);
            }
            catch (Exception e)
            {
                logger.Err(e.Message);
            }
        }
    }
}
