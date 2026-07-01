$htmlFiles = Get-ChildItem -Path . -Filter *.html | Where-Object { $_.Name -ne 'index.html' }

$newNav = @"
  <!-- Navigation -->
  <nav class="modern-nav">
    <a href="index.html" class="nav-logo">
      <img src="logo.png" alt="Hammertech Korea">
    </a>
    <button class="hamburger"><i class="fas fa-bars"></i></button>
    <ul class="nav-links">
      <li>
        <a href="#">CHASSIS <i class="fas fa-chevron-down" style="font-size: 0.8em; margin-left:5px;"></i></a>
        <ul class="dropdown-menu">
          <li><a href="re_home3_1211_h_series.html">H Series</a></li>
          <li><a href="re_home3_1211_m_series.html">M Series</a></li>
          <li><a href="re_home3_1211_i_series.html">I Series</a></li>
        </ul>
      </li>
      <li><a href="#">POWER SUPPLY</a></li>
      <li>
        <a href="#">KEYBOARD & MICE <i class="fas fa-chevron-down" style="font-size: 0.8em; margin-left:5px;"></i></a>
        <ul class="dropdown-menu">
          <li><a href="re_home3_1211_keyboard.html">Keyboard</a></li>
          <li><a href="re_home3_1211_mousepad.html">Mouse pad</a></li>
        </ul>
      </li>
      <li>
        <a href="#">ACCESSORY <i class="fas fa-chevron-down" style="font-size: 0.8em; margin-left:5px;"></i></a>
        <ul class="dropdown-menu">
          <li><a href="re_home3_1211_accessory.html">Accessory</a></li>
          <li><a href="re_home3_1211_cooling_tunning.html">Cooling & Tuning</a></li>
        </ul>
      </li>
      <li><a href="re_home3_contact.html">CONTACT</a></li>
    </ul>
  </nav>
"@

$newFooter = @"
  <!-- Modern Footer -->
  <footer class="modern-footer">
    <div class="footer-content">
      <div class="footer-info">
        <img src="hammer_ing_g.png" alt="Hammertech Logo">
        <p>
          <strong>Hammertech korea Co., Ltd</strong><br>
          Korea Office ｜ 3F, 67, Gajwa-ro, Seodaemun-gu, Seoul, 03663, Republic of Korea<br>
          E-mail : daniel@hammertech.co.kr
        </p>
      </div>
      <div class="footer-social">
        <a href="https://www.facebook.com/HammertechHK/" title="Facebook"><i class="fab fa-facebook-f"></i></a>
        <a href="#" title="YouTube"><i class="fab fa-youtube"></i></a>
        <a href="#" title="Instagram"><i class="fab fa-instagram"></i></a>
        <a href="#" title="Twitter"><i class="fab fa-twitter"></i></a>
      </div>
    </div>
    <div class="footer-bottom">
      Copyright &copy; 2026 Hammertech. All rights reserved.
    </div>
  </footer>

  <script>
    document.querySelector('.hamburger').addEventListener('click', function() {
      document.querySelector('.nav-links').classList.toggle('active');
    });
    window.addEventListener('scroll', function() {
      const nav = document.querySelector('.modern-nav');
      if (window.scrollY > 50) {
        nav.style.background = 'rgba(10, 10, 12, 0.8)';
        nav.style.backdropFilter = 'blur(15px)';
      } else {
        nav.style.background = 'rgba(15, 15, 20, 0.6)';
        nav.style.backdropFilter = 'blur(12px)';
      }
    });
  </script>
"@

$newHeadLinks = @"
  <link rel="stylesheet" href="css/modern.css">
  <link rel="stylesheet" href="css/modern-subpage.css">
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
"@

foreach ($file in $htmlFiles) {
    $content = [System.IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::UTF8)

    if ($content -match 'modern-nav') {
        continue
    }

    # Replace topfix nav
    $content = $content -replace '(?is)<div\s+class\s*=\s*"topfix">.*?</nav>[\s\n]*</div>[\s\n]*</div>', $newNav
    $content = $content -replace '(?is)<div\s+class\s*=\s*"topfix">.*?</nav>.*?(?=<!--\s*상단고정메뉴끝\s*-->)', $newNav
    $content = $content -replace '<!--\s*상단고정메뉴끝\s*-->', ''

    # Replace footer
    $content = $content -replace '(?is)<div\s+class\s*=\s*"footer">.*?</div>[\s\n]*</div>[\s\n]*</div>', $newFooter
    $content = $content -replace '(?is)<div\s+class\s*=\s*"footer">.*?</div>[\s\n]*</div>', $newFooter
    $content = $content -replace '(?is)<div\s+class\s*=\s*"footer">.*?</div>', $newFooter

    # Add head links
    $content = $content -replace '(?i)</head>', "$newHeadLinks`n</head>"

    # Remove bgcolor
    $content = $content -replace '(?i)<body[^>]*bgcolor\s*=\s*[^>]*>', '<body>'

    [System.IO.File]::WriteAllText($file.FullName, $content, [System.Text.Encoding]::UTF8)
}
Write-Output "Complete"
