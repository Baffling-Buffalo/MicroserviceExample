using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataTablesParser;
using Form.API.DTO;
using Form.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Form.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : Controller
    {
        private readonly FormContext _formContext;

        public FormController(FormContext formContext)
        {
            this._formContext = formContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var form = _formContext.OzItems;
                return Ok(form);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("DataTable")]
        [ProducesResponseType(typeof(IQueryable<FormDataTableDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFormsForDataTable()
        {
            IQueryable<FormDataTableDTO> forms;

            try
            {
                var query = _formContext.OzItems.Select(i => i);

                forms = query
                        .Select(i => new FormDataTableDTO()
                        {
                            Id = i.Id,
                            Name = i.Name,
                            FullPath = i.FullPath,
                            Description = i.Description,
                            ChkoutFolder = i.ChkoutFolder
                        });

                var parser = new Parser<FormDataTableDTO>(Request.Form, forms);

                return Ok(parser.Parse());
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Method returns true if form with passed id exists, otherwise it returns false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Exists/{id}")]
        [HttpGet]
        public async Task<bool> Exists(int id)
        {
            var exists = false;

            var formExists = await _formContext.OzItems.AnyAsync(f => f.Id == id);

            if (formExists)
                exists = true;

            return exists;
        }
    }
}