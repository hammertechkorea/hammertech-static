$pages = @(
    "index.html",
    "re_home3_1211_m_series.html",
    "re_home3_1211_h_series.html",
    "re_home3_1211_i_series.html",
    "re_home3_1211_keyboard.html",
    "re_home3_1211_mousepad.html",
    "re_home3_1211_accessory.html",
    "re_home3_1211_cooling_tunning.html"
)

foreach ($page in $pages) {
    $path = "I:\홈페이지\해머텍코리아\$page"
    if (Test-Path $path) {
        $c = [System.IO.File]::ReadAllText($path, [System.Text.Encoding]::UTF8)
        
        # Replace the product grid content
        # Note: the active pages currently might have different structures depending on the category.
        # But for M series and index, we'll try to find the grid.
        
        # Actually, since I am not sure about the exact HTML of each page, 
        # I will inject the products.js script before </body>
        if (-not $c.Contains('src="js/products.js"')) {
            $c = $c -replace '</body>', "<script src=`"js/products.js`"></script>`n</body>"
            [System.IO.File]::WriteAllText($path, $c, [System.Text.Encoding]::UTF8)
            Write-Host "Injected script to $page"
        }
    }
}
