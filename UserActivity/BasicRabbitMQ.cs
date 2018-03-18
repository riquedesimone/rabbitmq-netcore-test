using System;
using RabbitMQ.Client;
using UserActivity.Configs;

namespace UserActivity
{
    public abstract class BasicRabbitMQ
    {
        protected RabbitMQConfigs _rabbitMQConfigs;

        public BasicRabbitMQ(RabbitMQConfigs rabbitMQConfigs)
        {
            _rabbitMQConfigs = rabbitMQConfigs;
        }

        protected IModel SetupChannel() {
            var factory = new ConnectionFactory()
            {
                RequestedHeartbeat = 15,
                HostName = _rabbitMQConfigs.HostName,
                UserName = _rabbitMQConfigs.UserName,
                Password = _rabbitMQConfigs.Password,
                AutomaticRecoveryEnabled = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _rabbitMQConfigs.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            return channel;
        }
    }

}
