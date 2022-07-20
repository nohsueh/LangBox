using System;
using System.IO;
using System.Text;

namespace LangBox.Operaters
{
    internal class Logger
    {
        private readonly FileStream? fileStream;

        public Logger(string fileName)
        {
            try
            {
                fileStream = File.Open(fileName, FileMode.Append);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                fileStream = null;
            }
        }

        public void Info(string? context)
        {
            if (fileStream != null)
            {
                context = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "[INFO] " + context + "\n";
                byte[] text = Encoding.UTF8.GetBytes(context);
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
                Console.WriteLine(context);
            }
        }

        public void Warn(string? context)
        {
            if (fileStream != null)
            {
                context = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "[WARN] " + context + "\n";
                byte[] text = Encoding.UTF8.GetBytes(context);
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
                Console.WriteLine(context);
            }
        }

        public void Err(string? context)
        {
            if (fileStream != null)
            {
                context = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "[ERROR] " + context + "\n";
                byte[] text = Encoding.UTF8.GetBytes(context);
                fileStream.Write(text, 0, text.Length);
                fileStream.Flush();
                Console.WriteLine(context);
            }
        }

        public void Dispose()
        {
            if (fileStream != null)
                fileStream.Close();
        }
    }
}
