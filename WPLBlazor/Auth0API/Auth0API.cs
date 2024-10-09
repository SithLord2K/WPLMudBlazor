using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WPLBlazor.AuthenticationStateSyncer;




namespace WPLBlazor.Auth0API
{
    public class Auth0API
    {
        public async Task<string> GetUsers()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dev-8wot12xlghuts3nc.us.auth0.com/api/v2/users");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer 🔒");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var userProfile = await response.Content.ReadAsStringAsync();
            return userProfile;
        }

        public async Task<Task<string>> GetSingleUser(string id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://dev-8wot12xlghuts3nc.us.auth0.com/api/v2/users/{id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer 🔒");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync();
        }
        
        public async Task<Task<string>> GetUserRoles(string id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://dev-8wot12xlghuts3nc.us.auth0.com/api/v2/users/{id}/roles");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer 🔒");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsStringAsync();
        }


    }
}
