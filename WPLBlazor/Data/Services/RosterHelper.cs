using WPLBlazor.Data.Models;
using Roster = WPLBlazor.Data.Models.Roster;

namespace WPLBlazor.Data.Services
{

    public class RosterHelper
    {
        private readonly DataFactory aPIService;
        public List<Player> players = [];
        public List<Roster> rosters = [];
        public List<Schedule> schedules = [];
        public RosterHelper(DataFactory aPIService)
        {
            this.aPIService = aPIService;
        }

        public async Task<List<Roster>> GetRoster()
        {
            var teams = await aPIService.GetTeamDetails();
            foreach (var team in teams)
            {

                players = await aPIService.GetPlayers();
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
            players = [];
            rosters = [];
            var team = await aPIService.GetSingleTeam(teamId);
            players = await aPIService.GetPlayers();
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
    public async Task<List<Schedule>> GetSchedules()
    {
        var schedule = await aPIService.GetSchedule();
        foreach (var sched in schedule)
        {
            Schedule scheDule = new()
            {
                Date = sched.Date,
                Home_Team = sched.Home_Team,
                Away_Team = sched.Away_Team,
                Week_Id = sched.Week_Id,
                Table_Number = sched.Table_Number,
                Playoffs = sched.Playoffs,
                Week_Id_Playoff = sched.Week_Id_Playoff
            };
            schedules.Add(scheDule);
        }
        return schedules;
    }
}
}
