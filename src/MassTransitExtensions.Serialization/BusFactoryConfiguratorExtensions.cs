using MassTransit;
using MassTransit.EndpointConfigurators;

namespace MassTransitExtensions.Serialization
{
    public static class BusFactoryConfiguratorExtensions
    {
        public static void UseChangeMessageTypeSerializer(this IBusFactoryConfigurator configurator)
        {
            configurator.AddBusFactorySpecification(new SetMessageSerializerBusFactorySpecification<ChooseSerializer>());
        }
    }
}