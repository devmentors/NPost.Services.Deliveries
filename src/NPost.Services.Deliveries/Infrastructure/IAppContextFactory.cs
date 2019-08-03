using NPost.Services.Deliveries.Application;

namespace NPost.Services.Deliveries.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}