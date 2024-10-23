namespace TurnBasedGame.Networking
{
    public interface IMessageTransport
    {
        void Send<TMessage>(TMessage message) where TMessage : struct;
        IMessageTransport AddListener<TMessage>(MessageReceivedHandler handler) where TMessage : struct;
        IMessageTransport RemoveListener<TMessage>(MessageReceivedHandler handler) where TMessage : struct;
        IMessageTransport Clear();
    }
}
