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

        // Home page with optional search query
        public ActionResult Index(string searchQuery)
        {
            var products = _productRepository.GetAllProducts();

            // Filter products based on search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products
                    .Where(p => p.Nume.ToLower().Contains(searchQuery.ToLower()))
                    .ToList();
            }

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
                // Fix authentication logic to correctly query Utilizators
                var user = db.Utilizators
                    .FirstOrDefault(u => u.NumeUtilizator == numeUtilizator && u.Parola == parola);

                if (user != null)
                {
                    // Set cookies for user session
                    HttpCookie usernameCookie = new HttpCookie("username", user.NumeUtilizator);
                    usernameCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(usernameCookie);

                    HttpCookie roleCookie = new HttpCookie("role", user.Rol ?? "utilizator");
                    roleCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(roleCookie);

                    // Restore session for authenticated user
                    Session["Utilizator"] = user.NumeUtilizator;

                    TempData["Success"] = "Autentificare reușită!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MesajEroare = "Nume utilizator sau parolă incorecte.";
                    return View();
                }
            }
        }

        [HttpGet]
        public JsonResult LiveSearch(string searchQuery)
        {
            var products = _productRepository.GetAllProducts();

            // Filter products based on search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products
                    .Where(p => p.Nume.ToLower().Contains(searchQuery.ToLower()))
                    .ToList();
            }

            return Json(products.Select(p => new
            {
                Nume = p.Nume,
                Pret = p.Pret,
                CaleImagine = Url.Content(p.CaleImagine)
            }), JsonRequestBehavior.AllowGet);
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
                if (db.Utilizators.Any(u => u.NumeUtilizator == model.NumeUtilizator))
                {
                    ModelState.AddModelError("NumeUtilizator", "Acest nume de utilizator este deja folosit.");
                    return View(model);
                }

                // Set default role if not provided
                model.Rol = model.Rol ?? "utilizator";

                // Save user data to the database
                db.Utilizators.Add(model);
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
                var username = Session["Utilizator"] as string;

                // Ensure user is authenticated before adding to cart
                if (string.IsNullOrEmpty(username))
                {
                    TempData["Error"] = "Autentificare necesară.";
                    return RedirectToAction("Autentificare");
                }

                _cartService.AddToCart(produs, 1);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
            Session.Abandon();

            // Clear all cookies
            if (Request.Cookies != null)
            {
                foreach (var key in Request.Cookies.AllKeys)
                {
                    var cookie = new HttpCookie(key);
                    cookie.Expires = DateTime.Now.AddDays(-1); // Expire the cookie
                    Response.Cookies.Add(cookie);
                }
            }

            TempData["Success"] = "Te-ai deconectat cu succes!";
            return RedirectToAction("Index");
        }

        [UserMod]
        public ActionResult Checkout()
        {
            // Load items from the cart
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Coșul tău este gol. Adaugă produse înainte de checkout.";
                return RedirectToAction("Cos");
            }

            return View(cartItems); // Pass cart items to the checkout page
        }

        [HttpPost]
        [UserMod]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder()
        {
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Coșul tău este gol. Adaugă produse înainte de checkout.";
                return RedirectToAction("Cos");
            }

            using (var db = new SpiceMarketContext())
            {
                foreach (var item in cartItems)
                {
                    var produs = db.Produse.FirstOrDefault(p => p.Nume == item.Nume);

                    if (produs != null)
                    {
                        var purchase = new Purchase
                        {
                            ProdusId = produs.Id, 
                            CustomerId = 123, 
                            Quantity = (int)item.Cantitate, 
                            PurchaseDate = DateTime.Now
                        };

                        db.Purchases.Add(purchase);
                    }
                }

                db.SaveChanges();
            }

            TempData["Success"] = "Comanda a fost plasată cu succes!";
            return RedirectToAction("Cos");
        }
    }
}