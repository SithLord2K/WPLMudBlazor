using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public class WeekHelper
    {
        public List<WeekFullInfo> WeekHelperFullInfo { get; set; } = [];

        readonly IAPIService aPIService = new APIService();
        public async Task<List<WeekFullInfo>> GetFullWeek()
        {
            
            var fullWeeks = await aPIService.GetAllWeeks();
            List<PlayerData> playerInfo = [];
            var allPlayers = await aPIService.GetAllPlayers();
            foreach (var player in allPlayers)
            {
                var players = await aPIService.GetPlayerData(player.Id);
                foreach (var p in players)
                {
                    playerInfo.Add(p);
                }
            }
            foreach (Weeks week in fullWeeks)
            {
                List<TeamDetails>? whatTeam = await aPIService.GetTeamDetails();
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
