using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using WSMessageHandlerCore.Controllers;
using WSMessageHandlerCore.Interfaces;

namespace WSMessageHandler.WebSocketHook
{
    public class WebSocketMessageHandler : WebSocketHandler
    {
        private readonly ActiveMQController _controller;
        private readonly IBrokerRepository _repo;
        private string message;

        public WebSocketMessageHandler(WebSocketConnectionManager wsConnectionManager, IBrokerRepository repo)
            : base(wsConnectionManager)
        {
            _repo = repo;
            _controller = new ActiveMQController(_repo);
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
            var socketId = WebSocketConnectionManager.GetId(socket);
            await SendMessageAsync(socket, $"{socketId} you are now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            message += Encoding.UTF8.GetString(buffer, 0, result.Count);

            if (result.EndOfMessage)
            {
                await _controller.Add(message, "ws-message-handler-topic");
                await SendMessageAsync(socket, $"Socket '{socketId}' i got your message that says: '{message}'!");
                message = string.Empty;
            }
        }
    }
}
