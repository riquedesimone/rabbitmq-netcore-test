using System;
namespace UserActivity.Configs
{
    public class RabbitMQConfigs
    {
        public string HostName { get; set;  }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Queue { get; set; }
    }
}
