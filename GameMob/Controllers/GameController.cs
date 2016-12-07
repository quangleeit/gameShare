using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameMob.Models;
using PagedList;
using PagedList.Mvc;


namespace GameMob.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        GameStoreDBEntities db = new GameStoreDBEntities();

        public PartialViewResult PartialContentLeft(int? page)
        {

            var listGame = db.Games.OrderByDescending(n => n.pointOfCritic).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            int m = listGame.Count();
            return PartialView(listGame.ToPagedList(pageNumber, pageSize));
        }

        public PartialViewResult PartialContentRight()
        {

            var listGame = db.Games.OrderByDescending(n => n.luotDownLoad).Take(10).ToList();
            
            return PartialView(listGame);
        }


        static int idGamee = 0;
        public ActionResult Details(int? id)
        {   
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            if (game.idSeries == null)
            {
                ViewBag.IdSeries = 0;                
            }
            else {
                ViewBag.IdSeries = 1;
            }
           
            idGamee = (int) game.idGame;
            
            return View(game);
       
        }

        
        public ActionResult PartialRelatedSeries(int? id)
        {
            var gamesOfSeries = db.Games.Where(idGame => idGame.idSeries == id).OrderByDescending(n =>n.namSX).ToList();

            /*var sg = db.SeriesGames.Find(id);
            ViewBag.NameSG = sg.nameSeries;
            */

            ViewBag.ID = idGamee;
            return View(gamesOfSeries);
        }

        public ActionResult Download()
        {
            return View();
        }

        public ActionResult PartialNewGame(int? page)
        {
            var listNewGame = db.Games.Where(year => year.namSX == (DateTime.Now.Year)).OrderByDescending(n => n.luotDownLoad).ToList(); 
            
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return PartialView(listNewGame.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult PartialSlider()
        {
            var game = db.Games.Take(5).OrderBy(y => y.namSX).ToList();
            return PartialView(game);
        }

        public ActionResult GameOnline()
        {
            return View();
        }
        public ActionResult GameOffline()
        {
            return View();
        }

    }
}