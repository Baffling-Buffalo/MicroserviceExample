using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Middleware.Multiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIGateway.Aggregator
{
    public class Api1and2Aggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<DownstreamContext> responses)
        {
            HttpContent response;
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var r in responses)
            {
                stringBuilder.Append(await r.DownstreamResponse.Content.ReadAsStringAsync());
            }
            
            response = new StringContent(stringBuilder.ToString());
            return new DownstreamResponse(response, HttpStatusCode.OK, new List<Header>(), "");
        }
    }
}
