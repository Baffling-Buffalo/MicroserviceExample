using Audit.Core;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public class AuditLogService : IAuditLogService
    {
        private List<AuditScope> scopes = new List<AuditScope>();

        public AuditLogService(IEventBus eventBus, IOptions<AppSettings> options)
        {
            Audit.Core.Configuration.DataProvider = new AuditEventBusDataProvider(eventBus, options.Value.AppName);
        }

        /// <summary>
        /// Writes custom audit log to db. Remember to disable audit on dbContext if replacing default entity framework logging
        /// </summary>
        /// <param name="auditScope"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="entity"></param>
        public async Task CreateAsync(string description, string action, string entity)
        {
            scopes.Add(await AuditScope.CreateAsync(new AuditScopeOptions()
            {
                EventType = "Custom log",
                ExtraFields = new
                {
                    Description = description,
                    Action = action,
                    Entity = entity
                },
                CreationPolicy = EventCreationPolicy.Manual
            }));
        }

        public async Task SaveAsync()
        {
            if (scopes != null && scopes.Any())
            {
                for (int i = 0; i < scopes.Count; i++)
                {
                    await scopes[i].SaveAsync();
                    scopes[i].Discard();
                }
            }
        }

        /// <summary>
        /// Writes custom audit log to db. Remember to disable audit on dbContext if replacing default entity framework logging
        /// </summary>
        /// <param name="auditScope"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        /// <param name="entity"></param>
        public void Create(string description, string action, string entity)
        {
            scopes.Add(AuditScope.Create(new AuditScopeOptions()
            {
                EventType = "Custom log",
                ExtraFields = new
                {
                    Description = description,
                    Action = action,
                    Entity = entity
                },
                CreationPolicy = EventCreationPolicy.Manual
            }));
        }

        public void Save()
        {
            if (scopes != null && scopes.Any())
            {
                for (int i = 0; i < scopes.Count; i++)
                {
                    scopes[i].Save();
                    scopes[i].Discard();
                }
            }
        }
    }
}
