using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class AcceptLanguageHeaderDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IOptions<AppSettings> settings;

        public AcceptLanguageHeaderDelegatingHandler(IHttpContextAccessor httpContextAccesor, IOptions<AppSettings> settings)
        {
            _httpContextAccesor = httpContextAccesor;
            this.settings = settings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var defaultLanguage = settings.Value.DefaultCulture; // TODO: after the culture is set to language cookie take it from cookie

            var acceptLanguage = _httpContextAccesor.HttpContext
                 .Request.Headers["Accept-Language"];

            if (!string.IsNullOrEmpty(defaultLanguage))
            {
                request.Headers.Add("Accept-Language", new List<string>() { defaultLanguage });
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
