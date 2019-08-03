using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Application.Events;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class CompleteDeliveryHandler : ICommandHandler<CompleteDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IAppContext _appContext;
        private readonly ILogger<CompleteDeliveryHandler> _logger;

        public CompleteDeliveryHandler(IDeliveriesRepository deliveriesRepository, IMessageBroker messageBroker,
            IAppContext appContext, ILogger<CompleteDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _messageBroker = messageBroker;
            _appContext = appContext;
            _logger = logger;
        }

        public async Task HandleAsync(CompleteDelivery command)
        {
            var delivery = await _deliveriesRepository.GetAsync(command.DeliveryId);
            if (delivery is null)
            {
                throw new Exception($"Delivery with id: {command.DeliveryId} was not found.");
            }

            delivery.Complete();
            await _deliveriesRepository.UpdateAsync(delivery);
            var eventsToPublish = delivery.Parcels.Select(p => _messageBroker.PublishAsync(
                new DeliveryCompleted(command.DeliveryId, p.Id)));
            await Task.WhenAll(eventsToPublish);
            _logger.LogInformation($"Completed a delivery with id: {command.DeliveryId}");
        }
    }
}