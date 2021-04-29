using System.Collections.Generic;
using System.Linq;

namespace Pkgscan.Helpers
{
    public static class Extensions
    {
        public static int GetPadLength(this Dictionary<string, string> dictionary)
        {
            return dictionary.Keys.Max(x => x.Length);
        }
    }
}