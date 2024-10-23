using System;

namespace TurnBasedGame
{
    public class BattleMovesTimer
    {
        public bool IsRunning { get; private set; }
        public int TargetDuration { get; private set; }
        public int StartedMove { get; private set; }
        public int ElapsedMoves => _battle.MoveNumber - StartedMove;
        public int LeftMoves => TargetDuration - ElapsedMoves;

        public event Action Ticked;
        public event Action Completed;

        private Battle _battle;

        public void Start(Battle battle, int targetDuration)
        {
            if (IsRunning)
                throw new InvalidOperationException("Timer is already running.");

            if (battle == null)
                throw new ArgumentNullException(nameof(battle));

            if (targetDuration < 0)
                throw new ArgumentOutOfRangeException(nameof(targetDuration));

            IsRunning = true;
            _battle = battle;
            TargetDuration = targetDuration;
            StartedMove = _battle.MoveNumber;

            _battle.FullMoved += OnFullMoved;
        }

        public void Stop()
        {
            if (!IsRunning)
                throw new InvalidOperationException("Timer is not running.");

            IsRunning = false;

            _battle.FullMoved -= OnFullMoved;
        }

        private void OnFullMoved()
        {
            OnTicked();

            if (ElapsedMoves >= TargetDuration)
            {
                OnCompleted();
                Stop();
            }
        }

        private void OnTicked()
        {
            Ticked?.Invoke();
        }

        private void OnCompleted()
        {
            Completed?.Invoke();
        }
    }
}
