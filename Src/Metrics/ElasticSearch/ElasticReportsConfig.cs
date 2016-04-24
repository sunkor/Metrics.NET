namespace Metrics.ElasticSearch
{
    public enum RollingIndexType
    {
        None,
        Daily,
        Monthly
    }

    public class ElasticReportsConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Index { get; set; }

        public RollingIndexType RollingIndexType { get; set; }
    }
}
