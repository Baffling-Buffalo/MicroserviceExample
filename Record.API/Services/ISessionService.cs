using Microsoft.AspNetCore.Mvc;
using Record.API.DTO.SessionContact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Record.API.Services
{
    public interface ISessionService
    {
        Task CreateSessionContact(CreateSessionContactDTO model);
    }
}
