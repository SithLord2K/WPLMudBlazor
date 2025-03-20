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
    public class ScheduleController(WPLStatsDbContext context) : ControllerBase
    {
        private readonly WPLStatsDbContext _context = context;

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
            int? tmpId = 0;
            if (_context.Schedule == null)
            {
                return Problem("Schedule is null.");
            }
            if(schedule.Week_Id == 0)
            {
                tmpId = schedule.Week_Id_Playoff;
            }
            
            if (!ScheduleExists(tmpId))
            {
                _context.Schedule.Add(schedule);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Schedule.Update(schedule);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetShedule", new { id = schedule.Id }, schedule);
        }

        private bool ScheduleExists(int? id)
        {
            return (_context.Schedule?.Any(e => e.Week_Id == id || e.Week_Id_Playoff == id)).GetValueOrDefault();
        }
    }

}
