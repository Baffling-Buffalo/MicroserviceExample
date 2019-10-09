using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AspNetCore.Proxy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    public class FormController : Controller
    {
        private IFormService _formService;
        private readonly IStringLocalizer<FormController> _stringLocalizer;
        
        public FormController(IFormService _formService, IStringLocalizer<FormController> _stringLocalizer)
        {
            this._formService = _formService;
            this._stringLocalizer = _stringLocalizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method that returns forms for datatable
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> GetFormsForDataTable()
        {
            var result = await _formService.GetFormsForDataTable(Request.Form);

            return result;
        }

        [HttpGet]
        public IActionResult ViewForm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Frame()
        {
            return View();
        }

        public Task GetFrame()
        {
            return _formService.GetFrame(this);
        }
    }
}