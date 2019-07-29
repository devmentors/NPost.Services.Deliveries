using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.Docs.Swagger;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Convey.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NPost.Services.Deliveries.Application;
using NPost.Services.Deliveries.Application.Events.External;
using NPost.Services.Deliveries.Application.Services.Clients;
using NPost.Services.Deliveries.Core.Repositories;
using NPost.Services.Deliveries.Infrastructure.Mongo.Repositories;
using NPost.Services.Deliveries.Infrastructure.Services.Clients;

namespace NPost.Services.Deliveries.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddTransient<IDeliveriesRepository, DeliveriesMongoRepository>();
            builder.Services.AddTransient<IParcelsRepository, ParcelsMongoRepository>();
            builder.Services.AddTransient<IParcelsServiceClient, ParcelsServiceClient>();
            builder.Services.AddTransient<IRoutingServiceClient, RoutingServiceClient>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq<CorrelationContext>()
                .AddMongo()
                .AddSwaggerDocs()
                .AddWebApiSwaggerDocs();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseInitializers()
                .UsePublicContracts<ContractAttribute>()
                .UseConsul()
                .UseSwagger()
                .UseSwaggerUI()
                .UseSwaggerDocs()
                .UseRabbitMq()
                .SubscribeEvent<ParcelAdded>()
                .SubscribeEvent<ParcelDeleted>();

            return app;
        }
    }
}