using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharedLibraries;
using WebMVC.Controllers;
using WebMVC.Infrastructure;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Services.ModelDTOs;
using WebMVC.Services.ModelDTOs.Form;
using WebMVC.ViewModels.FormGroup;
using AspNetCore.Proxy;

namespace WebMVC.Services
{
    public class LogService : ILogService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;
        private readonly string _remoteServiceBaseUrl;
        private Dictionary<string, string[]> modelStateErrors;

        public LogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _remoteServiceBaseUrl = $"{_settings.Value.ApiGWUrl}";
        }

        public async Task DeleteAuditGroups(string[] correlationIds)
        {
            var uri = API.Log.DeleteAuditGroups(_remoteServiceBaseUrl);

            var idsContent = new StringContent(JsonConvert.SerializeObject(correlationIds), System.Text.Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = idsContent
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GetAuditLogsForDatatable(IFormCollection formCollection, DateTime? startDate, DateTime? endDate)
        {
            var uri = API.Log.GetAuditLogsForDataTable(_remoteServiceBaseUrl);

            var keyValues = formCollection.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString())).ToList();

            var dto = new
            {
                DataTableParameters = keyValues,
                StartDate = startDate,
                EndDate = endDate
            };

            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            var audits = await response.Content.ReadAsStringAsync();

            return audits;
        }
    }
}
