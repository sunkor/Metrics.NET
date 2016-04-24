using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metrics.ElasticSearch
{
    public class ElasticReportsConfig
    {
        public enum RollingIndex
        {
            None,
            Daily,
            Monthly
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Index { get; set; }

        public RollingIndex RollingIndexType { get; set; }
    }
}
