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
        public ActionResult Autentificare(string numeUtilizator, string parola)
        {
            if (numeUtilizator == "admin" && parola == "parola123")
            {
                Session["Utilizator"] = numeUtilizator;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.MesajEroare = "Nume utilizator sau parolă incorecte.";
                return View();
            }
        }

        public ActionResult Cos()
        {
            var cartItems = _cartService.GetCartItems();
            ViewBag.Total = _cartService.CalculateTotal();
            return View(cartItems);
        }

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

        public ActionResult EliminaDinCos(string produs)
        {
            _cartService.RemoveFromCart(produs);
            return RedirectToAction("Cos");
        }
    }
}