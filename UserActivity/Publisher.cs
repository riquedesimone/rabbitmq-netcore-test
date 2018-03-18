using System;
using System.Text;
using RabbitMQ.Client;
using UserActivity.Configs;

namespace UserActivity
{
    public class Publisher: BasicRabbitMQ
    {
        private IModel _channel;
        public Publisher(RabbitMQConfigs rabbitMQConfigs): base(rabbitMQConfigs)
        {
     
            this.SetupRabbitMQ();
        }

        private void SetupRabbitMQ()
        {
            _channel = base.SetupChannel();
        }

        public void send(string message) {

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: String.Empty,
                routingKey: base._rabbitMQConfigs.Queue,
                basicProperties: null,
                body: body
            );
        }
    }
}
