using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserActivity.Configs;

namespace UserActivity
{
    public class Receiver: BasicRabbitMQ
    {
        private EventHandler<BasicDeliverEventArgs> _Consumer_Received;

        public Receiver(
            RabbitMQConfigs rabbitMQConfigs,
            EventHandler<BasicDeliverEventArgs> receiver): base(rabbitMQConfigs)
        {
            
            _Consumer_Received = receiver;

            this.SetupRabbitMQ();
        }

        private void SetupRabbitMQ() {
            var channel = base.SetupChannel();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += _Consumer_Received;

            channel.BasicConsume(
                queue: base._rabbitMQConfigs.Queue,
                autoAck: true,
                consumer: consumer
            );
        }
    }
}
