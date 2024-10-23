using System;
using TurnBasedGame.Networking;
using TurnBasedGame.Networking.BattleMessages;
using UnityEngine;

namespace TurnBasedGame.Client
{
    public class ClientBattlePlayer : IBattlePlayer, IBattleStartListener, IBattleStopListener
    {
        public int Id { get; private set; }
        public Unit Unit { get; private set; }

        private readonly IMessageTransport _messageTransport;
        private bool _isWaitingMove;
        private string _selectedAbilityId;

        public ClientBattlePlayer(IMessageTransport messageTransport)
        {
            _messageTransport = messageTransport;
        }

        void IBattleStartListener.OnStart()
        {
            _messageTransport
                .AddListener<CannotMakeMove>(OnCannotMakeMove)
                .AddListener<MoveMadeSuccessfully>(OnMoveMadeSuccessfully);
        }

        void IBattleStopListener.OnStop()
        {
            _messageTransport
                .RemoveListener<CannotMakeMove>(OnCannotMakeMove)
                .RemoveListener<MoveMadeSuccessfully>(OnMoveMadeSuccessfully);
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

        public void SelectAbility(string abilityId)
        {
            if (!_isWaitingMove)
                throw new InvalidOperationException("No move is waiting.");

            _isWaitingMove = false;
            _selectedAbilityId = abilityId;

            _messageTransport.Send(new MakeMove(Id, _selectedAbilityId));
        }

        public void DeselectAbility()
        {
            _selectedAbilityId = string.Empty;
        }

        private void OnCannotMakeMove(MessageReceivedEventArgs e)
        {
            Debug.LogError(nameof(CannotMakeMove));
        }

        private void OnMoveMadeSuccessfully(MessageReceivedEventArgs e)
        {
            Unit.AbilitiesUser.Use(_selectedAbilityId);
            Unit.Battle.PassMove();

            DeselectAbility();
        }
    }
}
