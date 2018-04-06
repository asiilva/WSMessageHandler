using System;
using System.Threading.Tasks;
using WSMessageHandlerCore.Interfaces;

namespace WSMessageHandlerCore.Controllers
{
    public class ActiveMQController
    {
        private readonly IBrokerRepository _repo;

        public ActiveMQController(IBrokerRepository repo)
        {
            _repo = repo;
        }

        public async Task Add(string message, string topic)
        {
            try
            {
                await _repo.Add(message, topic);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
