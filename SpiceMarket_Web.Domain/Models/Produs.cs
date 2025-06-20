namespace SpiceMarket_Web.Domain.Models
{
    public class Produs
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public decimal Pret { get; set; }
        public string CaleImagine { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public string Descriere { get; set; }
        public int Stoc { get; set; }
    }
}