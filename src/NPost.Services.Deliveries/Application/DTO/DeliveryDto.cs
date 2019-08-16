using System;
using System.Collections.Generic;
using System.Linq;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Application.DTO
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public IEnumerable<ParcelInDeliveryDto> Parcels { get; set; }
        public RouteDto Route { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public DeliveryDto()
        {
        }

        public DeliveryDto(Delivery delivery)
        {
            Id = delivery.Id;
            Parcels = delivery.Parcels.Select(p => new ParcelInDeliveryDto(p));
            Route = new RouteDto(delivery.Route);
            Status = delivery.Status.ToString();
            Notes = delivery.Notes;
        }
    }
}