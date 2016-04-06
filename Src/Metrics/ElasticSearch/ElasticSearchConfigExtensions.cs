using System;
using Metrics.ElasticSearch;
using Metrics.Reports;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace Metrics
{
    public static class ElasticSearchConfigExtensions
    {
        public static MetricsReports WithElasticSearch(this MetricsReports reports, string host, int port, string index, TimeSpan interval)
        {
            var uri = new Uri(string.Format(@"http://{0}:{1}/_bulk", host, port));
            Task<ElasticSearchNodeInfo> nodeInfo = GetElasticSearchNodeInfoAsync(new Uri(string.Format(@"http://{0}:{1}", host, port)));
            return reports.WithReport(new ElasticSearchReport(uri, index, nodeInfo), interval);
        }

        private async static Task<ElasticSearchNodeInfo> GetElasticSearchNodeInfoAsync(Uri uri) {
            string json;
            using (var client = new WebClient())
            {
                json = await client.DownloadStringTaskAsync(uri);
            }
            var deserializer = new DataContractJsonSerializer(typeof(ElasticSearchNodeInfo));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return (ElasticSearchNodeInfo)deserializer.ReadObject(stream);
        }
    }
}
