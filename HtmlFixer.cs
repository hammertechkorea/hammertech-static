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
            string file = @"I:\홈페이지\해머텍코리아\caseclick_ta10.html";
            Encoding encoding = Encoding.Default;
            string content;
            using (StreamReader reader = new StreamReader(file, Encoding.Default, true))
            {
                content = reader.ReadToEnd();
                encoding = reader.CurrentEncoding;
            }
            content = content.Replace("<a href=\"#\">CHASSIS", "<a href=\"re_home3_case_1211.html\">CHASSIS");
            content = content.Replace("<a href=\"#\">KEYBOARD&MICE", "<a href=\"re_home3_1211_keyboard.html\">KEYBOARD&MICE");
            content = content.Replace("<a href=\"#\">ACCESSORY", "<a href=\"re_home3_1211_accessory.html\">ACCESSORY");
            File.WriteAllText(file, content, encoding);
            Console.WriteLine("Fixed caseclick_ta10.html");
        }
    }
}
