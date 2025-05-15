using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Filters; // your AdminModAttribute

namespace SpiceMarket_Web.Controllers
{
    [AdminMod] // entire controller locked to Admins
    public class AdminController : Controller
    {
        // GET: /Admin/Dashboard
        public ActionResult Dashboard()
        {
            using (var db = new SpiceMarketContext())
            {
                var totalProducts = db.Produse.Count();
                var totalUsers = db.Utilizatori.Count();
                // For sales, if you had an Orders table you would sum that
                var totalSales = 0;
                ViewBag.TotalProducts = totalProducts;
                ViewBag.TotalUsers = totalUsers;
                ViewBag.TotalSales = totalSales;
            }
            return View();
        }

        // GET: /Admin/Products
        public ActionResult Products()
        {
            using (var db = new SpiceMarketContext())
            {
                var produse = db.Produse.ToList();
                return View(produse);
            }
        }

        // GET: /Admin/CreateProduct
        public ActionResult CreateProduct()
        {
            return View();
        }
        // POST: /Admin/CreateProduct
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

        // GET: /Admin/EditProduct/5
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

        // GET: /Admin/DeleteProduct/5
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

        // GET: /Admin/Users
        public ActionResult Users()
        {
            using (var db = new SpiceMarketContext())
            {
                var users = db.Utilizatori.ToList();
                return View(users);
            }
        }

        // GET: /Admin/ToggleRole/5
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

        // GET: /Admin/Reports
        public ActionResult Reports()
        {
            // stub: in real life you'd query Orders
            ViewBag.Message = "Aici vor apărea rapoartele de vânzări.";
            return View();
        }
    }
}
