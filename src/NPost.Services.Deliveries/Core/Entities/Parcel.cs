using System;

namespace NPost.Services.Deliveries.Core.Entities
{
    public class Parcel : IEquatable<Parcel>
    {
        public Guid Id { get; private set; }
        public string Address { get; private set; }

        public Parcel(Guid id, string address)
        {
            Id = id;
            Address = address;
        }

        public bool Equals(Parcel other)
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