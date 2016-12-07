using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMob.Models;
using PagedList;
using PagedList.Mvc;


namespace GameMob.Controllers
{
    public class HomeController : Controller
    {
        GameStoreDBEntities db = new GameStoreDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewGame()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        } 
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQs()
        {
            return View();

        }

        
    }
}