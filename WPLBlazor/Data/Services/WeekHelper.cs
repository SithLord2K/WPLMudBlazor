using WPLMudBlazor.Data.Models;

namespace WPLMudBlazor.Data.Services
{
    public class WeekHelper(DataFactory aPIService)
    {
        public List<WeekFullInfo> WeekHelperFullInfo { get; set; } = [];

        private readonly DataFactory aPIService = aPIService;

        public async Task<List<WeekFullInfo>> GetFullWeek()
        {
            
            var fullWeeks = await aPIService.GetAllWeeks();
            List<PlayerDatum> playerInfo = [];
            var allPlayers = await aPIService.GetPlayers();
            foreach (var player in allPlayers)
            {
                
                var players = await aPIService.GetSinglePlayerData(player.Id);
                foreach (var p in players)
                {
                    playerInfo.Add(p);
                }
            }
            foreach (Week week in fullWeeks)
            {
                List<TeamDetail>? whatTeam = await aPIService.GetTeamDetails();
                bool testWeek = week.Forfeit;
                if (whatTeam is not null)
                {
                    WeekFullInfo weekFull = new()
                    {
                        WeekNumber = week.WeekNumber,
                        Home_Team = week.Home_Team,
                        Away_Team = week.Away_Team,
                        Home_TeamName = whatTeam.First(td => td.Id == week.Home_Team).TeamName,
                        Away_TeamName = whatTeam.First(td => td.Id == week.Away_Team).TeamName,
                        WinningTeamId = week.WinningTeamId

                    };
                    if (week.WeekNumber > 18)
                    {
                        weekFull.Playoff = true;
                    }
                    WeekHelperFullInfo.Add(weekFull);
                }
            }
            return WeekHelperFullInfo;

        }
    }
}
