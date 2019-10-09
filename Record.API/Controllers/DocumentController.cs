using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Record.API.DTO.SessionContact;
using Record.API.Models;
using Record.API.Services;

namespace Record.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public DocumentController(IConfiguration _configuration, IHttpClientFactory _httpClientFactory)
        {
            this._configuration = _configuration;
            this._httpClientFactory = _httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
        }

        // POST: api/document
        [HttpPost]
        public async Task<IActionResult> GenerateDocument([FromBody] JsonSet json)
        {
            try
            {
                var actionResult = await SaveDocument(json);


              
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        private async Task<IActionResult> SaveDocument(JsonSet json)
        {
            try
            {
                var data = GetFormData(json);
                var jsondata = JsonConvert.SerializeObject(data);
                var fileName = GetFileName(json);
                var sourceFile = GetSourceFile(json);

                var keyValues = new Dictionary<string, string>();
                keyValues.Add("jsondata", jsondata);
                keyValues.Add("fileName", fileName);
                keyValues.Add("sourceFile", sourceFile);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_configuration.GetSection("SaveDocumentRequestUrl").Value),
                    Content = new FormUrlEncodedContent(keyValues)
                };
                request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");

                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        private FormData GetFormData(JsonSet json)
        {
            var formData = new FormData();
            formData.DirectorateName = json.NAZIV_DIR;
            formData.Number = json.ARHIVSKI_BR_NAS;
            formData.Date = json.DATUM;
            formData.EmployeeEmail = json.EMAIL_RADNIKA;
            formData.ManagerEmail = json.EMAIL_RUKOVODIOCA;
            formData.SignatoryPosition = json.NAZIV_POTPISNIKA;
            formData.SignatoryFullName = json.POTPISNIK_PREZIME_IME;
            formData.PreambleText = Preamble.GeneratePreamble(json.ID_ODSUSTVA);
            formData.MainText = MainText.GenerateMainText(json);
            formData.RationaleText = Rationale.GenerateRationaleText(json.ID_ODSUSTVA);
            if (json.POL == "Z")
                formData.Deliver = "Zaposlenoj";
            else if (json.POL == "M")
                formData.Deliver = "Zaposlenom";

            return formData;
        }

        //PO_YYYY_IdOdsustva_Radbr_IdResenja_hhmiss(vreme generisanja fajla)
        private string GetFileName(JsonSet json)
        {
            StringBuilder fileName = new StringBuilder();
            fileName.Append("PO");
            fileName.Append("_" + DateTime.Now.Year.ToString());
            fileName.Append("_" + json.ID_ODSUSTVA);
            fileName.Append("_" + json.RADBR);
            fileName.Append("_" + json.ID_RESENJA);
            fileName.Append("_" + DateTime.Now.ToString("HHmmss"));

            return fileName.ToString();
        }

        private string GetSourceFile(JsonSet json)
        {
            string sourceFile = "";
            //Grupa izvestaja postoji samo za PO. Za GO je vrednost 0
            if (json.GRUPA_IZVESTAJA.Equals("0")) //TO DO: Godisnji odmori
            {
            }
            else
            {
                sourceFile = "/algotech/POTelekom.ozr"; //template za Telekom - Placena odsustva
            }

            return sourceFile;
        }
    }
}