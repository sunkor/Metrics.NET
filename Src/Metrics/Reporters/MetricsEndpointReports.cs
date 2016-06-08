using System;
using System.Collections.Generic;
using System.Text;
using Metrics.MetricData;
using Metrics.Visualization;

namespace Metrics.Reports
{
    public sealed class MetricsEndpointReports : Utils.IHideObjectMembers
    {
        private readonly MetricsDataProvider metricsDataProvider;
        private readonly Func<HealthStatus> healthStatus;

        private readonly List<MetricsEndpoint> endpoints = new List<MetricsEndpoint>();

        internal IReadOnlyList<MetricsEndpoint> Endpoints => this.endpoints;

        public MetricsEndpointReports(MetricsDataProvider metricsDataProvider, Func<HealthStatus> healthStatus)
        {
            this.metricsDataProvider = metricsDataProvider;
            this.healthStatus = healthStatus;
        }

        public MetricsEndpointReports WithEndpointReport(string endpoint, Func<MetricsData, Func<HealthStatus>, string> contentFactory, string contentType, Encoding encoding)
        {
            var metricsEndpoint = new MetricsEndpoint(endpoint, () => contentFactory(this.metricsDataProvider.CurrentMetricsData, this.healthStatus), contentType, encoding);
            this.endpoints.Add(metricsEndpoint);
            return this;
        }
    }
}
