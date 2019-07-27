using System;

namespace NPost.Services.Deliveries.Core.Entities
{
    public class ParcelInDelivery : IEquatable<ParcelInDelivery>
    {
        public Guid Id { get; private set; }
        public string Address { get; private set; }
        public bool IsAvailable { get; private set; }

        public ParcelInDelivery(Parcel parcel)
        {
            Id = parcel.Id;
            Address = parcel.Address;
            IsAvailable = true;
        }

        public void SetAvailable()
        {
            IsAvailable = true;
        }

        public void SetUnavailable()
        {
            IsAvailable = false;
        }

        public bool Equals(ParcelInDelivery other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Parcel) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}