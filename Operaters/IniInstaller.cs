using LangBox.Operaters.Installers;
using System.Collections.Generic;
using System.IO;

namespace LangBox.Operaters
{
    internal class IniInstaller
    {
        private Dictionary<string, bool> langMap;
        private string filesPath;
        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler();

        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        public IniInstaller(Dictionary<string, bool> langMap, string filesPath)
        {
            this.langMap = langMap;
            this.filesPath = filesPath;
        }

        public void Start()
        {
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }
            foreach(var lang in langMap.Keys)
            {
                if (lang == "C_CPP" && langMap[lang])
                {
                    C_CPPInstaller c_cppInstaller = new C_CPPInstaller(filesPath);
                    c_cppInstaller.OnProgressChangeEvent += UpdateProgress;
                    c_cppInstaller.Start();
                }
                if (lang == "Python" && langMap[lang])
                {
                    PythonInstaller pythonInstaller = new PythonInstaller(filesPath);
                    pythonInstaller.OnProgressChangeEvent += UpdateProgress;
                    pythonInstaller.Start();
                }
                if (lang == "Java" && langMap[lang])
                {
                    JavaInstaller javaInstaller = new JavaInstaller(filesPath);
                    javaInstaller.OnProgressChangeEvent += UpdateProgress;
                    javaInstaller.Start();
                }
                if (lang == "CSharp" && langMap[lang])
                {
                    CSharpInstaller csharpInstaller = new CSharpInstaller(filesPath);
                    csharpInstaller.OnProgressChangeEvent += UpdateProgress;
                    csharpInstaller.Start();
                }
            }
                
            }
        

        private void UpdateProgress()
        {
            OnProgressChangeEvent();
        }

        //private void UpdateDownloadProgress(string percent, string speed, string eta)
        //{
        //    string showString = operation + " (" + percent + ") " + speed + "/s 预计剩余: " + eta;
        //    OnProgressChangeEvent(showString);
        //}
    }
}
