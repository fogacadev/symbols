using Newtonsoft.Json;
using Symbols.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbols.Repositories
{
    internal class MeRepository : BaseRepository
    {
        public async Task<User> Find()
        {
            UseAuthorization();
            var response = await _httpClient.GetAsync("me");
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
