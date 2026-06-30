using System;
using System.IO;

class Program
{
    static void Main()
    {
        string dir = @"I:\홈페이지\해머텍코리아";
        string templatePath = Path.Combine(dir, "re_home3_case_1211.html");
        string template = File.ReadAllText(templatePath, System.Text.Encoding.UTF8);

        // Define pages to migrate
        Migrate(dir, template, "re_home3_1211_h_series.html", "CHASSIS", "HAMMER TECH CASE", "YOUR HAMMER TO CONQUER!", "CHASSIS", "H Series", GetChassisTabs("re_home3_1211_h_series.html"));
        Migrate(dir, template, "re_home3_1211_m_series.html", "CHASSIS", "HAMMER TECH CASE", "YOUR HAMMER TO CONQUER!", "CHASSIS", "M Series", GetChassisTabs("re_home3_1211_m_series.html"));
        Migrate(dir, template, "re_home3_1211_i_series.html", "CHASSIS", "HAMMER TECH CASE", "YOUR HAMMER TO CONQUER!", "CHASSIS", "I Series", GetChassisTabs("re_home3_1211_i_series.html"));
        
        Migrate(dir, template, "re_home3_1211_keyboard.html", "KEYBOARD & MICE", "KEYBOARD & MICE", "Premium Gaming Peripherals", "KEYBOARD & MICE", "", GetKeyboardTabs("re_home3_1211_keyboard.html"));
        Migrate(dir, template, "re_home3_1211_mousepad.html", "KEYBOARD & MICE", "KEYBOARD & MICE", "Premium Gaming Peripherals", "KEYBOARD & MICE", "Mouse pad", GetKeyboardTabs("re_home3_1211_mousepad.html"));
        
        Migrate(dir, template, "re_home3_1211_accessory.html", "ACCESSORY", "ACCESSORY", "Custom PC Components", "ACCESSORY", "", GetAccessoryTabs("re_home3_1211_accessory.html"));
        Migrate(dir, template, "re_home3_1211_cooling_tunning.html", "ACCESSORY", "ACCESSORY", "Custom PC Components", "ACCESSORY", "Cooling & Tunning", GetAccessoryTabs("re_home3_1211_cooling_tunning.html"));
        
        // Also update the main chassis page to include data-category
        Migrate(dir, template, "re_home3_case_1211.html", "CHASSIS", "HAMMER TECH CASE", "YOUR HAMMER TO CONQUER!", "CHASSIS", "", GetChassisTabs("re_home3_case_1211.html"));
        
        Console.WriteLine("All category pages migrated to new tab layout.");
    }

    static void Migrate(string dir, string template, string fileName, string title, string h1Title, string h1Desc, string dataCategory, string dataSeries, string tabsHtml)
    {
        string path = Path.Combine(dir, fileName);
        
        // Replace Title
        string html = System.Text.RegularExpressions.Regex.Replace(template, @"<title>.*?</title>", string.Format("<title>HAMMERTECH - {0}</title>", title));
        
        // Replace Banner
        html = System.Text.RegularExpressions.Regex.Replace(html, @"<h1>.*?</h1>", string.Format("<h1>{0}</h1>", h1Title));
        html = System.Text.RegularExpressions.Regex.Replace(html, @"<p>YOUR HAMMER TO CONQUER!</p>", string.Format("<p>{0}</p>", h1Desc));
        
        // Replace Tabs block
        string tabsSectionRegex = @"<section class=""tabs-section"">.*?</section>";
        html = System.Text.RegularExpressions.Regex.Replace(html, tabsSectionRegex, tabsHtml, System.Text.RegularExpressions.RegexOptions.Singleline);
        
        // Replace data attributes
        html = System.Text.RegularExpressions.Regex.Replace(html, @"<main class=""product-grid-container"".*?>", string.Format("<main class=\"product-grid-container\" data-category=\"{0}\" data-series=\"{1}\">", dataCategory, dataSeries));
        
        File.WriteAllText(path, html, new System.Text.UTF8Encoding(false)); // UTF-8 No BOM
    }
    
    static string GetChassisTabs(string activePage)
    {
        return string.Format(@"<section class=""tabs-section"">
            <div class=""tab-menu"">
                <button class=""tab-btn {0}"" onclick=""location.href='re_home3_case_1211.html'"">ALL CHASSIS</button>
                <button class=""tab-btn {1}"" onclick=""location.href='re_home3_1211_h_series.html'"">H Series</button>
                <button class=""tab-btn {2}"" onclick=""location.href='re_home3_1211_m_series.html'"">M Series</button>
                <button class=""tab-btn {3}"" onclick=""location.href='re_home3_1211_i_series.html'"">I Series</button>
            </div>
        </section>", 
        activePage == "re_home3_case_1211.html" ? "active" : "",
        activePage == "re_home3_1211_h_series.html" ? "active" : "",
        activePage == "re_home3_1211_m_series.html" ? "active" : "",
        activePage == "re_home3_1211_i_series.html" ? "active" : "");
    }

    static string GetKeyboardTabs(string activePage)
    {
        return string.Format(@"<section class=""tabs-section"">
            <div class=""tab-menu"">
                <button class=""tab-btn {0}"" onclick=""location.href='re_home3_1211_keyboard.html'"">ALL KEYBOARD</button>
                <button class=""tab-btn {1}"" onclick=""location.href='re_home3_1211_mousepad.html'"">Mouse pad</button>
            </div>
        </section>", 
        activePage == "re_home3_1211_keyboard.html" ? "active" : "",
        activePage == "re_home3_1211_mousepad.html" ? "active" : "");
    }

    static string GetAccessoryTabs(string activePage)
    {
        return string.Format(@"<section class=""tabs-section"">
            <div class=""tab-menu"">
                <button class=""tab-btn {0}"" onclick=""location.href='re_home3_1211_accessory.html'"">ALL ACCESSORY</button>
                <button class=""tab-btn {1}"" onclick=""location.href='re_home3_1211_cooling_tunning.html'"">Cooling & Tunning</button>
            </div>
        </section>", 
        activePage == "re_home3_1211_accessory.html" ? "active" : "",
        activePage == "re_home3_1211_cooling_tunning.html" ? "active" : "");
    }
}
