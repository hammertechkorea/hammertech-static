import os
import re

directory = '.'

new_nav = """
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
"""

new_footer = """
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
"""

new_head_links = """
  <link rel="stylesheet" href="css/modern.css">
  <link rel="stylesheet" href="css/modern-subpage.css">
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
"""

for root, dirs, files in os.walk(directory):
    for filename in files:
        if filename.endswith(".html") and filename != "index.html" and not filename.startswith("caseclick_h1_01"): # Just iterating all htmls
            filepath = os.path.join(root, filename)
            
            with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
                content = f.read()

            # Skip already modified files
            if "modern-nav" in content:
                continue

            # 1. Replace topfix to end of its wrapper
            # It starts with <div class ="topfix"> (with or without space) and ends after </nav> and some </div> tags.
            # A simpler regex: find <div class\s*=\s*"topfix"> ... </nav>[\s\n]*</div>[\s\n]*</div>
            content = re.sub(r'<div\s+class\s*=\s*"topfix">.*?</nav>[\s\n]*</div>[\s\n]*</div>', new_nav, content, flags=re.DOTALL)
            
            # Also handle if it's slightly different
            content = re.sub(r'<div\s+class\s*=\s*"topfix">.*?</nav>.*?(?=<!--\s*상단고정메뉴끝\s*-->)', new_nav, content, flags=re.DOTALL)
            content = re.sub(r'<!--\s*상단고정메뉴끝\s*-->', '', content)

            # 2. Replace footer
            content = re.sub(r'<div\s+class\s*=\s*"footer">.*?</div>[\s\n]*</div>[\s\n]*</div>', new_footer, content, flags=re.DOTALL)
            content = re.sub(r'<div\s+class\s*=\s*"footer">.*?</div>[\s\n]*</div>', new_footer, content, flags=re.DOTALL)

            # 3. Add CSS links before </head>
            content = re.sub(r'</head>', new_head_links + '\n</head>', content, flags=re.IGNORECASE)

            # 4. Remove bgcolor from body
            content = re.sub(r'<body[^>]*bgcolor\s*=\s*["\'][^"\']*["\'][^>]*>', '<body>', content, flags=re.IGNORECASE)

            with open(filepath, 'w', encoding='utf-8') as f:
                f.write(content)

print("HTML update complete.")
