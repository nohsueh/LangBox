using System.Collections.Generic;
using System.IO;
using LangBox.Operaters.Managers;

namespace LangBox.Operaters
{
    internal class ManageHelper
    {
        private readonly Dictionary<string, bool> langMap;
        private readonly string localPath;
        private const string filesPath = "LangBox Files";
        private const string C_CPPDirectoryName = "c_cpp";
        private const string PythonDirectoryName = "python";
        private const string JavaDirectoryName = "java";
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
            string filesPath = Path.Combine(localPath, ManageHelper.filesPath);
            if (!Directory.Exists(filesPath))
            {
                Directory.CreateDirectory(filesPath);
            }

            Logger logger = new Logger("debug.log");
            logger.Info(langMap["C_CPP"].ToString());
            C_CPPManager.Start(Path.Combine(filesPath, C_CPPDirectoryName), langMap["C_CPP"]);
            UpdateProgress(0);

            logger.Info(langMap["Python"].ToString());
            PythonManager.Start(Path.Combine(filesPath, PythonDirectoryName), langMap["Python"]);
            UpdateProgress(0);

            logger.Info(langMap["Java"].ToString());
            JavaManager.Start(Path.Combine(filesPath, JavaDirectoryName), langMap["Java"]);
            UpdateProgress(0);

            //CSharpManager.Start(Path.Combine(filesPath, CSharpDirectoryName), langMap["CSharp"]);
            //UpdateProgress(0);
        }

        public void UpdateProgress(int percentProgress)
        {
            OnProgressChangeEvent(percentProgress);
        }
    }
}
