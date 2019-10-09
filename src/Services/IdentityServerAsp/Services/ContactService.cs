using Identity.API.Services.ModelDTOs;
using Identity.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class ContactService : IContactService
    {
        private string _remoteServiceBaseUrl;
        private HttpClient _httpClient;
        private Dictionary<string, string[]> modelStateErrors;


        public ContactService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = settings.Value.ApiGWUrl;
        }

        public void Validate(ModelStateDictionary ModelState)
        {
            if (modelStateErrors == null)
                return;

            foreach (var model in modelStateErrors)
            {
                foreach (var error in model.Value)
                {
                    ModelState.AddModelError(model.Key.FirstCharToUpper(), error);
                }
            }
            modelStateErrors = null;
        }

        /// <summary>
        /// Tries to create contact on Contact.API. Returns contact id or -1 if operation didn't succeed
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateContact(NewContactDTO model)
        {
            string uri = SharedLibraries.API.Contact.CreateContact(_remoteServiceBaseUrl);

            var contact = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, contact);

            if(response.TrySaveModelStateErrors(ref modelStateErrors, throwException: false))
            {
                // Change model state errors to property names of NewApplicationUserDTO
                Dictionary<string, string[]> modifiedModelStateErrors = new Dictionary<string, string[]>();

                foreach (var item in modelStateErrors)
                {
                    modifiedModelStateErrors.Add("NewContact" + item.Key, item.Value);
                }
                modelStateErrors = modifiedModelStateErrors;
            }

            var responseString = await response.Content.ReadAsStringAsync();

            if (int.TryParse(responseString, out int id))
                return id;

            else
                return -1;
        }


        public async Task DeleteContacts(List<int> contactIds)
        {
            string uri = SharedLibraries.API.Contact.DeleteContacts(_remoteServiceBaseUrl);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonConvert.SerializeObject(contactIds), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }


    }
}
