using System;
using Convey.CQRS.Commands;

namespace NPost.Services.Deliveries.Application.Commands
{
    [Contract]
    public class CompleteDelivery : ICommand
    {
        public Guid DeliveryId { get; }

        public CompleteDelivery(Guid deliveryId)
        {
            DeliveryId = deliveryId;
        }
    }
}