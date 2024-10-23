namespace TurnBasedGame.Networking
{
    // “ак как реального интернет-соединени€ нет, дл€ его эмул€ции создан этот класс
    // и статичный MessageTransport, чтобы к нему можно было обращатьс€ от клиента и от сервера.
    public static class ConnectionEmulation
    {
        public static IMessageTransport MessageTransport { get; } = new MessageTransport();
    }
}
