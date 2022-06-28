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

        public LinkedList<string> GetProgressList()
        {
            return list;
        }

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

        public string AllText()
        {
            string text = "";
            foreach(var item in list)
            {
                text += item+"\n";
            }
            return text;
        }

        public void RemoveLast()
        {
            list.RemoveLast();
        }

        public void RemoveFirst()
        {
            list.RemoveFirst();
        }

        private void AddC_CPP()
        {
            list.AddLast("创建C/CPP文件夹");
        }

        private void AddPython()
        {
            list.AddLast("创建Python文件夹");
        }

        private void AddJava()
        {
            list.AddLast("创建Java文件夹");
        }

        private void AddCSharp()
        {
            list.AddLast("创建CSharp文件夹");
        }
    }
}
