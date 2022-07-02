using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoRental.Models;
using System.Data.Entity;

namespace VideoRental.Controllers.API
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/Movies
        [HttpGet]
        public IHttpActionResult GetAllMovies(string query = null)
        {
            //return Ok(_context.Movies.Include(c=>c.Genre).ToList().Select(Mapper.Map<Movie,MovieDto>)); //200

            var movieQuery = _context.Movies.Include(c => c.Genre);
            if (!string.IsNullOrWhiteSpace(query))
                movieQuery = movieQuery.Where(c => c.Name.Contains(query));

            var movieListDto = movieQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieListDto);

        }


        //Get /api/Movies/1
        [HttpGet]
        public IHttpActionResult GetMovie(int Id)
        {
            var movieFromDb = _context.Movies.FirstOrDefault(c => c.Id == Id);
            if (movieFromDb == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movieFromDb));//200
        }

        //POST /api/Movies - /api/Movies/1
        [HttpPost]
        public IHttpActionResult CreateCustomer(MovieDto movieDto)
        {
            if (!ModelState.IsValid || movieDto == null)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto); //201
        }

        //PUT /api/Movies/1
        [HttpPut]
        public void UpdateCustomer(int Id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (Id != movieDto.Id || movieDto == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieFromDb = _context.Movies.FirstOrDefault(c => c.Id == Id);

            if (movieFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDto, movieFromDb);

            //customerFromDb.Name = customer.Name;
            //customerFromDb.MemberShipTypeId = customer.MemberShipTypeId;
            //customerFromDb.DateOfBirth = customer.DateOfBirth;
            //customerFromDb.IsSubscribeToNewsLetter = customer.IsSubscribeToNewsLetter;
            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteCustomer(int Id)
        {
            var movieFromDb = _context.Movies.FirstOrDefault(c => c.Id == Id);

            if (movieFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieFromDb);
            _context.SaveChanges();
        }
    }
}
