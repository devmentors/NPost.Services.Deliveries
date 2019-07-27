using System.Collections.Generic;
using System.Threading.Tasks;
using NPost.Services.Deliveries.Application.DTO;

namespace NPost.Services.Deliveries.Application.Services.Clients
{
    public interface IRoutingServiceClient
    {
        Task<RouteDto> GetAsync(IEnumerable<string> addresses);
    }
}