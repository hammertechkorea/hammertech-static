using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"I:\홈페이지\해머텍코리아\style.css";
        string append = "\n/* --- Detail Page Mobile Overrides --- */\n" +
                        "body, html { max-width: 100%; overflow-x: hidden; }\n" +
                        "table, div, .container, .main_content { max-width: 100% !important; box-sizing: border-box; }\n" +
                        "img, iframe, video { max-width: 100% !important; height: auto !important; }\n" +
                        "@media (max-width: 768px) { \n" +
                        "    .container { width: 100% !important; padding: 0 10px !important; }\n" +
                        "    table { width: 100% !important; }\n" +
                        "    td, th { display: block; width: 100% !important; }\n" +
                        "}\n";
        File.AppendAllText(path, append);
        Console.WriteLine("Appended mobile CSS to style.css");
    }
}
