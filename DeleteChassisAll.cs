using System;
using System.IO;

class Program
{
    static void Main()
    {
        string dir = @"I:\홈페이지\해머텍코리아";
        string targetFile = "re_home3_case_1211.html";
        string targetPath = Path.Combine(dir, targetFile);

        // Delete the ALL CHASSIS file
        if (File.Exists(targetPath))
        {
            File.Delete(targetPath);
            Console.WriteLine("Deleted " + targetFile);
        }

        // Replace all references in HTML files
        string[] htmlFiles = Directory.GetFiles(dir, "*.html", SearchOption.AllDirectories);
        int replaceCount = 0;
        foreach (string file in htmlFiles)
        {
            try
            {
                string content = File.ReadAllText(file, System.Text.Encoding.Default);
                if (content.Contains("re_home3_case_1211.html"))
                {
                    content = content.Replace("re_home3_case_1211.html", "re_home3_1211_h_series.html");
                    File.WriteAllText(file, content, System.Text.Encoding.Default);
                    replaceCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing " + file + ": " + ex.Message);
            }
        }
        
        Console.WriteLine("Replaced re_home3_case_1211.html with re_home3_1211_h_series.html in " + replaceCount + " files.");
    }
}
