using System;

namespace SpiceMarket_Web.Domain.Models
{
    public class Purchase
    {
        public int Id { get; set; } // Primary Key
        public int ProdusId { get; set; } // Foreign Key to Produse
        public int CustomerId { get; set; } // Customer ID (optional for now)
        public int Quantity { get; set; } // Quantity purchased
        public DateTime PurchaseDate { get; set; } // Date of purchase
        public int Cantitate { get; set; }
    }
}