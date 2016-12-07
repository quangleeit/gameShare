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
    public class SearchController : Controller
    {
        // GET: Search
        GameStoreDBEntities db = new GameStoreDBEntities();
        [HttpGet]
        public ActionResult SearchResult(string currentFilter, string searchString, int? page)
        {
            //string gkeyword = f["txtSearch"].ToString();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var games = from g in db.Games select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(n => n.nameGame.Contains(searchString));
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            if (games.ToList().Count == 0)
            {
                ViewBag.Notif = "Không tìm thấy Game";
            }
            else ViewBag.Notif = "Tìm thấy " + games.ToList().Count + " kết quả";

            return View(games.OrderBy(y => y.namSX).ToPagedList(pageNumber, pageSize));

        }

    }
}