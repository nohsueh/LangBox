using System;
using System.IO.Compression;

namespace LangBox.Operaters
{
    internal class ExtractHelper
    {
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="filePath">解压文件路径</param>
        /// <param name="directoryPath">解压文件后路径</param>
        public void Extract(string filePath, string directoryPath)
        {
            try
            {
                Logger.Info("Extracted " + filePath);
                ZipFile.ExtractToDirectory(filePath, directoryPath, true);
            }
            catch (Exception e)
            {
                Logger.Error("解压异常",e);
            }
        }
    }
}
