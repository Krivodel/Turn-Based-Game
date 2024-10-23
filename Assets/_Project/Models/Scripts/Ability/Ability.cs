using System;

namespace TurnBasedGame
{
    public abstract class Ability
    {
        public abstract AbilityData BaseData { get; }
        public Unit OwnerUnit { get; }
        public BattleMovesTimer DurationTimer { get; } = new();
        public BattleMovesTimer ReloadTimer { get; } = new();

        public Ability(Unit ownerUnit)
        {
            OwnerUnit = ownerUnit;
        }

        public virtual bool CanUse()
        {
            return !DurationTimer.IsRunning
                && !ReloadTimer.IsRunning;
        }

        public void Use()
        {
            if (!CanUse())
                throw new InvalidOperationException($"Cannot use ability '{BaseData.Id}'.");

            if (BaseData.Duration > 0)
                StartDurationTimer();
            else if (BaseData.Cooldown > 0)
                StartReloadTimer();

            OnUse();
        }

        protected virtual void OnUse()
        {

        }

        protected virtual void OnDurationTicked()
        {

        }

        protected virtual void OnDurationCompleted()
        {

        }

        private void OnDurationTickedInternal()
        {
            OnDurationTicked();
        }

        private void OnDurationCompletedInternal()
        {
            DurationTimer.Ticked -= OnDurationTickedInternal;
            DurationTimer.Completed -= OnDurationCompletedInternal;

            if (BaseData.Cooldown > 0)
                StartReloadTimer();

            OnDurationCompleted();
        }

        private void StartDurationTimer()
        {
            if (BaseData.Duration <= 0)
                return;

            DurationTimer.Start(OwnerUnit.Battle, BaseData.Duration);

            DurationTimer.Ticked += OnDurationTickedInternal;
            DurationTimer.Completed += OnDurationCompletedInternal;
        }

        private void StartReloadTimer()
        {
            if (BaseData.Cooldown > 0)
                ReloadTimer.Start(OwnerUnit.Battle, BaseData.Cooldown);
        }
    }
}
