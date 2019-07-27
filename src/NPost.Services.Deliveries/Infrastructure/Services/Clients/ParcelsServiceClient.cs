using System;
using System.Threading.Tasks;
using Convey.HTTP;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Application.Services.Clients;

namespace NPost.Services.Deliveries.Infrastructure.Services.Clients
{
    public class ParcelsServiceClient : IParcelsServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public ParcelsServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["parcels"];
        }

        public Task<ParcelInDeliveryDto> GetAsync(Guid id)
            => _httpClient.GetAsync<ParcelInDeliveryDto>($"{_url}/parcels/{id}");
    }
}