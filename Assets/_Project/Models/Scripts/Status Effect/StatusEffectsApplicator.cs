using System;
using System.Collections.Generic;

namespace TurnBasedGame
{
    public class StatusEffectsApplicator
    {
        public event Action Changed;

        private readonly List<StatusEffect> _statusEffects = new();

        public IEnumerable<StatusEffect> GetAll()
        {
            return _statusEffects;
        }

        public void Apply(StatusEffect statusEffect)
        {
            if (statusEffect == null)
                throw new ArgumentNullException(nameof(statusEffect));

            _statusEffects.Add(statusEffect);

            if (statusEffect is IStatusEffectApplyListener listener)
                listener.OnApply(this);

            OnChanged();
        }

        public void Remove(StatusEffect statusEffect)
        {
            if (statusEffect == null)
                throw new ArgumentNullException(nameof(statusEffect));

            _statusEffects.Remove(statusEffect);

            if (statusEffect is IStatusEffectRemoveListener listener)
                listener.OnRemove();

            OnChanged();
        }

        public bool TryRemove(StatusEffect statusEffect)
        {
            if (statusEffect == null)
                throw new ArgumentNullException(nameof(statusEffect));

            if (statusEffect.Applicator != this)
                return false;

            if (_statusEffects.Remove(statusEffect))
            {
                if (statusEffect is IStatusEffectRemoveListener listener)
                    listener.OnRemove();

                OnChanged();

                return true;
            }

            return false;
        }

        public void RemoveFirstByType<TStatusEffect>() where TStatusEffect : StatusEffect
        {
            Remove(GetFirstByType<TStatusEffect>());
        }

        public void RemoveAllByType<TStatusEffect>() where TStatusEffect : StatusEffect
        {
            var statusEffects = GetAllByType<TStatusEffect>();

            foreach (var statusEffect in statusEffects)
                Remove(statusEffect);
        }

        public bool Has<TStatusEffect>() where TStatusEffect : StatusEffect
        {
            return GetFirstByTypeOrDefault<TStatusEffect>() != null;
        }

        public bool Has(StatusEffect statusEffect)
        {
            if (statusEffect == null)
                throw new ArgumentNullException(nameof(statusEffect));

            return _statusEffects.Contains(statusEffect);
        }

        public TStatusEffect GetFirstByTypeOrDefault<TStatusEffect>() where TStatusEffect : StatusEffect
        {
            foreach (var statusEffect in _statusEffects)
            {
                if (statusEffect is TStatusEffect tStatusEffect)
                    return tStatusEffect;
            }

            return null;
        }

        public TStatusEffect GetFirstByType<TStatusEffect>() where TStatusEffect : StatusEffect
        {
            return GetFirstByTypeOrDefault<TStatusEffect>()
                ?? throw new InvalidOperationException();
        }

        public void GetAllByType<TStatusEffect>(IList<TStatusEffect> putList)
        {
            foreach (var statusEffect in _statusEffects)
            {
                if (statusEffect is TStatusEffect tStatusEffect)
                    putList.Add(tStatusEffect);
            }
        }

        public IEnumerable<TStatusEffect> GetAllByType<TStatusEffect>() where TStatusEffect : StatusEffect
        {
            List<TStatusEffect> foundStatusEffects = new();

            GetAllByType(foundStatusEffects);

            return foundStatusEffects;
        }

        private void OnChanged()
        {
            Changed?.Invoke();
        }
    }
}
