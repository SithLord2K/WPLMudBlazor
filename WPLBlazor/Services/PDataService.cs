using Microsoft.AspNetCore.Razor.TagHelpers;
using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public class PDataService
    {
        private APIService aPIService = new();
        private TeamHelper teamHelper = new();
        private PlayerHelpers playerHelper = new();
        private List<TeamStats> teamStats = new();
        private List<TeamDetails>? teamDetails = [];
        private List<Players> playerData = [];
        private List<PDataModel> pData = [];
        private PDataModel pDataModel = new();
        public async Task<List<PDataModel>> GetFullPlayerData()
        {
            teamDetails = await playerHelper.GetTeamDetails();
            playerData = await playerHelper.ConsolidatePlayer();
            teamStats = await teamHelper.GetAllTeamStats();
            foreach (var player in playerData.Where(x => x.TeamId != 12))
            {

                PDataModel pDataModel = new();
                pDataModel.TeamName = teamDetails.Where(x => x.Id == player.TeamId).FirstOrDefault().TeamName;
                pDataModel.FirstName = player.FirstName;
                pDataModel.LastName = player.LastName;
                pDataModel.GamesWon = player.GamesWon;
                pDataModel.GamesLost = player.GamesLost;
                pDataModel.GamesPlayed = player.GamesPlayed;
                if (pDataModel.GamesPlayed > 0)
                {
                    pDataModel.Average = Decimal.Round(((decimal)player.GamesWon / (decimal)player.GamesPlayed), 2);
                }

                pData.Add(pDataModel);
            }

            return pData;
        }
    }
}
