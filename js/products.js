document.addEventListener('DOMContentLoaded', () => {
    const gridContainer = document.querySelector('.product-grid-container');
    if (!gridContainer) return;

    // Get filter parameters from data attributes if any
    const filterSeries = gridContainer.getAttribute('data-series');
    const filterCategory = gridContainer.getAttribute('data-category');

    fetch('products.json')
        .then(response => response.json())
        .then(products => {
            let filteredProducts = products;
            
            if (filterCategory) {
                filteredProducts = filteredProducts.filter(p => p.category === filterCategory);
            }
            if (filterSeries) {
                filteredProducts = filteredProducts.filter(p => p.series === filterSeries || p.category === filterSeries);
            }

            gridContainer.innerHTML = ''; // Clear loading or existing static content

            filteredProducts.forEach(product => {
                const card = document.createElement('a');
                card.href = product.detailUrl;
                card.className = 'product-item-card';

                let badge = "SIGNATURE";
                let badgeClass = "signature";
                if (product.name.includes("H4") || product.name.includes("M8") || product.name.includes("G1KB") || product.name.includes("TYPHOON")) {
                    badge = "FLAGSHIP";
                    badgeClass = "flagship";
                } else if (product.name.includes("NEW")) {
                    badge = "NEW";
                    badgeClass = "new";
                }

                let desc = "해머텍만의 프리미엄 설계<br>최상의 퍼포먼스와 내구성";
                if (product.category === "KEYBOARD & MICE") {
                    desc = "완벽한 타건감과 정밀한 컨트롤<br>게이머를 위한 최적의 기어";
                } else if (product.category === "ACCESSORY") {
                    desc = "PC 시스템의 완성도를 높이는<br>고성능 프리미엄 부품";
                }

                card.innerHTML = `
                    <div class="card-badge ${badgeClass}">${badge}</div>
                    <div class="product-card-image-box">
                        <img src="${product.thumbnail}" alt="${product.name}">
                    </div>
                    <div class="product-card-name">${product.name}</div>
                    <div class="product-card-desc">${desc}</div>
                    <button class="b2b-quote-btn" onclick="event.preventDefault(); event.stopPropagation(); location.href='re_home3_contact.html'">B2B 공급가 문의</button>
                `;
                
                gridContainer.appendChild(card);
            });
        })
        .catch(err => {
            console.error('Failed to load products:', err);
            gridContainer.innerHTML = '<p>Unable to load products. Please try again later.</p>';
        });
});
