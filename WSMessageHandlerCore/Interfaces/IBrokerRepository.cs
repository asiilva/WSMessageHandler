using System.Threading.Tasks;

namespace WSMessageHandlerCore.Interfaces
{
    public interface IBrokerRepository
    {
        Task Add(string message, string topic);
    }
}
