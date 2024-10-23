using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public class TeamHelper
    {
        private readonly APIService aPIService = new();
        private readonly PlayerHelpers playerHelpers = new();
        public async Task<List<TeamStats>> GetAllTeamStats()
        {
            List<TeamDetails>? teams = [];
            List<PlayerData>? players = [];
            List<Player>? plays = [];
            List<TeamStats>? allTeamStats = [];


            teams = await aPIService.GetTeamDetails();
            List<Players>? teamTotals = [];
            List<Weeks>? weekTotals = [];
            weekTotals = (List<Weeks>)await aPIService.GetAllWeeks();
            teamTotals = await playerHelpers.ConsolidatePlayer();

            foreach (var team in teams.Where(x => x.TeamName != "Bye"))
            {
                if (teamTotals is not null)
                {
                    TeamStats teamStats = new()
                    {

                        TeamName = team.TeamName,
                        TotalGamesWon = teamTotals.Where(y => y.TeamId == team.Id).Sum(x => x.GamesWon),
                        TotalGamesLost = teamTotals.Where(y => y.TeamId == team.Id).Sum(y => y.GamesLost),
                        WeeksWon = weekTotals.Where(z => z.WinningTeamId == team.Id).Count(),
                        WeeksPlayed = weekTotals.Count
                    };

                    //teamStats.Week_Id = teamTotals.Where(y => y.TeamId == team.Id).FirstOrDefault().WeekNumber;
                    teamStats.TotalGamesPlayed = teamStats.TotalGamesWon + teamStats.TotalGamesLost;
                    if (teamStats.TotalGamesWon > 0)
                    {
                        teamStats.TotalAverage = Decimal.Round(((decimal)teamStats.WeeksWon / (decimal)teamStats.WeeksPlayed), 2);
                    }
                    allTeamStats.Add(teamStats);
                }
            }
            return allTeamStats;
        }
    }
}
