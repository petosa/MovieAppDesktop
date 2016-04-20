using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kumquat.NET
{
    static class Utils
    {
        public static List<String> getAspect(String q, String aspect)
        {
            List<String> arr = new List<String>();
            while(q.Contains("\""+ aspect +"\":"))
            {
                int index = q.IndexOf("\"" + aspect + "\":");
                index += 4 + aspect.Length;
                String build = "";
                char at = q[index];
                while (q[index] != '"')
                {
                    build += q[index];
                    index++;
                }
                arr.Add(build);
                q = q.Substring(index);
                Console.WriteLine(build);
            }
            return arr;
        }

    }
}
