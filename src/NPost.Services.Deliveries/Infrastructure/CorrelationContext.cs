using System;
using Convey.MessageBrokers;

namespace NPost.Services.Deliveries.Infrastructure
{
    public class CorrelationContext : ICorrelationContext
    {
        public string CorrelationId { get; set; } = Guid.NewGuid().ToString("N");
        public string SpanContext { get; set; }
        public int Retries { get; set; }
    }
}