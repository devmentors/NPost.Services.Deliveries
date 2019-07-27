using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace NPost.Services.Deliveries.Application.Events.External
{
    [MessageNamespace("parcels")]
    public class ParcelDeleted : IEvent
    {
        public Guid ParcelId { get; }

        public ParcelDeleted(Guid parcelId)
        {
            ParcelId = parcelId;
        }
    }
}