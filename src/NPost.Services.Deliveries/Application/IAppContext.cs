using System.Collections.Generic;

namespace NPost.Services.Deliveries.Application
{
    public interface IAppContext
    {
        string RequestId { get; }
        string UserId { get; }
        string Role { get; }
        IDictionary<string, string> Claims { get; }
    }
}