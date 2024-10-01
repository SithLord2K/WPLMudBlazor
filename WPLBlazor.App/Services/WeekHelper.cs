using MvvmHelpers;
using WPLBlazor.App.Models;

namespace WPLBlazor.App.Services
{
    public class WeekHelper : BaseViewModel
    {
        public List<WeekFullInfo> WeekHelperFullInfo { get; set; } = [];

        readonly APIService aPIService = new();
        public async Task<List<WeekFullInfo>> GetFullWeek()
        {
            List<TeamDetails> whatTeam = [];
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
                whatTeam = await aPIService.GetTeamDetails();
                bool testWeek = week.Forfeit;
                WeekFullInfo weekFull = new()
                {

                    GamesWon = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesWon),
                    GamesLost = playerInfo.Where(w => w.WeekNumber == week.WeekNumber).Sum(g => g.GamesLost),
                    WeekNumber = week.WeekNumber,
                    WeekWon = week.WeekWon,
                    TeamName = whatTeam.FirstOrDefault(td => td.Id == week.TeamPlayed).TeamName,
                    DatePlayed = week.DatePlayed.ToString("MMM. dd yyyy"),
                    Home = week.Home

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
            return WeekHelperFullInfo;

        }
    }
}
