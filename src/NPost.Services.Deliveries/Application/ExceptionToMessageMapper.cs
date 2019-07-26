using System;
using Convey.MessageBrokers.RabbitMQ;

namespace NPost.Services.Deliveries.Application
{
    public class ExceptionToMessageMapper : IExceptionToMessageMapper
    {
        public object Map(Exception exception, object message)
        {
            return null;
        }
    }
}