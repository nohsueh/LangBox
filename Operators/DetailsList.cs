using System.Collections.Generic;
using System.Linq;

namespace LangBox.Operators
{
    internal class DetailsList
    {
        private LinkedList<string> list = new LinkedList<string>();


        public void SetProgressList(Dictionary<string, bool> langMap)
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
            foreach (var item in list)
            {
                text += item + "\n";
            }
            return text;
        }

        private void AddC_CPP()
        {
            list.AddLast("      C语言代表的过程性传统、C++在C语言基础上添加的类代表的面向对象语言的传统以及C++模板支持的通用编程传统。");
            list.AddLast("🌟MinGW-W64 GCC-8.1.0");
            list.AddLast("✨x86_64-win32-seh");
            list.AddLast("-------------------------------------------------");
        }

        private void AddPython()
        {
            list.AddLast("      Python对于初级程序员来说非常的友好，语法简单易懂，应用广泛，实用性强。");
            list.AddLast("🌟Python 3.10.5 - June 6, 2022");
            list.AddLast("✨Note that Python 3.10.5 cannot be used on Windows 7 or earlier.");
            list.AddLast("-------------------------------------------------");
        }

        private void AddJava()
        {
            list.AddLast("      Java是一门面向对象的编程语言，不仅吸收了C++语言的各种优点，还摒弃了C++里难以理解的多继承、指针等概念，因此Java语言具有功能强大和简单易用两个特征。");
            list.AddLast("🌟Java SE Development Kit 18.0.1.1");
            list.AddLast("✨JDK 18 will receive updates under these terms, until September 2022 when it will be superseded by JDK 19");
            list.AddLast("-------------------------------------------------");
        }

        private void AddCSharp()
        {
            list.AddLast("待定");
        }
    }
}
