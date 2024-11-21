using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    
    public class PlayerViewService
    {
        public List<PlayersView> playersView = [];
        public APIService aPIService = new();

        public async Task<List<PlayersView>?> GetPlayersView()
        {
            playersView.Clear();
            playersView = await aPIService.GetPlayersView();
            foreach (var player in playersView)
            {
                if(player.GamesPlayed > 0)
                {
                    player.Average =((decimal)player.GamesWon / (decimal)player.GamesPlayed);
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
