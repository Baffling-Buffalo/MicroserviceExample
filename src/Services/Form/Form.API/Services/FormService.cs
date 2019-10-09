using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Form.API.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Form.API.Services
{
    public class FormService : IFormService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly string _userUrl;
        public FormService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _apiClient = httpClient; //DI httpClient
            _settings = settings;
            _userUrl = $"{_settings.Value.ozUrl}"; //iz appsettings.json
        }

        public async Task<String> GetForm(string name)
        {
            var uri = Infrastructure.API.Form.GetForm(_userUrl); //dobijam kompletan URI (spajam onaj iz appsettings.json sa ovim iz API klase)

            string response = await _apiClient.GetStringAsync(uri);

            return response;
        }

    }
}
