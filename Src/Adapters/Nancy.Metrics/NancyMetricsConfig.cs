using System;
using Metrics;
using Metrics.Reports;
using Nancy.Bootstrapper;

namespace Nancy.Metrics
{
    public class NancyMetricsConfig
    {
        private readonly MetricsContext metricsContext;
        private readonly Func<HealthStatus> healthStatus;
        private readonly IPipelines nancyPipelines;

        public NancyMetricsConfig(MetricsContext metricsContext, Func<HealthStatus> healthStatus, IPipelines nancyPipelines)
        {
            this.metricsContext = metricsContext;
            this.healthStatus = healthStatus;
            this.nancyPipelines = nancyPipelines;
        }

        /// <summary>
        /// Configure global NancyFx Metrics.
        /// Available global metrics are: Request Timer, Active Requests Counter, Error Meter
        /// <code>
        /// protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        /// {
        ///     base.ApplicationStartup(container, pipelines);
        /// 
        ///     NancyMetrics.Configure()
        ///         .WithGlobalMetrics(config => config.RegisterAllMetrics(pipelines))
        ///         .WithMetricsEndpoint();
        /// }
        /// </code>
        /// </summary>
        /// <param name="config">Action to configure which global metrics to enable.</param>
        /// <param name="context">Name of the MetricsContext where to register the NancyFx metrics.</param>
        /// <returns>This instance to allow chaining of the configuration.</returns>
        public NancyMetricsConfig WithNancyMetrics(Action<NancyGlobalMetrics> config, string context = "NancyFx")
        {
            var globalMetrics = new NancyGlobalMetrics(this.metricsContext.Context(context), this.nancyPipelines);
            config(globalMetrics);
            return this;
        }

        /// <summary>
        /// Expose the metrics information at:
        /// /metrics in human readable format
        /// /metrics/json in json format
        /// <code>
        /// protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        /// {
        ///     base.ApplicationStartup(container, pipelines);
        /// 
        ///     NancyMetrics.Configure()
        ///         .WithGlobalMetrics(config => config.RegisterAllMetrics(pipelines))
        ///         .WithMetricsEndpoint(m => m.RequiresAuthentication()); // to enable authentication
        /// }
        /// </code>
        /// </summary>
        /// <param name="metricsPath">Path where to expose the metrics</param>
        /// <returns>This instance to allow chaining of the configuration.</returns>
        public NancyMetricsConfig WithMetricsModule(string metricsPath = "/metrics")
        {
            return WithMetricsModule(m => { }, c => { }, metricsPath);
        }

        /// <summary>
        /// Expose the metrics information at:
        /// /metrics in human readable format
        /// /metrics/json in json format
        /// <code>
        /// protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        /// {
        ///     base.ApplicationStartup(container, pipelines);
        /// 
        ///     NancyMetrics.Configure()
        ///         .WithGlobalMetrics(config => config.RegisterAllMetrics(pipelines))
        ///         .WithMetricsEndpoint(m => m.RequiresAuthentication()); // to enable authentication
        /// }
        /// </code>
        /// </summary>
        /// <param name="config">Action that can configure the endpoint reports</param>
        /// <param name="metricsPath">Path where to expose the metrics</param>
        /// <returns>This instance to allow chaining of the configuration.</returns>
        public NancyMetricsConfig WithMetricsModule(Action<MetricsEndpointReports> config, string metricsPath = "/metrics")
        {
            return WithMetricsModule(m => { }, config, metricsPath);
        }

        /// <summary>
        /// Expose the metrics information at:
        /// /metrics in human readable format
        /// /metrics/json in json format
        /// <code>
        /// protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        /// {
        ///     base.ApplicationStartup(container, pipelines);
        /// 
        ///     NancyMetrics.Configure()
        ///         .WithGlobalMetrics(config => config.RegisterAllMetrics(pipelines))
        ///         .WithMetricsEndpoint(m => m.RequiresAuthentication()); // to enable authentication
        /// }
        /// </code>
        /// </summary>
        /// <param name="moduleConfig">Action that can configure the Metrics Module ( for example to apply authentication )</param>
        /// <param name="config">Action that can configure the endpoint reports</param>
        /// <param name="metricsPath">Path where to expose the metrics</param>
        /// <returns>This instance to allow chaining of the configuration.</returns>
        public NancyMetricsConfig WithMetricsModule(Action<INancyModule> moduleConfig, Action<MetricsEndpointReports> config, string metricsPath = "/metrics")
        {
            var reportsConfig = new MetricsEndpointReports(this.metricsContext.DataProvider, this.healthStatus);
            config(reportsConfig);
            MetricsModule.Configure(moduleConfig, reportsConfig, metricsPath);
            return this;
        }
    }
}
