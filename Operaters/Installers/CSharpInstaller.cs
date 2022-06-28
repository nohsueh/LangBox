using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters.Installers
{
    internal class CSharpInstaller
    {
        string filePath;
        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler();

        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        public CSharpInstaller(string filePath)
        {
            this.filePath = filePath;
        }

        public void Start()
        {
            Logger logger = new Logger("debug.log");
            logger.Info("start install C#.");

            if (!Directory.Exists(filePath + @"\CSharp"))
            {
                Directory.CreateDirectory(filePath + @"\CSharp");
            }

            OnProgressChangeEvent();
        }
    }
}
