$utf8NoBom = New-Object System.Text.UTF8Encoding $false
foreach ($f in @('re_home3_contact.html', 'caseclick_ta10.html')) {
    $path = 'I:\홈페이지\해머텍코리아\' + $f
    $content = Get-Content $path -Raw -Encoding UTF8
    $content = $content -replace '<a href="#([^"]*)">\s*CHASSIS', '<a href="re_home3_case_1211.html">CHASSIS'
    $content = $content -replace '<a href="#([^"]*)">\s*KEYBOARD & MICE', '<a href="re_home3_1211_keyboard.html">KEYBOARD & MICE'
    $content = $content -replace '<a href="#([^"]*)">\s*ACCESSORY', '<a href="re_home3_1211_accessory.html">ACCESSORY'
    $content = $content -replace 'window\.innerWidth\s*<=\s*900', 'window.innerWidth <= 0'
    [System.IO.File]::WriteAllText($path, $content, $utf8NoBom)
    Write-Host 'Fixed ' $f
}
