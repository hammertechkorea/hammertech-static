document.addEventListener('DOMContentLoaded', () => {
    const gridContainer = document.querySelector('.product-grid-container');
    if (!gridContainer) return;

    // Get filter parameters from data attributes if any, e.g., data-series="M Series"
    const filterSeries = gridContainer.getAttribute('data-series');

    fetch('products.json')
        .then(response => response.json())
        .then(products => {
            let filteredProducts = products;
            if (filterSeries) {
                filteredProducts = products.filter(p => p.series === filterSeries || p.category === filterSeries);
            }

            gridContainer.innerHTML = ''; // Clear loading or existing static content

            filteredProducts.forEach(product => {
                const card = document.createElement('a');
                card.href = product.detailUrl;
                card.className = 'product-item-card';

                const imgBox = document.createElement('div');
                imgBox.className = 'product-card-image-box';
                const img = document.createElement('img');
                img.src = product.thumbnail;
                img.alt = product.name;
                imgBox.appendChild(img);

                const infoBox = document.createElement('div');
                infoBox.className = 'product-card-info-box';
                const categoryDiv = document.createElement('div');
                categoryDiv.className = 'product-card-category';
                categoryDiv.textContent = `${product.category} / ${product.series}`;
                
                const nameDiv = document.createElement('div');
                nameDiv.className = 'product-card-name';
                nameDiv.textContent = product.name;
                
                infoBox.appendChild(categoryDiv);
                infoBox.appendChild(nameDiv);

                card.appendChild(imgBox);
                card.appendChild(infoBox);
                gridContainer.appendChild(card);
            });
        })
        .catch(err => {
            console.error('Failed to load products:', err);
            gridContainer.innerHTML = '<p>Unable to load products. Please try again later.</p>';
        });
});
