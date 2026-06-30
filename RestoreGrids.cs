using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] files = {
            "re_home3_1211_h_series.html",
            "re_home3_1211_m_series.html",
            "re_home3_1211_i_series.html",
            "re_home3_1211_keyboard.html",
            "re_home3_1211_mousepad.html",
            "re_home3_1211_accessory.html",
            "re_home3_1211_cooling_tunning.html"
        };
        
        foreach (var file in files)
        {
            string oldPath = Path.Combine(@"I:\홈페이지\해머텍코리아\old_html", file);
            string newPath = Path.Combine(@"I:\홈페이지\해머텍코리아", file);
            if (!File.Exists(oldPath) || !File.Exists(newPath)) continue;
            
            string oldContent = File.ReadAllText(oldPath, System.Text.Encoding.Default);
            string newContent = File.ReadAllText(newPath, System.Text.Encoding.Default);
            
            int startIndex = oldContent.IndexOf("<div class=\"h_product_bg\">");
            int endIndex = oldContent.IndexOf("<!--홈화면내용끝-->", startIndex);
            
            if (startIndex != -1 && endIndex != -1)
            {
                string oldGridHtml = oldContent.Substring(startIndex, endIndex - startIndex);
                
                // Now replace the <main class="product-grid-container"...> block in the new file
                string newRegex = @"<main class=""product-grid-container"".*?</main>";
                
                if (Regex.IsMatch(newContent, newRegex, RegexOptions.Singleline))
                {
                    newContent = Regex.Replace(newContent, newRegex, oldGridHtml, RegexOptions.Singleline);
                    File.WriteAllText(newPath, newContent, System.Text.Encoding.Default);
                    Console.WriteLine("Restored original grid for " + file);
                }
                else
                {
                    Console.WriteLine(file + ": Grid container not found in new file.");
                }
            }
            else
            {
                Console.WriteLine(file + ": Start/End not found in old file.");
            }
        }
    }
}
