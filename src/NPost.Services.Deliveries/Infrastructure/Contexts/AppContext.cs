using System;
using System.Collections.Generic;
using NPost.Services.Deliveries.Application;

namespace NPost.Services.Deliveries.Infrastructure.Contexts
{
    internal class AppContext : IAppContext
    {
        public string RequestId { get; }
        public string UserId { get; }
        public string Role { get; }
        public IDictionary<string, string> Claims { get; }

        internal AppContext()
        {
            RequestId = Guid.NewGuid().ToString("N");
        }

        internal AppContext(CorrelationContext context)
        : this(context.CorrelationId, context.UserId, context.Role, context.Claims)
        {
        }

        internal AppContext(string correlationId, string userId, string role, IDictionary<string, string> claims)
        {
            RequestId = correlationId;
            UserId = userId;
            Role = role;
            Claims = claims;
        }
    }
}