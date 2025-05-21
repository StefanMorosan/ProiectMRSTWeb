using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Models; // Asigură-te că ai acest namespace

namespace SpiceMarket_Web.Presentation.Controllers
{
    public class ContController : Controller
    {
        private readonly SpiceMarketContext db = new SpiceMarketContext();

        public ActionResult Profil()
        {
            var email = Session["Utilizator"] as string;
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Autentificare");

            var utilizator = db.Utilizatori.FirstOrDefault(u => u.Email == email);
            if (utilizator == null)
                return HttpNotFound();

            var wishlistCount = db.Wishlist.Count(w => w.Utilizator == email);
            var comenzi = db.Comenzi.Where(c => c.Utilizator == email).ToList();

            var model = new UserDashboardViewModel
            {
                Nume = utilizator.NumeUtilizator,
                Email = utilizator.Email,
                Rol = utilizator.Rol,
                NrProduseWishlist = wishlistCount,
                NrComenzi = comenzi.Count,
                DataUltimaComanda = comenzi.OrderByDescending(c => c.Data).FirstOrDefault()?.Data
            };

            return View(model);
        }
    }
}
