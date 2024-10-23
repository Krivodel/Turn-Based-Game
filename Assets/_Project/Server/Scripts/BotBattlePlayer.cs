using System;
using System.Threading.Tasks;
using TurnBasedGame.Networking;
using TurnBasedGame.Networking.BattleMessages;

namespace TurnBasedGame.Server
{
    public class BotBattlePlayer : IBattlePlayer
    {
        public int Id { get; private set; }
        public Unit Unit { get; private set; }

        private readonly IMessageTransport _messageTransport;
        private readonly Random _random = new();
        private readonly TimeSpan _moveDelay;

        public BotBattlePlayer(IMessageTransport messageTransport, TimeSpan moveDelay)
        {
            _messageTransport = messageTransport;
            _moveDelay = moveDelay;
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

        public async void MakeMove()
        {
            await Task.Delay(_moveDelay);

            if (!Unit.Battle.IsRunning)
                return;

            var ability = GetRandomAbility();

            Unit.AbilitiesUser.Use(ability);
            Unit.Battle.PassMove();

            _messageTransport.Send(new MakeMove(Id, ability.BaseData.Id));
        }

        private Ability GetRandomAbility()
        {
            var abilities = Unit.AbilitiesUser.GetAbilities();
            Ability ability;

            do
            {
                int index = _random.Next(0, abilities.Count);

                ability = abilities[index];
            }
            while (!Unit.AbilitiesUser.CanUse(ability));

            return ability;
        }
    }
}
