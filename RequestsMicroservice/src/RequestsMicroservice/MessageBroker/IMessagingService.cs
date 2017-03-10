namespace RequestsMicroservice.MessageBroker
{
    public interface IMessagingService
    {
        void Send<TMessage>(TMessage message);
    }
}