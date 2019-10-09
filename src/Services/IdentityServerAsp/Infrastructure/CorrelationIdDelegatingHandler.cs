using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    public class CorrelationIdDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public CorrelationIdDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var correlationIdHeader = _httpContextAccesor.HttpContext
                 .Request.Headers["X-Correlation-ID"];

            if (!string.IsNullOrEmpty(correlationIdHeader))
            {
                request.Headers.Add("X-Correlation-ID", new List<string>() { correlationIdHeader });
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
