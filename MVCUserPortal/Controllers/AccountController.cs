using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCUserPortal.Models;

namespace MVCUserPortal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account        
        private SopraDbContext db = new SopraDbContext();
        private UserTable userTable;

      
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            UserTable user = db.UserTables.Where(i => i.Name.Equals(username)).SingleOrDefault();

            if (user != null)
            {
                if (user.Username == username && user.Password == password)
                    Session["Username"] = username;
                else
                    return RedirectToAction("Register");

            }
            else
            {
                return RedirectToAction("Register");
            }
            return RedirectToAction("Index", "Products");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name, Contact, Address, Username, Password")] UserTable userTable)
        {
            if (ModelState.IsValid)
            {
                db.UserTables.Add(userTable);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(userTable);
        }
        [HttpGet]

        public ActionResult Logout()
        {
            Session.Remove("Username");
            return RedirectToAction("Index", "Products");

        }
        public ActionResult CheckOut(string username)
        {
            UserTable user = db.UserTables.Where(i => i.Name.Equals(username)).SingleOrDefault();
            if (user != null)
            {
                ViewBag.user = user;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        public ActionResult Invoice()
        {
            return View();
        }
    }
}

