using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IAuditLogService
    {
        Task CreateAsync(string description, string action, string entity);
        Task SaveAsync();
        void Create(string description, string action, string entity);
        void Save();
    }
}
