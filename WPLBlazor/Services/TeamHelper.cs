using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public class TeamHelper
    {
        private readonly APIService aPIService = new();
        private readonly PlayerHelpers playerHelpers = new();
        public async Task<List<TeamStats>> GetAllTeamStats()
        {
            TeamStats teamTotals = new();
            List<TeamDetails> teams = [];
            List<PlayerData> players = [];
            List<TeamStats> teamStats = [];
            List<Player> plays = [];


            plays = await aPIService.GetAllPlayers();
            teams = await aPIService.GetTeamDetails();

            foreach (var team in teams)
            {
                foreach (var player in plays)
                {
                    players = await aPIService.GetPlayerData(player.Id);
                }
                teamTotals.TeamName = team.TeamName;
                teamTotals.TotalGamesLost += players.Sum(x => x.GamesLost);
                teamTotals.TotalGamesWon += players.Sum(x => x.GamesWon);
                teamTotals.TotalGamesPlayed += players.Sum(x => x.GamesPlayed);
                if (teamTotals.TotalGamesLost > 0 || teamTotals.TotalGamesWon > 0)
                {
                    teamTotals.TotalAverage = Decimal.Round(((decimal)teamTotals.TotalGamesWon / (decimal)teamTotals.TotalGamesLost) * 100, 2);
                }
                teamStats.Add(teamTotals);
            }
            return teamStats;
        }
    }
}
