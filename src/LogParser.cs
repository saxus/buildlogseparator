using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildLogSeparator
{
    public class LogParser
    {
        public static List<Region> ExplodeToRegions(string[] lines)
        {
            var list = new List<Region>();

            var currentRegion = "main";
            var sb = new StringBuilder();


            foreach (var line in lines)
            {
                if (line.Length > 30)
                {
                    var t = line.Substring(29);
                    if (t.StartsWith("======== ") && t.EndsWith(" logs"))
                    {
                        Swap();

                        currentRegion = t.Substring(9, t.Length - 9 - 5);
                        sb.Clear();
                    }
                }

                sb.AppendLine(line);
            }

            Swap();

            return list;

            void Swap()
            {
                var region = new Region(currentRegion, sb.ToString());
                list.Add(region);
            }
        }
    }
}
