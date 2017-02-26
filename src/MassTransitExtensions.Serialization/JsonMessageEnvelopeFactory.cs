using MassTransit;
using MassTransit.Serialization;
using MassTransit.Util;

namespace MassTransitExtensions.Serialization
{
    internal class JsonMessageEnvelopeFactory
    {
        public JsonMessageEnvelope CreateJsonMessageEnvelope<T>(SendContext<T> context) where T : class
        {
            var container = context.Message as MessageContainer;
            if (container == null)
            {
                return new JsonMessageEnvelope(context, context.Message, TypeMetadataCache<T>.MessageTypeNames);
            }

            var separatorIndex = container.MessageType.LastIndexOf('.');
            var classNamespace = container.MessageType.Substring(0, separatorIndex);
            var className = container.MessageType.Substring(separatorIndex + 1);

            var messageType = $"urn:message:{classNamespace}:{className}";

            return new JsonMessageEnvelope(context, container.Message, new[] { messageType });
        }
    }
}