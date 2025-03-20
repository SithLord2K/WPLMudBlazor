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
    public class PlayersViewController(WPLStatsDbContext context) : ControllerBase
    {
        private readonly WPLStatsDbContext _context = context;

        // GET: api/PlayersView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayersView>>> GetPlayersView()
        {
            if (_context.PlayersView == null)
            {
                return NotFound();
            }
            return await _context.PlayersView.ToListAsync();
        }
    }
}
