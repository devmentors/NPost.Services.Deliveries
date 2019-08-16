using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Microsoft.Extensions.Logging;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Application.Commands.Handlers
{
    public class StartDeliveryHandler : ICommandHandler<StartDelivery>
    {
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly ILogger<StartDeliveryHandler> _logger;

        public StartDeliveryHandler(IDeliveriesRepository deliveriesRepository, ILogger<StartDeliveryHandler> logger)
        {
            _deliveriesRepository = deliveriesRepository;
            _logger = logger;
        }
        
        public async Task HandleAsync(StartDelivery command)
        {
            _logger.LogInformation($"Starting a delivery: {command.DeliveryId}");
            await _deliveriesRepository.AddAsync(new Delivery(command.DeliveryId,
                new List<Parcel>(), new Route(new List<string>(), 0)));
            await Task.CompletedTask;
        }
    }
}