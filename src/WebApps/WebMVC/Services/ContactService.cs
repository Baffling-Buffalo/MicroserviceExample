using DataTablesParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SharedLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Infrastructure.Extensions;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels.Contact;
using WebMVC.ViewModels.List;

namespace WebMVC.Services
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;
        private readonly string _remoteServiceBaseUrl;
        private Dictionary<string, string[]> modelStateErrors;

        public ContactService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _remoteServiceBaseUrl = $"{_settings.Value.ApiGWUrl}";
        }

        //CONTACT

        public async Task<List<GetContactDTO>> GetContacts(List<int> contactIds = null)
        {
            var uri = API.Contact.GetContacts(_remoteServiceBaseUrl, contactIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            var contacts = JsonConvert.DeserializeObject<List<GetContactDTO>>(responseString);

            return contacts;
        }

        public async Task<GetContactDTO> GetContact(int contactId)
        {
            var uri = API.Contact.GetContact(_remoteServiceBaseUrl, contactId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var contact = JsonConvert.DeserializeObject<GetContactDTO>(responseString);

            return contact;
        }

        public async Task<string> GetContactsForDataTable(IFormCollection datatableParams, List<int> listIds = null, bool? active = null, List<int> excludeIds = null)
        {
            var uri = API.Contact.GetContactsForDataTable(_remoteServiceBaseUrl);

            var keyValues = datatableParams.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString())).ToList();

            var dto = new
            {
                DataTableParameters = keyValues,
                Active = active,
                ListIds = listIds,
                ExcludeIds = excludeIds
            };

            var content = new StringContent(JsonConvert.SerializeObject(dto), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            var contacts = await response.Content.ReadAsStringAsync();

            return contacts;
        }

        public Task<List<GetContactDTO>> GetContactsInList(int listId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateContact(ContactDTO model)
        {
            string uri = API.Contact.CreateContact(_remoteServiceBaseUrl);

            var contact = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, contact);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }
        public async Task<string> CreateUserAsync(ApplicationUserDTO model)
        {
            var uri = API.User.CreateUser(_remoteServiceBaseUrl);

            var newUserContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, newUserContent);

            if (response.TrySaveModelStateErrors(ref modelStateErrors))
            {
                return "";
            }

            return await response.Content.ReadAsStringAsync(); // created users Id
        }

        public async Task UpdateContact(ContactDTO model)
        {
            string uri = API.Contact.UpdateContact(_remoteServiceBaseUrl);

            var contact = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, contact);

            response.TrySaveModelStateErrors(ref modelStateErrors);

        }

        public async Task DeleteContacts(List<int> contactIds)
        {
            string uri = API.Contact.DeleteContacts(_remoteServiceBaseUrl);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonConvert.SerializeObject(contactIds), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task ActivateContacts(List<int> contactIds)
        {
            string uri = API.Contact.ActivateContacts(_remoteServiceBaseUrl);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonConvert.SerializeObject(contactIds), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeactivateContacts(List<int> contactIds)
        {
            var uri = API.Contact.DeactivateContacts(_remoteServiceBaseUrl);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonConvert.SerializeObject(contactIds), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public async Task AddContactsToLists(ContactListDTO model)
        {
            var uri = API.Contact.AddContactsToLists(_remoteServiceBaseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveContactsFromLists(ContactListDTO model)
        {
            var uri = API.Contact.RemoveContactsFromLists(_remoteServiceBaseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }

        //LIST

        public async Task<GetListDTO> GetList(int listId)
        {
            var uri = API.List.GetList(_remoteServiceBaseUrl, listId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var list = JsonConvert.DeserializeObject<GetListDTO>(responseString);

            return list;
        }

        public async Task<List<GetListDTO>> GetLists(List<int> listIds = null)
        {
            var uri = API.List.GetLists(_remoteServiceBaseUrl, listIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            var lists = JsonConvert.DeserializeObject<List<GetListDTO>>(responseString);

            return lists;
        }

        public async Task<string> GetListsForDataTable(IFormCollection formCollection, int? parentId = null)
        {
            var uri = API.List.GetListsForDataTable(_remoteServiceBaseUrl, parentId);

            var keyValues = formCollection.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString()));

            var content = new FormUrlEncodedContent(keyValues);

            var responseString = await _httpClient.PostAsync(uri, content);

            var contacts = await responseString.Content.ReadAsStringAsync();

            return contacts;
        }


        public async Task<string> GetListsForComboTreePlugin(int[] listIds = null)
        {
            var uri = API.List.GetListsForComboTree(_remoteServiceBaseUrl, listIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }

        public async Task<List<ListTreeNode>> GetListsForListTree(int[] listIds = null)
        {
            var uri = API.List.GetListsForListTree(_remoteServiceBaseUrl, listIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            var lists = JsonConvert.DeserializeObject<List<ListTreeNode>>(responseString);

            return lists;
        }

        public Task<List<GetListDTO>> GetListsInContact(int contactId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateList(ListDTO model)
        {
            string uri = API.List.CreateList(_remoteServiceBaseUrl);

            var list = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, list);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task UpdateList(ListDTO model)
        {
            string uri = API.List.UpdateList(_remoteServiceBaseUrl);

            var list = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, list);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task<DeleteDTO> DeleteLists(List<int> listIds)
        {
            string uri = API.List.DeleteLists(_remoteServiceBaseUrl);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonConvert.SerializeObject(listIds), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            var lists = JsonConvert.DeserializeObject<DeleteDTO>(response.Content.ReadAsStringAsync().Result);

            return lists;
        }

        public Task<List<GetListDTO>> GetChildren(int listId)
        {
            throw new NotImplementedException();
        }



        //OTHER

        public ContactDTO MapContactVMToContactDTO(ContactViewModel model)
        {
            return new ContactDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Active = model.Active,
                ListIds = model.ListIds,
                AllowLogin = model.AllowLogin,
                Username = model.Username
            };
        }

        public NewApplicationUserDTO MapContactVMToUserDTO(ContactViewModel model)
        {
            return new NewApplicationUserDTO()
            {
                UserName = model.Username,
                Password = model.Password
            };
        }

        public ContactViewModel MapGetContactDTOToContactViewModel(GetContactDTO model)
        {
            return new ContactViewModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Active = model.Active,
                ListIds = model.Lists.Select(l => l.Key).ToList()
            };
        }

        public ListDTO MapListVMToListDTO(ListViewModel model)
        {
            return new ListDTO()
            {
                Id = model.Id,
                ListName = model.ListName,
                Description = model.Description,
                ParentId = model.ParentId,
                ContactIds = model.ContactIds
            };
        }

        public ListViewModel MapGetListDTOToListViewModel(GetListDTO model)
        {
            return new ListViewModel
            {
                Id = model.Id,
                ListName = model.ListName,
                Description = model.Description,
                ParentId = model.ParentId
            };
        }

        public ContactDetailsViewModel MapGetContactDTOToContactDetailsViewModel(GetContactDTO model)
        {
            return new ContactDetailsViewModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Active = (model.Active == true ? "Active" : "Inactive"),
                Lists = string.Join(',', model.Lists.Select(l => l.Value))
            };
        }

        public ListDetailsViewModel MapGetListDTOToListDetailsViewModel(GetListDTO model)
        {
            return new ListDetailsViewModel
            {
                Id = model.Id,
                ListName = model.ListName,
                Description = model.Description,
                ParentId = model.ParentId,
                ParentName = model.ParentName,
                ChildLists = string.Join(',', model.ChildLists.Select(l => l.Value))
            };
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

        public async Task<List<GetListDTO>> GetListsOfContacts(List<int> contactIds)
        {
            var uri = API.List.GetListsOfContacts(_remoteServiceBaseUrl, contactIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            var lists = JsonConvert.DeserializeObject<List<GetListDTO>>(responseString);

            return lists;
        }


    }
}
