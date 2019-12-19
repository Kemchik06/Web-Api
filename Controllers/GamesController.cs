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
    public class GamesController : ControllerBase
    {

        private MyApiContext _context;
        private readonly IMapper _mapper;

        public GamesController(MyApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetGames(string query = null)
        {
            var gamesquery = _context.Games;
            var movie = _context.Games.ToList();

            if (!String.IsNullOrWhiteSpace(query))
                gamesquery = (DbSet<Game>)gamesquery.Where(m => m.Name.Contains(query));

            var moviesDto = gamesquery
             .ToList()
             .Select(_mapper.Map<Game, GameDto>);
            return Ok(moviesDto);
        }


        // Get api/movies/1

        [HttpGet("{id}")]
        public ActionResult GetGame(int id)
        {
            var game = _context.Games.SingleOrDefault(c => c.Id == id);

            if (game == null)
                return NotFound();

            return Ok(_mapper.Map<Game, GameDto>(game));
        }
        [HttpPost]
        public ActionResult CreateGame(GameDto gameDto)
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
            var game = new Game();
            game = _mapper.Map(gameDto, game);

            gameDto.Id = game.Id;

            _context.Games.Add(game);
            _context.SaveChanges();

            return Ok(gameDto);

        }
        [HttpPut("{id}")]
        public ActionResult UpdateGame(int id, GameDto gameDto)
        {
            if (!ModelState.IsValid)
                BadRequest();
            var gameInDb = _context.Games.SingleOrDefault(c => c.Id == id);

            if (gameInDb == null)
                return NotFound();
            _mapper.Map(gameDto, gameInDb);

            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            var gameinDb = _context.Games.SingleOrDefault(c => c.Id == id);

            if (gameinDb == null)
                return NotFound();
            _context.Games.Remove(gameinDb);
            _context.SaveChanges();
            return Ok();
        }

    }
}
