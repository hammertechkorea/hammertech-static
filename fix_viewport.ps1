$files = Get-ChildItem -Path . -Filter "*.html" -Recurse

foreach ($f in $files) {
    $content = Get-Content $f.FullName -Raw -Encoding UTF8
    $pattern = '<meta\s+name="viewport"\s+content="width=device-width,\s*initial-scale=1\.0,\s*maximum-scale=0\.25[^>]*>(?:<!--.*?-->)?'
    $replacement = '<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />'
    
    $newContent = [regex]::Replace($content, $pattern, $replacement, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
    
    if ($content -cne $newContent) {
        Set-Content -Path $f.FullName -Value $newContent -Encoding UTF8
        Write-Host "Updated $($f.FullName)"
    }
}
