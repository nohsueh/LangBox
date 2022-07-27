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

            PythonManager.Start(Path.Combine(directoriesPath, pythonDirectoryName), langMap["Python"]);

            JavaManager.Start(Path.Combine(directoriesPath, javaDirectoryName), langMap["Java"]);

            //CSharpManager.Start(Path.Combine(directoryName, CSharpDirectoryName), langMap["CSharp"]);
        }
    }
}
