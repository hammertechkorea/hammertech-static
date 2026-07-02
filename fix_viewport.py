import os
import re

replacement = '<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />'
pattern = re.compile(r'<meta name="viewport" content="width=device-width, initial-scale=1\.0, maximum-scale=0\.25[^>]*>(?:<!--.*?-->)?', re.IGNORECASE)

updated_count = 0

for root, dirs, files in os.walk('.'):
    for f in files:
        if f.endswith('.html'):
            path = os.path.join(root, f)
            with open(path, 'r', encoding='utf-8', errors='ignore') as file:
                content = file.read()
            
            new_content = pattern.sub(replacement, content)
            
            if new_content != content:
                with open(path, 'w', encoding='utf-8') as file:
                    file.write(new_content)
                print(f"Updated viewport in {path}")
                updated_count += 1

print(f"Total updated files: {updated_count}")
