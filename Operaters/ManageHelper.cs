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
        private C_CPPManager? c_cppManager;
        private PythonManager? pythonManager;
        private JavaManager? javaManager;
        private int count = 0;
        private readonly int total = 0;

        /// <summary>
        /// 显示进度委托
        /// </summary>
        /// <param name="progressText">进度信息字符串</param>
        public delegate void OnProgressChangeHandler(string progressText);

        /// <summary>
        /// 当进度变化时的事件操作
        /// </summary>
        public event OnProgressChangeHandler OnProgressChangeEvent;

        public ManageHelper(Dictionary<string, bool> langMap, string localPath)
        {
            this.langMap = langMap;
            this.localPath = localPath;

            total += langMap["C_CPP"] ? 3 : 1;
            total += langMap["Python"] ? 4 : 1;
            total += langMap["Java"] ? 3 : 1;
        }

        public void Start()
        {
            string directoriesPath = Path.Combine(localPath, directoryName);
            if (!Directory.Exists(directoriesPath))
            {
                Directory.CreateDirectory(directoriesPath);
            }

            
            c_cppManager = new C_CPPManager(Path.Combine(directoriesPath, c_cppDirectoryName));
            if (langMap["C_CPP"])
            {
                UpdateProgress();
                c_cppManager.Download();

                UpdateProgress();
                c_cppManager.Extract();

                UpdateProgress();
                c_cppManager.AddPath();
            }
            else
            {
                UpdateProgress();
                c_cppManager.Uninstall();
            }

            pythonManager = new PythonManager(Path.Combine(directoriesPath, pythonDirectoryName));
            if (langMap["Python"])
            {
                UpdateProgress();
                pythonManager.Download();

                UpdateProgress();
                pythonManager.Extract();

                UpdateProgress();
                pythonManager.CmdRun();

                UpdateProgress();
                pythonManager.AddPath();
            }
            else
            {
                UpdateProgress();
                pythonManager.Uninstall();
            }

            javaManager = new JavaManager(Path.Combine(directoriesPath, javaDirectoryName));
            if (langMap["Java"])
            {
                UpdateProgress();
                javaManager.Download();

                UpdateProgress();
                javaManager.Extract();

                UpdateProgress();
                javaManager.AddPath();
            }
            else
            {
                UpdateProgress();
                javaManager.Uninstall();
            }
        }

        private void UpdateProgress()
        {
            OnProgressChangeEvent((++count).ToString() + " / " + total.ToString()+" -- ");
        }
    }
}
