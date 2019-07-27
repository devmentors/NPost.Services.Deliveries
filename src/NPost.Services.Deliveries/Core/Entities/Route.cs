using System.Collections.Generic;

namespace NPost.Services.Deliveries.Core.Entities
{
    public class Route
    {
        public IEnumerable<string> Addresses { get; private set; }
        public double TotalDistance { get; private set; }

        public Route(IEnumerable<string> addresses, double totalDistance)
        {
            Addresses = addresses;
            TotalDistance = totalDistance;
        }
    }
}