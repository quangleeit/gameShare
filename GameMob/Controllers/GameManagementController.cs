using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

using GameMob.Models;
using System.IO;

namespace GameMob.Controllers
{
    public class GameManagementController : Controller
    {
        GameStoreDBEntities db = new GameStoreDBEntities();
        // GET: GameManagement
        public PartialViewResult ListGame(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return PartialView(db.Games.OrderByDescending(n=>n.nameGame).ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult CreateNewGame()
        {
            ViewBag.idSeries = new SelectList(db.SeriesGames.ToList().OrderBy(n => n.nameSeries), "idSeries", "nameSeries");
            ViewBag.idNhaSX = new SelectList(db.nhaSanXuats.ToList().OrderBy(n => n.nameNhaSX), "idNhaSX", "nameNhaSX");
            return View();

        }

        [HttpPost]
        public ActionResult CreateNewGame(Game game, HttpPostedFileBase fileUpload)
        {
          
            ViewBag.idSeries = new SelectList(db.SeriesGames.ToList().OrderBy(n => n.nameSeries), "idSeries", "nameSeries");
            ViewBag.idNhaSX = new SelectList(db.nhaSanXuats.ToList().OrderBy(n => n.nameNhaSX), "idNhaSX", "nameNhaSX");

            var fileName = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/IMG/poster/"), fileName);
            if (System.IO.File.Exists(path))
            {
                ViewBag.Notif = "This image is already exist!";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            game.poster = fileUpload.FileName;
            db.Games.Add(game);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            
            Game  game = db.Games.SingleOrDefault(n => n.idGame == id);
            if(game == null){
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.idSeries = new SelectList(db.SeriesGames.ToList().OrderBy(n => n.nameSeries), "idSeries", "nameSeries", game.idSeries);
            ViewBag.idNhaSX = new SelectList(db.nhaSanXuats.ToList().OrderBy(n => n.nameNhaSX), "idNhaSX", "nameNhaSX",game.idNhaSX);

            return View(game);    
        }

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase fileUpdate)
        {
            Game gameEdit = db.Games.SingleOrDefault(n => n.idGame == game.idGame);
            gameEdit.idSeries = game.idSeries;
            gameEdit.idNhaSX = game.idNhaSX;
            gameEdit.nameGame = game.nameGame;
            gameEdit.namSX = game.namSX;
            gameEdit.pointOfCritic = game.pointOfCritic;
            gameEdit.linkDownload = gameEdit.linkDownload;

            if (fileUpdate == null) {
                gameEdit.poster = gameEdit.poster;
            }
            else
            {
                var fileName = Path.GetFileName(fileUpdate.FileName);
                var path = Path.Combine(Server.MapPath("~/IMG/poster/"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Notif = "This image is already exist!";
                }
                else
                {
                    fileUpdate.SaveAs(path);
                }
                game.poster = fileUpdate.FileName;
                gameEdit.poster = game.poster;

            }
                        

            ViewBag.idSeries = new SelectList(db.SeriesGames.ToList().OrderBy(n => n.nameSeries), "idSeries", "nameSeries", game.idSeries);
            ViewBag.idNhaSX = new SelectList(db.nhaSanXuats.ToList().OrderBy(n => n.nameNhaSX), "idNhaSX", "nameNhaSX", game.idNhaSX);
            db.SaveChanges();
            return RedirectToAction("Game", "Admin");
            
        }

       

        
        public ActionResult Delete(int id)
        {
//            Game game = db.Games.SingleOrDefault(n => n.idGame == i

            Game game = (from s in db.Games where s.idGame == id select s).FirstOrDefault<Game>();

            Genre genre = game.Genres.FirstOrDefault<Genre>();

            //genre.Games.Remove(game);
            
           // Game game = db.Genres.
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }    
    }
}