using Newtonsoft.Json;
using Symbols.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Symbols.Repositories
{
    internal class SymbolsRepository : BaseRepository
    {
        public async Task<bool> Send(SendSymbol sendSymbol)
        {
            UseAuthorization();
            var json = JsonConvert.SerializeObject(sendSymbol);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("symbols", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<Symbol> Find(string code)
        {
            UseAuthorization();
            var response = await _httpClient.GetAsync($"symbols/{code}");

            if (response.IsSuccessStatusCode)
            {
                var symbol = JsonConvert.DeserializeObject<Symbol>(await response.Content.ReadAsStringAsync());
                if (symbol != null)
                    return symbol;
            }

            return null;
        }


        public async Task<List<Symbol>> All()
        {
            UseAuthorization();
            var response = await _httpClient.GetAsync("symbols");

            if (response.IsSuccessStatusCode)
            {
                var symbols = JsonConvert.DeserializeObject<List<Symbol>>(await response.Content.ReadAsStringAsync());
                if (symbols != null)
                    return symbols;
            }

            return new List<Symbol>();
        }
    }
}
