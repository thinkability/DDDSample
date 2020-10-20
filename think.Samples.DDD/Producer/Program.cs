using System;
using Domain.Persistence;
using Lamar;
using Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Producer.Registry;

namespace Producer
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

                x.IncludeRegistry<ProducerRegistry>();
            });
            
            var producer = Container.GetInstance<ContinousTestDataProducer>();
            producer.Produce().Wait();
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