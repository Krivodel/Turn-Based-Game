using System;
using TurnBasedGame.Networking;
using TurnBasedGame.Networking.BattleMessages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBasedGame.Client
{
    public class BattleNetwork : IBattleNetwork
    {
        public ClientBattlePlayer ClientBattlePlayer { get; private set; }
        public BotBattlePlayer BotBattlePlayer { get; private set; }

        public event Action BattleCreated;
        public event Action BattleStarted;
        public event Action BattleStopped;
        public event Action BattleCompleted;

        private readonly IMessageTransport _messageTransport;
        private Battle _battle;

        public BattleNetwork(IMessageTransport messageTransport)
        {
            _messageTransport = messageTransport;
        }

        public void StartService()
        {
            _messageTransport
                .AddListener<BattleAlreadyExists>(OnBattleAlreadyExists)
                .AddListener<BattleCreatedSuccessfully>(OnBattleCreatedSuccessfully)
                .AddListener<BattleNotExist>(OnBattleNotExist)
                .AddListener<BattleStartedSuccessfully>(OnBattleStartedSuccessfully)
                .AddListener<BattleStoppedSuccessfully>(OnBattleStoppedSuccessfully);
        }

        public void StopService()
        {
            _messageTransport
                .RemoveListener<BattleAlreadyExists>(OnBattleAlreadyExists)
                .RemoveListener<BattleCreatedSuccessfully>(OnBattleCreatedSuccessfully)
                .RemoveListener<BattleNotExist>(OnBattleNotExist)
                .RemoveListener<BattleStartedSuccessfully>(OnBattleStartedSuccessfully)
                .RemoveListener<BattleStoppedSuccessfully>(OnBattleStoppedSuccessfully);

            if (_battle != null && _battle.IsRunning)
                _battle.Stop();
        }

        public bool CanCreate()
        {
            return _battle == null;
        }

        public bool CanStop()
        {
            return _battle != null;
        }

        public void CreateBattle()
        {
            if (!CanCreate())
                throw new InvalidOperationException("Cannot create battle.");

            _messageTransport.Send(new CreateBattle());
        }

        public void StopBattle()
        {
            if (!CanStop())
                throw new InvalidOperationException("Cannot stop battle.");

            _messageTransport.Send(new StopBattle());
        }

        private void OnBattleAlreadyExists(MessageReceivedEventArgs e)
        {
            Debug.LogError(nameof(BattleAlreadyExists));
        }

        private void OnBattleCreatedSuccessfully(MessageReceivedEventArgs e)
        {
            ClientBattlePlayer = new(_messageTransport);
            BotBattlePlayer = new(_messageTransport);

            var players = new IBattlePlayer[]
            {
                ClientBattlePlayer,
                BotBattlePlayer
            };

            _battle = new BattleCreator().Create(players);

            BattleCreated?.Invoke();

            _messageTransport.Send(new StartBattle());
        }

        private void OnBattleNotExist(MessageReceivedEventArgs e)
        {
            Debug.LogError(nameof(BattleNotExist));
        }

        private void OnBattleStartedSuccessfully(MessageReceivedEventArgs e)
        {
            _battle.Stopped += OnBattleStopped;
            _battle.Completed += OnBattleCompleted;

            _battle.Start();

            BattleStarted?.Invoke();
        }

        private void OnBattleStoppedSuccessfully(MessageReceivedEventArgs e)
        {
            _battle.Stop();
        }

        private void OnBattleStopped()
        {
            if (_battle != null)
            {
                _battle.Stopped -= OnBattleStopped;
                _battle.Completed -= OnBattleCompleted;
            }

            BattleStopped?.Invoke();

            _battle = null;
        }

        private void OnBattleCompleted()
        {
            _battle = null;

            BattleCompleted?.Invoke();
        }
    }
}
