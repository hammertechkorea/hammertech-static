using System;
using System.IO;

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
            if (!File.Exists(oldPath)) continue;
            
            string oldContent = File.ReadAllText(oldPath, System.Text.Encoding.Default);
            
            // Try to find the container
            int startIndex = oldContent.IndexOf("<div class=\"case_main_area\">");
            if (startIndex == -1) startIndex = oldContent.IndexOf("<div class=\"keyboard_main_area\">");
            if (startIndex == -1) startIndex = oldContent.IndexOf("<div class=\"ac_main_area\">");
            if (startIndex == -1) startIndex = oldContent.IndexOf("<div class=\"h_product_bg\">");
            
            if (startIndex != -1)
            {
                Console.WriteLine(file + ": Found container at " + startIndex);
            }
            else
            {
                Console.WriteLine(file + ": NOT FOUND");
            }
        }
    }
}
