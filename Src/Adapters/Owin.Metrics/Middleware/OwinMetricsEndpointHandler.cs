using System.Collections.Generic;
using Metrics.Visualization;

namespace Owin.Metrics.Middleware
{
    public class OwinMetricsEndpointHandler : AbstractMetricsEndpointHandler<IDictionary<string, object>>
    {
        public OwinMetricsEndpointHandler(IEnumerable<MetricsEndpoint> endpoints) : base(endpoints) { }

        protected override MetricsEndpointRequest CreateRequest(IDictionary<string, object> requestInfo)
        {
            var headers = requestInfo["owin.RequestHeaders"] as IDictionary<string, string>;

            return new MetricsEndpointRequest(headers);
        }
    }
}
