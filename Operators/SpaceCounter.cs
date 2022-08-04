using System.Collections.Generic;

namespace LangBox.Operators
{
    internal class SpaceCounter
    {
        public static Dictionary<string, double> space = new Dictionary<string, double> {
            { "C_CPP", 47.1 },
            { "Python", 8.2 },
            { "Java", 173 }
        };

        //返回所需的总空间
        public static double SpaceRequired(Dictionary<string, bool> langMap)
        {
            double TotalSpace = 0;
            foreach (var kvp in langMap)
            {
                TotalSpace += kvp.Value ? space[kvp.Key] : 0;
            }
            return TotalSpace;
        }
    }
}
