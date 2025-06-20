using System.Linq;
using System.Web.Mvc;
using SpiceMarket_Web.Domain.Models;
using SpiceMarket_Web.Presentation.Filters;

namespace SpiceMarket_Web.Controllers
{
    [AdminMod(Roles = "admin,manager")] // Allow Admin and Manager roles
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            // Debugging logs for session state
            var sessionRole = Session["RoleLevel"]?.ToString();
            System.Diagnostics.Debug.WriteLine($"Session Role: {sessionRole}");

            if (string.IsNullOrEmpty(sessionRole))
            {
                return new HttpUnauthorizedResult("Session Role is missing or invalid.");
            }

            using (var db = new SpiceMarketContext())
            {
                var totalProducts = db.Produse.Count();
                var totalUsers = db.Utilizators.Count();
                var totalSales = db.Purchases.Sum(p => p.Quantity); // Calculate total sales
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
                var users = db.Utilizators.ToList();
                return View(users);
            }
        }

        public ActionResult Reports()
        {
            using (var db = new SpiceMarketContext())
            {
                // Fetch detailed reports data
                var purchaseReports = db.Purchases
                    .OrderByDescending(p => p.PurchaseDate)
                    .Select(p => new PurchaseReportModel
                    {
                        ProductName = p.ProductName,
                        Username = p.Username,
                        Quantity = p.Quantity,
                        TotalPrice = p.TotalPrice,
                        PurchaseDate = p.PurchaseDate
                    })
                    .ToList();

                ViewBag.PurchaseReports = purchaseReports;
            }

            return View();
        }
    }

    public class PurchaseReportModel
    {
        public string ProductName { get; set; }
        public string Username { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public System.DateTime PurchaseDate { get; set; }
    }
}