using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters
{
    internal class SpaceCounter
    {
        private static Dictionary<string, double> space = new Dictionary<string, double> {
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
                TotalSpace += space[kvp.Key]*(kvp.Value ? 1 : 0);
            }
            return TotalSpace;
        }
    }
}
