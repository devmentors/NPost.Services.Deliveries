using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Application.Services.Clients;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Events.External.Handlers
{
    public class ParcelAddedHandler : IEventHandler<ParcelAdded>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IParcelsServiceClient _parcelsServiceClient;
        private readonly ILogger<ParcelAddedHandler> _logger;

        public ParcelAddedHandler(IParcelsRepository parcelsRepository, IParcelsServiceClient parcelsServiceClient,
            ILogger<ParcelAddedHandler> logger)
        {
            _parcelsRepository = parcelsRepository;
            _parcelsServiceClient = parcelsServiceClient;
            _logger = logger;
        }

        public async Task HandleAsync(ParcelAdded @event)
        {
            var parcel = await _parcelsServiceClient.GetAsync(@event.ParcelId);
            if (parcel is null)
            {
                throw new Exception($"Parcel with id: {@event.ParcelId} was not found.");
            }

            await _parcelsRepository.AddAsync(new Parcel(@event.ParcelId, parcel.Address));
            _logger.LogInformation($"Added a parcel with id: {@event.ParcelId}");
        }
    }
}