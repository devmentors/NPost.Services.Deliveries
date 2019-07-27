using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Infrastructure.Mongo.Repositories
{
    public class DeliveriesMongoRepository : IDeliveriesRepository
    {
        private readonly IMongoDatabase _database;

        public DeliveriesMongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<bool> IsParcelInDeliveryAsync(Guid parcelId)
            => await Deliveries.Find(d => d.Parcels.Any(p => p.Id == parcelId && !p.IsAvailable))
                   .CountDocumentsAsync() > 0;

        public Task<Delivery> GetAsync(Guid id) => Deliveries.Find(d => d.Id == id).SingleOrDefaultAsync();
        public Task AddAsync(Delivery delivery) => Deliveries.InsertOneAsync(delivery);
        public Task UpdateAsync(Delivery delivery) => Deliveries.ReplaceOneAsync(d => d.Id == delivery.Id, delivery);
        private IMongoCollection<Delivery> Deliveries => _database.GetCollection<Delivery>("deliveries");
    }
}