using System;
using System.Collections.Generic;
using Convey.CQRS.Commands;

namespace NPost.Services.Deliveries.Application.Commands
{
    [Contract]
    public class StartDelivery : ICommand
    {
        public Guid DeliveryId { get; }
        public IEnumerable<Guid> Parcels { get; }

        public StartDelivery(Guid deliveryId, IEnumerable<Guid> parcels)
        {
            DeliveryId = deliveryId == Guid.Empty ? Guid.NewGuid() : deliveryId;
            Parcels = parcels;
        }
    }
}