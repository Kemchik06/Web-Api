using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesShopApi.Dtos;
using MoviesShopApi.Models;

namespace MoviesShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private MyApiContext _context;
        private readonly IMapper _mapper;

        public BooksController(MyApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetBooks(string query = null)
        {
            var booksquery = _context.AudioBooks;

            if (!String.IsNullOrWhiteSpace(query))
                booksquery = (DbSet<AudioBook>)booksquery.Where(m => m.Name.Contains(query));

            var moviesDto = booksquery
             .ToList()
             .Select(_mapper.Map<AudioBook, AudioBookDto>);
            return Ok(moviesDto);
        }


        // Get api/movies/1

        [HttpGet("{id}")]
        public ActionResult GetBook(int id)
        {
            var game = _context.AudioBooks.SingleOrDefault(c => c.Id == id);

            if (game == null)
                return NotFound();

            return Ok(_mapper.Map<AudioBook, AudioBookDto>(game));
        }
        [HttpPost]
        public ActionResult CreateBook(AudioBookDto audioBookDto)
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
            var audioBook = new AudioBook();
            audioBook = _mapper.Map(audioBookDto, audioBook);

            audioBookDto.Id = audioBook.Id;

            _context.AudioBooks.Add(audioBook);
            _context.SaveChanges();

            return Ok(audioBookDto);

        }
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, AudioBookDto audioBookDto)
        {
            if (!ModelState.IsValid)
                BadRequest();
            var bookInDb = _context.AudioBooks.SingleOrDefault(c => c.Id == id);

            if (bookInDb == null)
                return NotFound();
            _mapper.Map(audioBookDto, bookInDb);

            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteBooke(int id)
        {
            var bookInDb = _context.AudioBooks.SingleOrDefault(c => c.Id == id);

            if (bookInDb == null)
                return NotFound();
            _context.AudioBooks.Remove(bookInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}