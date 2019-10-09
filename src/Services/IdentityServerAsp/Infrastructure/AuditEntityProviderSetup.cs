using Audit.EntityFramework.Providers;
using Identity.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    public static class AuditEntityProviderSetup
    {
        //public static EntityFrameworkDataProvider EntityFrameworkDataProvider(IOptions<AppSettings> settings)
        //{
        //    DbContextOptions<AuditLogDbContext> dbOptions = new DbContextOptionsBuilder<AuditLogDbContext>().UseSqlServer(settings.Value.ConnectionStrings["AuditDb"], sqlServerOptionsAction: sqlOptions =>
        //    {
        //        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
        //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        //    }).Options;

        //    return new EntityFrameworkDataProvider()
        //    {
        //        DbContextBuilder = c => new AuditLogDbContext(dbOptions),
        //        AuditTypeMapper = (t, a) => typeof(AuditLog),
        //        AuditEntityAction = (ev, entry, aEntity) =>
        //        {
        //            var entity = (AuditLog)aEntity;
        //            entity.AuditData = entry.Action == "Update" ? JsonConvert.SerializeObject(entry.Changes) : JsonConvert.SerializeObject(entry.ColumnValues);
        //            entity.EntityType = entry.EntityType.Name;
        //            entity.AuditDate = DateTime.Now;
        //            entity.Title = ev.CustomFields.TryGetValue("Title", out var title) ? title.ToString() : null;
        //            entity.AuditAction = entry.Action;
        //            entity.TablePk = entry.PrimaryKey.First().Value.ToString();
        //            entity.AuditUsername = ev.Environment.UserName != "Unauthenticated" ? ev.Environment.UserName : ev.CustomFields.TryGetValue("AuditUsername", out var username) ? username.ToString() : "Unauthenticated";
        //            entity.TableName = entry.Table;
        //            entity.CorrelationId = ev.CustomFields["CorrelationId"].ToString();
        //            return true;
        //        },
        //        IgnoreMatchedProperties = true
        //    };
        //}
    }
}
