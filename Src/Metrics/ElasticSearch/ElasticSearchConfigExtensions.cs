using System;
using Metrics.ElasticSearch;
using Metrics.Reports;

namespace Metrics
{
    public static class ElasticSearchConfigExtensions
    {
        public static MetricsReports WithElasticSearch(this MetricsReports reports, string host, int port, string index, TimeSpan interval)
        {
            var bulkUri = new Uri(string.Format(@"http://{0}:{1}/_bulk", host, port));
            var nodeInfoUri = new Uri(string.Format(@"http://{0}:{1}", host, port));
            return reports.WithReport(new ElasticSearchReport(bulkUri, index, nodeInfoUri), interval);
        }

    }
}
