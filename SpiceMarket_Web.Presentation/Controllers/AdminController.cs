using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Filters;

namespace SpiceMarket_Web.Controllers
{
    [AdminMod(Roles = "admin,manager")] // Allow Admin and Manager roles
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            using (var db = new SpiceMarketContext())
            {
                var totalProducts = db.Produse.Count();
                var totalUsers = db.Utilizatori.Count();
                var totalSales = 0; // Placeholder
                ViewBag.TotalProducts = totalProducts;
                ViewBag.TotalUsers = totalUsers;
                ViewBag.TotalSales = totalSales;
            }
            return View();
        }

        public ActionResult Products()
        {
            using (var db = new SpiceMarketContext())
            {
                var produse = db.Produse.ToList();
                return View(produse);
            }
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Produs model)
        {
            if (!ModelState.IsValid) return View(model);
            using (var db = new SpiceMarketContext())
            {
                db.Produse.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        public ActionResult EditProduct(int id)
        {
            using (var db = new SpiceMarketContext())
            {
                var produs = db.Produse.Find(id);
                if (produs == null) return HttpNotFound();
                return View(produs);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(Produs model)
        {
            if (!ModelState.IsValid) return View(model);
            using (var db = new SpiceMarketContext())
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        public ActionResult DeleteProduct(int id)
        {
            using (var db = new SpiceMarketContext())
            {
                var produs = db.Produse.Find(id);
                if (produs == null) return HttpNotFound();
                return View(produs);
            }
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductConfirmed(int id)
        {
            using (var db = new SpiceMarketContext())
            {
                var produs = db.Produse.Find(id);
                db.Produse.Remove(produs);
                db.SaveChanges();
            }
            return RedirectToAction("Products");
        }

        public ActionResult Users()
        {
            using (var db = new SpiceMarketContext())
            {
                var users = db.Utilizatori.ToList();
                return View(users);
            }
        }

        [AdminMod(Roles = "admin")] // Restrict ToggleRole to Admin only
        public ActionResult ToggleRole(int id)
        {
            using (var db = new SpiceMarketContext())
            {
                var u = db.Utilizatori.Find(id);
                if (u == null) return HttpNotFound();
                u.Rol = (u.Rol == "admin") ? "utilizator" : "admin";
                db.SaveChanges();
            }
            return RedirectToAction("Users");
        }

        public ActionResult Reports()
        {
            ViewBag.Message = "Aici vor apărea rapoartele de vânzări.";
            return View();
        }
    }
}