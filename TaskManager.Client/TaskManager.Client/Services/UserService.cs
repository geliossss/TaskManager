using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskManager.Domain.DTO;
using TaskManager.Domain.Models;

namespace TaskManager.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string?> RegisterAsync(RegisterRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/users/register", request);
            if (response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<User?> LoginAsync(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/users/login", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }

            return null;
        }
    }
}
