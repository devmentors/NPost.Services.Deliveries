using System;
using Convey.CQRS.Events;

namespace NPost.Services.Deliveries.Application.Events
{
    [Contract]
    public class DeliveryCompleted : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }

        public DeliveryCompleted(Guid deliveryId, Guid parcelId)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
        }
    }
}