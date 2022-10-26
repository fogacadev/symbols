using Newtonsoft.Json;
using Symbols.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symbols.Repositories
{
    internal class UsersRepository : BaseRepository
    {
        public async Task<List<User>> All()
        {
            var response = await _httpClient.GetAsync("users");

            if (response.IsSuccessStatusCode)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
                if(users != null)
                    return users;
            }

            return new List<User>();
        }

        public async Task<User> Find(string username)
        {
            var response = await _httpClient.GetAsync($"users/{username}");

            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                if (user != null)
                    return user;
            }

            return null;
        }
    }
}
