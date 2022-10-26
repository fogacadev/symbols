using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Symbols.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly HttpClient _httpClient;
        public BaseRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(AppConfiguration.BaseUrl);
        }


        protected void UseAuthorization()
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppConfiguration.LoginResponse.Token);
        }

    }
}
