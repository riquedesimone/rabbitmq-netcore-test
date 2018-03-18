using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using System.Text;
using UserActivity;
using UserActivity.Configs;
using Newtonsoft.Json;
using UserActivity.Domain.Entities;
using UserActivity.Data.Context;
using LoadUserActivity.Data.Data;

namespace LoadUserActivity
{
    class Program
    {
        private static IConfiguration _configuration;
        private static AppDbContext _appDbContext;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"application.json");

            _configuration = builder.Build();

            DbInitializer.Initialize();
            _appDbContext = new AppDbContext();

            var rabbitMQConfigs = new RabbitMQConfigs();
            new ConfigureFromConfigurationOptions<RabbitMQConfigs>(
                _configuration.GetSection("RabbitMQConfigs"))
                .Configure(rabbitMQConfigs);

            var receiver = new Receiver(rabbitMQConfigs, Consumer_Received);

            Console.Write("Aguardando Interações.");
        }


        private static void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body);
            var activity = JsonConvert.DeserializeObject<Activity>(message);
            _appDbContext.Activities.Add(activity);
            _appDbContext.SaveChanges();
            Console.WriteLine(Environment.NewLine +
                              "[Atividade recebida] " + message);
        }

    }
}
