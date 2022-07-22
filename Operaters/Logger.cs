using System;
using System.IO;
using System.Text;

namespace LangBox.Operaters
{
    internal class Logger
    {
        private StreamWriter writer;
        private readonly string fileName;

        public Logger(string fileName)
        {
            try
            {
                this.fileName = fileName;
                //向指定的文件操作，如果文件存在就追加，不存在就新建
                writer = new StreamWriter(fileName, true, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                if(writer != null)
                {
                //关闭写文件的流
                writer.Close();
                }
            }
        }

        public void Info(string? context)
        {
            writer = new StreamWriter(fileName, true, Encoding.UTF8);
            if (writer != null)
            {
                //写入文件
                writer.WriteLine(DateTime.Now.ToString("G")+"[INFO] "+context);
                writer.Close();
            }
        }

        public void Warn(string? context)
        {
            writer = new StreamWriter(fileName, true, Encoding.UTF8);
            if (writer != null)
            {
                writer.WriteLine(DateTime.Now.ToString("G") + "[WARN] " + context);
                writer.Close();
            }
        }

        public void Err(string? context)
        {
            writer = new StreamWriter(fileName, true, Encoding.UTF8);
            if (writer != null)
            {
                writer.WriteLine(DateTime.Now.ToString("G") + "[ERROR] " + context);
                writer.Close();
            }
        }

        public void Dispose()
        {
            if (writer != null)
            {
                //关闭写文件的流
                writer.Close();
            }
        }
    }
}
