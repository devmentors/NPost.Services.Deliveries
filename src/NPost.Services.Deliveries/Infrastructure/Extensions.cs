using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NPost.Services.Deliveries.Application;

namespace NPost.Services.Deliveries.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo();
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseInitializers()
                .UsePublicContracts<ContractAttribute>()
                .UseConsul()
                .UseRabbitMq();

            return app;
        }
    }
}