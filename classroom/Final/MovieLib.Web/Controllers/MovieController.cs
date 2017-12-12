/*
 * ITSE 1430
 * Sample implementation
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MovieLib.Data.Sql;
using MovieLib.Web.Models;

namespace MovieLib.Web.Controllers
{
    public class MovieController : Controller
    {
        #region Construction

        public MovieController () : this(GetDefaultStore())
        { }

        public MovieController ( IMovieDatabase database )
        {
            _database = database;
        }
        #endregion

        public ActionResult Add ()
        {
            return View(new MovieViewModel());
        }

        [HttpPost]
        public ActionResult Add ( MovieViewModel model )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _database.Add(model.ToMovie());

                    return RedirectToAction("List");
                } catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                };                
            };
            
            return View(model);
        }

        public ActionResult Delete ( int id )
        {
            var movie = _database.Get(id);
            if (movie == null)
                return HttpNotFound();

            return View(movie.ToViewModel());
        }

        [HttpPost]
        public ActionResult Delete ( MovieViewModel model )
        {
            _database.Remove(model.Id);

            return RedirectToAction("List");
        }

        public ActionResult Edit ( int id )
        {
            var movie = _database.Get(id);
            if (movie == null)
                return HttpNotFound();
            
            return View(movie.ToViewModel());
        }

        [HttpPost]
        public ActionResult Edit ( MovieViewModel model )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _database.Update(model.ToMovie());

                    return RedirectToAction("List");
                } catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                };
            };

            return View(model);
        }

        public ActionResult List()
        {
            var movies = from m in _database.GetAll()
                         select m;
            
            return View(movies.ToViewModel());
        }

        #region Private Members

        private static IMovieDatabase GetDefaultStore ()
        {
            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"].ConnectionString;

            return new SqlMovieDatabase(connString);
        }

        private readonly IMovieDatabase _database;
        #endregion
    }
}