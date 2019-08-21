using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCUserPortal.Models;

namespace MVCUserPortal.Controllers
{
    public class ProductsController : Controller
    {
        private SopraDbContext db = new SopraDbContext();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}
