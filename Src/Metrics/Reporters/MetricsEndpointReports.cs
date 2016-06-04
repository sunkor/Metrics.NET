using System;
using System.Collections.Generic;
using System.Text;
using Metrics.Visualization;

namespace Metrics.Reports
{
    public sealed class MetricsEndpointReports : Utils.IHideObjectMembers
    {
        private readonly List<MetricsEndpoint> endpoints = new List<MetricsEndpoint>();

        internal IReadOnlyList<MetricsEndpoint> Endpoints => this.endpoints;

        public MetricsEndpointReports() { }

        public MetricsEndpointReports WithEndpointReport(string endpoint, Func<string> contentFactory, string contentType, Encoding encoding)
        {
            var metricsEndpoint = new MetricsEndpoint(endpoint, contentFactory, contentType, encoding);
            this.endpoints.Add(metricsEndpoint);
            return this;
        }
    }
}
