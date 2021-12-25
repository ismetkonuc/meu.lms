using System.Threading.Tasks;
using meu.lms.api.Models;
using Microsoft.AspNetCore.SignalR;

namespace meu.lms.api.Hubs
{
    public class MessageHub : Hub
    {
        public async Task NewMessage(Message msg)
        {
            await Clients.All.SendAsync("MessageReceived", msg);
        }
    }
}