using System;
using System.Threading.Tasks;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Core.Repositories
{
    public interface IParcelsRepository
    {
        Task<Parcel> GetAsync(Guid id);
        Task AddAsync(Parcel parcel);
        Task UpdateAsync(Parcel parcel);
        Task DeleteAsync(Guid id);
    }
}