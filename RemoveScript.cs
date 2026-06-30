using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] files = Directory.GetFiles(@"I:\홈페이지\해머텍코리아", "*.html", SearchOption.AllDirectories);
        
        foreach (var file in files)
        {
            if (file.Contains("old_html")) continue;
            string content = File.ReadAllText(file, System.Text.Encoding.Default);
            if (content.Contains("<script src=\"js/products.js\"></script>"))
            {
                content = content.Replace("<script src=\"js/products.js\"></script>", "");
                File.WriteAllText(file, content, System.Text.Encoding.Default);
                Console.WriteLine("Removed products.js script from " + file);
            }
        }
    }
}
