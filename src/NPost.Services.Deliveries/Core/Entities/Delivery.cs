using System;
using System.Collections.Generic;
using System.Linq;

namespace NPost.Services.Deliveries.Core.Entities
{
    public class Delivery
    {
        private ISet<ParcelInDelivery> _parcels = new HashSet<ParcelInDelivery>();
        public Guid Id { get; private set; }

        public IEnumerable<ParcelInDelivery> Parcels
        {
            get => _parcels;
            private set => _parcels = new HashSet<ParcelInDelivery>(value);
        }

        public Route Route { get; private set; }
        public Status Status { get; private set; }
        public string Notes { get; private set; }

        public Delivery(Guid id, IEnumerable<Parcel> parcels, Route route)
        {
            Id = id;
            Parcels = parcels is null
                ? throw new ArgumentException("No parcels to be delivered.")
                : parcels.Select(p => new ParcelInDelivery(p));
            Route = route ?? throw new ArgumentException("Route cannot be empty.");
            Status = Status.Started;
            Notes = string.Empty;
            foreach (var parcel in Parcels)
            {
                parcel.SetUnavailable();
            }
        }

        public void Complete() => TryChangeStatus(Status.Completed, () => Status == Status.Started);

        public void Fail(string reason)
        {
            TryChangeStatus(Status.Failed, () => Status == Status.Started);
            Notes = reason ?? string.Empty;
            foreach (var parcel in Parcels)
            {
                parcel.SetAvailable();
            }
        }

        private void TryChangeStatus(Status status, Func<bool> validator)
        {
            if (!validator())
            {
                throw new InvalidOperationException($"Delivery status cannot be changed to: {status}");
            }

            Status = status;
        }
    }
}