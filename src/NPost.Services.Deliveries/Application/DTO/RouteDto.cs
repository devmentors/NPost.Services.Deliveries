using System.Collections.Generic;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Application.DTO
{
    public class RouteDto
    {
        public IEnumerable<string> Addresses { get; set; }
        public double TotalDistance { get; set; }

        public RouteDto()
        {
        }

        public RouteDto(Route route)
        {
            Addresses = route.Addresses;
            TotalDistance = route.TotalDistance;
        }
    }
}