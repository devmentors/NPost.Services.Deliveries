using System;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Application.DTO
{
    public class ParcelInDeliveryDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }

        public ParcelInDeliveryDto()
        {
        }

        public ParcelInDeliveryDto(ParcelInDelivery parcel)
        {
            Id = parcel.Id;
            Address = parcel.Address;
        }
    }
}