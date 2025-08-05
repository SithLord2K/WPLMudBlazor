using WPLMudBlazor.Data.Models;

namespace WPLMudBlazor.Data.Services
{
    
    public class PlayerViewService
    {
        public List<PlayersView> playersView = [];
        private readonly DataFactory aPIService;

        public async Task<List<PlayersView>?> GetPlayersView()
        {
            playersView.Clear();
            playersView = await aPIService.GetPlayersView();
            foreach (var player in playersView)
            {
                if(player.GamesPlayed > 0)
                {
                    player.Average = player.GamesWon / (decimal)player.GamesPlayed;
                }
            }
            if (playersView.Count > 0)
            {
                return playersView;
            }
            else
            {
                return null;
            }
        }

    }
}
