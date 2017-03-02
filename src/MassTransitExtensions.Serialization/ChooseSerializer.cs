using System.IO;
using System.Net.Mime;
using MassTransit;
using MassTransit.Serialization;

namespace MassTransitExtensions.Serialization
{
    public class ChooseSerializer : IMessageSerializer
    {
        private readonly JsonMessageSerializer _jsonSerializer = new JsonMessageSerializer();
        private readonly ChangeMessageTypeSerializer _changeMessageTypeSerializer = new ChangeMessageTypeSerializer();

        public void Serialize<T>(Stream stream, SendContext<T> context) where T : class
        {
            var messageContainer = context.Message as MessageContainer;
            if (messageContainer != null)
            {
                _changeMessageTypeSerializer.Serialize(stream, context);
            }
            _jsonSerializer.Serialize(stream, context);
        }

        public ContentType ContentType { get; }
    }
}