using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinemaAPI.Models;

namespace cinemaAPI.Controllers
{
    [Route("cinemas")]
    [ApiController]
    public class CinemasController : ControllerBase
    {
        private readonly CinemaContext _context;

        public CinemasController(CinemaContext context)
        {
            _context = context;
        }

        // GET: cinemas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cinema>>> GetCinemas()
        {
            if (_context.Cinemas == null)
            {
                return NotFound();
            }
            return await _context.Cinemas.ToListAsync();
        }

        // GET: cinemas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cinema>> GetCinema(int id)
        {
            if (_context.Cinemas == null)
            {
                return NotFound();
            }
            var cinema = await _context.Cinemas.FindAsync(id);

            if (cinema == null)
            {
                return NotFound();
            }

            return cinema;
        }

        // POST: cinemas
        [HttpPost]
        public async Task<ActionResult<Cinema>> PostCinema(Cinema cinema)
        {
            if (_context.Cinemas == null)
            {
                return Problem("Entity set 'CinemaContext.Cinemas'  is null.");
            }
            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCinema", new { id = cinema.Id }, cinema);
        }

        // DELETE cinemas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            if (_context.Cinemas == null)
            {
                return NotFound();
            }
            var cinema = await _context.Cinemas.FindAsync(id);
            
            if (cinema == null)
            {
                return NotFound();
            }

            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}