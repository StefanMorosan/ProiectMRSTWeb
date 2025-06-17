namespace SpiceMarket_Web.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; } 
        public string Nume { get; set; }
        public decimal Cantitate { get; set; }
        public decimal Pret { get; set; }
    }

}