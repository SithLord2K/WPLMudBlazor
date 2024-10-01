using System.Runtime.InteropServices;
using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public interface IAPIService
    {
        //Players
        Task<List<Player>> GetAllPlayers();
        Task<Player> GetSinglePlayer(int id);
        Task SavePlayer(PlayerData player);
        Task DeletePlayer(int id);

        //PlayerData
        Task<List<PlayerData>> GetAllPlayerData();
        Task<List<PlayerData>> GetPlayerData(int playerId);
        Task<PlayerData> GetSinglePlayerData(int playerId);
        Task SavePlayerData(PlayerData playerData);

        //Weeks
        Task<IEnumerable<Weeks>> GetAllWeeks([Optional] bool forceRefresh);
        Task AddWeeks(Weeks weeks);
        Task UpdateWeeks(Weeks weeks);
        Task RemoveWeeks(int id);

        //TeamDetails
        Task<List<TeamDetails>> GetTeamDetails();
        Task<TeamDetails> GetSingleTeam(int Id);

        //Schedule
        Task<List<Schedules>> GetSchedule();
        Task AddSchedule(Schedules schedule);
    }
}
