using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPLBlazor.API.Models;
using WPLBlazor.API.Filters;
using WPLBlazor.API.Data;

namespace WPLBlazor.API.Controllers
{
#if !DEBUG
    [APIKey]
#endif
    [Route("/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly WPLStatsDbContext _context;

        public ScheduleController(WPLStatsDbContext context)
        {
            _context = context;
        }

        // GET: api_v2/Schedule
        [HttpGet]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]

        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedule()
        {
            if (_context.Schedule == null)
            {
                return NotFound();
            }
            return await _context.Schedule.ToListAsync();
        }

        // GET: api_v2/Schedule/1
        [HttpGet("{id}")]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<Schedule>> GetScheduleWeek(int id)
        {
            if (_context.Schedule == null)
            {
                return NotFound();
            }
            var scheduleWeek = await _context.Schedule.FindAsync(id);

            if (scheduleWeek == null)
            {
                return NotFound();
            }

            return scheduleWeek;
        }

        // POST: api_v2/Schedule
        [HttpPost]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]

        public async Task<ActionResult<Schedule>> SaveSchedule(Schedule schedule)
        {
            if (_context.Players == null)
            {
                return Problem("Schedule is null.");
            }
            _context.Schedule.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShedule", new { id = schedule.Week_Id }, schedule);
        }


    }

}
