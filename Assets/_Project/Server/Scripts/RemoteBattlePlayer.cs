using TurnBasedGame.Networking;
using TurnBasedGame.Networking.BattleMessages;

namespace TurnBasedGame.Server
{
    public class RemoteBattlePlayer : IBattlePlayer, IBattleStartListener, IBattleStopListener
    {
        public int Id { get; private set; }
        public Unit Unit { get; private set; }

        private readonly IMessageTransport _messageTransport;
        private bool _isWaitingMove;

        public RemoteBattlePlayer(IMessageTransport messageTransport)
        {
            _messageTransport = messageTransport;
        }

        void IBattleStartListener.OnStart()
        {
            _messageTransport.AddListener<MakeMove>(OnMakeMove);
        }

        void IBattleStopListener.OnStop()
        {
            _messageTransport.RemoveListener<MakeMove>(OnMakeMove);
        }

        void IBattlePlayer.Init(int id, Unit unit)
        {
            Id = id;
            Unit = unit;
        }

        public bool CanMove()
        {
            return !Unit.ModifiableHealth.Health.IsDied;
        }

        public void MakeMove()
        {
            _isWaitingMove = true;
        }

        private void OnMakeMove(MessageReceivedEventArgs e)
        {
            var message = e.GetMessage<MakeMove>();

            if (message.PlayerId != Id)
                return;

            if (!_isWaitingMove || !CanMove())
            {
                _messageTransport.Send(new CannotMakeMove());

                return;
            }

            _isWaitingMove = false;

            UseAbility(message.AbilityId);
            Unit.Battle.PassMove();

            _messageTransport.Send(new MoveMadeSuccessfully());
        }

        private void UseAbility(string abilityId)
        {
            var ability = Unit.AbilitiesUser.GetAbilityByIdOrNull(abilityId);

            if (ability == null || !Unit.AbilitiesUser.CanUse(ability))
            {
                _messageTransport.Send(new CannotUseAbility());

                return;
            }

            Unit.AbilitiesUser.Use(ability);
        }
    }
}
