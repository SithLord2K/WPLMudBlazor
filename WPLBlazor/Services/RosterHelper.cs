using MvvmHelpers;
using WPLBlazor.Models;
using Roster = WPLBlazor.Models.Roster;

namespace WPLBlazor.Services
{

    public class RosterHelper : BaseViewModel
    {
        private readonly IAPIService aPIService = new APIService();
        private List<Player> players = [];
        private List<Roster> rosters = [];

        public async Task<List<Roster>> GetRoster()
        {
            var teams = await aPIService.GetTeamDetails();
            foreach (var team in teams)
            {
                
                players = await aPIService.GetAllPlayers();
                var play = players.Where(x => x.TeamId == team.Id).ToList();

                foreach (var player in play)
                {
                    Roster roster = new()
                    {
                        TeamId = team.Id,
                        TeamName = team.TeamName,
                        Captain_Player_Id = team.Captain_Player_Id,
                        Player_Id = player.Id,
                        Player_Name = player.FirstName + " " + player.LastName
                    };
                    rosters.Add(roster);
                }
            }
            return rosters;
        }
    }
}
