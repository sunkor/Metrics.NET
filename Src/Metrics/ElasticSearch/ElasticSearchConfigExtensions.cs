using System;
using Metrics.ElasticSearch;
using Metrics.Reports;

namespace Metrics
{
    public static class ElasticSearchConfigExtensions
    {
        public static MetricsReports WithElasticSearch(this MetricsReports reports, string host, int port, string index, TimeSpan interval)
        {
            var bulkUri = new Uri($@"http://{host}:{port}/_bulk");
            var nodeInfoUri = new Uri($@"http://{host}:{port}");
            return reports.WithReport(new ElasticSearchReport(bulkUri, index, nodeInfoUri), interval);
        }

        public static MetricsReports WithElasticSearch(this MetricsReports reports, ElasticReportsConfig reportConfig, TimeSpan interval)
        {
            var bulkUri = new Uri($@"http://{reportConfig.Host}:{reportConfig.Port}/_bulk");
            var nodeInfoUri = new Uri($@"http://{reportConfig.Host}:{reportConfig.Port}");

            return reports.WithReport(new ElasticSearchReport(bulkUri, reportConfig.Index, nodeInfoUri, reportConfig.RollingIndexType), interval);
        }

    }
}
