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
    public class WeeksViewController: ControllerBase
    {
        private readonly WPLStatsDbContext _context;
        public WeeksViewController(WPLStatsDbContext context)
        {
            _context = context;
        }

        // GET: api/WeeksView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeeksView>>> GetWeeksView()
        {
            if (_context.WeeksView == null)
            {
                return NotFound();
            }
            return await _context.WeeksView.ToListAsync();
        }
    }
}
