using DataTablesParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels.Contact;
using WebMVC.ViewModels.List;

namespace WebMVC.Services
{
    public interface IContactService
    {
        /// <summary>
        /// Adds ModelErrors to passed ModelState if any were found by last service method
        /// </summary>
        /// <param name="modelState"></param>
        void Validate(ModelStateDictionary modelState);

        //CONTACT
        Task<GetContactDTO> GetContact(int contactId);
        Task<List<GetContactDTO>> GetContacts(List<int> contactIds = null);
        //Task<List<ContactParserDTO>> GetContactsForDataTable(string[] listIds=null, bool active = true);
        Task<string> GetContactsForDataTable(IFormCollection formCollection, List<int> listIds = null, bool? active = null, List<int> excludeIds = null);
        Task<List<GetContactDTO>> GetContactsInList(int listId);
        Task CreateContact(ContactDTO model);
        Task UpdateContact(ContactDTO model);
        Task DeleteContacts(List<int> ids);
        Task ActivateContacts(List<int> contactIds);
        Task DeactivateContacts(List<int> contactIds);
        Task AddContactsToLists(ContactListDTO model);
        Task RemoveContactsFromLists(ContactListDTO model);
        ContactDTO MapContactVMToContactDTO(ContactViewModel model);
        ContactViewModel MapGetContactDTOToContactViewModel(GetContactDTO model);
        ContactDetailsViewModel MapGetContactDTOToContactDetailsViewModel(GetContactDTO model);

        //LIST
        Task<GetListDTO> GetList(int listId);
        Task<List<GetListDTO>> GetLists(List<int> listIds = null);
        Task<string> GetListsForDataTable(IFormCollection formCollection, int? parentId = null);
        Task<string> GetListsForComboTreePlugin(int[] listIds = null);
        Task<List<ListTreeNode>> GetListsForListTree(int[] listIds = null);
        Task<List<GetListDTO>> GetListsInContact(int contactId);
        Task<List<GetListDTO>> GetListsOfContacts(List<int> contactIds);
        Task<List<GetListDTO>> GetChildren(int listId);
        Task CreateList(ListDTO model);
        Task UpdateList(ListDTO model);
        Task<DeleteDTO> DeleteLists(List<int> ids);
        //Task<DeleteListDTO> DeleteLists(List<int> ids, bool deleteSubLists);
        ListDTO MapListVMToListDTO(ListViewModel model);
        ListViewModel MapGetListDTOToListViewModel(GetListDTO model);
        ListDetailsViewModel MapGetListDTOToListDetailsViewModel(GetListDTO model);
    }
}
