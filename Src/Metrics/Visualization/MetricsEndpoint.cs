using System;

namespace Metrics.Visualization
{
    public sealed class MetricsEndpoint
    {
        private readonly Func<string> contentFactory;

        public readonly string Endpoint;
        public readonly string ContentType;
        public string ProduceContent() => this.contentFactory();

        public MetricsEndpoint(string endpoint, Func<string> contentFactory, string contentType)
        {
            this.Endpoint = endpoint;
            this.ContentType = contentType;

            this.contentFactory = contentFactory;
        }
    }
}
