using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.API.DTO.SessionDocumentRole;
using Session.API.Models;

namespace Session.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionDocumentRoleController : ControllerBase
    {
        private readonly SessionContext _sessionContext;

        public SessionDocumentRoleController(SessionContext _sessionContext)
        {
            this._sessionContext = _sessionContext;
        }

        /// <summary>
        /// Get SessionDocumentRoles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSessionDocumentRoleDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sessionDocumentRoles = _sessionContext.SessionDocumentRoles
                    .Select(sd => new GetSessionDocumentRoleDTO()
                    {
                        Id = sd.Id,
                        FieldId = sd.FieldId,
                        FieldType = sd.FieldType,
                        FieldDescription = sd.FieldDescription,
                        SessionContactId = sd.FSessionContact
                    })
                    .ToList();

                if (sessionDocumentRoles == null)
                    return BadRequest();

                return Ok(sessionDocumentRoles);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SessionDocumentRole with passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetSessionDocumentRoleDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSessionDocumentRole(int id)
        {
            try
            {
                var sessionDocumentRole = await _sessionContext.SessionDocumentRoles
                    .Where(sd => sd.Id == id)
                    .Select(sd => new GetSessionDocumentRoleDTO()
                    {
                        Id = sd.Id,
                        FieldId = sd.FieldId,
                        FieldType = sd.FieldType,
                        FieldDescription = sd.FieldDescription,
                        SessionContactId = sd.FSessionContact
                    })
                    .SingleOrDefaultAsync();

                if (sessionDocumentRole == null)
                    return BadRequest();

                return Ok(sessionDocumentRole);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new SessionDocumentRole
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionDocumentRole created</response>
        /// <response code="400">Model is not valid, Passed SessionContact does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateSessionDocumentRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionContactExists = await _sessionContext.SessionContacts.AnyAsync(sc => sc.Id == model.SessionContactId);
            if (!sessionContactExists)
                return BadRequest("SessionContact does not exist");

            var sessionDocumentRole = new SessionDocumentRole()
            {
                FieldId = model.FieldId,
                FieldType = model.FieldType,
                FieldDescription = model.FieldDescription,
                FSessionContact=model.SessionContactId
            };

            try
            {
                await _sessionContext.SessionDocumentRoles.AddAsync(sessionDocumentRole);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update existing SessionDocumentRole
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionDocumentRole updated</response>
        /// <response code="400">Model is not valid; SessionDocumentRole could not be found; SessionContact does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateSessionDocumentRoleDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionDocumentRole = await _sessionContext.SessionDocumentRoles.FindAsync(model.Id);
            if (sessionDocumentRole == null)
                return BadRequest();

            var sessionContactExists = await _sessionContext.SessionContacts.AnyAsync(sc => sc.Id == model.SessionContactId);
            if (!sessionContactExists)
                return BadRequest("SessionContact does not exist");

            sessionDocumentRole.FieldId = model.FieldId;
            sessionDocumentRole.FieldType = model.FieldType;
            sessionDocumentRole.FieldDescription = model.FieldDescription;
            sessionDocumentRole.FSessionContact = model.SessionContactId;

            try
            {
                _sessionContext.SessionDocumentRoles.Update(sessionDocumentRole);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete SessionDocumentRole
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">SessionDocumentRole deleted</response>
        /// <response code="400">SessionDocumentRole does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var sessionDocument = await _sessionContext.SessionDocumentRoles.FindAsync(id);
            if (sessionDocument == null)
                return BadRequest("SessionDocumentRole does not exist");

            try
            {
                _sessionContext.SessionDocumentRoles.Remove(sessionDocument);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}