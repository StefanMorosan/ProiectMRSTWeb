// WishlistItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiceMarket_Web.Domain.Models
{
    public class WishlistItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Utilizator { get; set; } // Email, username etc.

        [Required]
        public string Produs { get; set; } // Nume produs
    }
}
