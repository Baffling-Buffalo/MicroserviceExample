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
    public class FormService : IFormService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AppSettings> _settings;
        private readonly string _remoteServiceBaseUrl;
        private Dictionary<string, string[]> modelStateErrors;

        public FormService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _remoteServiceBaseUrl = $"{_settings.Value.ApiGWUrl}";
        }

        //FORMS

        public async Task<string> GetFormsForDataTable(IFormCollection formCollection)
        {
            var uri = API.Form.GetFormsForDataTable(_remoteServiceBaseUrl);

            var nameValueCollection = formCollection.Select(dt => new KeyValuePair<string, string>(dt.Key, dt.Value.ToString()));

            var content = new FormUrlEncodedContent(nameValueCollection);

            var response = await _httpClient.PostAsync(uri, content);

            var forms = await response.Content.ReadAsStringAsync();

            return forms;
        }

        public Task GetFrame(FormController controller)
        {
            var uri = API.Form.GetFrame(_remoteServiceBaseUrl);

            var token = GetToken(controller.HttpContext).Result;

            if (token != null)
            {
                controller.Request.Headers.Append("Authorization", "Bearer " + token);
            }
            return controller.ProxyAsync(uri);
        }

        private async Task<string> GetToken(HttpContext httpContext)
        {
            string accessToken = await httpContext.GetTokenAsync("access_token");
            return accessToken;
        }

        //FORM GROUPS

        public async Task<GetGroupDTO> GetGroup(int groupId)
        {
            var uri = API.FormGroup.GetGroup(_remoteServiceBaseUrl, groupId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var group = JsonConvert.DeserializeObject<GetGroupDTO>(responseString);

            return group;
        }

        public async Task<string> GetGroupsForComboTreePlugin(List<int> groupIds = null)
        {
            var uri = API.FormGroup.GetGroupsForComboTree(_remoteServiceBaseUrl, groupIds);

            var responseString = await _httpClient.GetStringAsync(uri);

            return responseString;
        }

        public async Task<List<GroupTreeNode>> GetGroupsForTreeTable()
        {
            var uri = API.FormGroup.GetGroupsForTreeTable(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var groups = JsonConvert.DeserializeObject<List<GroupTreeNode>>(responseString);

            return groups;
        }

        public async Task CreateGroup(GroupDTO model)
        {
            string uri = API.FormGroup.CreateGroup(_remoteServiceBaseUrl);

            var group = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, group);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task UpdateGroup(GroupDTO model)
        {
            string uri = API.FormGroup.UpdateGroup(_remoteServiceBaseUrl);

            var group = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, group);

            response.TrySaveModelStateErrors(ref modelStateErrors);
        }

        public async Task<DeleteDTO> DeleteGroups(List<int> groupIds)
        {
            string uri = API.FormGroup.DeleteGroups(_remoteServiceBaseUrl);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(uri),
                Content = new StringContent(JsonConvert.SerializeObject(groupIds), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            var groups = JsonConvert.DeserializeObject<DeleteDTO>(response.Content.ReadAsStringAsync().Result);

            return groups;
        }

        //OTHER

        public GroupDTO MapGroupVMToGroupDTO(FormGroupViewModel model)
        {
            return new GroupDTO
            {
                Id = model.Id,
                GroupName = model.GroupName,
                Description = model.Description,
                ParentId = model.ParentId
            };
        }

        public FormGroupViewModel MapGetGroupDTOToFormGroupViewModel(GetGroupDTO model)
        {
            return new FormGroupViewModel
            {
                Id = model.Id,
                GroupName = model.GroupName,
                Description = model.Description,
                ParentId = model.ParentId
            };
        }

        public GroupDTO MapFormGroupVMToGroupDTO(FormGroupViewModel model)
        {
            return new GroupDTO
            {
                Id = model.Id,
                GroupName = model.GroupName,
                Description = model.Description,
                ParentId = model.ParentId
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

    }
}
