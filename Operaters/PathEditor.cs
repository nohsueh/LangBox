using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters
{
    internal class PathEditor
    {
        public static void AddInUserPath(string newPath)
        {
            string pathVar = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if (!pathVar.Contains(newPath))
            {
                if (!pathVar.EndsWith(";") && pathVar != string.Empty)
                {
                    pathVar += ";";
                }

                pathVar += newPath;
            }
            Environment.SetEnvironmentVariable("PATH", pathVar, EnvironmentVariableTarget.User);
        }

        public static void RemoveInUserPath(string oldPath)
        {
            string pathVar = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if (pathVar.Contains(oldPath))
            {
                int len = System.Text.RegularExpressions.Regex.Matches(pathVar, ";").Count;

                if (!pathVar.EndsWith(oldPath))
                {
                    oldPath += ";";
                }

                pathVar = pathVar.Replace(oldPath, "");
            }
            Environment.SetEnvironmentVariable("PATH", pathVar, EnvironmentVariableTarget.User);
        }
    }
}
