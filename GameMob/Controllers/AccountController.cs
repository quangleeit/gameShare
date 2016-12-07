using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMob.Models;
using System.Web.Security;

namespace GameMob.Controllers
{
    public class AccountController : Controller
    {
        GameStoreDBEntities db = new GameStoreDBEntities();
        // GET: Account
        public ActionResult Register()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Email, UserName, FullName, Password")]Admin account)
        {
            if (ModelState.IsValid)
            {
                var checkUserName = db.Admins.Any(s => s.UserName == account.UserName);
                var checkEmail = db.Admins.Any(s => s.Email == account.Email);
                if (checkUserName)
                {
                    ModelState.AddModelError("", "This account has been registed!");

                }
                if (checkEmail)
                {
                    ModelState.AddModelError("", "This email has been registed!");
                }
                if (checkUserName == true || checkEmail == true)
                {
                    return View("Register");
                }
                else
                {
                    db.Admins.Add(account);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(account.UserName, false);
                    return Redirect("Login");
                    
                }
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel account)
        {
            if (ModelState.IsValid)
            {
                var checkLogin = db.Admins.Any(s => s.UserName == account.UserName && s.Password == account.Password);

                if (checkLogin)
                {
                    FormsAuthentication.SetAuthCookie(account.UserName, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password, please try again!");
                    return View("Login", account);
                }
            }
            return Redirect("/");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}