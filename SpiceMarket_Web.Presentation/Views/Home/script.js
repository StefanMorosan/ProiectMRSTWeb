const produse = [
    { id: 1, denumire: "Laptop Asus", pret: 3200 },
    { id: 2, denumire: "Telefon Samsung", pret: 2100 },
    { id: 3, denumire: "Mouse Logitech", pret: 150 },
    { id: 4, denumire: "Casti Sony", pret: 300 }
];
const searchInput = document.getElementById("search");
const resultsDiv = document.getElementById("search-results");
const cartList = document.getElementById("cart");
const cart = [];

searchInput.addEventListener("input", () => {
    const query = searchInput.value.toLowerCase();
    resultsDiv.innerHTML = "";

    if (query.length === 0) return;

    const rezultate = produse.filter(p => p.denumire.toLowerCase().includes(query));

    if (rezultate.length === 0) {
        resultsDiv.innerHTML = "<p>Produsul nu a fost găsit.</p>";
        return;
    }

    rezultate.forEach(p => {
        const container = document.createElement("div");
        container.innerHTML = `
      <strong>${p.denumire}</strong><br>
      Preț: ${p.pret} RON<br>
      <button onclick="adaugaInCos(${p.id})">Adaugă în coș</button>
      <hr>
    `;
        resultsDiv.appendChild(container);
    });
});

function adaugaInCos(id) {
    const produs = produse.find(p => p.id === id);
    cart.push(produs);
    actualizeazaCos();
}

function actualizeazaCos() {
    cartList.innerHTML = "";
    cart.forEach(p => {
        const li = document.createElement("li");
        li.textContent = `${p.denumire} - ${p.pret} RON`;
        cartList.appendChild(li);
    });
}
