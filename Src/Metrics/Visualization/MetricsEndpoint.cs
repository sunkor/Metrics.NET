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
            if (contentFactory == null)
            {
                throw new ArgumentNullException("contentFactory");
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentException("Invalid content type");
            }

            this.Endpoint = NormalizeEndpoint(endpoint);
            this.ContentType = contentType;

            this.contentFactory = contentFactory;
        }

        private string NormalizeEndpoint(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint) || endpoint == "/")
            {
                throw new ArgumentException("Endpoint path cannot be empty");
            }

            if (!endpoint.StartsWith("/"))
            {
                endpoint = '/' + endpoint;
            }

            return endpoint;
        }
    }
}
