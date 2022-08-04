using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operators
{
    internal class PathEditor
    {
        public static void AddInUserPath(string variable, string newPath)
        {
            string? pathVar = Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.User);
            if (pathVar == null)
            {
                Environment.SetEnvironmentVariable(variable, newPath, EnvironmentVariableTarget.User);
                return;
            }
            if (!pathVar.Contains(newPath))
            {
                if (!pathVar.StartsWith(";") && pathVar != string.Empty)
                {
                    pathVar = ";"+ pathVar;
                }

                pathVar = newPath+ pathVar;
            }
            Environment.SetEnvironmentVariable(variable, pathVar, EnvironmentVariableTarget.User);
        }

        public static void RemoveInUserPath(string variable,string oldPath)
        {
            string? pathVar = Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.User);
            if (pathVar == null)
            {
                return;
            }
            if (pathVar.Contains(oldPath))
            {
                if (!pathVar.EndsWith(oldPath))
                {
                    oldPath += ";";
                }

                pathVar = pathVar.Replace(oldPath, "");
            }
            Environment.SetEnvironmentVariable(variable, pathVar, EnvironmentVariableTarget.User);
        }
    }
}
