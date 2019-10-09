using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Controllers;
using WebMVC.Services.ModelDTOs;
using WebMVC.Services.ModelDTOs.Form;
using WebMVC.ViewModels.FormGroup;

namespace WebMVC.Services
{
    public interface IFormService
    {
        /// <summary>
        /// Adds ModelErrors to passed ModelState if any were found by last service method
        /// </summary>
        /// <param name="modelState"></param>
        void Validate(ModelStateDictionary modelState);

        //Form
        Task<string> GetFormsForDataTable(IFormCollection formCollection);
        Task GetFrame(FormController controller);

        //Group
        Task<GetGroupDTO> GetGroup(int groupId);
        Task<string> GetGroupsForComboTreePlugin(List<int> groupIds = null);
        Task<List<GroupTreeNode>> GetGroupsForTreeTable();
        Task CreateGroup(GroupDTO model);
        Task UpdateGroup(GroupDTO model);
        Task<DeleteDTO> DeleteGroups(List<int> groupIds);
        GroupDTO MapGroupVMToGroupDTO(FormGroupViewModel model);
        FormGroupViewModel MapGetGroupDTOToFormGroupViewModel(GetGroupDTO model);
        GroupDTO MapFormGroupVMToGroupDTO(FormGroupViewModel model);
    }
}
