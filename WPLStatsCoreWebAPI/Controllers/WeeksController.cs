using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPLBlazor.API.Data;
using WPLBlazor.API.Filters;
using WPLBlazor.API.Models;

namespace WPLBlazor.API.Controllers
{
#if !DEBUG
    [APIKey]
#endif
    [Route("/[controller]")]
    [ApiController]
    public class WeeksController : ControllerBase
    {
        private readonly WPLStatsDbContext _context;

        public WeeksController(WPLStatsDbContext context)
        {
            _context = context;
        }

        // GET: api/Weeks
        [HttpGet]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<IEnumerable<Week>>> GetWeeks()
        {
            if (_context.Weeks == null)
            {
                return NotFound();
            }
            return await _context.Weeks.ToListAsync();
        }

        // GET: api/Weeks/5
        [HttpGet("{Week_Id}")]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<Week>> GetWeek(int Week_Id)
        {
            if (_context.Weeks == null)
            {
                return NotFound();
            }
            var week = await _context.Weeks.FindAsync(Week_Id);

            if (week == null)
            {
                return NotFound();
            }

            return week;
        }

        // PUT: api/Weeks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Week_Id}")]
        public async Task<IActionResult> PutWeek(int Week_Id, Week week)
        {
            if (Week_Id != week.WeekNumber)
            {
                return BadRequest();
            }

            _context.Entry(week).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeekExists(Week_Id))
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

        // POST: api/Weeks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Week>> PostWeek(Week week)
        {
            if (_context.Weeks == null)
            {
                return Problem("Entity set 'WPLStatsDbContext.Weeks'  is null.");
            }
            if (!WeekExists(week.WeekNumber))
            {
                _context.Weeks.Add(week);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Update(week);
                await _context.SaveChangesAsync();
            }
            return week;
        }

        // DELETE: api/Weeks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeek(int id)
        {
            if (_context.Weeks == null)
            {
                return NotFound();
            }
            var week = await _context.Weeks.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }

            _context.Weeks.Remove(week);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeekExists(int id)
        {
            return (_context.Weeks?.Any(e => e.WeekNumber == id)).GetValueOrDefault();
        }
    }
}
