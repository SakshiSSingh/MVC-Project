using MVCUserPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

using System.Web.Mvc;

namespace MVCUserPortal.Controllers
{

    public class CartController : Controller
    {
        private SopraDbContext db = new SopraDbContext();

        // GET: Cart
        public ActionResult Index()

        {
            if (Session["cart"] == null)
                return Content("Your Cart is Empty");
            else
            return View();
        }
        public ActionResult Buy(int id)
        {

            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = db.Products.Find(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = db.Products.Find(id), Quantity = 1 });
                }
                Session["cart"] = cart;

            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId.Equals(id))
                    return i;

            }
            return -1;
        }
    }
}