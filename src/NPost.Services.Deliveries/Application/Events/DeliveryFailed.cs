using System;
using Convey.CQRS.Events;

namespace NPost.Services.Deliveries.Application.Events
{
    [Contract]
    public class DeliveryFailed : IEvent
    {
        public Guid DeliveryId { get; }
        public Guid ParcelId { get; }
        public string Reason { get; }

        public DeliveryFailed(Guid deliveryId, Guid parcelId, string reason)
        {
            DeliveryId = deliveryId;
            ParcelId = parcelId;
            Reason = reason;
        }
    }
}