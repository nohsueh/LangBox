using LangBox.Operaters.Installers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters
{
    internal class InstallHelper
    {
        private static string langPath = @"\lang";
        private Dictionary<string, bool> langMap;
        private string filesPath;
        private string operation;
        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler(String progressText);

        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        public InstallHelper(Dictionary<string, bool> langMap, string filesPath)
        {
            this.langMap = langMap;
            this.filesPath = filesPath;
        }

        public void Start()
        {
            string temp = filesPath + langPath;
            if (!Directory.Exists(temp))
            {
                Directory.CreateDirectory(temp);
            }
            foreach (string lang in langMap.Keys)
            {
                if (lang == "C_CPP")
                {
                    C_CPPInstaller c_cppInstaller = new C_CPPInstaller();
                }
                if (lang == "Python")
                {
                    PythonInstaller pythonInstaller = new PythonInstaller();
                }
                if (lang == "Java")
                {
                    JavaInstaller javaInstaller = new JavaInstaller();
                }
                if (lang == "CSharp")
                {
                    CSharpInstaller csharpInstaller = new CSharpInstaller();
                }
            }
        }

        private void ChangeProgress(string newOperation)
        {
            operation = newOperation;
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            OnProgressChangeEvent(operation);
        }

        private void UpdateDownloadProgress(string percent, string speed, string eta)
        {
            string showString = operation + " (" + percent + ") " + speed + "/s 预计剩余: " + eta;
            OnProgressChangeEvent(showString);
        }
    }
}
