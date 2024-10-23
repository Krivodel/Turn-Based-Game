using System;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedGame
{
    public class Battle
    {
        public bool IsRunning { get; private set; }
        public int MoveNumber { get; private set; }

        public event Action MovePassed;
        public event Action FullMoved;
        public event Action Stopped;
        public event Action Completed;

        private IBattlePlayer[] _players;
        private int _moveTurnNumber;
        private int _currentPlayerIndex;

        public void Init(IEnumerable<IBattlePlayer> players)
        {
            CheckIsRunning();

            _players = players.ToArray();

            if (_players.Length < 2)
                throw new InvalidOperationException($"There must be at least 2 players. Current players: {_players.Length}");
        }

        public IEnumerable<IBattlePlayer> GetPlayers()
        {
            return _players;
        }

        public IEnumerable<Unit> GetOpponentUnitsFor(Unit forUnit)
        {
            var forPlayer = _players.First(v => v.Unit == forUnit);

            return _players
                .Where(v => v != forPlayer)
                .Select(v => v.Unit);
        }

        public void Start()
        {
            CheckIsRunning();

            IsRunning = true;
            _moveTurnNumber = -1;
            _currentPlayerIndex = -1;

            foreach (var player in _players)
            {
                if (player is IBattleStartListener listener)
                    listener.OnStart();
            }

            PassMove();
        }

        public void Stop()
        {
            CheckIsNotRunning();

            IsRunning = false;

            foreach (var player in _players)
            {
                if (player is IBattleStopListener listener)
                    listener.OnStop();
            }

            Stopped?.Invoke();
        }

        public void PassMove()
        {
            CheckIsNotRunning();

            var nextMoveablePlayer = NextMoveablePlayerOrNullIfWin();

            if (nextMoveablePlayer == null)
            {
                OnCompleted();
            }
            else
            {
                OnMovePassed();
                nextMoveablePlayer.MakeMove();

                if (_moveTurnNumber != 0 && (_moveTurnNumber % _players.Length == 0))
                    OnFullMoved();
            }
        }

        private IBattlePlayer NextPlayer()
        {
            _currentPlayerIndex++;

            if (_currentPlayerIndex == _players.Length)
                _currentPlayerIndex = 0;

            return _players[_currentPlayerIndex];
        }

        private IBattlePlayer NextMoveablePlayerOrNullIfWin()
        {
            IBattlePlayer player;
            int maxIterations = _players.Length - 1;
            int iterations = 0;

            do
            {
                if (iterations == maxIterations)
                {
                    player = null;

                    break;
                }

                player = NextPlayer();
                iterations++;
            }
            while (!player.CanMove());

            return player;
        }

        private void CheckIsRunning()
        {
            if (IsRunning)
                throw new InvalidOperationException("Battle already running.");
        }

        private void CheckIsNotRunning()
        {
            if (!IsRunning)
                throw new InvalidOperationException("Battle is not running.");
        }

        private void OnMovePassed()
        {
            _moveTurnNumber++;

            MovePassed?.Invoke();
        }

        private void OnFullMoved()
        {
            MoveNumber++;

            FullMoved?.Invoke();
        }

        private void OnCompleted()
        {
            Completed?.Invoke();

            Stop();
        }
    }
}
