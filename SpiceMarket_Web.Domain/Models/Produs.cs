namespace SpiceMarket_Web.Domain.Models
{
    public class Produs
    {
        public int Id { get; set; } // Primary Key
        public string Nume { get; set; } // Product Name
        public string Descriere { get; set; } // Description
        public decimal Pret { get; set; } // Price
        public int Stoc { get; set; } // Stock Quantity

        // New Properties for Image Handling
        public string CaleImagine { get; set; } // Image Path
        public int ImageHeight { get; set; } // Image Height
        public int ImageWidth { get; set; } // Image Width
    }
}