using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.Models;

namespace SpiceMarket_Web.Controllers
{
    public class HomeController : Controller
    {
        private static List<Produs> Produse = new List<Produs>
        {
            new Produs { Nume = "Scorțișoară", CaleImagine = "~/Content/images/scortisoara.png", Pret = 80m },
            new Produs { Nume = "Curcuma", CaleImagine = "~/Content/images/curcuma.png", Pret = 70m },
            new Produs { Nume = "Piper", CaleImagine = "~/Content/images/piper.png", Pret = 90m },
            new Produs { Nume = "Ghimbir", CaleImagine = "~/Content/images/ghimbir.png", Pret = 60m },
            new Produs { Nume = "Paprika", CaleImagine = "~/Content/images/paprika.png", Pret = 75m },
            new Produs { Nume = "Cardamom", CaleImagine = "~/Content/images/cardamom.png", Pret = 120m },
            new Produs { Nume = "Turmeric", CaleImagine = "~/Content/images/turmeric.png", Pret = 70m }
        };

        public ActionResult Index()
        {
            var products = new List<Produs>
    {
        new Produs { Nume = "Scorțișoară", CaleImagine = "~/Content/images/scortisoara.png", Pret = 80m, ImageHeight = 120, ImageWidth = 270},
        new Produs { Nume = "Piper", CaleImagine = "~/Content/images/piper.png", Pret = 90m, ImageHeight = 170, ImageWidth = 210},
        new Produs { Nume = "Curcuma", CaleImagine = "~/Content/images/curcuma.png", Pret = 70m, ImageHeight = 195, ImageWidth = 240 },
        new Produs { Nume = "Ghimbir", CaleImagine = "~/Content/images/ghimbir.png", Pret = 60m, ImageHeight = 140, ImageWidth = 210 },
        new Produs { Nume = "Turmeric", CaleImagine = "~/Content/images/turmeric.png", Pret = 70m, ImageHeight = 140, ImageWidth = 210 },
        new Produs { Nume = "Paprika", CaleImagine = "~/Content/images/paprika.png", Pret = 75m , ImageHeight = 160, ImageWidth = 210 },
        new Produs { Nume = "Cardamom", CaleImagine = "~/Content/images/cardamom.png", Pret = 120m, ImageHeight = 155, ImageWidth = 175 },
    };
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
            var cosCumparaturi = Session["CosCumparaturi"] as List<CartItem> ?? new List<CartItem>();
            return View(cosCumparaturi);
        }

        public ActionResult AdaugaInCos(string produs)
        {
            var cosCumparaturi = Session["CosCumparaturi"] as List<CartItem> ?? new List<CartItem>();
            if (!string.IsNullOrEmpty(produs))
            {
                var existingItem = cosCumparaturi.FirstOrDefault(item => item.Nume == produs);
                if (existingItem != null)
                {
                    existingItem.Cantitate++;
                }
                else
                {
                    cosCumparaturi.Add(new CartItem { Nume = produs, Cantitate = 1 });
                }
                Session["CosCumparaturi"] = cosCumparaturi;
            }
            return RedirectToAction("Index");
        }

        public ActionResult EliminaDinCos(string produs)
        {
            var cosCumparaturi = Session["CosCumparaturi"] as List<CartItem> ?? new List<CartItem>();
            if (!string.IsNullOrEmpty(produs))
            {
                var item = cosCumparaturi.FirstOrDefault(i => i.Nume == produs);
                if (item != null)
                {
                    if (item.Cantitate > 1)
                    {
                        item.Cantitate--;
                    }
                    else
                    {
                        cosCumparaturi.Remove(item);
                    }
                }
                Session["CosCumparaturi"] = cosCumparaturi;
            }
            return RedirectToAction("Cos");
        }
    }
}