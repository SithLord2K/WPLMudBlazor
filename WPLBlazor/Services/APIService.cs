using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WPLBlazor.Models;

namespace WPLBlazor.Services
{
    public class APIService : IAPIService
    {
        private static HttpClient? client;

        static readonly string BaseURL = "https://wileysoft.codersden.com";
        public APIService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(BaseURL),
            };
            client.DefaultRequestHeaders.Add("X-API-Key", "TDLoRo8deL0Bd9p6HfFMNONvtWAlz76YFXy3HIKMkgbSTA3Gkhllrle1a5FPiTkUjAuHcSicguMOQMUO7OuGj6nJg5h3VXc8h5gBrx2YRftwc7NRGl2R4cqv22aRJPnB");
        }

        static async Task<T> GetAsync<T>(string url, string key, int mins, bool forceRefresh = false)
        {
            var json = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    //await Shell.Current.DisplayAlert("URL Used:", url, "OK");
                    json = await client.GetStringAsync(url);

                    //Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                var data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw;
            }
        }

        //Player Tasks
        public Task<List<Player>> GetAllPlayers() =>
            GetAsync<List<Player>>($"/api_v2/Players", "getallplayers_v2", 30, true);

        public Task<Player> GetSinglePlayer(int id) =>
            GetAsync<Player>($"/api_v2/Players/{id}", "getsingleplayer_v2", 30, true);
        public Task SavePlayer(PlayerData playerData)
        {
            throw new NotImplementedException();
        }
        public Task DeletePlayer(int id)
        {
            throw new NotImplementedException();
        }

        //PlayerData Tasks
        public Task<List<PlayerData>> GetAllPlayerData() =>
            GetAsync<List<PlayerData>>($"/api_v2/PlayerData", "getallplayerdata_v2", 0, true);
        public Task<List<PlayerData>> GetPlayerData(int playerId) =>
            GetAsync<List<PlayerData>>($"/api_v2/PlayerData/{playerId}", "getplayerdata_v2", 0, true);
        public Task<PlayerData> GetSinglePlayerData(int playerId) =>
            GetAsync<PlayerData>($".api_v2/PlayerData/{playerId}", "getsingleplayerdata_v2", 0, true);
        public async Task SavePlayerData(PlayerData playerData)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/PlayerData");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<PlayerData>(playerData)
            };
            Barrel.Current.EmptyAll();
            _ = await client.SendAsync(message);
        }

        //Week Tasks
        public Task<IEnumerable<Weeks>> GetAllWeeks([Optional] bool forceRefresh) =>
            GetAsync<IEnumerable<Weeks>>("/api_v2/Weeks", "getweeks_v2", 30, forceRefresh);
        public async Task AddWeeks(Weeks weeks)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/Weeks");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Weeks>(weeks)
            };
            Barrel.Current.EmptyAll();
            _ = await client.SendAsync(message);
        }
        public Task RemoveWeeks(int id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateWeeks(Weeks weeks)
        {
            throw new NotImplementedException();
        }

        //TeamDetail Tasks
        public Task<List<TeamDetails>> GetTeamDetails() =>
            GetAsync<List<TeamDetails>>("/api_v2/TeamDetails", "getteamdetails_v2", 30);
        public Task<TeamDetails> GetSingleTeam(int id) =>
          GetAsync<TeamDetails>($"/api_v2/TeamDetails/{id}", "getsingleteam_v2", 30, true);

        //Schedule Tasks
        public Task<List<Schedules>> GetSchedule() =>
            GetAsync<List<Schedules>>($"/api_v2/Schedule", "getschedule_v2", 30, true);
        
    }

}
