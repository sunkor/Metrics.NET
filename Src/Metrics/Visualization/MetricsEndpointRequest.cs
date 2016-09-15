using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metrics.Reports;

namespace Metrics.Visualization
{
    public class MetricsEndpointRequest
    {
        public readonly IDictionary<string, string> Headers;

        public MetricsEndpointRequest(IDictionary<string, string> headers)
        {
            this.Headers = headers;
        }
    }
}
