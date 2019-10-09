using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Tries to get ModelState errors from the response if it's unsuccessful and save them to modelStateErrors.
        /// If none were found Throws HttpRequestException.
        /// </summary>
        /// <param name="modelStateErrors">Dictionary to hold model state errors</param>
        /// <returns>False if request is successfull. True if modelState errors were found and saved. Throws if none were found</returns>
        public static bool TrySaveModelStateErrors(this HttpResponseMessage response, ref Dictionary<string, string[]> modelStateErrors)
        {
            if (response.IsSuccessStatusCode)
                return false; // no need to look, request is successfull

            var content =  response.Content.ReadAsStringAsync().Result;
            try
            {
                modelStateErrors = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(content);
                if (modelStateErrors.Any())
                    return true;
                else
                {
                    throw new HttpRequestException();
                }
            }
            catch (Exception)
            {
                throw new HttpRequestException();
            }
        }
    }
}
