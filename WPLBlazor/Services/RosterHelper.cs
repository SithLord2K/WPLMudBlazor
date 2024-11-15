using WPLBlazor.Models;
using Roster = WPLBlazor.Models.Roster;

namespace WPLBlazor.Services
{

    public class RosterHelper
    {
        private readonly IAPIService aPIService = new APIService();
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

        public async Task<List<Roster>> GetSingleTeamRoster(int teamId)
        {
            players = new();
            rosters = new();
            var team = await aPIService.GetSingleTeam(teamId);
            players = await aPIService.GetAllPlayers();
            var play = players.Where(x => x.TeamId == team.Id).ToList();
            bool cap;

            foreach (var player in play)
            {
                if(player.Id == team.Captain_Player_Id)
                {
                    cap = true;
                }
                else
                {
                    cap = false;
                }
                Roster roster = new()
                {
                    TeamId = team.Id,
                    TeamName = team.TeamName,
                    Captain_Player_Id = team.Captain_Player_Id,
                    Player_Id = player.Id,
                    Player_Name = player.FirstName + " " + player.LastName,
                    IsCaptain = cap
            };
            rosters.Add(roster);
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
