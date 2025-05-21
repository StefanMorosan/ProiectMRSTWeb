using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Domain;

namespace SpiceMarket_Web.Presentation.Controllers
{
    public class WishlistController : Controller
    {
        private readonly SpiceMarketContext db = new SpiceMarketContext();

        [HttpGet]
        public ActionResult AdaugaLaWishlist(string produs)
        {
            var utilizator = Session["Utilizator"] as string;
            if (string.IsNullOrEmpty(utilizator) || string.IsNullOrEmpty(produs))
            {
                return RedirectToAction("Autentificare", "Home");
            }

            // Evităm duplicate
            var dejaAdaugat = db.Wishlist.Any(w => w.Produs == produs && w.Utilizator == utilizator);
            if (!dejaAdaugat)
            {
                db.Wishlist.Add(new WishlistItem
                {
                    Produs = produs,
                    Utilizator = utilizator
                });
                db.SaveChanges(); // <-- ACESTA este critic!
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Lista()
        {
            var utilizator = Session["Utilizator"] as string;
            if (string.IsNullOrEmpty(utilizator))
                return RedirectToAction("Autentificare", "Home");

            var wishlistItems = db.Wishlist
                .Where(w => w.Utilizator == utilizator)
                .Select(w => w.Produs)
                .ToList();

            var produse = db.Produse
                .Where(p => wishlistItems.Contains(p.Nume))
                .ToList();

            return View(produse); // trebuie să returneze lista de obiecte Produs
        }


    }
}
