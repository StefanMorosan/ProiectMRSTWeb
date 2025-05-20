const products = [
    { name: "Scorțișoară", price: 80 },
    { name: "Ghimbir", price: 60 },
    { name: "Turmeric", price: 70 },
    { name: "Curcuma", price: 70 },
    { name: "Paprika", price: 75 },
    { name: "Piper", price: 90 },
    { name: "Cardamom", price: 120 }
];
function renderProducts(productList) {
    const container = document.getElementById('productContainer');
    container.innerHTML = ''; // șterge produsele vechi

    productList.forEach(product => {
        const item = document.createElement('div');
        item.style.border = '1px solid #ccc';
        item.style.padding = '10px';
        item.style.marginBottom = '10px';
        item.style.borderRadius = '8px';

        item.innerHTML = `
      <h3>${product.name}</h3>
      <p>${product.price} MDL/kg</p>
      <button>Adaugă în coș</button>
    `;
        container.appendChild(item);
    });
}
document.getElementById('searchInput').addEventListener('input', function (e) {
    const query = e.target.value.toLowerCase();

    const filtered = products.filter(product =>
        product.name.toLowerCase().includes(query)
    );

    renderProducts(filtered);
});

