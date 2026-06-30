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

                // 1. Change main links to point to overview pages instead of #
                content = Regex.Replace(content, @"<a href=""#([^""]*)"">\s*CHASSIS", "<a href=\"re_home3_case_1211.html\">CHASSIS");
                content = Regex.Replace(content, @"<a href=""#([^""]*)"">\s*KEYBOARD & MICE", "<a href=\"re_home3_1211_keyboard.html\">KEYBOARD & MICE");
                content = Regex.Replace(content, @"<a href=""#([^""]*)"">\s*ACCESSORY", "<a href=\"re_home3_1211_accessory.html\">ACCESSORY");

                // 2. Disable the mobile accordion JS by changing window.innerWidth <= 900 to window.innerWidth <= 0
                // This allows the main links to be clicked on mobile without e.preventDefault() blocking them.
                content = Regex.Replace(content, @"window\.innerWidth\s*<=\s*900", "window.innerWidth <= 0");

                if (content != original)
                {
                    File.WriteAllText(file, content, encoding);
                    Console.WriteLine("Updated links & JS in " + file + " (Encoding: " + encoding.EncodingName + ")");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
