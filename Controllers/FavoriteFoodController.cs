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
    public class FavoriteFoodController : ControllerBase
    {

        private readonly ILogger<FavoriteFoodController> _logger;
        private readonly DataContext _context;

        public FavoriteFoodController(ILogger<FavoriteFoodController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("FindByID")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FavoriteFood>>> GetFavoriteFoods(int? id = null)
        {

            if (id == null || id == 0)
            {
                return await _context.FavoriteFoods.Take(5).ToListAsync();
            }

            return await _context.FavoriteFoods.ToListAsync();
        }
        [HttpGet("ShowAll")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteFood>> GetFavoriteFood(int id)
        {
            var food = await _context.FavoriteFoods.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }
            return food;
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutFavoriteFood(int id, FavoriteFood food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteFoodExists(id))
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

        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteFood>> DeleteFavoriteFood(int id)
        {
            var food = await _context.FavoriteFoods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.FavoriteFoods.Remove(food);
            await _context.SaveChangesAsync();

            return food;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FavoriteFood>> PostFavoriteFood(FavoriteFood food)
        {
            _context.FavoriteFoods.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavoriteFood", new { id = food.Id }, food);
        }
            private bool FavoriteFoodExists(int id)
        {
            return _context.FavoriteFoods.Any(e => e.Id == id);
        }
    }
}