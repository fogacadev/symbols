using Newtonsoft.Json;
using Symbols.Models;
using System.Threading.Tasks;

namespace Symbols.Repositories
{
    internal class CommunicationPairsRepository : BaseRepository
    {
        public async Task<CommunicationPair> Find()
        {
            UseAuthorization();
            var response = await _httpClient.GetAsync("communicationpairs");

            if (response.IsSuccessStatusCode)
            {
                var pair = JsonConvert.DeserializeObject<CommunicationPair>(await response.Content.ReadAsStringAsync());
                if(pair != null)
                    return pair;
            }

            return null;
        }
    }
}
