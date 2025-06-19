using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using SpiceMarket_Web.BusinessLogic.Interfaces;
using SpiceMarket_Web.BusinessLogic.Services;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Filters;
using System;

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
                    // Set cookies for user session
                    HttpCookie usernameCookie = new HttpCookie("username", user.NumeUtilizator);
                    usernameCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(usernameCookie);

                    HttpCookie roleCookie = new HttpCookie("role", user.Rol);
                    roleCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(roleCookie);

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

        [AdminMod(Roles = "admin")] // Restrict to Admin only
        public ActionResult SecretReport()
        {
            return View();
        }

        public ActionResult Deconectare()
        {
            // Clear session and cookies
            Session.Clear();

            HttpCookie usernameCookie = new HttpCookie("username");
            usernameCookie.Expires = DateTime.Now.AddDays(-1); // Expire the cookie
            Response.Cookies.Add(usernameCookie);

            HttpCookie roleCookie = new HttpCookie("role");
            roleCookie.Expires = DateTime.Now.AddDays(-1); // Expire the cookie
            Response.Cookies.Add(roleCookie);

            return RedirectToAction("Index");
        }
    }
}