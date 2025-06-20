CREATE TABLE Purchases (
    Id INT PRIMARY KEY IDENTITY,
    ProdusId INT FOREIGN KEY REFERENCES Produs(Id),
    CustomerId INT, -- Optional: Add a Customers table later to track customer details
    Quantity INT NOT NULL,
    PurchaseDate DATETIME NOT NULL
);
-- Ensure the Purchases table is created in the SpiceMarketDB database