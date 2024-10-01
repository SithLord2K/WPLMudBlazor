using MvvmHelpers;
using WPLBlazor.App.Models;
using Roster = WPLBlazor.App.Models.Roster;

namespace WPLBlazor.App.Services
{

    public class RosterHelper : BaseViewModel
    {
        private readonly APIService aPIService = new();
        public List<Player> players = [];
        public List<Roster> rosters = [];
        public List<Schedules> schedules = [];
        

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

        public async Task<List<Schedules>> GetSchedules()
        {
            var schedule = await aPIService.GetSchedule();
            foreach (var sched in schedule)
            {
                Schedules scheDule = new()
                {
                    Date = sched.Date,
                    Home_Team = sched.Home_Team,
                    Away_Team = sched.Away_Team,
                    Week_Id = sched.Week_Id,
                    Table_Number = sched.Table_Number
                };
                schedules.Add(scheDule);
            }
            return schedules;
        }
    }
}
