using Blazorise;
using Microsoft.AspNetCore.Components;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WPLBlazor.Models;
using Blazorise.Components;

namespace WPLBlazor.Services
{
    public class APIService : IAPIService
    {
        
        private static HttpClient client = new();

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
        public async Task<bool> AddPlayer(Player player)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/Players");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Player>(player)
            };
            string testing = message.Content.ToString();
            HttpResponseMessage response = await client.SendAsync(message);
           if(response.IsSuccessStatusCode)
           {
                return true;
           }
            return false;
        }

        public Task<List<Player>> GetAllPlayers() =>
            GetAsync<List<Player>>($"/api_v2/Players", "getallplayers_v2", 30, true);

        public Task<Player> GetSinglePlayer(int id) =>
            GetAsync<Player>($"/api_v2/Players/{id}", "getsingleplayer_v2", 30, true);
        public Task<List<PlayersView>> GetPlayersView() =>
            GetAsync<List<PlayersView>>($"/api_v2/PlayersView", "getaplayersview_v2", 30, true);

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
        public async Task<bool> SavePlayerData(PlayerData playerData)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/PlayerData");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<PlayerData>(playerData)
            };
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //Week Tasks
        public Task<List<Weeks>> GetAllWeeks([Optional] bool forceRefresh) =>
            GetAsync<List<Weeks>>("/api_v2/Weeks", "getweeks_v2", 30, forceRefresh);
        //Get WeeksView
        public Task<List<WeeksView>> GetWeeksView() =>
           GetAsync<List<WeeksView>>($"/api_v2/WeeksView", "getweeksview_v2", 0, true);
        public async Task<bool> AddWeeks(Weeks weeks)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/Weeks");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Weeks>(weeks)
            };
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public Task RemoveWeeks(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateWeeks(Weeks weeks)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/Weeks");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Weeks>(weeks)
            };
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        //TeamDetail Tasks
        public Task<List<TeamDetails>> GetTeamDetails() =>
            GetAsync<List<TeamDetails>>("/api_v2/TeamDetails", "getteamdetails_v2", 30);
        public Task<TeamDetails> GetSingleTeam(int id) =>
          GetAsync<TeamDetails>($"/api_v2/TeamDetails/{id}", "getsingleteam_v2", 30, true);

        public async Task<bool> AddTeam(TeamDetails team)
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/TeamDetails");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<TeamDetails>(team)
            };
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        //Schedule Tasks
        public Task<List<Schedules>> GetSchedule() =>
            GetAsync<List<Schedules>>($"/api_v2/Schedule", "getschedule_v2", 30, true);

        public Task<Schedules> GetSingleSchedule(int Id) =>
            GetAsync<Schedules>($"/api_v2/Schedule/{Id}", "getsingleschedule_v2", 30, true);

        public async Task<bool> AddSchedule(Schedules schedule) 
        {
            Uri uri = new("https://wileysoft.codersden.com/api_v2/Schedule");
            HttpRequestMessage message = new(HttpMethod.Post, uri)
            {
                Content = JsonContent.Create<Schedules>(schedule)
            };
            HttpResponseMessage response = await client.SendAsync(message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }

}
