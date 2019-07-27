using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using NPost.Services.Deliveries.Core.Entities;
using NPost.Services.Deliveries.Core.Repositories;

namespace NPost.Services.Deliveries.Infrastructure.Mongo.Repositories
{
    public class ParcelsMongoRepository : IParcelsRepository
    {
        private readonly IMongoDatabase _database;

        public ParcelsMongoRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public Task<Parcel> GetAsync(Guid id) => Parcels.Find(p => p.Id == id).SingleOrDefaultAsync();
        public Task AddAsync(Parcel parcel) => Parcels.InsertOneAsync(parcel);
        public Task UpdateAsync(Parcel parcel) => Parcels.ReplaceOneAsync(p => p.Id == parcel.Id, parcel);
        public Task DeleteAsync(Guid id) => Parcels.DeleteOneAsync(p => p.Id == id);
        private IMongoCollection<Parcel> Parcels => _database.GetCollection<Parcel>("parcels");
    }
}