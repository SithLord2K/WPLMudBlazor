using Microsoft.AspNetCore.Components;
using WPLMudBlazor.Data.Models;


namespace WPLMudBlazor.Data.Services
{
    public class PlayerHelpers(DataFactory aPIService)
    {
        private readonly DataFactory aPIService = aPIService;
        List<Player> players = [];
        public async Task<List<Players>> ConsolidatePlayer()
        {
            var pList = new List<Players>();
             players = await aPIService.GetPlayers();

            if (players.Count == 0)
            {
                return pList;
            }
            else
            {
                foreach (var item in players)
                {
                    Players playerTotals = new();
                    var getPlayerData = await aPIService.GetSinglePlayerData(item.Id);

                    if (item.FirstName != string.Empty)
                    {
                        playerTotals.Id = item.Id;
                        playerTotals.TeamId = item.TeamId;
                        playerTotals.FirstName = item.FirstName;
                        if (item.LastName != string.Empty)
                        {
                            playerTotals.LastName = item.LastName;
                        }
                        else
                        {
                            playerTotals.LastName = string.Empty;
                        }
                        if (getPlayerData.Count != 0)
                        {
                            playerTotals.GamesWon = getPlayerData.Sum(x => x.GamesWon);
                            playerTotals.GamesLost = getPlayerData.Sum(y => y.GamesLost);
                            playerTotals.GamesPlayed = getPlayerData.Sum(z => z.GamesPlayed);
                            //if (playerTotals.GamesWon != 0 && playerTotals.GamesPlayed != 0)
                            //{
                            //    playerTotals.Average = playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed;
                            //}
                            //playerTotals.WeekNumber = getPlayerData.Count();
                        }
                    }
                    pList.Add(playerTotals);
                }

            }
            return pList;
        }
        
        public async Task<Players> GetPlayerDetails(int id)
        {
            Players playerTotals = new();
            List<PlayerDatum> getPlayerData = await aPIService.GetSinglePlayerData(id);
            var playerInfo = await aPIService.GetSinglePlayer(id);
            if (getPlayerData.Count > 0)
            {
                playerTotals.Id = getPlayerData.First().PlayerId;
                playerTotals.FirstName = playerInfo.FirstName;
                playerTotals.LastName = playerInfo.LastName ?? string.Empty;
                playerTotals.GamesWon = getPlayerData.Sum(gw => gw.GamesWon);
                playerTotals.GamesLost = getPlayerData.Sum(y => y.GamesLost);
                playerTotals.GamesPlayed = getPlayerData.Sum(y => y.GamesPlayed);
                //if (playerTotals.GamesWon != 0 && playerTotals.GamesPlayed != 0)
                //{
                //    playerTotals.Average = playerTotals.GamesWon / (decimal)playerTotals.GamesPlayed;
                //}
                //playerTotals.WeekNumber = getPlayerData.Count;
            }

            return playerTotals;
        }

        public async Task<TeamStats> GetTeamStats()
        {
            List<PlayerDatum> teamTotals = [];
            List<Week> weekTotals = [];
            weekTotals = await aPIService.GetAllWeeks();
            teamTotals = await aPIService.GetAllPlayerData();
            TeamStats teamStats = new()
            {
                TotalGamesWon = teamTotals.Sum(x => x.GamesWon),
                TotalGamesLost = teamTotals.Sum(y => y.GamesLost)
            };
            teamStats.TotalGamesPlayed = teamStats.TotalGamesWon + teamStats.TotalGamesLost;
            teamStats.TotalAverage = decimal.Round(teamStats.TotalGamesWon / (decimal)teamStats.TotalGamesPlayed, 2);
            teamStats.WeeksPlayed = weekTotals.Count;

            return teamStats;
        }


        public async Task<List<TeamDetail>> GetTeamDetails()
        {
            List<TeamDetail> teamDetails = [];
            teamDetails = await aPIService.GetTeamDetails();

            return [.. teamDetails.Where(x => x.TeamName != "Bye")];
        }
    }
}
