using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Application.Events;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository, IParcelsRepository parcelsRepository,
            IMessageBroker messageBroker, ILogger<StartDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _parcelsRepository = parcelsRepository;
            _messageBroker = messageBroker;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDelivery command)
        {
            if (command.Parcels is null || !command.Parcels.Any())
            {
                throw new ArgumentException("Missing parcels", nameof(command.Parcels));
            }

            var parcels = new List<Parcel>();
            foreach (var parcelId in command.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelId);
                if (parcel is null)
                {
                    throw new Exception($"Parcel with id: {parcelId} was not found.");
                }
                
                parcels.Add(parcel);
            }    
            
            var delivery = new Delivery(command.DeliveryId, parcels, new Route(new List<string>(), 0));
            await _deliveriesRepository.AddAsync(delivery);
            foreach (var parcel in parcels)
            {
                await _messageBroker.PublishAsync(new DeliveryStarted(command.DeliveryId, parcel.Id));
            }
            
            _logger.LogInformation($"Started a delivery: {command.DeliveryId}");
        }
    }
}