using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiceMarket_Web.ViewModels
{
    public class PurchaseViewModel
    {
        public string Nume { get; set; }
        public int Cantitate { get; set; }
        public decimal PretPerKg { get; set; }
        public DateTime DataAchizitie { get; set; }
    }
}