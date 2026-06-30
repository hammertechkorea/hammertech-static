$files = Get-ChildItem -Path "I:\홈페이지\해머텍코리아" -Recurse -Filter "*.html"

foreach ($file in $files) {
    # Read as an array, join with `n
    $content = (Get-Content $file.FullName) -join "`n"
    $original = $content
    
    $content = $content -replace '(?i)\s*<li>\s*<a[^>]*>\s*WHERE TO BUY\s*</a>\s*</li>', ''
    $content = $content -replace '(?i)\s*<li>\s*WHERE TO BUY\s*</li>', ''
    $content = $content -replace '(?i)WHERE TO BUY', ''
    
    $content = $content -replace '\(Preview\)', ''
    $content = $content -replace '>CONTACT<', '>INQUIRY<'
    $content = $content -replace '단종|지원 종료|품절|판매 종료', ''

    if ($content -ne $original) {
        Set-Content -Path $file.FullName -Value $content -Encoding UTF8
        Write-Host "Updated $($file.FullName)"
    }
}
Write-Host "Done Update 2"
