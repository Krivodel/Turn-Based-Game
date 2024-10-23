using System;

namespace TurnBasedGame
{
    public abstract class StatusEffect : IStatusEffectApplyListener, IStatusEffectRemoveListener
    {
        public StatusEffectsApplicator Applicator { get; private set; }

        public event Action Applied;
        public event Action Removed;

        void IStatusEffectApplyListener.OnApply(StatusEffectsApplicator applicator)
        {
            if (HasApplicator())
                throw new InvalidOperationException("Status-effect already applied.");

            Applicator = applicator;

            OnApply();
            OnApplied();
        }

        void IStatusEffectRemoveListener.OnRemove()
        {
            if (!HasApplicator())
                throw new InvalidOperationException("Status-effect is not applied anywhere.");

            OnRemove();
            OnRemoved();

            Applicator = null;
        }

        public bool HasApplicator()
        {
            return Applicator != null;
        }

        public bool TryRemove()
        {
            if (HasApplicator())
            {
                Applicator.Remove(this);

                return true;
            }
            else
            {
                return false;
            }
        }

        protected virtual void OnApply()
        {

        }

        protected virtual void OnRemove()
        {

        }

        private void OnApplied()
        {
            Applied?.Invoke();
        }

        private void OnRemoved()
        {
            Removed?.Invoke();
        }
    }
}
