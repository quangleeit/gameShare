using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameMob.Models;

namespace GameMob.Controllers
{
    public class CategoryController : Controller
    {
        GameStoreDBEntities db = new GameStoreDBEntities();
        public ActionResult PartialCategoriesLeft()
        {
            var listCategory = db.Genres.ToList();
            return PartialView(listCategory);
        }

        public ActionResult CategoryDetails(int? id)
        {
            //var listGame = new List<Game>();
            //var genres = db.Genres;
            //for (int i = 0; i < db.Genres.Count(); i++) {
            //    var game = db.Games.Where(x => x.Genres.ElementAt(i).idGenre == id);

            //    listGame.Equals(game);  
            
            //}
            var gen = db.Genres.Find(id);
            ViewBag.NameGen = gen.nameGenre;

            var listGame = db.Games.Where(g=>g.Genres.Any(gn => gn.idGenre==id)).OrderByDescending(y => y.namSX).ToList();
            //foreach(var gen in genres){
            //    listGame.Add(new Game
            //    {
                    
            //    });
            //}
            
            

            return View(listGame);
        }
    }
}