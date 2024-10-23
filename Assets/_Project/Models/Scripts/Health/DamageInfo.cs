using System;

namespace TurnBasedGame
{
    public readonly struct DamageInfo
    {
        public int Damage { get; }

        public DamageInfo(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            Damage = damage;
        }
    }
}
