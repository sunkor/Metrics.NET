using System.Text;
using Metrics.Reports;

namespace Metrics.Reporters
{
    public static class ReporterConfig
    {
        public static MetricsEndpointReports WithTextReportEndpoint(this MetricsEndpointReports reports, string endpoint)
        {
            return reports.WithEndpointReport(endpoint, StringReport.RenderMetrics, "text/plain", Encoding.UTF8);
        }
    }
}
