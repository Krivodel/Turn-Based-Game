using System;
using TurnBasedGame.Networking;
using TurnBasedGame.Networking.BattleMessages;

namespace TurnBasedGame.Server
{
    public class BattleNetwork
    {
        private readonly IMessageTransport _messageTransport;
        private Battle _battle;

        public BattleNetwork(IMessageTransport messageTransport)
        {
            _messageTransport = messageTransport;
        }

        public void Start()
        {
            _messageTransport
                .AddListener<CreateBattle>(OnCreateBattle)
                .AddListener<StartBattle>(OnStartBattle)
                .AddListener<StopBattle>(OnStopBattle);
        }

        public void Stop()
        {
            _messageTransport
                .RemoveListener<CreateBattle>(OnCreateBattle)
                .RemoveListener<StartBattle>(OnStartBattle)
                .RemoveListener<StopBattle>(OnStopBattle);

            if (_battle != null && _battle.IsRunning)
                _battle.Stop();
        }

        private void OnCreateBattle(MessageReceivedEventArgs e)
        {
            if (_battle != null)
            {
                _messageTransport.Send(new BattleAlreadyExists());

                return;
            }

            var players = new IBattlePlayer[]
            {
                new RemoteBattlePlayer(_messageTransport),
                new BotBattlePlayer(_messageTransport, TimeSpan.FromSeconds(1d))
            };

            _battle = new BattleCreator().Create(players);

            _battle.Stopped += OnBattleStopped;

            _messageTransport.Send(new BattleCreatedSuccessfully());
        }

        private void OnStartBattle(MessageReceivedEventArgs e)
        {
            if (_battle == null)
            {
                _messageTransport.Send(new BattleNotExist());

                return;
            }

            _battle.Start();

            _messageTransport.Send(new BattleStartedSuccessfully());
        }

        private void OnStopBattle(MessageReceivedEventArgs e)
        {
            if (_battle == null || !_battle.IsRunning)
            {
                _messageTransport.Send(new BattleNotExist());

                return;
            }

            _battle.Stopped -= OnBattleStopped;

            _battle.Stop();

            _messageTransport.Send(new BattleStoppedSuccessfully());
        }

        private void OnBattleStopped()
        {
            _battle = null;

            _messageTransport.Send(new BattleStoppedSuccessfully());
        }
    }
}
