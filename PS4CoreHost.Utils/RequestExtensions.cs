using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace PS4CoreHost.Utils
{
    public static class RequestExtensions
    {
        public static bool IsUserManuals(this HttpRequest request)
        {
            return Regex.IsMatch(request.Path.Value.ToLower(), PS4RequestPatterns.UserManuals);
        }
    }
}
