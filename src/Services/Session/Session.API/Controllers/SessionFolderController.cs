using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.API.DTO.SessionFolder;
using Session.API.Models;

namespace Session.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionFolderController : ControllerBase
    {
        private readonly SessionContext _sessionContext;
        public SessionFolderController(SessionContext _sessionContext)
        {
            this._sessionContext = _sessionContext;
        }

        /// <summary>
        /// Get SessionFolders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSessionFolderDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sessionFolders = _sessionContext.SessionFolders
                    .Select(sf => new GetSessionFolderDTO()
                    {
                        Id = sf.Id,
                        FolderName = sf.FolderName,
                        SessionMainId = sf.FSessionMain,
                        SessionMainName = sf.FSessionMainNavigation.SessionName
                    })
                    .ToList();

                if (sessionFolders == null)
                    return BadRequest();

                return Ok(sessionFolders);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SessionFolder with passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetSessionFolderDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSessionFolder(int id)
        {
            try
            {
                var sessionFolder = await _sessionContext.SessionFolders
                    .Where(sf => sf.Id == id)
                    .Select(sf => new GetSessionFolderDTO()
                    {
                        Id = sf.Id,
                        FolderName = sf.FolderName,
                        SessionMainId = sf.FSessionMain,
                        SessionMainName = sf.FSessionMainNavigation.SessionName
                    })
                    .SingleOrDefaultAsync();

                if (sessionFolder == null)
                    return BadRequest();

                return Ok(sessionFolder);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new SessionFolder
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionFolder created</response>
        /// <response code="400">Model is not valid, Passed SessionMain does not exist</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateSessionFolderDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionMainExists = await _sessionContext.SessionMains.AnyAsync(sm => sm.Id == model.SessionMainId);
            if (!sessionMainExists)
                return BadRequest("Session main does not exist");

            var sessionFolder = new SessionFolder()
            {
                FolderName = model.FolderName,
                FSessionMain = model.SessionMainId
            };

            try
            {
                await _sessionContext.SessionFolders.AddAsync(sessionFolder);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update existing SessionFolder
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionFolder updated</response>
        /// <response code="400">Model is not valid; SessionFolder could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateSessionFolderDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionFolder = await _sessionContext.SessionFolders.FindAsync(model.Id);
            if (sessionFolder == null)
                return BadRequest();

            var sessionMainExists = await _sessionContext.SessionMains.AnyAsync(sm => sm.Id == model.SessionMainId);
            if (!sessionMainExists)
                return BadRequest("Session main does not exist");

            sessionFolder.FolderName = model.FolderName;
            sessionFolder.FSessionMain = model.SessionMainId;
           
            try
            {
                _sessionContext.SessionFolders.Update(sessionFolder);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete SessionFolder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">SessionFolder deleted</response>
        /// <response code="400">SessionFolder could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var sessionFolder = await _sessionContext.SessionFolders.FindAsync(id);
            if (sessionFolder == null)
                return BadRequest("SessionFolder with given id does not exist");

            try
            {
                _sessionContext.SessionFolders.Remove(sessionFolder);
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