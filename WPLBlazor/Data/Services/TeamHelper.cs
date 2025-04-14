using WPLBlazor.Data.Models;

namespace WPLBlazor.Data.Services
{
    public class TeamHelper
    {
        private readonly DataFactory aPIService;
        private readonly PlayerHelpers playerHelpers;
        public TeamHelper(DataFactory aPIService, PlayerHelpers playerHelpers)
        {
            this.aPIService = aPIService;
            this.playerHelpers = playerHelpers;
        }
        public async Task<List<TeamStats>> GetAllTeamStats()
        {
            List<TeamDetail>? teams = [];
            List<PlayerDatum>? players = [];
            List<Player>? plays = [];
            List<TeamStats>? allTeamStats = [];


            teams = await aPIService.GetTeamDetails();
            List<Players>? teamTotals = [];
            List<Week>? weekTotals = [];
            weekTotals = await aPIService.GetAllWeeks();
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
                        WeeksLost = weekTotals.Where(z => z.WinningTeamId != team.Id).Count(),
                        WeeksPlayed = weekTotals.Count
                    };

                    //teamStats.Week_Id = teamTotals.Where(y => y.TeamId == team.Id).FirstOrDefault().WeekNumber;
                    teamStats.TotalGamesPlayed = teamStats.TotalGamesWon + teamStats.TotalGamesLost;
                    if (teamStats.TotalGamesWon > 0)
                    {
                        teamStats.TotalAverage = teamStats.WeeksWon / (decimal)teamStats.WeeksPlayed;
                    }
                    allTeamStats.Add(teamStats);
                }
            }
            return allTeamStats;
        }
    }
}
