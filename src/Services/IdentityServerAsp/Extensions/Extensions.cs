using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Identity.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Determines whether the client is configured to use PKCE.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="client_id">The client identifier.</param>
        /// <returns></returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }

        /// <summary>
        /// See if this enumeration has all items in passed enumeration. Returns true if passed enum is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            if (!b.Any()) return true;
            return !b.Except(a).Any();
        }

        /// <summary>
        /// See if this enumeration has all items in passed enumeration. Returns true if passed enum is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public async static Task<bool> ContainsAllItems<T>(this IAsyncEnumerable<T> a, IAsyncEnumerable<T> b)
        {
            if (! await b.Any()) return true;
            return ! await b.Except(a).Any();
        }

        /// <summary>
        /// See if this enumeration has all items in passed enumeration. Returns true if passed enum is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool ContainsAllItems<T>(this List<T> a, List<T> b)
        {
            if (!b.Any()) return true;
            return !b.Except(a).Any();
        }

        public static int[] ToIntArray(this string str, char? splitByChar = null)
        {
            try
            {
                splitByChar = splitByChar ?? ',';
                int[] ia = str.Split(splitByChar.Value).Select(n => Convert.ToInt32(n)).ToArray();
                return ia;
            }
            catch (Exception)
            {
                return new int[0];
            }
            
        }

        /// <summary>
        /// Tries to get ModelState errors from the response if it's unsuccessful and save them to modelStateErrors.
        /// If none were found Throws HttpRequestException.
        /// Returns false if request is successfull. True if modelState errors were found and saved. Throws if none were found
        /// </summary>
        /// <param name="modelStateErrors">Dictionary to hold model state errors</param>
        /// <returns></returns>
        public static bool TrySaveModelStateErrors(this HttpResponseMessage response, ref Dictionary<string, string[]> modelStateErrors,  bool throwException = true)
        {
            if (response.IsSuccessStatusCode)
                return false; // no need to look, request is successfull

            var content = response.Content.ReadAsStringAsync().Result;
            try
            {
                modelStateErrors = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(content);
                if (modelStateErrors.Any())
                    return true;
                else
                {
                    if (throwException)
                        throw new HttpRequestException();
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                if (throwException)
                    throw new HttpRequestException();
                else
                    return false;
            }
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string GetCorrelationId(this IHttpContextAccessor context)
        {
            var correlationHeader = context.HttpContext.Request.Headers["X-Correlation-ID"];

            if (!string.IsNullOrWhiteSpace(correlationHeader))
                return correlationHeader;

            else return "";
        }

        public static string GetCorrelationId(this HttpContext context)
        {
            var correlationHeader = context.Request.Headers["X-Correlation-ID"];

            if (!string.IsNullOrWhiteSpace(correlationHeader))
                return correlationHeader;

            else return "";
        }
    }
}
