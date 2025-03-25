using WPLBlazor.Data.Models;

namespace WPLBlazor.Data.Services
{
    public class PDataService
    {
        private TeamHelper teamHelper;
        private PlayerHelpers playerHelper;
        private List<TeamStats> teamStats = new();
        private List<TeamDetail>? teamDetails = [];
        private List<Players> playerData = [];
        private List<PDataModel> pData = [];
        public PDataService(TeamHelper teamHelper, PlayerHelpers playerHelper)
        {
            this.teamHelper = teamHelper;
            this.playerHelper = playerHelper;
        }
        public async Task<List<PDataModel>> GetFullPlayerData()
        {
            teamDetails = await playerHelper.GetTeamDetails();
            playerData = await playerHelper.ConsolidatePlayer();
            teamStats = await teamHelper.GetAllTeamStats();
            foreach (var player in playerData.Where(x => x.TeamId != 12))
            {

                PDataModel pDataModel = new();
                pDataModel.TeamName = teamDetails.Where(x => x.Id == player.TeamId).First().TeamName;
                pDataModel.FirstName = player.FirstName;
                pDataModel.LastName = player.LastName;
                pDataModel.GamesWon = player.GamesWon;
                pDataModel.GamesLost = player.GamesLost;
                pDataModel.GamesPlayed = player.GamesPlayed;
                if (pDataModel.GamesPlayed > 0)
                {
                    pDataModel.Average = player.GamesWon / (decimal)player.GamesPlayed;
                }

                pData.Add(pDataModel);
            }

            return pData;
        }
    }
}
