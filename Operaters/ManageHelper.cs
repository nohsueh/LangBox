using System.Collections.Generic;
using System.IO;
using LangBox.Operaters.Managers;

namespace LangBox.Operaters
{
    internal class ManageHelper
    {
        private readonly Dictionary<string, bool> langMap;
        private readonly string localPath;
        private const string directoryName = "LangBox Files";
        private const string c_cppDirectoryName = "c_cpp";
        private const string pythonDirectoryName = "python";
        private const string javaDirectoryName = "java";
        //private const string CSharpDirectoryName = "csharp";
        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="percentProgress">进度信息字符串</param>
        public delegate void OnProgressChangeHandler(int percentProgress);
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
            string directoriesPath = Path.Combine(localPath, directoryName);
            if (!Directory.Exists(directoriesPath))
            {
                Directory.CreateDirectory(directoriesPath);
            }

            C_CPPManager.Start(Path.Combine(directoriesPath, c_cppDirectoryName), langMap["C_CPP"]);
            UpdateProgress(0);

            PythonManager.Start(Path.Combine(directoriesPath, pythonDirectoryName), langMap["Python"]);
            UpdateProgress(0);

            JavaManager.Start(Path.Combine(directoriesPath, javaDirectoryName), langMap["Java"]);
            UpdateProgress(0);

            //CSharpManager.Start(Path.Combine(directoryName, CSharpDirectoryName), langMap["CSharp"]);
            //UpdateProgress(0);
        }

        public void UpdateProgress(int percentProgress)
        {
            OnProgressChangeEvent(percentProgress);
        }
    }
}
