using System.Runtime.InteropServices;
using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public interface IAPIService
    {
        //Players
        Task<List<Player>> GetAllPlayers();
        Task<Player> GetSinglePlayer(int id);
        Task<bool> AddPlayer(Player player);
        Task DeletePlayer(int id);

        //PlayerData
        Task<List<PlayerData>> GetAllPlayerData();
        Task<List<PlayerData>> GetPlayerData(int playerId);
        Task<PlayerData> GetSinglePlayerData(int playerId);
        Task<bool> SavePlayerData(PlayerData playerData);

        //Schedule
        Task<List<Schedules>> GetSchedule();
        Task<Schedules> GetSingleSchedule(int Id);
        Task<bool> AddSchedule(Schedules schedule);

        //TeamDetails
        Task<List<TeamDetails>> GetTeamDetails();
        Task<TeamDetails> GetSingleTeam(int Id);
        Task<bool> AddTeam(TeamDetails team);

        //Weeks
        Task<List<Weeks>> GetAllWeeks([Optional] bool forceRefresh);
        Task<bool> AddWeeks(Weeks weeks);
        Task UpdateWeeks(Weeks weeks);
        Task RemoveWeeks(int id);
    }
}
