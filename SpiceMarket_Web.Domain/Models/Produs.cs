namespace SpiceMarket_Web.Domain.Models
{
    public class Produs
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public decimal Pret { get; set; }
        public string CaleImagine { get; set; }

        public int ImageHeight { get; set; } = 200;
        public int ImageWidth { get; set; } = 200;
    }
}