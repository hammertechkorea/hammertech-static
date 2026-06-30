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
                // To safely detect encoding, we use StreamReader with true for detectEncodingFromByteOrderMarks
                // and fallback to Default (ANSI/EUC-KR on Korean Windows)
                Encoding encoding = Encoding.Default;
                string content;

                using (StreamReader reader = new StreamReader(file, Encoding.Default, true))
                {
                    content = reader.ReadToEnd();
                    encoding = reader.CurrentEncoding;
                }

                string original = content;

                // 1. Remove WHERE TO BUY line entirely
                content = Regex.Replace(content, @"(?is)<li[^>]*>\s*<a[^>]*>\s*WHERE TO BUY\s*</a>\s*</li>", "");
                content = Regex.Replace(content, @"(?is)<li[^>]*>\s*WHERE TO BUY\s*</li>", "");
                content = Regex.Replace(content, @"(?i)WHERE TO BUY", "");

                // 2. Remove (Preview) and Preview
                content = Regex.Replace(content, @"\(Preview\)", "");
                content = Regex.Replace(content, @"(?i)Preview", "");

                // 3. CONTACT to INQUIRY
                content = Regex.Replace(content, @">CONTACT<", ">INQUIRY<");

                // 4. Remove forbidden words
                content = Regex.Replace(content, @"단종|지원 종료|품절|판매 종료", "");

                if (content != original)
                {
                    // Write back with the EXACT SAME encoding
                    File.WriteAllText(file, content, encoding);
                    Console.WriteLine("Fixed " + file + " (Encoding: " + encoding.EncodingName + ")");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
