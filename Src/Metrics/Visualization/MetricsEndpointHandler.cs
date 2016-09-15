using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Metrics.Visualization
{
    public sealed class MetricsEndpointHandler
    {
        private readonly MetricsEndpoint[] endpoints;

        public MetricsEndpointHandler(IEnumerable<MetricsEndpoint> endpoints)
        {
            this.endpoints = endpoints.ToArray();
        }

        public MetricsEndpointResponse Process(string urlPath, HttpListenerContext context)
        {
            foreach (var endpoint in this.endpoints)
            {
                if (endpoint.IsMatch(urlPath))
                {
                    var request = CreateRequest(context);
                    return endpoint.ProduceResponse(request);
                }
            }

            return null;
        }

        private static MetricsEndpointRequest CreateRequest(HttpListenerContext context)
        {
            var headers = context.Request.Headers
                .AllKeys.ToDictionary(key => key, key => context.Request.Headers[key]);

            return new MetricsEndpointRequest(headers);
        }
    }
}
