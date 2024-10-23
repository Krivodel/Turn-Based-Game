using TurnBasedGame.Networking;
using TurnBasedGame.Networking.BattleMessages;

namespace TurnBasedGame.Client
{
    public class BotBattlePlayer : IBattlePlayer, IBattleStartListener, IBattleStopListener
    {
        public int Id { get; private set; }
        public Unit Unit { get; private set; }

        private readonly IMessageTransport _messageTransport;

        public BotBattlePlayer(IMessageTransport messageTransport)
        {
            _messageTransport = messageTransport;
        }

        void IBattlePlayer.Init(int id, Unit unit)
        {
            Id = id;
            Unit = unit;
        }

        void IBattleStartListener.OnStart()
        {
            _messageTransport.AddListener<MakeMove>(OnMakeMove);
        }

        void IBattleStopListener.OnStop()
        {
            _messageTransport.RemoveListener<MakeMove>(OnMakeMove);
        }

        public bool CanMove()
        {
            return !Unit.ModifiableHealth.Health.IsDied;
        }

        public void MakeMove()
        {

        }

        private void OnMakeMove(MessageReceivedEventArgs e)
        {
            var message = e.GetMessage<MakeMove>();

            if (message.PlayerId != Id)
                return;

            Unit.AbilitiesUser.Use(message.AbilityId);
            Unit.Battle.PassMove();
        }
    }
}
