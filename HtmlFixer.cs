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

                // Remove the "CHASSIS 전체보기" list item
                content = Regex.Replace(content, @"<li><a href=""re_home3_case_1211\.html"" style=""color:#d8232a; font-weight:800;"">CHASSIS 전체보기</a></li>\s*", "");

                if (content != original)
                {
                    File.WriteAllText(file, content, encoding);
                    Console.WriteLine("Removed 'CHASSIS 전체보기' from " + file + " (Encoding: " + encoding.EncodingName + ")");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
