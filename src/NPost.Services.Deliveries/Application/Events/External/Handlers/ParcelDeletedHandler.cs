using System.Threading.Tasks;
using Convey.CQRS.Events;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Events.External.Handlers
{
    public class ParcelDeletedHandler : IEventHandler<ParcelDeleted>
    {
        private readonly IParcelsRepository _parcelsRepository;
        private readonly ILogger<ParcelDeleted> _logger;

        public ParcelDeletedHandler(IParcelsRepository parcelsRepository, ILogger<ParcelDeleted> logger)
        {
            _parcelsRepository = parcelsRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(ParcelDeleted @event)
        {
            await _parcelsRepository.DeleteAsync(@event.ParcelId);
            _logger.LogInformation($"Deleted a parcel with id: {@event.ParcelId}");
        }
    }
}