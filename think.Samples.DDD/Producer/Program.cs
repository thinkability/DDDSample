using System;
using Lamar;
using Producer.Registry;

namespace Producer
{
    class Program
    {
        public static readonly IContainer Container = new Container(new ProducerRegistry());
            
        static void Main(string[] args)
        {
            var producer = Container.GetInstance<ContinousTestDataProducer>();

            producer.Produce().Wait();
        }
    }
}