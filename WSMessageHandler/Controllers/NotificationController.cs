using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WSMessageHandler.Entities;
using WSMessageHandler.WebSocketHook;
using WSMessageHandlerCore.Interfaces;

namespace WSMessageHandler.Controllers
{
    [Route("api/v1/[controller]")]
    public class NotificationController : Controller
    {
        private readonly WebSocketMessageHandler _messageHandler;

        public NotificationController(IBrokerRepository repo)
        {
            _messageHandler = new WebSocketMessageHandler(new WebSocketConnectionManager(), repo);
        }

        [HttpPost()]
        public async Task<IActionResult> NotifyAllConnectedSockets([FromBody]RequestMessage req)
        {
            try
            {
                if (!req.IsValid())
                    return BadRequest("Invalid request body");

                await _messageHandler.SendMessageToAllAsync(req.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
