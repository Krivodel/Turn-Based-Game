using System;

namespace TurnBasedGame
{
    public class Health
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsDied => CurrentHealth == 0;

        public event Action CurrentHealthChanged;
        public event Action DamageReceived;
        public event Action RegenerationReceived;
        public event Action Died;

        public Health(int maxHealth, int currentHealth)
        {
            if (maxHealth < 0)
                throw new ArgumentOutOfRangeException($"'{nameof(maxHealth)}' cannot be less than 0.");

            if (currentHealth < 0)
                throw new ArgumentOutOfRangeException($"'{nameof(currentHealth)}' cannot be less than 0.");

            if (currentHealth > maxHealth)
                throw new ArgumentOutOfRangeException($"'{nameof(currentHealth)}' cannot be greater than '{nameof(maxHealth)}'.");

            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        public Health(int maxHealth)
            : this(maxHealth, maxHealth)
        {

        }

        public void Regenerate(RegenerationInfo regenerationInfo)
        {
            CurrentHealth += regenerationInfo.Regeneration;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;

            OnRegenerationReceived();
        }

        public void Damage(DamageInfo damageInfo)
        {
            CurrentHealth -= damageInfo.Damage;

            if (CurrentHealth < 0)
                CurrentHealth = 0;

            OnDamageReceived();

            if (CurrentHealth == 0)
                OnDied();
        }

        private void OnCurrentHealthChanged()
        {
            CurrentHealthChanged?.Invoke();
        }

        private void OnDamageReceived()
        {
            OnCurrentHealthChanged();

            DamageReceived?.Invoke();
        }

        private void OnRegenerationReceived()
        {
            OnCurrentHealthChanged();

            RegenerationReceived?.Invoke();
        }

        private void OnDied()
        {
            Died?.Invoke();
        }
    }
}
