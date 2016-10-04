using System.Collections.Generic;

namespace Metrics.Visualization
{
    public class MetricsEndpointRequest
    {
        public readonly IDictionary<string, string[]> Headers;

        public MetricsEndpointRequest(IDictionary<string, string[]> headers)
        {
            this.Headers = headers;
        }
    }
}
