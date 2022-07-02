using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoRental.Models;
using System.Data.Entity;
using VideoRental.ViewModels;
using VideoRental.Utility;

namespace VideoRental.Controllers
{
    
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movie
        public ActionResult Index()
        {
            //var movieList = _context.Movies.Include(c => c.Genre).ToList();
            //return View(movieList);            
            if (User.IsInRole(SD.Role_Admin))
                return View("List");
            return View("ReadOnly");
            //return View();
        }

        [Authorize(Roles =SD.Role_Admin)]
        public ActionResult New()
        {
            var movieFormView = new MovieFormVM()
            {
                Movie = new Movie(),
                Genres = _context.Genres.ToList()
            };
           
            return View("MovieForm", movieFormView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Save(Movie movie)
        {
            if (movie == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
            {
                var movieFormView = new MovieFormVM()
                {
                    Movie = new Movie(),
                    Genres = GetGenreList()
                };
                return View("MovieForm", movieFormView);
            }

            if (movie.Id == 0)
            {
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
                
            else
            {
                var movieInDb = _context.Movies.FirstOrDefault(c => c.Id == movie.Id);
                if (movieInDb == null)
                    return HttpNotFound();
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                return HttpNotFound();
            var movieInDb = _context.Movies.FirstOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return HttpNotFound();
            var movieFormView = new MovieFormVM()
            {
                Movie = movieInDb,
                Genres = GetGenreList()
            };

            return View("MovieForm", movieFormView);
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Details(int id)
        {
            var movieInDb = _context.Movies.Include(c => c.Genre).FirstOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return HttpNotFound();
            return View(movieInDb);
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.FirstOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return HttpNotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<Genre> GetGenreList()
        {
           return _context.Genres.ToList();
        }
    }
}