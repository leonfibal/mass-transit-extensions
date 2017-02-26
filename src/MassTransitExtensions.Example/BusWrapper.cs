using MassTransit;
using MassTransitExtensions.Serialization;
using System;
using System.Threading.Tasks;

namespace MassTransitExtensions.Example
{
    public class BusWrapper : IDisposable
    {
        private readonly IBusControl _bus;

        public BusWrapper()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.UseChangeMessageTypeSerializer();
            });

            _bus.Start();
        }

        public Task<ISendEndpoint> GetSendEnpoint(string queueName)
        {
            return _bus.GetSendEndpoint(new Uri($"rabbitmq://localhost/{queueName}"));
        }

        public void Dispose()
        {
            _bus.Stop();
        }
    }
}