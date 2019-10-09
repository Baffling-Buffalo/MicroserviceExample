using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.API.DTO.SessionDocument;
using Session.API.Models;
using Session.API.Services;

namespace Session.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionDocumentController : ControllerBase
    {
        private readonly SessionContext _sessionContext;
        private readonly IFormService _formService;

        public SessionDocumentController(SessionContext _sessionContext, IFormService _formService)
        {
            this._sessionContext = _sessionContext;
            this._formService = _formService;
        }

        /// <summary>
        /// Get SessionDocuments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSessionDocumentDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sessionDocuments = _sessionContext.SessionDocuments
                    .Select(sd => new GetSessionDocumentDTO()
                    {
                        Id = sd.Id,
                        OzItemId = sd.FOzItem,
                        OzItemContent = sd.OzItemContent,
                        SessionFolderId = sd.FSessionFolder,
                        SessionFolderName = sd.FSessionFolderNavigation.FolderName,
                    })
                    .ToList();

                if (sessionDocuments == null)
                    return BadRequest();

                return Ok(sessionDocuments);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SessionDocument with passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetSessionDocumentDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSessionDocument(int id)
        {
            try
            {
                var sessionDocument = await _sessionContext.SessionDocuments
                    .Where(sd => sd.Id == id)
                    .Select(sd => new GetSessionDocumentDTO()
                    {
                        Id = sd.Id,
                        OzItemId = sd.FOzItem,
                        OzItemContent = sd.OzItemContent,
                        SessionFolderId = sd.FSessionFolder,
                        SessionFolderName = sd.FSessionFolderNavigation.FolderName,
                    })
                    .SingleOrDefaultAsync();

                if (sessionDocument == null)
                    return BadRequest();

                return Ok(sessionDocument);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new SessionDocument
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionDocument created</response>
        /// <response code="400">Model is not valid, Passed SessionFolder does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateSessionDocumentDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionFolderExists = await _sessionContext.SessionFolders.AnyAsync(sf => sf.Id == model.SessionFolderId);
            if (!sessionFolderExists)
                return BadRequest("SessionFolder does not exist");

            var ozItemExists = await _formService.Exists(model.OzItemId);
            if (!ozItemExists)
                return BadRequest("OzItem does not exist");

            var sessionDocument = new SessionDocument()
            {
                FOzItem = model.OzItemId,
                FSessionFolder = model.SessionFolderId,
                OzItemContent = model.OzItemContent
            };

            try
            {
                await _sessionContext.SessionDocuments.AddAsync(sessionDocument);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update existing SessionDocument
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionDocument updated</response>
        /// <response code="400">Model is not valid; SessionDocument could not be found; SessionFolder does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateSessionDocumentDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionDocument = await _sessionContext.SessionDocuments.FindAsync(model.Id);
            if (sessionDocument == null)
                return BadRequest();

            var sessionFolderExists = await _sessionContext.SessionFolders.AnyAsync(sf => sf.Id == model.SessionFolderId);
            if (!sessionFolderExists)
                return BadRequest("SessionFolder does not exist");

            var ozItemExists = await _formService.Exists(model.OzItemId);
            if (!ozItemExists)
                return BadRequest("OzItem does not exist");

            sessionDocument.FOzItem = model.OzItemId;
            sessionDocument.FSessionFolder = model.SessionFolderId;
            sessionDocument.OzItemContent = model.OzItemContent;

            try
            {
                _sessionContext.SessionDocuments.Update(sessionDocument);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete SessionDocument
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">SessionDocument deleted</response>
        /// <response code="400">SessionDocument does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var sessionDocument = await _sessionContext.SessionDocuments.FindAsync(id);
            if (sessionDocument == null)
                return BadRequest("SessionDocument does not exist");

            try
            {
                _sessionContext.SessionDocuments.Remove(sessionDocument);
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