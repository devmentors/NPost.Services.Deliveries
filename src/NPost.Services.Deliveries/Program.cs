using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.Types;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NPost.Services.Deliveries.Application;
using NPost.Services.Deliveries.Application.Commands;
using NPost.Services.Deliveries.Infrastructure;

namespace NPost.Services.Deliveries
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync(ctx.RequestServices.GetService<AppOptions>().Name))
                        .Post<StartDelivery>("deliveries",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"deliveries/{cmd.DeliveryId}"))))
                .UseLogging()
                .Build()
                .RunAsync();
    }
}
