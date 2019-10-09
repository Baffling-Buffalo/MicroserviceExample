using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SharedLibraries;
using Newtonsoft.Json;

namespace Session.API.Services
{
    public class ContactService : IContactService
    {
        private string _remoteServiceBaseUrl;
        private HttpClient _httpClient;

        public ContactService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = settings.Value.ApiGWUrl;
        }

        public async Task<bool> Exists(int id)
        {
            var uri = SharedLibraries.API.Contact.Exists(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var exists = JsonConvert.DeserializeObject<bool>(responseString);

            return exists;

        }
    }
}
