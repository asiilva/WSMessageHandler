using Apache.NMS;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WSMessageHandlerCore.Entities;
using WSMessageHandlerCore.Interfaces;

namespace WSMessageHandlerCore.Repositories
{
    public class ActiveMQRepository : IBrokerRepository
    {
        private readonly AppConfiguration _config;

        public ActiveMQRepository(IOptions<AppConfiguration> config)
        {
            _config = config.Value;
        }

        public async Task Add(string message, string topic)
        {
            NMSConnectionFactory factory = new NMSConnectionFactory(_config.ActiveMQUri);

            using (IConnection connection = factory.CreateConnection())
            {
                connection.Start();

                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                using (IDestination dest = session.GetTopic(topic))
                using (IMessageProducer producer = session.CreateProducer(dest))
                {
                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
                    producer.Send(session.CreateTextMessage(message));
                }
            }
        }
    }
}
