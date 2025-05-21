using System;

namespace SpiceMarket_Web.Presentation.Models
{
    public class UserDashboardViewModel
    {
        public string Nume { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public int NrProduseWishlist { get; set; }
        public int NrComenzi { get; set; }
        public DateTime? DataUltimaComanda { get; set; }
    }
}
