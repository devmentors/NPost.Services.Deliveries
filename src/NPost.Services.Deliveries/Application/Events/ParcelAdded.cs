using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace NPost.Services.Deliveries.Application.Events
{
    [MessageNamespace("parcels")]
    public class ParcelAdded : IEvent
    {
        public Guid ParcelId { get; }

        public ParcelAdded(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}