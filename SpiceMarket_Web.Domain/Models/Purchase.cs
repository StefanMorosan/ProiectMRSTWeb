using System;

public class Purchase
{
    public int Id { get; set; }
    public int ProdusId { get; set; }
    public string ProductName { get; set; }
    public int CustomerId { get; set; }
    public string Username { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
}