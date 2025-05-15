using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.BusinessLogic.Interfaces;
using SpiceMarket_Web.BusinessLogic.Services;
using SpiceMarket_Web.Domain.Models;

namespace SpiceMarket_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly CartService _cartService;

        public HomeController(IProductRepository productRepository, CartService cartService)
        {
            _productRepository = productRepository;
            _cartService = cartService;
        }

        // GET: /Home/Index
        public ActionResult Index()
        {
            var products = _productRepository.GetAllProducts();
            return View(products);
        }

        // GET: /Home/Autentificare
        public ActionResult Autentificare()
        {
            return View();
        }

        // POST: /Home/Autentificare
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autentificare(string numeUtilizator, string parola)
        {
            using (var db = new SpiceMarketContext())
            {
                var user = db.Utilizatori
                             .FirstOrDefault(u =>
                                 u.NumeUtilizator == numeUtilizator &&
                                 u.Parola == parola);

                if (user != null)
                {
                    Session["Utilizator"] = user.NumeUtilizator;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.MesajEroare = "Nume utilizator sau parolă incorecte.";
            return View();
        }

        // GET: /Home/Inregistrare
        [HttpGet]
        public ActionResult Inregistrare()
        {
            return View();
        }

        // POST: /Home/Inregistrare
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

                db.Utilizatori.Add(model);
                db.SaveChanges();
            }

            TempData["Success"] = "Înregistrare reușită! Te poți autentifica acum.";
            return RedirectToAction("Autentificare");
        }

        // GET: /Home/Cos
        public ActionResult Cos()
        {
            var cartItems = _cartService.GetCartItems();
            ViewBag.Total = _cartService.CalculateTotal();
            return View(cartItems);
        }

        // GET: /Home/AdaugaInCos
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

        // GET: /Home/EliminaDinCos
        public ActionResult EliminaDinCos(string produs)
        {
            _cartService.RemoveFromCart(produs);
            return RedirectToAction("Cos");
        }
    }
}
