using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters
{
    internal class ProgressList
    {
        private LinkedList<string> list = new LinkedList<string>();


        public void SetProgressList(Dictionary<string,bool> langMap)
        {
            list.Clear();

            foreach (var lang in langMap.Keys)
            {
                if (lang == "C_CPP" && langMap[lang])
                {
                    AddC_CPP();
                }
                if (lang == "Python" && langMap[lang])
                {
                    AddPython();
                }
                if (lang == "Java" && langMap[lang])
                {
                    AddJava();
                }
                if (lang == "CSharp" && langMap[lang])
                {
                    AddCSharp();
                }
            }
        }

        public int RestCount()
        {
            return list.Count();
        }

        public string AllText()
        {
            string text = "";
            foreach(var item in list)
            {
                text += item+"\n";
            }
            return text;
        }

        private void AddC_CPP()
        {
            list.AddLast("安装mingw\n配置c/c++环境变量");
        }

        private void AddPython()
        {
            list.AddLast("安装python\n配置python环境变量");
        }

        private void AddJava()
        {
            list.AddLast("安装java\n配置java环境变量");
        }

        private void AddCSharp()
        {
            list.AddLast("安装nodejs\n配置nodejs");
        }
    }
}
