using Newtonsoft.Json;
using RestSharp;
using System.Text.Json.Serialization;
using WPLBlazor.AuthenticationStateSyncer;
using WPLBlazor.Models;




namespace WPLBlazor.Auth0API
{
    public class Auth0API
    {
        private Auth0Token token = new();
        private async Task<Auth0Token> GetToken()
        {
            var client = new RestClient("https://dev-8wot12xlghuts3nc.us.auth0.com/oauth/token");
            var request = new RestRequest()
            {
                Method = Method.Post
            };
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"jg9fsNKIoyiRAFzms3qEZfyCdeaoyOQb\",\"client_secret\":\"w3SpTePgjn2FEC8CrR-8MdY3djm2JCIydmSwGF5qwvZRBgvyBTNf49clQUakAVLb\",\"audience\":\"https://dev-8wot12xlghuts3nc.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            token = (Auth0Token)JsonConvert.DeserializeObject(response.Content);
            
            return token;
        }
        public async Task<Task<string>> GetUsers()
        {
            var token = await GetToken();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dev-8wot12xlghuts3nc.us.auth0.com/api/v2/users");
            request.Headers.Add("authorization", (IEnumerable<string?>)token);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Bearer 🔒");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync();
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
