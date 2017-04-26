using System;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{

    public interface IRabbitMQPersisterConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
