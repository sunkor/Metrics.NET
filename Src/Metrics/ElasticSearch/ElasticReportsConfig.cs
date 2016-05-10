using System;
using System.Reflection;

namespace Metrics.ElasticSearch
{
    /// <summary>
    /// Manipulates the Elastic to be broken into partitions for maintenance purposes
    /// </summary>
    public enum RollingIndexType
    {
        /// <summary>
        /// the Elastic Index would be the same as provided to Config
        /// </summary>
        None,
        /// <summary>
        /// the Elastic Index would be the INDEX-yyyy-MM-dd, use this to be able to delete Index by day for maintenance purposes
        /// </summary>
        Daily,
        /// <summary>
        /// the Elastic Index would be the INDEX-yyyy-MM, use this to be able to delete Index by month for maintenance purposes
        /// </summary>
        Monthly
    }

    public class ElasticReportsConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Index { get; set; }

        public RollingIndexType RollingIndexType { get; set; }
        /// <summary>
        /// optional, provide this interface to be reflected on the health checks document reports
        /// </summary>
        public IApplicationInfo ReportingApplication { get; set; }
    }

    public interface IApplicationInfo
    {
        string Name { get; }
        string Version { get; }
        DateTime StartTime { get; }
        TimeSpan UpTime { get; }
    }

    public class ApplicationInfo : IApplicationInfo
    {
        public ApplicationInfo(string name, string version)
        {
            StartTime = DateTime.UtcNow;
            Name = name;
            Version = version;
        }
        public ApplicationInfo() : this(Assembly.GetCallingAssembly().GetName().Name, Assembly.GetCallingAssembly().GetName().Version.ToString())
        { }

        public string Name { get; private set; }
        public DateTime StartTime { get; private set; }
        public TimeSpan UpTime { get { return DateTime.UtcNow - StartTime; } }
        public string Version { get; private set; }


    }

}
