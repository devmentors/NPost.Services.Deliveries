using System;
using System.Threading.Tasks;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Core.Repositories
{
    public interface IDeliveriesRepository
    {
        Task<Delivery> GetAsync(Guid id);
        Task AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
    }
}