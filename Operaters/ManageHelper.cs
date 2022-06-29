using System.Collections.Generic;
using System.IO;
using LangBox.Operaters.Managers;

namespace LangBox.Operaters
{
    internal class ManageHelper
    {
        private Dictionary<string, bool> langMap;
        private string localPath;
        private const string filesPath = "LangBox Files";
        private const string C_CPPFilesName = "c_cpp";
        private const string PythonFilesName = "python";
        private const string JavaFilesName = "java";
        private const string CSharpFilesName = "csharp";
        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler();
        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        public ManageHelper(Dictionary<string, bool> langMap, string localPath)
        {
            this.langMap = langMap;
            this.localPath = localPath;
        }

        public void Start()
        {
            string filesPath = Path.Combine(localPath, ManageHelper.filesPath);
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            ////模拟一下进度
            //for(int i = 0; i < 100; i++)
            //{
                UpdateProgress();
            //}

            //foreach(string lang in langMap.Keys)
            //{
            //    if (lang == "C_CPP")
            //    {
            //        C_CPPManager.Start(Path.Combine(filesPath, C_CPPFilesName), langMap[lang]);
            //    }
            //    if (lang == "Python")
            //    {
            //        PythonManager.Start(Path.Combine(filesPath, PythonFilesName), langMap[lang]);
            //    }
            //    if (lang == "Java")
            //    {
            //        JavaManager.Start(Path.Combine(filesPath, JavaFilesName), langMap[lang]);
            //    }
            //    if (lang == "CSharp")
            //    {
            //        CSharpManager.Start(Path.Combine(filesPath, CSharpFilesName), langMap[lang]);
            //    }
            //}
        }

        private void UpdateProgress()
        {
            OnProgressChangeEvent();
        }
    }
}
