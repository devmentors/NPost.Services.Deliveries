using System;
using NPost.Services.Deliveries.Application;

namespace NPost.Services.Deliveries.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}