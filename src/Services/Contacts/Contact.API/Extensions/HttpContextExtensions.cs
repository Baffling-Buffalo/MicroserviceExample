using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetCorrelationId(this HttpContext context)
        {
            var correlationHeader = context.Request.Headers["X-Correlation-ID"];

            if (!string.IsNullOrWhiteSpace(correlationHeader))
                return correlationHeader;

            else return "";
        }
    }
}
