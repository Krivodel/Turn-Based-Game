using System;

namespace TurnBasedGame
{
    public class DamageModifier
    {
        public DamageModifierMode Mode { get; }
        public int Modificator { get; }

        public DamageModifier(DamageModifierMode mode, int modificator)
        {
            if (modificator < 0)
                throw new ArgumentOutOfRangeException(nameof(modificator));

            if (mode == DamageModifierMode.Percentage)
            {
                if (modificator > 100)
                    throw new ArgumentOutOfRangeException(nameof(modificator), $"'{nameof(modificator)}' with {nameof(mode)} '{mode}' must be in the range 0-100.");
            }

            Mode = mode;
            Modificator = modificator;
        }

        public int ModifyDamage(int damage)
        {
            int modifiedDamage = Mode switch
            {
                DamageModifierMode.Absolute => damage - Modificator,
                DamageModifierMode.Percentage => damage / 100 * Modificator,
                _ => throw new NotImplementedException($"{nameof(DamageModifierMode)}.{Mode} not implemented."),
            };

            if (modifiedDamage < 0)
                modifiedDamage = 0;

            return modifiedDamage;
        }
    }
}
