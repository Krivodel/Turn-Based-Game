namespace TurnBasedGame.Networking
{
    // ��� ��� ��������� ��������-���������� ���, ��� ��� �������� ������ ���� �����
    // � ��������� MessageTransport, ����� � ���� ����� ���� ���������� �� ������� � �� �������.
    public static class ConnectionEmulation
    {
        public static IMessageTransport MessageTransport { get; } = new MessageTransport();
    }
}
