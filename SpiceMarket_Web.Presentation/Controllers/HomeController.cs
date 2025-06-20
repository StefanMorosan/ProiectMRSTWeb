using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using SpiceMarket_Web.BusinessLogic.Interfaces;
using SpiceMarket_Web.BusinessLogic.Services;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Filters;
using System;
using SpiceMarket_Web.ViewModels;

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

        public ActionResult Index(string searchQuery)
        {
            var products = _productRepository.GetAllProducts();

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
                var user = db.Utilizators
                    .FirstOrDefault(u => u.NumeUtilizator == numeUtilizator && u.Parola == parola);

                if (user != null)
                {
                    HttpCookie usernameCookie = new HttpCookie("username", user.NumeUtilizator);
                    usernameCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(usernameCookie);

                    HttpCookie roleCookie = new HttpCookie("role", user.Rol ?? "utilizator");
                    roleCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(roleCookie);

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

                model.Rol = model.Rol ?? "utilizator";

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
            var username = Session["Utilizator"] as string;

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Autentificare necesară.";
                return RedirectToAction("Autentificare");
            }

            using (var db = new SpiceMarketContext())
            {
                var purchaseHistory = db.Purchases
                    .Where(p => p.Username == username)
                    .OrderByDescending(p => p.PurchaseDate)
                    .ToList();

                return View(purchaseHistory);
            }
        }

        [UserMod]
        public ActionResult Setari()
        {
            return View();
        }

        [AdminMod(Roles = "admin")]
        public ActionResult SecretReport()
        {
            return View();
        }

        public ActionResult Deconectare()
        {
            Session.Clear();
            Session.Abandon();

            if (Request.Cookies != null)
            {
                foreach (var key in Request.Cookies.AllKeys)
                {
                    var cookie = new HttpCookie(key);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }

            TempData["Success"] = "Te-ai deconectat cu succes!";
            return RedirectToAction("Index");
        }

        [UserMod]
        public ActionResult Checkout()
        {
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                TempData["Error"] = "Coșul tău este gol. Adaugă produse înainte de checkout.";
                return RedirectToAction("Cos");
            }

            decimal totalPrice = cartItems.Sum(item => item.Cantitate * item.Pret);
            ViewBag.TotalPrice = totalPrice;

            return View(cartItems);
        }

        [HttpPost]
        [UserMod]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder()
        {
            try
            {
                var cartItems = _cartService.GetCartItems();

                if (cartItems == null || !cartItems.Any())
                {
                    TempData["Error"] = "Coșul tău este gol. Adaugă produse înainte de checkout.";
                    return RedirectToAction("Cos");
                }

                using (var db = new SpiceMarketContext())
                {
                    var username = Session["Utilizator"] as string;

                    if (string.IsNullOrEmpty(username))
                    {
                        TempData["Error"] = "Autentificare necesară.";
                        return RedirectToAction("Autentificare");
                    }

                    var user = db.Utilizators.FirstOrDefault(u => u.NumeUtilizator == username);

                    if (user == null)
                    {
                        TempData["Error"] = "Utilizatorul nu există.";
                        return RedirectToAction("Index");
                    }

                    foreach (var item in cartItems)
                    {
                        var produs = db.Produse.FirstOrDefault(p => p.Nume == item.Nume);

                        if (produs == null || item.Cantitate <= 0)
                        {
                            TempData["Error"] = $"Produs invalid sau cantitate invalidă pentru '{item.Nume}'.";
                            continue; // Skip invalid item
                        }

                        var purchase = new Purchase
                        {
                            ProdusId = produs.Id,
                            ProductName = produs.Nume,
                            CustomerId = user.Id,
                            Username = username,
                            Quantity = (int)item.Cantitate,
                            TotalPrice = produs.Pret * item.Cantitate,
                            PurchaseDate = DateTime.Now
                        };

                        // Ensure all fields are populated before adding to the database
                        if (purchase.ProdusId != null && !string.IsNullOrEmpty(purchase.ProductName) &&
                            purchase.CustomerId != null && purchase.Quantity > 0)
                        {
                            db.Purchases.Add(purchase);
                        }
                        else
                        {
                            TempData["Error"] = $"Date incomplete pentru produsul '{item.Nume}'.";
                        }
                    }

                    db.SaveChanges();
                }

                TempData["Success"] = "Comanda a fost plasată cu succes!";
                return RedirectToAction("Index"); // Redirect to Index after successful checkout
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"A apărut o eroare: {ex.Message}";
                return RedirectToAction("Cos");
            }
        }

        [UserMod]
        public ActionResult Profil()
        {
            var username = Session["Utilizator"] as string;

            if (string.IsNullOrEmpty(username))
            {
                TempData["Error"] = "Trebuie să fii autentificat pentru a vedea profilul.";
                return RedirectToAction("Autentificare");
            }

            using (var db = new SpiceMarketContext())
            {
                var user = db.Utilizators.FirstOrDefault(u => u.NumeUtilizator == username);

                if (user == null)
                {
                    TempData["Error"] = "Utilizatorul nu există.";
                    return RedirectToAction("Index");
                }

                var purchases = db.Purchases
                    .Where(p => p.CustomerId == user.Id)
                    .Join(db.Produse,
                          purchase => purchase.ProdusId,
                          product => product.Id,
                          (purchase, product) => new PurchaseViewModel
                          {
                              Nume = product.Nume,
                              Cantitate = purchase.Quantity,
                              PretPerKg = product.Pret,
                              DataAchizitie = purchase.PurchaseDate
                          })
                    .ToList();

                return View(purchases);
            }
        }

        public ActionResult ProduseList()
        {
            using (var db = new SpiceMarketContext())
            {
                var produse = db.Produse.ToList();
                return View(produse);
            }
        }
    }
}