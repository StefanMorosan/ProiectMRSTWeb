using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpiceMarket_Web.Domain.Models
{
    public class Utilizator
    {
        public int Id { get; set; }
        public string NumeUtilizator { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}