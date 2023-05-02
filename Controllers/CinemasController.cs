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

        // GET: cinemas/1/showtimes
        // openingHour=13 closingHour=23 duration=60
        // [(13, 0), (14, 15), (15, 30), (16, 45), (18, 0), (19, 15), (20, 30), (21, 45)]
        [HttpGet("{id}/showtimes")]
        public async Task<ActionResult<IEnumerable<(int, int)[]>>> GetShowtimes(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }
            Tuple<int, int>[] showtimes = CalculateShowtimes(cinema.OpeningHour, cinema.ClosingHour, cinema.ShowDuration);
            return Ok(new { showtimes = showtimes });
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

        // PUT: cinemas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCinema(int id, Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return BadRequest();
            }

            var cinemaToUpdate = await _context.Cinemas.FindAsync(id);
            if (cinemaToUpdate == null)
            {
                return NotFound();
            }
            // TODO: check if required fields are provided
            cinemaToUpdate.Name = cinema.Name;
            cinemaToUpdate.OpeningHour = cinema.OpeningHour;
            cinemaToUpdate.ClosingHour = cinema.ClosingHour;
            cinemaToUpdate.ShowDuration = cinema.ShowDuration;

            _context.Cinemas.Update(cinemaToUpdate);
            await _context.SaveChangesAsync();

            return Ok(cinemaToUpdate);
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

        private Tuple<int, int>[] CalculateShowtimes(int openingHour, int closingHour, int duration)
        {
            List<Tuple<int, int>> times = new List<Tuple<int, int>>();
            int startingHour = openingHour;
            int startingMinute = 0;
            int minutesToNextShow = duration + 15;
            int hoursToAdd = 0;
            times.Add(Tuple.Create(startingHour, startingMinute));
            while (true)
            {
                hoursToAdd = (startingMinute + minutesToNextShow) / 60;
                startingHour += hoursToAdd;
                startingMinute = (startingMinute + minutesToNextShow) % 60;

                if (closingHour >= 0 & closingHour <= 6)
                {
                    if (startingHour >= closingHour + 24)
                    {
                        break;
                    }
                }
                else
                {
                    if (startingHour >= closingHour)
                    {
                        break;
                    }
                }

                times.Add(Tuple.Create((startingHour >= 24 ? startingHour - 24 : startingHour), startingMinute));

            }
            Tuple<int, int>[] showtimes = times.ToArray();
            return showtimes;
        }
    }
}