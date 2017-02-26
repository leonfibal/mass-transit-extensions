using Newtonsoft.Json.Linq;

namespace MassTransitExtensions.Serialization
{
    public class MessageContainer
    {
        public MessageContainer(string messageType, JObject message)
        {
            MessageType = messageType;
            Message = message;
        }

        public string MessageType { get; }

        public JObject Message { get; }
    }
}