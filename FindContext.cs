using System;
using System.IO;

class Program
{
    static void Main()
    {
        string content = File.ReadAllText(@"I:\홈페이지\해머텍코리아\old_html\re_home3_1211_h_series.html", System.Text.Encoding.Default);
        int index = content.IndexOf("class=\"h1_wrap\"");
        if (index > 0)
        {
            int start = Math.Max(0, index - 300);
            Console.WriteLine(content.Substring(start, 400));
        }
        else
        {
            Console.WriteLine("Not found");
        }
    }
}
