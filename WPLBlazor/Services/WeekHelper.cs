using MvvmHelpers;
using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public class WeekHelper : BaseViewModel
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

                        GamesWon = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesWon),
                        GamesLost = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesLost),
                        WeekNumber = week.WeekNumber,
                        Home_Team = week.Home_Team,
                        Away_Team = week.Away_Team,
                        Home_TeamName = whatTeam.FirstOrDefault(td => td.Id == week.Home_Team).TeamName,
                        Away_TeamName = whatTeam.FirstOrDefault(td => td.Id == week.Away_Team).TeamName,
                        Home_Won = week.Home_Won

                    };
                    if (week.WeekNumber > 18)
                    {
                        weekFull.Playoff = true;
                    }
                    if (weekFull.GamesWon != 0)
                    {
                        weekFull.Average = Decimal.Round((decimal)weekFull.GamesWon / ((decimal)weekFull.GamesLost + (decimal)weekFull.GamesWon) * 100, 2);
                    }
                    else
                    {
                        weekFull.Forfeit = week.Forfeit;
                        weekFull.GamesWon = 0;
                        weekFull.GamesLost = 0;
                        weekFull.Average = 0;
                    }
                    WeekHelperFullInfo.Add(weekFull);
                }
            }
            return WeekHelperFullInfo;

        }
    }
}
