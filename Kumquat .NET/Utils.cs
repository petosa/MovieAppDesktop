using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommender
{
    public static class Utils
    {
        public static List<String> getTitles(String q, String aspect)
        {
            List<String> arr = new List<String>();
            while(q.Contains("\""+ aspect +"\":"))
            {
                int index = q.IndexOf("\"" + aspect + "\":");
                index += 4 + aspect.Length;
                String build = "";
                char at = q[index];
                while(q[index] != '"')
                {
                    build += q[index];
                }
                arr.Add(build);
            }
            return arr;
        }

    }
}
