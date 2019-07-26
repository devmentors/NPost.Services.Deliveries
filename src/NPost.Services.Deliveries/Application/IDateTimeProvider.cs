using System;

namespace NPost.Services.Deliveries.Application
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}