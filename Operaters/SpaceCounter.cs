using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangBox.Operaters
{
    internal class SpaceCounter
    {
        //返回所需的总空间
        public static int SpaceRequired(Dictionary<string, bool> LangMap)
        {
            int TotalSpace = 0;
            foreach(var kvp in LangMap)
            {
                TotalSpace += kvp.Value ? 1 : 0;
            }
            return TotalSpace;
        }
    }
}
