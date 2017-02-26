using MassTransitExtensions.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MassTransitExtensions.Example.Controllers
{
    [Route("publish")]
    public class PublishController : Controller
    {
        private readonly BusWrapper _busWrapper;

        public PublishController(BusWrapper busWrapper)
        {
            _busWrapper = busWrapper;
        }

        [Route("")]
        [HttpPost]
        public async Task Publish([FromBody]Model model)
        {
            var endpoint = await _busWrapper.GetSendEnpoint(model.QueueName).ConfigureAwait(false);

            var ev = new MessageContainer(model.MessageType, model.Message);

            await endpoint.Send(ev).ConfigureAwait(false);
        }
    }

    public class Model
    {
        public string QueueName { get; set; }

        public string MessageType { get; set; }

        public JObject Message { get; set; }
    }
}