using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NPost.Services.Deliveries.Application.Events;
using NPost.Services.Deliveries.Application.Services.Clients;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IParcelsRepository _parcelsRepository;
        private readonly IRoutingServiceClient _routingServiceClient;
        private readonly IMessageBroker _messageBroker;
        private readonly IAppContext _appContext;
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository, IParcelsRepository parcelsRepository,
            IRoutingServiceClient routingServiceClient, IMessageBroker messageBroker,
            IAppContext appContext, ILogger<StartDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _parcelsRepository = parcelsRepository;
            _routingServiceClient = routingServiceClient;
            _messageBroker = messageBroker;
            _appContext = appContext;
            _logger = logger;
        }

        public async Task HandleAsync(StartDelivery command)
        {
            if (command.Parcels is null)
            {
                throw new Exception("Parcels to be delivered were not specified.");
            }

            var parcels = new HashSet<Parcel>();
            foreach (var parcelId in command.Parcels)
            {
                var parcel = await _parcelsRepository.GetAsync(parcelId);
                if (parcel is null)
                {
                    throw new Exception($"Parcel with id: {parcelId} to be delivered was not found.");
                }

                if (await _deliveriesRepository.IsParcelInDeliveryAsync(parcelId))
                {
                    throw new Exception($"Parcel with id: {parcelId} is unavailable.");
                }

                parcels.Add(parcel);
            }

            if (!parcels.Any())
            {
                throw new Exception($"Delivery cannot be started without the parcels.");
            }

            _logger.LogInformation("Calculating the route...");
            var route = await _routingServiceClient.GetAsync(parcels.Select(p => p.Address));
            if (route is null)
            {
                throw new Exception("Route was not found.");
            }

            _logger.LogInformation("Route was calculated.");
            await _deliveriesRepository.AddAsync(new Delivery(command.DeliveryId, parcels,
                new Route(route.Addresses, route.TotalDistance)));
            var eventsToPublish = parcels.Select(p => _messageBroker.PublishAsync(
                new DeliveryStarted(command.DeliveryId, p.Id)));
            await Task.WhenAll(eventsToPublish);
            _logger.LogInformation($"Started a delivery with id: {command.DeliveryId}");
        }
    }
}