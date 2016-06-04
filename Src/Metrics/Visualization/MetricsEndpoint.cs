using System;
using System.Text;

namespace Metrics.Visualization
{
    public sealed class MetricsEndpoint
    {
        private readonly Func<string> contentFactory;

        public readonly string Endpoint;
        public readonly string ContentType;
        public readonly Encoding Encoding;
        public string ProduceContent() => this.contentFactory();

        public MetricsEndpoint(string endpoint, Func<string> contentFactory, string contentType)
            : this(endpoint, contentFactory, contentType, Encoding.UTF8) { }


        public MetricsEndpoint(string endpoint, Func<string> contentFactory, string contentType, Encoding encoding)
        {
            if (contentFactory == null)
            {
                throw new ArgumentNullException("contentFactory");
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentException("Invalid content type");
            }

            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }

            this.Endpoint = NormalizeEndpoint(endpoint);
            this.ContentType = contentType;
            this.Encoding = encoding;

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
