using System;
using FluentAssertions;
using Metrics.Visualization;
using Xunit;

namespace Metrics.Tests.Visualization
{
    public class MetricsEndpointTests
    {
        [Fact]
        public void MetricsEndpoint_CannotCreateWithEmptyPath()
        {
            var action1 = new Action(() => new MetricsEndpoint("", c => new MetricsEndpointResponse("test", "text/plain")));
            var action2 = new Action(() => new MetricsEndpoint(" ", c => new MetricsEndpointResponse("test", "text/plain")));
            var action3 = new Action(() => new MetricsEndpoint("/", c => new MetricsEndpointResponse("test", "text/plain")));

            action1.ShouldThrow<ArgumentException>();
            action2.ShouldThrow<ArgumentException>();
            action3.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void MetricsEndpoint_PathAlwaysStartsWithForwardSlash()
        {
            var endpoint1 = new MetricsEndpoint("test", c => new MetricsEndpointResponse("test", "text/plain"));
            endpoint1.Endpoint.StartsWith("/").Should().BeTrue();
            endpoint1.Endpoint.StartsWith("//").Should().BeFalse();

            var endpoint2 = new MetricsEndpoint("/test", c => new MetricsEndpointResponse("test", "text/plain"));
            endpoint2.Endpoint.StartsWith("/").Should().BeTrue();
            endpoint2.Endpoint.StartsWith("//").Should().BeFalse();
        }
    }
}
