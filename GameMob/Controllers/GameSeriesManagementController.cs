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
    public class GameSeriesManagementController : Controller
    {
        GameStoreDBEntities db = new GameStoreDBEntities();
        // GET: GameManagement
        public PartialViewResult ListGameSeries(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return PartialView(db.SeriesGames.OrderByDescending(n => n.nameSeries).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult CreateNewGameSeries()
        {

            return View();

        }

        [HttpPost]
        public ActionResult CreateNewGameSeries(SeriesGame series, HttpPostedFileBase fileUpload)
        {


            var fileName = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/IMG/imgSeries/"), fileName);
            if (System.IO.File.Exists(path))
            {
                ViewBag.Notif = "This image is already exist!";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            series.imgSeries = fileUpload.FileName;
            db.SeriesGames.Add(series);
            db.SaveChanges();
            return RedirectToAction("GameSeries", "Admin");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            SeriesGame series = db.SeriesGames.SingleOrDefault(n => n.idSeries == id);
            if (series == null)
            {
                Response.StatusCode = 404;
                return null;
            }


            return View(series);
        }

        [HttpPost]
        public ActionResult Edit(SeriesGame series, HttpPostedFileBase fileUpdate)
        {
            SeriesGame seriesEdit = db.SeriesGames.SingleOrDefault(n => n.idSeries == series.idSeries);

            seriesEdit.nameSeries = series.nameSeries;

            if (fileUpdate == null)
            {

            }
            else
            {
                var fileName = Path.GetFileName(fileUpdate.FileName);
                var path = Path.Combine(Server.MapPath("~/IMG/imgSeries/"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Notif = "This image is already exist!";
                }
                else
                {
                    fileUpdate.SaveAs(path);
                }
                series.imgSeries = fileUpdate.FileName;
                seriesEdit.imgSeries = series.imgSeries;
            }


            db.SaveChanges();
            return RedirectToAction("GameSeries", "Admin");

        }


        public ActionResult Delete(int? id)
        {
            SeriesGame series = db.SeriesGames.SingleOrDefault(n => n.idSeries == id);
            var game = db.Games.Where(n => n.idSeries == id).ToList();
            if (series != null) {
                for (int i = 0; i < game.Count(); i++)
                {
                    game.ElementAt(i).idSeries = null;
                }
            db.SeriesGames.Remove(series);
            db.SaveChanges();
            }

            return RedirectToAction("GameSeries", "Admin");
        }
    }
}