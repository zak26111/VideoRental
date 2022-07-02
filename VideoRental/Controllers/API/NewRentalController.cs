using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoRental.Dtos;
using VideoRental.Models;

namespace VideoRental.Controllers.API
{
    public class NewRentalController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public NewRentalController()
        {
            _context = new ApplicationDbContext();            
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(RentalDto rental)
        {
            var customer = _context.Customers.Single(c => c.Id == rental.CustomerId);

            var movies = _context.Movies.Where(m => rental.MovieIds.Contains(m.Id));

            foreach (var movie in movies)
            {

                if (movie.NumberAvailable ==null)
                {
                    return BadRequest("Movie is not available");                   
                }
                    

                movie.NumberAvailable--;

                var rentalModel = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rentalModel);

            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
