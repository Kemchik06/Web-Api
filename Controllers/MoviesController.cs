using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesShopApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;
using MoviesShopApi;
using MoviesShopApi.Dtos;

namespace MoviesShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private MyApiContext _context;
        private readonly IMapper _mapper;

        public MoviesController(MyApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies;
            var movie = _context.Movies.ToList();

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery =(DbSet<Movie>) moviesQuery.Where(m => m.Name.Contains(query));

            var moviesDto = moviesQuery
             .ToList()
             .Select(_mapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        }


        // Get api/movies/1

        [HttpGet("{id}")]
        public ActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }
        [HttpPost]
        public ActionResult CreateMovie(MovieDto movieDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            //var movie = new Movie
            //{
            //    Name = movieDto.Name,
            //    GenreId = movieDto.GenreId,
            //    DateAdded = DateTime.Now,
            //    ReleaseDate = movieDto.ReleaseDate,
            //    NumberInStock = movieDto.NumberInStock

            //};
            var movie = new Movie();
            movie = _mapper.Map(movieDto,movie);


            movie.DateAdded = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;

            return Ok(movieDto);

        }
        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                BadRequest();
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();
            _mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                return NotFound();
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }

    }
}