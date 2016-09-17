using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Metrics.Visualization
{
    public sealed class MetricsEndpointHandler : AbstractMetricsEndpointHandler<HttpListenerContext>
    {
        public MetricsEndpointHandler(IEnumerable<MetricsEndpoint> endpoints) : base(endpoints) { }

        protected override MetricsEndpointRequest CreateRequest(HttpListenerContext requestInfo)
        {
            var headers = requestInfo.Request.Headers
                .AllKeys.ToDictionary(key => key, key => requestInfo.Request.Headers[key]);

            return new MetricsEndpointRequest(headers);
        }
    }
}
