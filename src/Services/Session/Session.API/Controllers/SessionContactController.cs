using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.API.DTO.SessionContact;
using Session.API.Filter;
using Session.API.Models;
using Session.API.Services;

namespace Session.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionContactController : ControllerBase
    {
        private readonly SessionContext _sessionContext;
        private readonly IContactService _contactService;

        public SessionContactController(SessionContext _sessionContext, IContactService _contactService)
        {
            this._sessionContext = _sessionContext;
            this._contactService = _contactService;
        }

        /// <summary>
        /// Get SessionContacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSessionContactDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sessionContacts = _sessionContext.SessionContacts
                    .Select(s => new GetSessionContactDTO()
                    {
                        Id = s.Id,
                        FillOrder = s.FillOrder,
                        DocCompleted = s.DocCompleted,
                        ContactId = s.FContact,
                        SessionDocumentId = s.FSessionDocument
                    })
                    .ToList();

                if (sessionContacts == null)
                    return BadRequest();

                return Ok(sessionContacts);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SessionContact with passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetSessionContactDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSessionContact(int id)
        {
            try
            {
                var sessionContact = await _sessionContext.SessionContacts
                    .Where(s => s.Id == id)
                    .Select(s => new GetSessionContactDTO()
                    {
                        Id = s.Id,
                        FillOrder = s.FillOrder,
                        DocCompleted = s.DocCompleted,
                        ContactId = s.FContact,
                        SessionDocumentId = s.FSessionDocument
                    })
                    .SingleOrDefaultAsync();

                if (sessionContact == null)
                    return BadRequest();

                return Ok(sessionContact);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Create new SessionContact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionContact created</response>
        /// <response code="400">Model is not valid, Passed SessionDocument does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateSessionContactDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionDocumentExists = await _sessionContext.SessionDocuments.AnyAsync(sd => sd.Id == model.SessionDocumentId);
            if (!sessionDocumentExists)
                return BadRequest("SessionDocument does not exist");

            var contactExists = await _contactService.Exists(model.ContactId);
            if (!contactExists)
                return BadRequest("Contact does not exist");

            var sessionContact = new SessionContact()
            {
                FillOrder = model.FillOrder,
                DocCompleted = model.DocCompleted,
                FContact = model.ContactId,
                FSessionDocument = model.SessionDocumentId
            };

            try
            {
                await _sessionContext.SessionContacts.AddAsync(sessionContact);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update existing SessionContact
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionContact updated</response>
        /// <response code="400">Model is not valid; SessionContact could not be found; Passed SessionDocument does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateSessionContactDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionContact = await _sessionContext.SessionContacts.FindAsync(model.Id);
            if (sessionContact == null)
                return BadRequest();

            var sessionDocumentExists = await _sessionContext.SessionDocuments.AnyAsync(sd => sd.Id == model.SessionDocumentId);
            if (!sessionDocumentExists)
                return BadRequest("SessionDocument does not exist");

            var contactExists = await _contactService.Exists(model.ContactId);
            if (!contactExists)
                return BadRequest("Contact does not exist");

            sessionContact.FillOrder = model.FillOrder;
            sessionContact.DocCompleted = model.DocCompleted;
            sessionContact.FContact = model.ContactId;
            sessionContact.FSessionDocument = model.SessionDocumentId;

            try
            {
                _sessionContext.SessionContacts.Update(sessionContact);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete SessionContact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">SessionContact deleted</response>
        /// <response code="400">SessionContact could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var sessionContact = await _sessionContext.SessionContacts.FindAsync(id);
            if (sessionContact == null)
                return BadRequest("SessionContact with given id does not exist");

            try
            {
                _sessionContext.SessionContacts.Remove(sessionContact);
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