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
        private Dictionary<string,bool> langMap;
        private string filesPath;

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
            foreach(string lang in langMap.Keys)
            {
                if (lang == "C")
                {

                }
                if (lang == "CPlusPlus")
                {

                }
                if (lang == "Python")
                {

                }
                if (lang == "Java")
                {

                }
                if (lang == "CSharp")
                {

                }
            }
        }
    }
}
