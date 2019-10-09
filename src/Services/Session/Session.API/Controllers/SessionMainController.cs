using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Session.API.DTO.SessionMain;
using Session.API.Models;

namespace Session.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionMainController : ControllerBase
    {
        private readonly SessionContext _sessionContext;

        public SessionMainController(SessionContext _sessionContext)
        {
            this._sessionContext = _sessionContext;
        }

        /// <summary>
        /// Get SessionMains
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSessionMainDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sessionMains = _sessionContext.SessionMains
                    .Select(s => new GetSessionMainDTO()
                    {
                        Id = s.Id,
                        SessionName = s.SessionName,
                        Description = s.Description,
                        DateStart = s.DateStart,
                        DateEnd = s.DateEnd,
                        TimeStart = s.TimeStart,
                        TimeEnd = s.TimeEnd,
                        IsInfinite = s.IsInfinite,
                        IsTemplate = s.IsTemplate,
                        SessionFolders = s.SessionFolders.Select(sf => new { id = sf.Id, name = sf.FolderName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .ToList();

                if (sessionMains == null)
                    return BadRequest();

                return Ok(sessionMains);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get SessionMain with passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetSessionMainDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetSessionMain(int id)
        {
            try
            {
                var sessionMain = await _sessionContext.SessionMains
                    .Where(s => s.Id == id)
                    .Select(s => new GetSessionMainDTO()
                    {
                        Id = s.Id,
                        SessionName = s.SessionName,
                        Description = s.Description,
                        DateStart = s.DateStart,
                        DateEnd = s.DateEnd,
                        TimeStart = s.TimeStart,
                        TimeEnd = s.TimeEnd,
                        IsInfinite = s.IsInfinite,
                        IsTemplate = s.IsTemplate,
                        SessionFolders = s.SessionFolders.Select(sf => new { id = sf.Id, name = sf.FolderName }).ToDictionary(x => x.id, x => x.name)
                    })
                    .SingleOrDefaultAsync();

                if (sessionMain == null)
                    return BadRequest();

                return Ok(sessionMain);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create new SessionMain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionMain created</response>
        /// <response code="400">Model is not valid</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateSessionMainDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionMain = new SessionMain()
            {
                SessionName = model.SessionName,
                Description = model.Description,
                DateStart = model.DateStart,
                DateEnd = model.DateEnd,
                TimeStart = model.TimeStart,
                TimeEnd = model.TimeEnd,
                IsInfinite = model.IsInfinite,
                IsTemplate = model.IsTemplate
            };

            try
            {
                await _sessionContext.SessionMains.AddAsync(sessionMain);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update existing SessionMain
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">SessionMain updated</response>
        /// <response code="400">Model is not valid; SessionMain could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UpdateSessionMainDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sessionMain = await _sessionContext.SessionMains.FindAsync(model.Id);
            if (sessionMain == null)
                return BadRequest();

            sessionMain.SessionName = model.SessionName;
            sessionMain.Description = model.Description;
            sessionMain.DateStart = model.DateStart;
            sessionMain.DateEnd = model.DateEnd;
            sessionMain.TimeStart = model.TimeStart;
            sessionMain.TimeEnd = model.TimeEnd;
            sessionMain.IsInfinite = model.IsInfinite;
            sessionMain.IsTemplate = model.IsTemplate;

            try
            {
                _sessionContext.SessionMains.Update(sessionMain);
                await _sessionContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete SessionMain
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">SessionMain deleted</response>
        /// <response code="400">SessionMain could not be found</response>
        /// <response code="500">Exeption while saving data</response>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var sessionMain = await _sessionContext.SessionMains.FindAsync(id);
            if (sessionMain == null)
                return BadRequest("Session with given id does not exist");

            try
            {
                _sessionContext.SessionMains.Remove(sessionMain);
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