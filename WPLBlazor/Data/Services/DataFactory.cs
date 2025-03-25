using Microsoft.EntityFrameworkCore;
using WPLBlazor.Data.Models;

namespace WPLBlazor.Data.Services
{
    public class DataFactory(IDbContextFactory<WPLStatsDBContext> contextFactory)
    {
        private readonly IDbContextFactory<WPLStatsDBContext> _contextFactory = contextFactory;

        public async Task<List<Player>> GetPlayers()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.Players.ToListAsync();
            }
        }
        public async Task<List<PlayerDatum>> GetPlayerData()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.PlayerData.ToListAsync();
            }
        }
        public async Task<List<PlayersView>> GetPlayersView()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.PlayersViews.ToListAsync();
            }
        }
        public async Task<Player?> GetSinglePlayer(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.Players.FindAsync(id);
            }
        }
        public async Task<List<PlayerDatum>> GetSinglePlayerData(int playerId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.PlayerData.Where(e => e.PlayerId == playerId).ToListAsync();
            }
        }
        public Task<List<PlayerDatum>> GetAllPlayerData()
        {
            using(var _context = _contextFactory.CreateDbContext())
            {
                return _context.PlayerData.ToListAsync();
            }
        }
        public async Task<List<PlayerData>> GetPlayerData_All()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var pData = await _context.PlayerData.ToListAsync();
                var players = await _context.Players.ToListAsync();
                List<PlayerData> playerData = new();
                foreach (var item in pData)
                {
                    PlayerData player = new();
                    player.PlayerId = item.PlayerId;
                    player.GamesWon = item.GamesWon;
                    player.GamesLost = item.GamesLost;
                    player.WeekNumber = item.WeekNumber;
                    player.GamesPlayed = item.GamesPlayed;
                    if (item.GamesPlayed != 0)
                    {
                        player.Average = (decimal)item.GamesWon / item.GamesPlayed;
                    }
                    else
                    {
                        player.Average = 0;
                    }
                    player.TeamId = players.Where(e => e.Id == item.PlayerId).Select(e => e.TeamId).FirstOrDefault();
                    playerData.Add(player);
                }
                return playerData;
            }
        }
        public async Task<bool> AddPlayer(Player player)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> SavePlayerData(PlayerDatum playerData)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                _context.PlayerData.Add(playerData);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> DeletePlayer(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var player = await _context.Players.FindAsync(id);
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<List<Schedule>> GetSchedule()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.Schedules.ToListAsync();
            }
        }
        public async Task<Schedule> GetSingleSchedule(int Id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.Schedules.FindAsync(Id);
            }
        }
        public async Task<bool> AddSchedule(Schedule schedule)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var exists = await _context.Schedules.AnyAsync(e => e.Id == schedule.Id);
                if (exists)
                {
                    _context.Schedules.Update(schedule);
                    await _context.SaveChangesAsync();
                    return true;
                }
                if(schedule.Date.ToString() == "0001-01-01" || schedule.Date.ToString() == null)
                {
                    return false;
                }
                _context.Schedules.Add(schedule);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<List<TeamDetail>> GetTeamDetails()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.TeamDetails.ToListAsync();
            }
        }
        public async Task<TeamDetail> GetSingleTeam(int Id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.TeamDetails.FindAsync(Id);
            }
        }
        public async Task<bool> AddTeam(TeamDetail team)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                _context.TeamDetails.Add(team);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<List<Week>> GetAllWeeks()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.Weeks.ToListAsync();
            }
        }
        public async Task<List<WeeksView>> GetWeeksView()
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
               return await _context.WeeksViews.ToListAsync();
            }
        }
        public async Task<bool> AddWeeks(Week weeks)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                _context.Weeks.Add(weeks);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> UpdateWeeks(Week weeks)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                _context.Weeks.Update(weeks);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task RemoveWeeks(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var weeks = await _context.Weeks.FindAsync(id);
                _context.Weeks.Remove(weeks);
                await _context.SaveChangesAsync();
            }
        }

    }
}
