using Identity.API.Services.ModelDTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IContactService 
    {
        /// <summary>
        /// Adds ModelErrors to passed ModelState if any were found and stored by last service method
        /// </summary>
        /// <param name="modelState"></param>
        void Validate(ModelStateDictionary modelState);
        Task<int> CreateContact(NewContactDTO newContact);
        Task DeleteContacts(List<int> contactIds);
    }
}
