using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using meu.lms.api.Hubs;
using meu.lms.api.Models;
using Microsoft.AspNetCore.SignalR;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {

        private readonly IHubContext<MessageHub> _hub;

        public ChatController(IHubContext<MessageHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Message msg)
        {
            MessageHub hub = new MessageHub();


            await _hub.Clients.All.SendAsync("MessageReceived", msg);
            return Ok();
        }

    }
}
