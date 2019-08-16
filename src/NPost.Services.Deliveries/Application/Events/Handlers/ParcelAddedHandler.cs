using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Events.Handlers
{
    public class ParcelAddedHandler : IEventHandler<ParcelAdded>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<ParcelAdded> _logger;

        public ParcelAddedHandler(IParcelsRepository parcelsRepository, ILogger<ParcelAdded> logger)
        {
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(ParcelAdded @event)
        {
            var parcel = new Parcel(@event.ParcelId, string.Empty);
            await _parcelsRepository.AddAsync(parcel);
            _logger.LogInformation($"Added a parcel: {@event.ParcelId}");
        }
    }
}