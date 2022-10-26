using Newtonsoft.Json;
using Symbols.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Symbols.Repositories
{
    internal class SessionsRepository : BaseRepository
    {
        public async Task<LoginResponse> Login(string username)
        {
            var login = new Login() { Username = username };
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("sessions", content);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = JsonConvert
                    .DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());

                if(loginResponse != null)
                    return loginResponse;
            }

            return null;
        }
    }
}
