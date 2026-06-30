using System;
using System.IO;

class Program
{
    static void Main()
    {
        string dir = @"I:\홈페이지\해머텍코리아";
        string[] files = Directory.GetFiles(dir, "*.html", SearchOption.AllDirectories);
        
        foreach (string file in files)
        {
            string content = File.ReadAllText(file, System.Text.Encoding.Default);
            bool changed = false;
            
            if (content.Contains("<section class=\"page-intro-banner\">"))
            {
                if (file.Contains("case") || file.Contains("series"))
                {
                    content = content.Replace("<section class=\"page-intro-banner\">", "<section class=\"page-intro-banner banner-CHASSIS\">");
                    changed = true;
                }
                else if (file.Contains("keyboard") || file.Contains("mousepad"))
                {
                    content = content.Replace("<section class=\"page-intro-banner\">", "<section class=\"page-intro-banner banner-KEYBOARD\">");
                    changed = true;
                }
                else if (file.Contains("accessory") || file.Contains("cooling"))
                {
                    content = content.Replace("<section class=\"page-intro-banner\">", "<section class=\"page-intro-banner banner-ACCESSORY\">");
                    changed = true;
                }
            }
            
            if (changed)
            {
                File.WriteAllText(file, content, System.Text.Encoding.Default);
                Console.WriteLine("Updated banner in " + file);
            }
        }
    }
}
