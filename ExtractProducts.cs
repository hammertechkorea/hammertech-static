using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string dir = @"I:\홈페이지\해머텍코리아";
        string jsonPath = Path.Combine(dir, "products.json");
        string json = File.ReadAllText(jsonPath);
        
        json = json.TrimEnd();
        if (json.EndsWith("]")) json = json.Substring(0, json.Length - 1);
        
        string newProducts = "";
        
        newProducts += ExtractProducts(Path.Combine(dir, "re_home3_1211_keyboard.html"), "KEYBOARD & MICE", "Keyboard");
        newProducts += ExtractProducts(Path.Combine(dir, "re_home3_1211_mousepad.html"), "KEYBOARD & MICE", "Mouse pad");
        newProducts += ExtractProducts(Path.Combine(dir, "re_home3_1211_accessory.html"), "ACCESSORY", "Accessory");
        newProducts += ExtractProducts(Path.Combine(dir, "re_home3_1211_cooling_tunning.html"), "ACCESSORY", "Cooling & Tunning");
        
        json += newProducts + "\n]";
        File.WriteAllText(jsonPath, json, System.Text.Encoding.UTF8);
        Console.WriteLine("Added new products to products.json");
    }
    
    static string ExtractProducts(string filePath, string category, string series)
    {
        if (!File.Exists(filePath)) return "";
        string html = File.ReadAllText(filePath, System.Text.Encoding.Default);
        
        string result = "";
        Regex itemRegex = new Regex(@"<li>\s*<a[^>]*href\s*=\s*""([^""]+)""[^>]*>\s*<img[^>]*src\s*=\s*""([^""]+)""[^>]*>(.*?)<div[^>]*class=""h1_casename""[^>]*>\s*([^<]+)\s*</div>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
        
        MatchCollection matches = itemRegex.Matches(html);
        foreach (Match m in matches)
        {
            string url = m.Groups[1].Value.Trim();
            string img = m.Groups[2].Value.Trim();
            string name = "";
            Match nameMatch = Regex.Match(m.Groups[3].Value, @"<h>([^<]+)</h>", RegexOptions.IgnoreCase);
            if (nameMatch.Success) name = nameMatch.Groups[1].Value.Trim();
            if (name == "") name = m.Groups[4].Value.Trim();
            
            result += ",\n  {\n";
            result += string.Format("    \"category\": \"{0}\",\n", category);
            result += string.Format("    \"series\": \"{0}\",\n", series);
            result += string.Format("    \"name\": \"{0}\",\n", name);
            result += string.Format("    \"thumbnail\": \"{0}\",\n", img);
            result += string.Format("    \"detailUrl\": \"{0}\",\n", url);
            result += "    \"specImages\": []\n  }";
            Console.WriteLine(string.Format("Extracted: {0} from {1}/{2}", name, category, series));
        }
        
        return result;
    }
}
