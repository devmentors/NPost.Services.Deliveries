using System;
using System.Net.Http;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Convey.HTTP;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Application.Services.Clients;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Events.Handlers
{
    public class ParcelAddedHandler : IEventHandler<ParcelAdded>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<ParcelAdded> _logger;
        private readonly IParcelsServiceClient _parcelsServiceClient;

        public ParcelAddedHandler(IParcelsRepository parcelsRepository, ILogger<ParcelAdded> logger,
            IParcelsServiceClient parcelsServiceClient)
        {
            _parcelsRepository = parcelsRepository;
            _logger = logger;
            _parcelsServiceClient = parcelsServiceClient;
        }
        
        public async Task HandleAsync(ParcelAdded @event)
        {
            var parcelDto = await _parcelsServiceClient.GetAsync(@event.ParcelId);
            if (parcelDto is null)
            {
                throw new Exception($"Parcel: {@event.ParcelId} was not found");
            }
            
            var parcel = new Parcel(@event.ParcelId, parcelDto.Address);
            await _parcelsRepository.AddAsync(parcel);
            _logger.LogInformation($"Added a parcel: {@event.ParcelId}");
        }
    }
}