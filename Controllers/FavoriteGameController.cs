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
    public class FavoriteGameController : ControllerBase
    {

        private readonly ILogger<FavoriteGameController> _logger;
        private readonly DataContext _context;

        public FavoriteGameController(ILogger<FavoriteGameController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FavoriteGame>>> GetFavoriteGames(int? id = null)
        {

            if (id == null || id == 0)
            {
                return await _context.FavoriteGames.Take(5).ToListAsync();
            }

            return await _context.FavoriteGames.ToListAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteGame>> GetFavoriteGame(int id)
        {
            var game = await _context.FavoriteGames.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }
            return game;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutFavoriteGame(int id, FavoriteGame game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteGameExists(id))
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
        public async Task<ActionResult<FavoriteGame>> DeleteFavoriteGame(int id)
        {
            var game = await _context.FavoriteGames.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.FavoriteGames.Remove(game);
            await _context.SaveChangesAsync();

            return game;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteGame>> PostFavoriteGame(FavoriteGame game)
        {
            _context.FavoriteGames.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoriteGame", new { id = game.Id }, game);
        }
            private bool FavoriteGameExists(int id)
        {
            return _context.FavoriteGames.Any(e => e.Id == id);
        }
    }
}