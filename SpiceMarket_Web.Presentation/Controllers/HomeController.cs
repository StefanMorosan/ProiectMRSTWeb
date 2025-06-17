using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.BusinessLogic.Interfaces;
using SpiceMarket_Web.BusinessLogic.Services;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Filters; // Import custom filters

namespace SpiceMarket_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly CartService _cartService;
        private readonly SpiceMarketContext db = new SpiceMarketContext();


        public JsonResult SearchProducts(string term)
        {
            var produse = db.Produse
                            .Where(p => p.Nume.Contains(term))
                            .Select(p => new { p.Id, p.Nume, p.Pret })
                            .Take(5)
                            .ToList();

            return Json(produse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AdaugaInCos(int id)
        {
            var produs = db.Produse.FirstOrDefault(p => p.Id == id);
            if (produs == null)
                return HttpNotFound();

            // Coș în sesiune
            List<CartItem> cos = Session["Cos"] as List<CartItem>;
            if (cos == null)
                cos = new List<CartItem>();

            var existent = cos.FirstOrDefault(c => c.Produs.Id == id);
            if (existent != null)
            {
                existent.Cantitate++;
            }
            else
            {
                cos.Add(new CartItem
                {
                    Produs = produs,
                    Cantitate = 1
                });
            }

            Session["Cos"] = cos;

            return new HttpStatusCodeResult(200);
        }


        public HomeController(IProductRepository productRepository, CartService cartService)
        {
            _productRepository = productRepository;
            _cartService = cartService;
        }

        public ActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            return View(products);
        }

        public ActionResult Autentificare()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autentificare(string numeUtilizator, string parola)
        {
            using (var db = new SpiceMarketContext())
            {
                var user = db.Utilizatori
                    .FirstOrDefault(u => u.NumeUtilizator == numeUtilizator && u.Parola == parola);

                if (user != null)
                {
                    Session["Utilizator"] = user.NumeUtilizator;
                    Session["Rol"] = user.Rol; 
                    return RedirectToAction("Index");
                }
            }

            ViewBag.MesajEroare = "Nume utilizator sau parolă incorecte.";
            return View();
        }

        [HttpGet]
        public ActionResult Inregistrare()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inregistrare(Utilizator model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var db = new SpiceMarketContext())
            {
                if (db.Utilizatori.Any(u => u.NumeUtilizator == model.NumeUtilizator))
                {
                    ModelState.AddModelError("NumeUtilizator", "Acest nume de utilizator este deja folosit.");
                    return View(model);
                }

                model.Rol = "utilizator"; // Utilizator normal
                db.Utilizatori.Add(model);
                db.SaveChanges();
            }

            TempData["Success"] = "Înregistrare reușită! Te poți autentifica acum.";
            return RedirectToAction("Autentificare");
        }

        [UserMod]
        public ActionResult Cos()
        {
            var cartItems = _cartService.GetCartItems();
            ViewBag.Total = _cartService.CalculateTotal();
            return View(cartItems);
        }

        [UserMod]
        public ActionResult AdaugaInCos(string produs)
        {
            try
            {
                _cartService.AddToCart(produs, 1);
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [UserMod]
        public ActionResult EliminaDinCos(string produs)
        {
            _cartService.RemoveFromCart(produs);
            return RedirectToAction("Cos");
        }

        [UserMod]
        public ActionResult Achizitii()
        {
            return View();
        }

        [UserMod]
        public ActionResult Setari()
        {
            return View();
        }

        [AdminMod]
        public ActionResult SecretReport()
        {
            return View(); // Accesibil doar adminului
        }

        public ActionResult Deconectare()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
