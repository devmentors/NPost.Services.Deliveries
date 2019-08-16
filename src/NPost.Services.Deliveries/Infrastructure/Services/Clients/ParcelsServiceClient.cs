using System;
using System.Threading.Tasks;
using Convey.HTTP;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Application.Services.Clients;

namespace NPost.Services.Deliveries.Infrastructure.Services.Clients
{
    public class ParcelsServiceClient : IParcelsServiceClient
    {
        private readonly IHttpClient _client;
        private readonly string _url;

        public ParcelsServiceClient(IHttpClient client, HttpClientOptions options)
        {
            _client = client;
            _url = options.Services["parcels"];
        }

        public Task<ParcelDto> GetAsync(Guid id)
            => _client.GetAsync<ParcelDto>($"{_url}/parcels/{id}");
    }
}