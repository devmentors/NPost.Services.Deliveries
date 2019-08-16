using System;
using Convey.CQRS.Queries;
using NPost.Services.Deliveries.Application.DTO;

namespace NPost.Services.Deliveries.Application.Queries
{
    public class GetDelivery : IQuery<DeliveryDto>
    {
        public Guid DeliveryId { get; set; }
    }
}