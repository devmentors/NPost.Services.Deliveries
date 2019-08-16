using System;
using Convey.CQRS.Events;

namespace NPost.Services.Deliveries.Application.Events
{
    public class DeliveryStarted : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }

        public DeliveryStarted(Guid deliveryId, Guid parcelId)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
        }
    }
}