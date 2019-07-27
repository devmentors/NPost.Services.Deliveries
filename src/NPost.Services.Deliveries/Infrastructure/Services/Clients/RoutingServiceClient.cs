using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.HTTP;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Application.Services.Clients;

namespace NPost.Services.Deliveries.Infrastructure.Services.Clients
{
    public class RoutingServiceClient : IRoutingServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public RoutingServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["routing"];
        }

        public Task<RouteDto> GetAsync(IEnumerable<string> addresses)
        {
            var addressesParam = string.Join(",", addresses.Select(a => $"\"{a}\""));

            return _httpClient.GetAsync<RouteDto>($"{_url}/route?addresses=[{addressesParam}]");
        }
    }
}