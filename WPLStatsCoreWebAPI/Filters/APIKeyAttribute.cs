using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace WPLBlazor.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class APIKeyAttribute : Attribute, IAuthorizationFilter
    {
        private const string API_KEY_HEADER_NAME = "X-API-Key";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string submittedApiKey = GetSubmittedApiKey(context.HttpContext);
            string apiKey = GetApiKey(context.HttpContext);
            if (!IsApiKeyValid(apiKey, submittedApiKey))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private static string? GetSubmittedApiKey(HttpContext context)
        {
            if (context != null)
            {
                return context.Request.Headers[API_KEY_HEADER_NAME];
            }
            return null;
        }

        private static string? GetApiKey(HttpContext context)
        {
            var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            if (configuration != null)
            {
                return configuration.GetValue<string>($"APIKey");
            }
            return null;
        }

        private static bool IsApiKeyValid(string apiKey, string submittedApiKey)
        {
            if (string.IsNullOrEmpty(submittedApiKey)) return false;
            var apiKeySpan = MemoryMarshal.Cast<char, byte>(apiKey.AsSpan());
            var submittedApiKeySpan = MemoryMarshal.Cast<char, byte>(submittedApiKey.AsSpan());

            return CryptographicOperations.FixedTimeEquals(apiKeySpan, submittedApiKeySpan);
        }
    }
}
