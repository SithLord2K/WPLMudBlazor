using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
    public class PlayerDataController(WPLStatsDbContext context) : ControllerBase
    {
        private readonly WPLStatsDbContext _context = context;

        // GET: api/PlayerData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerData>>> GetPlayerData()
        {
            if (_context.PlayerData == null)
            {
                return NotFound();
            }
            return await _context.PlayerData.ToListAsync();
        }

        // GET: api/PlayerData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<PlayerData>>> GetPlayerData(int id)
        {
            List<PlayerData> playerData = [];
            if (_context.PlayerData == null)
            {
                return NotFound();
            }
            playerData = await _context.PlayerData.Where(x => x.PlayerId == id).ToListAsync();
            if (playerData == null)
            {
                return NotFound();
            }

            return playerData;
        }

        // PUT: api/PlayerData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerData(int id, PlayerData playerData)
        {
            if (id != playerData.PlayerId)
            {
                return BadRequest();
            }

            _context.Entry(playerData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerDataExists(id))
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

        // POST: api/PlayerData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerData>> PostPlayerData(PlayerData playerData)
        {
            if (_context.PlayerData != null)
            {
                if (!PlayerDataExists(playerData.PlayerId))
                {
                    _context.PlayerData.Add(playerData);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetPlayerData", new { id = playerData.PlayerId }, playerData);
                }
                else
                {
                    _context.PlayerData.Update(playerData);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetPlayerData", new { id = playerData.PlayerId }, playerData);
                }
            }
            return NotFound();
        }

        // DELETE: api/PlayerData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerData(int id)
        {
            if (_context.PlayerData == null)
            {
                return NotFound();
            }
            var playerData = await _context.PlayerData.FindAsync(id);
            if (playerData == null)
            {
                return NotFound();
            }

            _context.PlayerData.Remove(playerData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerDataExists(int id)
        {
            return (_context.PlayerData?.Any(e => e.PlayerId == id)).GetValueOrDefault();
        }
    }
}
