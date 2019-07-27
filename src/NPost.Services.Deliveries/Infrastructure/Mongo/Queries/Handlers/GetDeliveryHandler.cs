using System.Threading.Tasks;
using Convey.CQRS.Queries;
using MongoDB.Driver;
using NPost.Services.Deliveries.Application.DTO;
using NPost.Services.Deliveries.Application.Queries;
using NPost.Services.Deliveries.Core.Entities;

namespace NPost.Services.Deliveries.Infrastructure.Mongo.Queries.Handlers
{
    public class GetDeliveryHandler : IQueryHandler<GetDelivery, DeliveryDto>
    {
        private readonly IMongoDatabase _database;

        public GetDeliveryHandler(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<DeliveryDto> HandleAsync(GetDelivery query)
        {
            var delivery = await _database.GetCollection<Delivery>("deliveries")
                .Find(p => p.Id == query.DeliveryId)
                .SingleOrDefaultAsync();

            return delivery is null ? null : new DeliveryDto(delivery);
        }
    }
}