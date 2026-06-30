using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = @"I:\홈페이지\해머텍코리아";
            string[] files = Directory.GetFiles(dir, "*.html", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                Encoding encoding = Encoding.Default;
                string content;

                using (StreamReader reader = new StreamReader(file, Encoding.Default, true))
                {
                    content = reader.ReadToEnd();
                    encoding = reader.CurrentEncoding;
                }

                string original = content;

                // Add "CHASSIS 전체보기" link to the Chassis dropdown across all files
                if (!content.Contains("CHASSIS 전체보기") && content.Contains("re_home3_1211_h_series.html"))
                {
                    content = Regex.Replace(content, 
                        @"(<li>\s*<a href=""re_home3_1211_h_series\.html"">)", 
                        "<li><a href=\"re_home3_case_1211.html\" style=\"color:#d8232a; font-weight:800;\">CHASSIS 전체보기</a></li>\n                            $1");
                }

                if (content != original)
                {
                    File.WriteAllText(file, content, encoding);
                    Console.WriteLine("Added CHASSIS Link to " + file + " (Encoding: " + encoding.EncodingName + ")");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
