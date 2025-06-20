using System.ComponentModel.DataAnnotations;

namespace SpiceMarket_Web.Domain.Models
{
    public class Utilizator
    {
        public int Id { get; set; }
        public string NumeUtilizator { get; set; }
        public string Parola { get; set; }

        [Required]
        public string Email { get; set; }

        public string Rol { get; set; }

        public int RoleLevel { get; set; }
    }
}