/* ITSE1430
 * Kevin Belknap
 * December 8, 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieLib.Data.Sql;
using MovieLib.Web.Models;
using System.ComponentModel;
using System.Configuration;

namespace MovieLib.Web.Controllers
{   [DescriptionAttribute("Handles Movie requests")]
    public class MovieController : Controller
    {
        public MovieController() : this(GetDatabase())
        {
        }

        public MovieController(IMovieDatabase database)
        {
            _database = database;
        }

        // GET: Movie

        public ActionResult List()
        {
            var movies = _database.GetAll(); 

            return View(movies.ToModel());
        }

        //Get for Adding a Movie
        public ActionResult Add()
        {
            MovieViewModel model = new MovieViewModel();

            return View(model);
        }

        //Post for Adding a Movie
        [HttpPost]
        public ActionResult Add(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    _database.Add(model.ToDomain());

                    return RedirectToAction("List");
                }   catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                };
            };

            return View(model);
        }

        //Get for Edting a Movie
        public ActionResult Edit(int id)
        {
            var movie = _database.Get(id);
            if (movie == null)
                return HttpNotFound();

            return View(movie.ToModel());
        }

        //Post for Edting a Movie
        [HttpPost]
        public ActionResult Edit(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _database.Update(model.ToDomain());

                    return RedirectToAction("List");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                };
            };
            return View(model);
        }

        //Get for Deleting a Movie
        public ActionResult Delete(int id)
        {
            var movie = _database.Get(id);
            if (movie == null)
                return HttpNotFound();

            return View(movie.ToModel());
        }

        //Post for Deleting a Movie
        [HttpPost]
        public ActionResult Delete(MovieViewModel model)
        {
            try
            {
                _database.Remove(model.Id);

                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            };
            
            return View(model);
        }

        //Establish Database connection
        private static IMovieDatabase GetDatabase()
        {
            var connstring = ConfigurationManager.ConnectionStrings["MovieDatabase"];

            return new SqlMovieDatabase(connstring.ConnectionString);
        }
        private readonly IMovieDatabase _database;
    }
}