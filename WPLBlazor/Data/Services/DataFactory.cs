using Microsoft.EntityFrameworkCore;
using WPLMudBlazor.Data.Models;

namespace WPLMudBlazor.Data.Services
{
    public class DataFactory(IDbContextFactory<WPLStatsDBContext> contextFactory)
    {
        private readonly IDbContextFactory<WPLStatsDBContext> _contextFactory = contextFactory;

        public async Task<List<Player>> GetPlayers()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Players.ToListAsync();
        }
        public async Task<List<PlayerDatum>> GetPlayerData()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.PlayerData.ToListAsync();
        }
        public async Task<List<PlayersView>> GetPlayersView()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.PlayersViews.ToListAsync();
        }
        public async Task<Player?> GetSinglePlayer(int id)
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Players.FindAsync(id);
        }
        public async Task<List<PlayerDatum>> GetSinglePlayerData(int playerId)
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.PlayerData.Where(e => e.PlayerId == playerId).ToListAsync();
        }
        public Task<List<PlayerDatum>> GetAllPlayerData()
        {
            using var _context = _contextFactory.CreateDbContext();
            return _context.PlayerData.ToListAsync();
        }
        public async Task<List<PlayerData>> GetPlayerData_All()
        {
            using var _context = _contextFactory.CreateDbContext();
            var pData = await _context.PlayerData.ToListAsync();
            var players = await _context.Players.ToListAsync();
            List<PlayerData> playerData = [];
            foreach (var item in pData)
            {
                PlayerData player = new()
                {
                    PlayerId = item.PlayerId,
                    GamesWon = item.GamesWon,
                    GamesLost = item.GamesLost,
                    WeekNumber = item.WeekNumber,
                    GamesPlayed = item.GamesPlayed
                };
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
        public async Task<bool> AddPlayer(Player player)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SavePlayerData(PlayerDatum playerData)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.PlayerData.Add(playerData);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePlayer(int id)
        {
            using var _context = _contextFactory.CreateDbContext();
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Schedule>> GetSchedule()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Schedules.ToListAsync();
        }
        public async Task<Schedule> GetSingleSchedule(int Id)
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Schedules.FindAsync(Id);
        }
        public async Task<bool> AddSchedule(Schedule schedule)
        {
            using var _context = _contextFactory.CreateDbContext();
            var exists = await _context.Schedules.AnyAsync(e => e.Id == schedule.Id);
            if (exists)
            {
                _context.Schedules.Update(schedule);
                await _context.SaveChangesAsync();
                return true;
            }
            if (schedule.Date.ToString() == "0001-01-01" || schedule.Date.ToString() == null)
            {
                return false;
            }
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<TeamDetail>> GetTeamDetails()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.TeamDetails.ToListAsync();
        }
        public async Task<TeamDetail> GetSingleTeam(int Id)
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.TeamDetails.FindAsync(Id);
        }
        public async Task<bool> AddTeam(TeamDetail team)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.TeamDetails.Add(team);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Week>> GetAllWeeks()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.Weeks.ToListAsync();
        }
        public async Task<List<WeeksView>> GetWeeksView()
        {
            using var _context = _contextFactory.CreateDbContext();
            return await _context.WeeksViews.ToListAsync();
        }
        public async Task<bool> AddWeeks(Week weeks)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Weeks.Add(weeks);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateWeeks(Week weeks)
        {
            using var _context = _contextFactory.CreateDbContext();
            _context.Weeks.Update(weeks);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task RemoveWeeks(int id)
        {
            using var _context = _contextFactory.CreateDbContext();
            var weeks = await _context.Weeks.FindAsync(id);
            _context.Weeks.Remove(weeks);
            await _context.SaveChangesAsync();
        }

        public async Task AddPlayersToArchive(List<Player> players)
        {
            using var _context = _contextFactory.CreateDbContext();
            foreach (var player in players)
            {
                // Map Player to Players_Archive
                var playerArchive = new Players_Archive
                {
                    Id = player.Id,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    TeamId = player.TeamId,
                    Season_Year = DateTime.Now.Year.ToString()
                };

                _context.Players_Archives.Add(playerArchive);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddPlayerDataToArchive(List<PlayerDatum> playerData)
        {
            using var _context = _contextFactory.CreateDbContext();
            foreach (var data in playerData)
            {
                // Map PlayerDatum to PlayerData_Archive
                var playerDataArchive = new PlayerData_Archive
                {
                    PlayerId = data.PlayerId,
                    WeekNumber = data.WeekNumber,
                    GamesWon = data.GamesWon,
                    GamesLost = data.GamesLost,
                    GamesPlayed = data.GamesPlayed,
                    Season_Year = DateTime.Now.Year.ToString()
                };
                _context.PlayerData_Archives.Add(playerDataArchive);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddTeamsToArchive(List<TeamDetail> teams)
        {
            using var _context = _contextFactory.CreateDbContext();
            foreach (var team in teams)
            {
                // Map TeamDetail to TeamDetails_Archive
                var teamArchive = new TeamDetails_Archive
                {
                    Id = team.Id,
                    TeamName = team.TeamName,
                    Season_Year = DateTime.Now.Year.ToString()
                };
                _context.TeamDetails_Archives.Add(teamArchive);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddWeeksToArchive(List<Week> weeks)
        {
            using var _context = _contextFactory.CreateDbContext();
            foreach (var week in weeks)
            {
                // Map Week to Weeks_Archive
                var weekArchive = new Weeks_Archive
                {
                    Id = week.Id,
                    WeekNumber = week.WeekNumber,
                    Home_Team = week.Home_Team,
                    Away_Team = week.Away_Team,
                    WinningTeamId = week.WinningTeamId,
                    Forfeit = week.Forfeit,
                    Season_Year = DateTime.Now.Year.ToString()
                };
                _context.Weeks_Archives.Add(weekArchive);
            }
            await _context.SaveChangesAsync();
        }
        public async Task AddScheduleToArchive(List<Schedule> schedules)
        {
            using var _context = _contextFactory.CreateDbContext();
            foreach (var schedule in schedules)
            {
                // Map Schedule to Schedule_Archive
                var scheduleArchive = new Schedule_Archive
                {
                    Id = schedule.Id,
                    Week_Id = schedule.Week_Id,
                    Home_Team = schedule.Home_Team,
                    Away_Team = schedule.Away_Team,
                    Date = schedule.Date,
                    Week_Id_Playoff = schedule.Week_Id_Playoff,
                    Table_Number = schedule.Table_Number,
                    Playoffs = schedule.Playoffs,
                    Season_Year = DateTime.Now.Year.ToString()
                };
                _context.Schedule_Archives.Add(scheduleArchive);
            }
            await _context.SaveChangesAsync();
        }
    }
}
