using System;
using System.Linq;
using System.Text;

namespace BuildLogSeparator
{
    public class ContentSorter
    {
        public static string Sort(string content)
        {
            var lines = content.Split('\n')
                               .Where(x => !string.IsNullOrWhiteSpace(x))
                               .Select(x =>
                               {
                                   var t = x.Length == 30 && x.EndsWith(" \r")
                                            ? "" : x.Trim().Substring(29);

                                   if (t.Length > 0 && t.StartsWith("==="))
                                   {
                                       return new { Key = "", Line = t };
                                   }

                                   var key = "";
                                   var line = "";

                                   if (t.Length < 1 || !char.IsNumber(t[0]))
                                   {
                                       key = "0000-00-00T00:00:00.0000000Z ";
                                       line = t;
                                   }
                                   else
                                   {
                                       key = t.Substring(0, 29);
                                       line = t.Substring(29);
                                   }

                                   return new
                                   {
                                       Key = key,
                                       Line = line,
                                   };
                               })
                               .OrderBy(x => x.Key)
                               .Select(x => $"{x.Key}{x.Line}");

            return string.Join(Environment.NewLine, lines);
        }
    }
}