using System;
using MassTransit;
using MassTransit.Serialization;

namespace MassTransitExtensions.Serialization
{
    internal class JsonMessageEnvelopeFactory
    {
        public JsonMessageEnvelope CreateJsonMessageEnvelope<T>(SendContext<T> context) where T : class
        {
            var container = context.Message as MessageContainer;
            if (container == null)
            {
                throw new ArgumentException("Message must be MessageContainer type");
            }

            var separatorIndex = container.MessageType.LastIndexOf('.');
            var classNamespace = container.MessageType.Substring(0, separatorIndex);
            var className = container.MessageType.Substring(separatorIndex + 1);

            var messageType = $"urn:message:{classNamespace}:{className}";

            return new JsonMessageEnvelope(context, container.Message, new[] { messageType });
        }
    }
}