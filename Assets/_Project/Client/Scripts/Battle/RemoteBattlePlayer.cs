using System;
using TurnBasedGame.Networking;

namespace TurnBasedGame.Client
{
    public class RemoteBattlePlayer : IBattlePlayer
    {
        public int Id { get; private set; }
        public Unit Unit { get; private set; }

        private readonly IMessageTransport _messageTransport;

        public RemoteBattlePlayer(IMessageTransport messageTransport)
        {
            _messageTransport = messageTransport;
        }

        void IBattlePlayer.Init(int id, Unit unit)
        {
            Id = id;
            Unit = unit;
        }

        public bool CanMove()
        {
            throw new NotImplementedException();
        }

        public void MakeMove()
        {
            throw new NotImplementedException();
        }
    }
}
