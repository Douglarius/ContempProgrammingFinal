using ContempProgrammingFinal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ContempProgrammingFinal
{
    [ApiController]
    [Route("[controller]")]
    public class FavoriteMovieController : ControllerBase
    {
        private readonly ILogger<FavoriteMovieController> _logger;
        private readonly DataContext _context;

        public FavoriteMovieController(ILogger<FavoriteMovieController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FavoriteMovie>>> GetFavoriteMovies(int? id = null)
        {

            if (id == null || id == 0)
            {
                return await _context.FavoriteMovies.Take(5).ToListAsync();
            }

            return await _context.FavoriteMovies.ToListAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteMovie>> GetFavoriteMovie(int id)
        {
            var movie = await _context.FavoriteMovies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutFavoriteMovie(int id, FavoriteMovie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteMovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteMovie>> DeleteFavoriteMovie(int id)
        {
            var movie = await _context.FavoriteMovies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.FavoriteMovies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteMovie>> PostFavoriteMovie(FavoriteMovie movie)
        {
            _context.FavoriteMovies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoriteMovie", new { id = movie.Id }, movie);
        }
            private bool FavoriteMovieExists(int id)
        {
            return _context.FavoriteMovies.Any(e => e.Id == id);
        }
    }
}