$files = Get-ChildItem -Filter *.html -Recurse
foreach ($f in $files) {
    (Get-Content $f.FullName) -replace '.*WHERE TO BUY.*', '' -replace '\(Preview\)', '' -replace '>CONTACT<', '>INQUIRY<' | Set-Content $f.FullName -Encoding UTF8
}
Write-Host "Done Replacement 4"
