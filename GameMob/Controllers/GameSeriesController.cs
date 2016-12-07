using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMob.Models;
using System.Net;
using PagedList;
using PagedList.Mvc;


namespace GameMob.Controllers
{
    public class GameSeriesController : Controller
    {
        // GET: GameSeries

        GameStoreDBEntities db = new GameStoreDBEntities();
        public ActionResult Index(int? page)
        {   
            var listGameSeries = db.SeriesGames.Take(20).ToList();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(listGameSeries.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int? id)
        {
            var gamesOfSeries = db.Games.Where(idGame => idGame.idSeries == id).OrderByDescending(y => y.namSX).ToList();            
            
            var sg = db.SeriesGames.Find(id);
            ViewBag.NameSG = sg.nameSeries;
           
            return View(gamesOfSeries);
        }

    }
}