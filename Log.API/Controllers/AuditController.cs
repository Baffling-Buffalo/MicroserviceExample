using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Audit.Core;
using Audit.EntityFramework;
using DataTablesParser;
using Log.API.Filter;
using Log.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SharedLibraries;

namespace Log.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    //[PermissionClaimFilter()]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly AuditLogDbContext dbContext;

        public AuditController(AuditLogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{correlationId}")]
        [ProducesResponseType(typeof(AuditGroup), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAuditGroup(string correlationId)
        {
            var logs = await dbContext.AuditLogs.Where(a => a.CorrelationId == correlationId)
                                            .OrderBy(a => a.AuditDate)
                                            .ToListAsync();

            if (!logs.Any())
                return BadRequest();

            return Ok(new AuditGroup()
            {
                CorrelationId = correlationId,
                Logs = logs,
                ActionType = logs.First().AuditAction ?? "",
                CreationDate = logs.First().AuditDate,
                EntityType = logs.First().EntityType ?? "",
                Action = string.IsNullOrEmpty(logs.First().AuditAction) ? logs.First().Description : logs.First().AuditAction,
                Username = logs.First().AuditUsername,
            });
        }

        [HttpPost]
        [Route("datatable")]
        [PermissionClaimFilter(Permissions.AuditRead)]
        [ProducesResponseType(typeof(Results<AuditGroup>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetAuditGroupsForDatatable([FromBody]AuditsForDTParamsDTO model)
        {
            var query = dbContext.AuditLogs.GroupBy(
                a => a.CorrelationId,
                a => a,
                (key, als) => new AuditGroup()
                {
                    Logs = als.OrderBy(a => a.AuditDate) // Order chronologically cause we'll take attributes from first log created
                                .ToList(), 
                    CorrelationId = key
                });

            query = query.Select(ag =>
                new AuditGroup()
                {
                    CorrelationId = ag.CorrelationId,
                    Logs = ag.Logs,
                    ActionType = ag.Logs.First().AuditAction ?? "",
                    CreationDate = ag.Logs.First().AuditDate,
                    EntityType = ag.Logs.First().EntityType ?? "",
                    Action = string.IsNullOrEmpty(ag.Logs.First().AuditAction) ? ag.Logs.First().Description : ag.Logs.First().AuditAction,
                    Username = ag.Logs.First().AuditUsername
                }
            );

            if (model.StartDate.HasValue)
                query = query.Where(a => a.CreationDate > model.StartDate);

            if (model.EndDate.HasValue)
                query = query.Where(a => a.CreationDate < model.EndDate);

            if (!string.IsNullOrWhiteSpace(model.ActionType))
                query = query.Where(a => a.ActionType != null && a.ActionType == model.ActionType);

            var keyValues = model.DataTableParameters.Select(kv => new KeyValuePair<string, StringValues>(kv.Key, new StringValues(kv.Value)));

            try
            {
                return Ok(new Parser<AuditGroup>(keyValues, query).Parse());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationIds"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteLogGroups([Required][FromBody]string[] correlationIds)
        {
            List<Models.AuditLog> logsToDelete = new List<Models.AuditLog>();
            foreach (var id in correlationIds)
            {
                var logsInDb = await dbContext.AuditLogs.Where(a => a.CorrelationId == id).ToListAsync();
                if (!logsInDb.Any())
                    return BadRequest();

                logsToDelete.AddRange(logsInDb);
            }

            try
            {
                dbContext.RemoveRange(logsToDelete);
                dbContext.AddAuditCustomField("Title", "Deleting logs");
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }
    }
}
