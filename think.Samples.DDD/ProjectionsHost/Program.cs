using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Domain.Aggregates.Champagne.Events;
using Domain.Persistence;
using Lamar;
using Messaging;
using Messaging.Contracts;
using Messaging.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectionsHost.Registry;

namespace ProjectionsHost
{
    class Program
    {
        public static IContainer Container;

        static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Container = new Container(x =>
            {
                x.AddOptions();
                x.Configure<MessagingConfig>(config.OrSection(nameof(MessagingConfig)));
                x.Configure<PersistenceConfiguration>(config.OrSection(nameof(PersistenceConfiguration)));

                x.IncludeRegistry<ProjectionsHostRegistry>();
            });

            StartConsumer().Wait();
        }

        private static async Task StartConsumer()
        {
            var eventHandlers = Container.GetInstance<IConsumer>();
            await eventHandlers.StartConsumer(new Subscription[]
            {
                new Subscription<ChampagneCreated>("thinkSample", "ChampagneCreated"),
                new Subscription<ChampagneRenamed>("thinkSample", "ChampagneRenamed"), 
            });

            var integrationEventPublisher = Container.GetInstance<IConsumer>();
            await integrationEventPublisher.StartConsumer(new[]
            {
                new Subscription<ChampagneCreated>("thinkSample", "ChampagneCreated"),
            });

            while (true)
            {
                // Don't shut down yet
            }
        }
    }


    public static class ConfigurationExtensions
    {
        public static IConfigurationSection OrSection(this IConfiguration config, string key)
        {
            return config as IConfigurationSection ?? config.GetSection(key);
        }
    }
}