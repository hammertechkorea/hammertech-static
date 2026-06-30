using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string dir = @"I:\홈페이지\해머텍코리아";
        string[] targets = {
            "re_home3_1211_h_series.html",
            "re_home3_1211_m_series.html",
            "re_home3_1211_i_series.html",
            "re_home3_1211_keyboard.html",
            "re_home3_1211_mousepad.html",
            "re_home3_1211_accessory.html",
            "re_home3_1211_cooling_tunning.html"
        };
        
        foreach (string file in targets)
        {
            string path = Path.Combine(dir, file);
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path, System.Text.Encoding.Default);
                if (!content.Contains("js/products.js"))
                {
                    content = content.Replace("</body>", "<script src=\"js/products.js\"></script>\n</body>");
                    File.WriteAllText(path, content, System.Text.Encoding.Default);
                    Console.WriteLine("Injected JS into " + file);
                }
                else
                {
                    Console.WriteLine("Already has JS: " + file);
                }
            }
        }
    }
}
