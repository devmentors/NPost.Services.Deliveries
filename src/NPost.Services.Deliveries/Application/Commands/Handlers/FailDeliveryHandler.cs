using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPost.Services.Deliveries.Application.Events;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class FailDeliveryHandler : ICommandHandler<FailDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IAppContext _appContext;
        private readonly ILogger<FailDeliveryHandler> _logger;

        public FailDeliveryHandler(IDeliveriesRepository deliveriesRepository, IMessageBroker messageBroker,
            IAppContext appContext, ILogger<FailDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _messageBroker = messageBroker;
            _appContext = appContext;
            _logger = logger;
        }

        public async Task HandleAsync(FailDelivery command)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(_appContext));
            var delivery = await _deliveriesRepository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new Exception($"Delivery with id: {command.DeliveryId} was not found.");
            }

            delivery.Fail(command.Reason);
            await _deliveriesRepository.UpdateAsync(delivery);
            var eventsToPublish = delivery.Parcels.Select(p => _messageBroker.PublishAsync(
                new DeliveryFailed(command.DeliveryId, p.Id, command.Reason)));
            await Task.WhenAll(eventsToPublish);
            _logger.LogInformation($"Failed a delivery with id: {command.DeliveryId}");
        }
    }
}