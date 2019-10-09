using System.Linq;
using Audit.Core;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Audit.EntityFramework;
using BuildingBlocks.EventBusProjects.EventBus.Abstractions;
using System.Collections.Generic;
using Identity.API.Models;
using Newtonsoft.Json;
using SharedLibraries;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Identity.API.Services;
#if NETSTANDARD1_5 || NETSTANDARD2_0 || NETSTANDARD2_1 || NET461
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
#elif NET45
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
#endif

namespace Audit.Core
{
    /// <summary>
    /// Store the audits logs in the same EntityFramework model as the audited entities.
    /// </summary>
    /// <remarks>
    /// Settings:
    /// - AuditTypeMapper: A function that maps an entity type to its audited type (i.e. Order -> OrderAudit, etc)
    /// - AuditEntityAction: An action to perform on the audit entity before saving it
    /// - IgnoreMatchedProperties: Set to true to avoid the property values copy from the entity to the audited entity (default is false)
    /// </remarks>
    public class AuditEventBusDataProvider : AuditDataProvider
    {
        private IEventBus eventBus;
        private readonly string applicationName;

        public AuditEventBusDataProvider()
        {

        }

        public AuditEventBusDataProvider(IEventBus eventBus, string applicationName)
        {
            this.eventBus = eventBus;
            this.applicationName = applicationName;
        }


        public override object InsertEvent(AuditEvent auditEvent)
        {
            List<AuditLog> audits = new List<AuditLog>();

            if ((auditEvent is AuditEventEntityFramework efEvent)) // FOR DBCONTEXT LOGS
            {
                foreach (var entry in efEvent.EntityFrameworkEvent.Entries) // MAKE LOG FOR EACH DB CHANGE
                {
                    audits.Add(new AuditLog()
                    {
                        AuditAction = entry.Action,
                        AuditData = entry.Action == "Update" ? JsonConvert.SerializeObject(entry.Changes) : JsonConvert.SerializeObject(entry.ColumnValues),
                        AuditDate = DateTime.Now,
                        AuditUsername = efEvent.Environment.UserName != "Unauthenticated" ? efEvent.Environment.UserName : efEvent.CustomFields.TryGetValue("AuditUsername", out var username) ? username.ToString() : "Unauthenticated",
                        CorrelationId = entry.CustomFields["CorrelationId"].ToString(),
                        EntityType = entry.GetEntry().Entity.GetType().Name,
                        Description = entry.CustomFields.TryGetValue("Description", out var description) ? description.ToString() : "",
                        TablePk = entry.PrimaryKey.First().Value.ToString(),
                        TableName = entry.Table,
                        ApplicationName = applicationName
                    });
                }
            }
            else // FOR CUSTOM LOGS
            {
                audits.Add(new AuditLog()
                {
                    AuditAction = auditEvent.CustomFields.TryGetValue("Action", out var action) ? action.ToString() : "",
                    AuditData = auditEvent.CustomFields.TryGetValue("Data", out var data) ? data.ToString() : "",
                    AuditDate = DateTime.Now,
                    AuditUsername = auditEvent.Environment.UserName != "Unauthenticated" ? auditEvent.Environment.UserName : auditEvent.CustomFields.TryGetValue("AuditUsername", out var username) ? username.ToString() : "Unauthenticated",
                    CorrelationId = auditEvent.CustomFields["CorrelationId"].ToString(),
                    EntityType = auditEvent.CustomFields.TryGetValue("Entity", out var entity) ? entity.ToString() : "",
                    Description = auditEvent.CustomFields.TryGetValue("Description", out var description) ? description.ToString() : "",
                    ApplicationName = applicationName
                });
            }

            eventBus.Publish(new AuditLogIntegrationEvent()
            {
                AuditLogs = audits
            });

            return 0;
        }

