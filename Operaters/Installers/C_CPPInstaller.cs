using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters.Installers
{
    internal class C_CPPInstaller
    {
        private string filePath;
        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler();

        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        public C_CPPInstaller(string filePath)
        {
            this.filePath = filePath+@"\C_CPP\";
        }

        public void Start()
        {
            Logger logger = new Logger("debug.log");
            logger.Info("start install C/C++.");

            DownloadFile_URL("mingw-get-setup.exe", "https://mirrors.gigenet.com/OSDN//mingw/68260/mingw-get-setup.exe", filePath);

            OnProgressChangeEvent();
        }

        private void DownloadFile_URL(string fileName,string url,string filePath)
        {
            WebClient wc = new WebClient();
            if (File.Exists(filePath + fileName))
            {
                File.Delete(filePath + fileName);
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            wc.DownloadFile(url, filePath + fileName);
        }
    }
}   
