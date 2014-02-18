using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTumblr
{
    public static class ExtracterHelper
    {
        public static string ExtractString(string source, string start, string end)
        {
            int posStart = source.IndexOf(start) + start.Length;
            int posEnd = source.IndexOf(end);

            string result = source.Substring(posStart, posEnd - posStart);
            return result;
        }
    }
}