        public async override Task<object> InsertEventAsync(AuditEvent auditEvent)
        {
            List<AuditLog> audits = new List<AuditLog>();

            if ((auditEvent is AuditEventEntityFramework efEvent)) // FOR DBCONTEXT LOGS
            {
                foreach (var entry in efEvent.EntityFrameworkEvent.Entries)
                {
                    audits.Add(new AuditLog()
                    {
                        AuditAction = entry.Action,
                        AuditData = entry.Action == "Update" ? JsonConvert.SerializeObject(entry.Changes) : JsonConvert.SerializeObject(entry.ColumnValues),
                        AuditDate = DateTime.Now,
                        AuditUsername = efEvent.Environment.UserName != "Unauthenticated" ? efEvent.Environment.UserName : efEvent.CustomFields.TryGetValue("AuditUsername", out var username) ? username.ToString() : "Unauthenticated",
                        CorrelationId = efEvent.CustomFields["CorrelationId"].ToString(),
                        EntityType = entry.GetEntry().Entity.GetType().Name,
                        Description = efEvent.CustomFields.TryGetValue("Description", out var description) ? description.ToString() : "",
                        TablePk = entry.PrimaryKey.First().Value.ToString(),
                        TableName = entry.Table,
                        ApplicationName = applicationName
                    });
                }
            }
            else // FOR CUSTOM LOGS
            {
                audits.Add(new AuditLog() // MAKE LOG FOR EACH DB CHANGE
                {
                    AuditAction = auditEvent.CustomFields.TryGetValue("Action", out var action) ? action.ToString() : "",
                    AuditData = auditEvent.CustomFields.TryGetValue("Data", out var data) ? data.ToString() : "",
                    AuditDate = DateTime.Now,
                    AuditUsername = auditEvent.Environment.UserName != "Unauthenticated" ? auditEvent.Environment.UserName : auditEvent.CustomFields.TryGetValue("AuditUsername", out var username) ? username.ToString() : "Unauthenticated",
                    CorrelationId = auditEvent.CustomFields["CorrelationId"].ToString(),
                    EntityType = auditEvent.CustomFields.TryGetValue("Entity", out var entity) ? entity.ToString() : "",
                    Description = auditEvent.CustomFields.TryGetValue("Description", out var description) ? description.ToString() : "",
                    ApplicationName = applicationName
                });
            }

            eventBus.Publish(new AuditLogIntegrationEvent()
            {
                AuditLogs = audits
            });

            return 0;
        }



        //        private Type GetEntityType(EventEntry entry, DbContext localDbContext)
        //        {
        //            var entryType = entry.Entry.Entity.GetType();
        //            if (entryType.FullName.StartsWith("Castle.Proxies."))
        //            {
        //                entryType = entryType.GetTypeInfo().BaseType;
        //            }
        //            Type type;
        //#if NETSTANDARD2_0 || NETSTANDARD2_1
        //            IEntityType definingType = entry.Entry.Metadata.DefiningEntityType ?? localDbContext.Model.FindEntityType(entryType);
        //                type = definingType?.ClrType;
        //#elif NETSTANDARD1_5 || NET461
        //            IEntityType definingType = localDbContext.Model.FindEntityType(entryType);
        //            type = definingType?.ClrType;
        //#else
        //            type = ObjectContext.GetObjectType(entryType);
        //#endif
        //            return type;
        //        }
    }

    public class AuditEventBusMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuditLogService auditLogService;

        public AuditEventBusMiddleware(RequestDelegate next, IAuditLogService auditLogService)
        {
            _next = next;
            this.auditLogService = auditLogService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                if (httpContext == null) // Context will be null in case of event handling
                {
                    scope.Event.Environment.UserName = "Unauthenticated";
                    scope.SetCustomField("CorrelationId", "Uncorrelated");
                }
                else
                {
                    scope.Event.Environment.UserName = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : "Unauthenticated";
                    // Get or set correlationId header
                    string correlationId = "Uncorrelated";
                    if (httpContext != null)
                    {
                        if (string.IsNullOrWhiteSpace(httpContext.Request.Headers["X-Correlation-ID"]))
                            httpContext.Request.Headers["X-Correlation-ID"] = Guid.NewGuid().ToString();

                        correlationId = httpContext.Request.Headers["X-Correlation-ID"];
                    }

                    scope.SetCustomField("CorrelationId", correlationId);
                }
            });

            await _next(httpContext); // calling next middleware

        }
    }

    public static class AuditEventBusMiddlewareExtension
    {
        public static IApplicationBuilder UseAuditWithEventBus(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuditEventBusMiddleware>();
        }
    }
}